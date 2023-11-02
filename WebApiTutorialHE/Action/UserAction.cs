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
using WebApiTutorialHE.Service.Interface;
using WebApiTutorialHE.Models.Mail;
using System.Text;
using WebApiTutorialHE.Query.Interface;

namespace WebApiTutorialHE.Action
{

    public class UserAction:IUserAction
    {
        

        private readonly SharingContext _sharingContext;
        private readonly ICloudMediaService _cloudMediaService;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserQuery _userQuery;
        public UserAction(SharingContext sharingContext,ICloudMediaService cloudMediaService, IMailService mailService, IHttpContextAccessor httpContextAccessor, IUserQuery userQuery)
        {
            _sharingContext = sharingContext;
            _cloudMediaService = cloudMediaService;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
            _userQuery = userQuery;
        }



        public async Task<User> DeleteUser(ForceInfo forceInfo, int id)
        {
            var user = await _sharingContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new BadHttpRequestException($"Không có User {id} tồn tại !");
            }

            if (user.IsDeleted)
            {
                throw new BadHttpRequestException($"User {id} đã bị xóa!");
            }

            var userRole = await _sharingContext.UserRoles
                .Where(ur => ur.UserId == forceInfo.UserId)
                .Select(ur => ur.Role)
                .FirstOrDefaultAsync();

            if (userRole != null)
            {
                if (userRole.Id == 1)
                {
                    // Xóa user khi role = 1
                    user.IsDeleted = true;
                    user.LastModifiedBy = forceInfo.UserId;
                    await _sharingContext.SaveChangesAsync();
                }
                else if (userRole.Id == 2)
                {
                    var targetUserRole = await _sharingContext.UserRoles
                        .Where(ur => ur.UserId == user.Id)
                        .Select(ur => ur.Role)
                        .FirstOrDefaultAsync();

                    if (targetUserRole != null && targetUserRole.Id == 1)
                    {
                        throw new BadHttpRequestException($"Không có quyền xóa user này");
                    }
                    else
                    {
                        // Xóa user khi role = 2 và user role != 1
                        user.IsDeleted = true;
                        user.LastModifiedBy = forceInfo.UserId;
                        await _sharingContext.SaveChangesAsync();
                    }
                }
                else if (userRole.Id == 3)
                {
                    throw new BadHttpRequestException($"Không có quyền xóa user");
                }
            }

            return user;
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


