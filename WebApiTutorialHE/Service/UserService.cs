using iTextSharp.text.pdf;
using iTextSharp.text;
using WebApiTutorialHE.Action;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service.Interface;
using WebApiTutorialHE.Query.Interface;
using System.Data;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Models.Mail;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P2N_Pet_API.Module.AdminManager.Models.Admin;

namespace WebApiTutorialHE.Service
{
    public class UserService:IUserService
    {
        private readonly IUserAction _userAction;
        private readonly IUserQuery _userQuery;
        private readonly IMailService _mailService;
        private readonly SharingContext _sharingContext;
        public UserService(IUserAction userAction,IUserQuery userQuery,IMailService mailService,SharingContext sharingContext)
        {
            _userAction = userAction;
            _userQuery = userQuery;
            _mailService = mailService;
            _sharingContext = sharingContext;
        }

        public static string RandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public async Task<ObjectResponse>ForgotPassword(UserForgotPasswordModel userForgot)
        {
            var random = RandomPassword();

            var changePassword = _userAction.ChangePassword(new UserChangePasswordModel()
            {
                Email= userForgot.Email,
                NewPassword= random,
                RepeatPassword=random,
            });
            try
            {
                if (changePassword != null)
                {
                    var mailSetting = new MailSettings();
                    var mailData = new MailDataWithAttachments()
                    {
                        From = mailSetting.UserName,
                        To = new List<string>()
                {
                    userForgot.Email
                },
                        Subject = "Verification",
                        Body = "Password thay đổi: " + random.ToString()
                    };

                    var sent = await _mailService.SendMail(mailData, default);
                    if (sent) return new ObjectResponse { result = 1,message = "Password đã được thay đổi và gửi thành công qua email." };
                    else return new ObjectResponse { result = 0, message = "Email không được gửi thành công. Vui lòng thử lại sau." };
                }
                return null;
            }
            catch (Exception ex)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = ex.Message,
                    content = false
                };
            }
            

           
        }
        public async Task<ObjectResponse> ChangePassword(UserChangePasswordModel userForgotPassword)
        {
            

            if (string.IsNullOrEmpty(userForgotPassword.Email.Trim()))
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng nhập email"
                };
            }

            if (!userForgotPassword.NewPassword.Trim().Equals(userForgotPassword.RepeatPassword.Trim()))
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Password và RepeatPassword không trùng khớp. Vui lòng thử lại."
                };
            }

            var fpwd = await _userAction.ChangePassword(userForgotPassword);

            if (fpwd == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Thay đổi password thất bại. Vui lòng thử lại."
                };
            }

            return new ObjectResponse
            {
                result = 1,
                message = "Thay đổi password thành công."
            };
        }


        public async Task<ObjectResponse> ChangePasswordUser(ChangepasswordModel changepassword, ForceInfo forceInfo)
        {
            try
            {
                var oldPass = Encryptor.SHA256Encode(changepassword.CurrentPassword);
                var user = await _sharingContext.Users
                    .Where(u => u.Id == forceInfo.UserId)
                    .FirstOrDefaultAsync();

                if (user != null && user.Password == oldPass && changepassword.NewPassword == changepassword.ConfirmNewPassword)
                {
                     await _userAction.ChangePasswordUser(forceInfo, changepassword.NewPassword);
                    var profile = await _userQuery.QueryFrofile(forceInfo.UserId);
                    return new ObjectResponse
                    {
                        result = 1,
                        message = "Thay đổi mật khẩu thành công.",
                        content = profile
                    };
                }
                else
                {
                    if(changepassword.NewPassword != changepassword.ConfirmNewPassword)
                    {
                        return new ObjectResponse
                        {
                            result = 0,
                            message = "Mật khẩu xác thực không trùng khớp. Vui lòng thử lại."
                        };
                    }
                    else
                    {
                        return new ObjectResponse
                        {
                            result = 0,
                            message = "Mật khẩu cũ không chính xác. Vui lòng thử lại."
                        };
                    }
                    
                }
            }
            catch (Exception e)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Đã có lỗi xảy ra. Vui lòng thử lại.",
                    content = e.Message.ToString(),
                };
            }
        }

        public async Task<ObjectResponse> Register(UserRegisterModel userRegisterModel, IFormFile fileName)
        {
            try
            {
                if (await _userAction.IsEmailDuplicate(userRegisterModel.Email))
                {
                    return new ObjectResponse
                    {
                        result = 0,
                        message = "Mail đã tồn tại"
                    };
                }

                if (await _userAction.IsPhoneDuplicate(userRegisterModel.PhoneNumber))
                {
                    return new ObjectResponse
                    {
                        result = 0,
                        message = "Số điện thoại đã tồn tại"
                    };
                }
                
                var register = await _userAction.Register(userRegisterModel, fileName);
                if(register == "Mật khẩu và Mật khẩu xác nhận không trùng khớp.")
                {
                    return new ObjectResponse
                    {
                        result = 0,
                        message = "Mật khẩu và Mật khẩu xác nhận không trùng khớp."
                    };
                }
                else
                {
                    return new ObjectResponse
                    {
                        result = 1,
                        content = register
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = ex.Message
                };
            }
        }
        public async Task<string> IdentifyOTP(int userId, string otpCode)
        {
            return await _userAction.IdentifyOTP(userId, otpCode);
        }
        public async Task<List<UserListModel>> GetAllUser()
        {
           var accounts = await _userQuery.QueryListUser();
            //var userRoles = await _userQuery.QueryUserRoles();

            //foreach (var account in accounts)
            //{
            //    account.roles = userRoles.Where(x => x.UserID == account.id).ToList();
            //}

            return accounts;
        }


        public async Task<List<UserListModel>> GetAllAdmin()
        {
            var accounts = await _userQuery.QueryListAdmin();

            return accounts;
        }
        public async Task<User> DeleteUser(ForceInfo forceInfo,AdminDeleteModel adminDelete)
        {
            return await _userAction.DeleteUser(forceInfo,adminDelete.UserId);
        }
        
        public async Task<UserProfileModel> QueryFrofile(int id)
        {
            return await _userQuery.QueryFrofile(id);
        }

        public async Task<List<UserProfileSharingModel>> QueryFrofileSharing(int id)
        {
            return await _userQuery.QueryFrofileSharing(id);
        }

        public async Task<List<UserProfileFeedback>> QueryItemFeedback(int id)
        {
            return await _userQuery.QueryItemFeedback(id);
        }
        public async Task<ObjectResponse> UpdateProfile(UserUpdateModel userUpdate, ForceInfo forceInfo)
        {
           
            var updateProfile = await _userAction.UpdateProfile(userUpdate,forceInfo);
            if (updateProfile == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Xác thực không thành công !!"
                };
            }
            return new ObjectResponse
            {
                result = 1,
                message = "Cập nhật hồ sơ thành công."
            };

        }

        public async Task<string> IdentifyOTPUpdate(ForceInfo forceInfo, UserUpdateModel userUpdate, string otpCode)
        {
            return await _userAction.IdentifyOTPUpdate(forceInfo,userUpdate,otpCode);
        }

        public void ExportDataTableToPdf(DataTable dataTable, string filePath)
        {
            // Tạo một tài liệu PDF mới
            Document document = new Document();

            // Tạo một writer để ghi dữ liệu vào tài liệu
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.CreateNew, FileAccess.Write));


            // Mở tài liệu để bắt đầu viết
            document.Open();

            // Tạo một bảng để hiển thị dữ liệu
            PdfPTable table = new PdfPTable(dataTable.Columns.Count);

            // Đặt tiêu đề cho các cột
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                table.AddCell(new Phrase(dataTable.Columns[i].ColumnName));
            }

            // Đặt dữ liệu cho từng dòng
            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                {
                    table.AddCell(new Phrase(dataTable.Rows[rowIndex][colIndex].ToString()));
                }
            }

            // Thêm bảng vào tài liệu
            document.Add(table);

            // Đóng tài liệu
            document.Close();
        }
       
        public async Task<RecipientInformationModel> RecipientInfor(int id)
        {
            return await _userQuery.QueryRecipientInfor(id);
        }
    }
}
