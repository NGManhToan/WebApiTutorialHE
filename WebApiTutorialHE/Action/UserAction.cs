using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiTutorialHE.Database.SharingModels;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.UtilsService.Interface;
using System.Linq;
using Google.Cloud.Storage.V1;
using System.IO;
using Microsoft.Win32;
using DocumentFormat.OpenXml.Bibliography;
using WebApiTutorialHE.Service.Interface;
using WebApiTutorialHE.Models.Mail;
using System.Text;

namespace WebApiTutorialHE.Action
{

    public class UserAction:IUserAction
    {
        

        private readonly SharingContext _sharingContext;
        private readonly ICloudMediaService _cloudMediaService;
        private readonly IMailService _mailService;

        public UserAction(SharingContext sharingContext,ICloudMediaService cloudMediaService, IMailService mailService)
        {
            _sharingContext = sharingContext;
            _cloudMediaService = cloudMediaService;
            _mailService = mailService;
        }



        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var deleteAccount = await _sharingContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (deleteAccount == null)
            {
                return new NotFoundResult();
            }

            deleteAccount.IsDeleted = true;
            await _sharingContext.SaveChangesAsync();

            return "Đã xóa";
        }



        public async Task<ActionResult<User>> ChangePassword(UserChangePasswordModel userForgotPassword)
        {
            var user = await _sharingContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(userForgotPassword.Email));
            if (user != null)
            {
                user.Password = Encryptor.SHA256Encode(userForgotPassword.NewPassword.Trim());
                _sharingContext.Update(user);
                await _sharingContext.SaveChangesAsync();
            }

            return user;
        }


        public async Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata)
        {
            var cloudOneMediaConfig = new CloudOneMediaConfig
            {
                Folder = Path.Combine("wwwroot", "Upload","Avata"),
                FileName = "Image_Avatar",
                FormFile = avata,
            };
            return await _cloudMediaService.SaveOneFileData(cloudOneMediaConfig);
        }
        public async Task<bool> IsEmailDuplicate(string email)
        {
            return await _sharingContext.Users.AnyAsync(u => u.Email.Equals(email) );
        }

        public async Task<bool> IsPhoneDuplicate(string phoneNumber)
        {
            return await _sharingContext.Users.AnyAsync(u => u.PhoneNumber.Equals(phoneNumber));
        }

        public string GenerateOTPCode()
        {
            int length = 6;

            Random random = new Random();
            StringBuilder otpCode = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int digit = random.Next(0, 10);
                otpCode.Append(digit);
            }

            return otpCode.ToString();
        }


        public async Task<ActionResult<string>> Register(UserRegisterModel userRegisterModel, IFormFile imageFile)
        {

            var user = new User
            {
                Email = userRegisterModel.Email,
                Password = Encryptor.SHA256Encode(userRegisterModel.Password.Trim()),
                FullName = userRegisterModel.FullName ?? string.Empty,
                PhoneNumber = userRegisterModel.PhoneNumber,
                Class = userRegisterModel.Class,
                StudentCode = userRegisterModel.StudentCode,
                FacultyId = userRegisterModel.FacultyId,
                UrlAvatar = imageFile.FileName, // Sử dụng tên tệp tin gốc của ảnh làm tên UrlAvatar
            };

            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            _sharingContext.Users.Add(user);
            await _sharingContext.SaveChangesAsync();



            var otpCode = GenerateOTPCode();
            var mailSetting = new MailSettings();
            var mailData = new MailDataWithAttachments()
            {
                From = mailSetting.UserName,
                To = new List<string>()
                {
                    userRegisterModel.Email
                },
                Subject = "Verification",
                Body = $"Mã xác thực: {otpCode}"
            };
            var sent = await _mailService.SendMail(mailData, default);

            var verificationCode = new VerificationCode
            {
                UserId = user.Id,
                Code = otpCode,
                ExpirationTime = DateTime.Now.AddMinutes(60),
            };

            _sharingContext.VerificationCodes.Add(verificationCode);
            await _sharingContext.SaveChangesAsync();

            return "Đã gửi mã xác thực đến mail.";
  
        }

        public async Task<string> IdentifyOTP(int userId, string otpCode)
        {

            try
            {
                var user = await _sharingContext.Users.FindAsync(userId);

                if (user == null)
                {
                    return ("Không tìm thấy người dùng.");
                }

                // Lấy mã OTP từ cơ sở dữ liệu dựa trên userId và thời gian còn hạn
                var verificationCode = await _sharingContext.VerificationCodes
                    .FirstOrDefaultAsync(v => v.UserId == userId && v.Code == otpCode && DateTime.Now <= v.ExpirationTime);

                if (verificationCode == null)
                {
                    return ("Mã xác thực không hợp lệ hoặc đã hết hạn.");
                }

                if (otpCode == verificationCode.Code)
                {
                    // Save the user data
                    var role = await _sharingContext.Roles.FindAsync(3);
                    user.Roles.Add(role);

                    //var imageUrl = await UploadAvtarFireBase(); // Pass the URL directly

                    // Save the imageUrl to the user data (assuming there's a property for it)
                    var uploader = new Uploadfirebase();

                   // var image = await uploader.UploadAvatar(imageData, user.UrlAvatar);
                    //user.UrlAvatar = image;

                    _sharingContext.VerificationCodes.Remove(verificationCode);
                    await _sharingContext.SaveChangesAsync();

                    return "Xác thực thành công.";
                }
                else
                {
                    _sharingContext.VerificationCodes.Remove(verificationCode);
                    await _sharingContext.SaveChangesAsync();

                    _sharingContext.Users.Remove(user);

                    await _sharingContext.SaveChangesAsync();
                    return ("Mã xác thực không hợp lệ hoặc đã hết hạn.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }

        public async Task<User> UpdateProfile(UserUpdateModel userUpdate, string filename)
        {
            var update = await _sharingContext.Users.FindAsync(userUpdate.Id);

            if (update != null)
            {
                if (!string.IsNullOrEmpty(userUpdate.FullName))
                {
                    update.FullName = userUpdate.FullName;
                }

                if (!string.IsNullOrEmpty(userUpdate.Class))
                {
                    update.Class = userUpdate.Class;
                }

                if (!string.IsNullOrEmpty(userUpdate.StudentCode))
                {
                    update.StudentCode = userUpdate.StudentCode;
                }

                if (userUpdate.FacultyId != null && userUpdate.FacultyId != 0)
                {
                    update.FacultyId = userUpdate.FacultyId.Value; // Use Value property to get the non-nullable value
                }


                // Cập nhật các trường không thay đổi

                update.LastModifiedDate = Utils.DateNow();
                update.LastModifiedBy = update.Id;

                if (userUpdate.UrlImage != null)
                {
                    update.UrlAvatar = filename;
                }

                _sharingContext.Users.Update(update);
                await _sharingContext.SaveChangesAsync();
            }

            return update;
        }



        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("user_id", typeof(int));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("password", typeof(string));
            var _list = this._sharingContext.Users.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.Email, item.Password);
                });
            }
            return dt;
        }


    }
}