        public async Task ChangePasswordUser(ForceInfo forceInfo, string newPassword)
        {
            var user = await _sharingContext.Users.FindAsync(forceInfo.UserId);
            if (user != null && user.IsActive == true)
            {
                user.Password = Encryptor.SHA256Encode(newPassword.Trim());
                _sharingContext.Update(user);
                await _sharingContext.SaveChangesAsync();
            }
           
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


        public async Task<string> Register(UserRegisterModel userRegisterModel, IFormFile? imageFile)
        {
            var repeat = Encryptor.SHA256Encode(userRegisterModel.RepeatPassword.Trim());
            var user = new User
            {
                Email = userRegisterModel.Email,
                Password = Encryptor.SHA256Encode(userRegisterModel.Password.Trim()),
                FullName = userRegisterModel.FullName ?? string.Empty,
                PhoneNumber = userRegisterModel.PhoneNumber,
                Class = userRegisterModel.Class,
                StudentCode = userRegisterModel.StudentCode,
                FacultyId = userRegisterModel.FacultyId,
                //UrlAvatar = imageFile.FileName, // Sử dụng tên tệp tin gốc của ảnh làm tên UrlAvatar
                IsActive = userRegisterModel.IsActive,

            };
            if (user.Password.CompareTo(repeat) != 0)
            {
                return "Mật khẩu và Mật khẩu xác nhận không trùng khớp.";

            }

            if (imageFile != null)
            {
                var uploader = new Uploadfirebase();
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
                string imageUrl = await uploader.UploadAvatar(imageData, imageFile.FileName);
                // Lưu link của ảnh vào thuộc tính UrlAvatar của user
                user.UrlAvatar = imageUrl;

            }
            else
            {
                user.UrlAvatar = null;
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

                if(user.IsActive == false)
                {

                    var verificationCode = await _sharingContext.VerificationCodes
                    .FirstOrDefaultAsync(v => v.UserId == userId && DateTime.Now <= v.ExpirationTime);

                    if (verificationCode == null)
                    {
                        return ("Mã xác thực không hợp lệ hoặc đã hết hạn.");
                    }

                    
                    if (otpCode == verificationCode.Code)
                    {
                        // Save the user data
                        var role = await _sharingContext.Roles.FindAsync(3);
                        user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });

                        //_sharingContext.UserRoles.Add(role);
                        await _sharingContext.SaveChangesAsync();

                        user.IsActive = true;

                        

                        _sharingContext.VerificationCodes.Remove(verificationCode);
                        await _sharingContext.SaveChangesAsync();

                        return "Xác thực thành công.";
                    }
                    else
                    {
                        user.IsActive = false;

                        _sharingContext.VerificationCodes.Remove(verificationCode);
                        await _sharingContext.SaveChangesAsync();

                        return "Xác thực thất bại";
                    }
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return null;

        }

        public async Task RefreshOTPCode(int userId)
        {
            var user = await _sharingContext.Users.FindAsync(userId);

            if (user != null)
            {
                // Get the verification code for the user
                var verificationCodedata = await _sharingContext.VerificationCodes
                    .FirstOrDefaultAsync(v => v.UserId == userId);
                if (DateTime.Now > verificationCodedata.ExpirationTime)
                {
                    var otpCode = GenerateOTPCode();
                    var mailSetting = new MailSettings();
                    var mailData = new MailDataWithAttachments()
                    {
                        From = mailSetting.UserName,
                        To = new List<string>()
                {
                       user.Email
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

                }
            }
            
        }


        public async Task<User> UpdateProfile(UserUpdateModel userUpdate,ForceInfo forceInfo)
        {
            var update = await _sharingContext.Users.FindAsync(forceInfo.UserId);

            if (update != null)
            {
                bool isEmailChanged = false;

                if (!string.IsNullOrEmpty(userUpdate.Email) && update.Email != userUpdate.Email.Trim())
                {
                    isEmailChanged = true;  // Đánh dấu là email đã thay đổi
                    _httpContextAccessor.HttpContext.Session.SetString("TempEmail", userUpdate.Email);
                }


                bool isPhoneChanged = false;

                if (!string.IsNullOrEmpty(userUpdate.PhoneNumber) && update.PhoneNumber != userUpdate.PhoneNumber.Trim())
                {
                    isPhoneChanged = true;  // Đánh dấu là số điện thoại đã thay đổi

                    _httpContextAccessor.HttpContext.Session.SetString("TempPhone", userUpdate.PhoneNumber);

                }


                //update.LastModifiedDate = Utils.DateNow();
                //update.LastModifiedBy = forceInfo.UserId;

                if (userUpdate.fileName != null)
                {
                    var uploader = new Uploadfirebase();
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        await userUpdate.fileName.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                    string imageUrl = await uploader.UploadAvatar(imageData, userUpdate.fileName.FileName);
                    // Lưu link của ảnh vào thuộc tính UrlAvatar của user
                    update.UrlAvatar = imageUrl;

                }
               

                //_sharingContext.Users.Update(update);
                //await _sharingContext.SaveChangesAsync();

                if (isEmailChanged)
                {
                    var otpCode = GenerateOTPCode();
                    var mailSetting = new MailSettings();
                    var mailData = new MailDataWithAttachments()
                    {
                        From = mailSetting.UserName,
                        To = new List<string>()
                {
                    userUpdate.Email
                },
                        Subject = "Verification",
                        Body = $"Mã xác thực: {otpCode}"
                    };
                    var sent = await _mailService.SendMail(mailData, default);

                    var verificationCode = new VerificationCode
                    {
                        UserId = forceInfo.UserId,
                        Code = otpCode,
                        ExpirationTime = DateTime.Now.AddMinutes(60),
                    };

                    _sharingContext.VerificationCodes.Add(verificationCode);
                    await _sharingContext.SaveChangesAsync();
                }

                if (isPhoneChanged)
                {
                    var otpCode = GenerateOTPCode();
                    var mailSetting = new MailSettings();
                     var mailData = new MailDataWithAttachments()
                     {
                         From = mailSetting.UserName,
                         To = new List<string>()
                {
                    update.Email
                },
                         Subject = "Verification",
                         Body = $"Mã xác thực: {otpCode}"
                     };
                    var sent = await _mailService.SendMail(mailData, default);

                    var verificationCode = new VerificationCode
                    {
                        UserId = forceInfo.UserId,
                        Code = otpCode,
                        ExpirationTime = DateTime.Now.AddMinutes(60),
                    };

                    _sharingContext.VerificationCodes.Add(verificationCode);
                    await _sharingContext.SaveChangesAsync();
                }

            }
            else
            {
                return null;
            }

            return update;
        }


        public async Task<User> IdentifyOTPUpdate(ForceInfo forceInfo, string otpCode)
        {
            try
            {
                var verificationCodeData = await _sharingContext.VerificationCodes
                   .Where(v => v.UserId == forceInfo.UserId && v.Code == otpCode)
                   .OrderByDescending(v => v.ExpirationTime)
                    .FirstOrDefaultAsync();

                

                if (verificationCodeData != null && DateTime.Now <= verificationCodeData.ExpirationTime)
                {
                    var user = await _sharingContext.Users.FindAsync(forceInfo.UserId);
                    
                    if (user != null)
                    {
                        
                        string newEmail = _httpContextAccessor.HttpContext.Session.GetString("TempEmail");
                        string newPhone = _httpContextAccessor.HttpContext.Session.GetString("TempPhone");
                        if(newEmail != null)
                        {
                            user.Email = newEmail;
                        }
                        if(newPhone != null)
                        {
                            user.PhoneNumber = newPhone;
                        }
                        
                        user.LastModifiedDate = Utils.DateNow();
                        user.LastModifiedBy = forceInfo.UserId;

                        _sharingContext.Users.Update(user);
                        await _sharingContext.SaveChangesAsync();

                        _sharingContext.VerificationCodes.Remove(verificationCodeData);
                        await _sharingContext.SaveChangesAsync();

                        return user;
                    }
                    else
                    {
                        _sharingContext.VerificationCodes.Remove(verificationCodeData);
                        await _sharingContext.SaveChangesAsync();

                        return null;
                    }
                }
                else
                {
                    if (verificationCodeData != null)
                    {
                        _sharingContext.VerificationCodes.Remove(verificationCodeData);
                        await _sharingContext.SaveChangesAsync();
                    }

                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }






        //public DataTable GetDataTable()
        //{
        //    DataTable dt = new DataTable();
        //    dt.TableName = "Empdata";
        //    dt.Columns.Add("user_id", typeof(int));
        //    dt.Columns.Add("email", typeof(string));
        //    dt.Columns.Add("password", typeof(string));
        //    var _list = this._sharingContext.Users.ToList();
        //    if (_list.Count > 0)
        //    {
        //        _list.ForEach(item =>
        //        {
        //            dt.Rows.Add(item.Id, item.Email, item.Password);
        //        });
        //    }
        //    return dt;
        //}


    }
}
