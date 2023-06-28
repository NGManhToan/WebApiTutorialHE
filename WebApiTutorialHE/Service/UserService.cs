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

namespace WebApiTutorialHE.Service
{
    public class UserService:IUserService
    {
        private readonly IUserAction _userAction;
        private readonly IUserQuery _userQuery;
        private readonly IMailService _mailService;
        public UserService(IUserAction userAction,IUserQuery userQuery,IMailService mailService)
        {
            _userAction = userAction;
            _userQuery = userQuery;
            _mailService = mailService;
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

            if(changePassword != null )
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
                    Body ="Password thay đổi: " +random.ToString()
                };

                var sent = await _mailService.SendMail(mailData, default);
                if (sent) return new ObjectResponse { result = 1 };
                else return new ObjectResponse { result = 0 };
            }
            return null;

           
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

        public async Task<ObjectResponse> Register(UserRegisterModel userRegisterModel)
        {
            if(await _userAction.IsEmailDuplicate(userRegisterModel.Email))
            {
                return new ObjectResponse
                {
                    result=0,
                    message="Mail đã tồn tại"
                };
            }
            if (await _userAction.IsPhoneDuplicate( userRegisterModel.PhoneNumber))
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Số điện thoại đã tồn tại"
                };
            }
            var mailSetting = new MailSettings();
            var mailData = new MailDataWithAttachments()
            {
                From = mailSetting.UserName,
                To = new List<string>()
                {
                    userRegisterModel.Email
                },
                Subject = "Verification",
                Body = "Email xác thực"
            };

            var sent = await _mailService.SendMail(mailData, default);
            if (!sent) return new ObjectResponse
            {
                result = 0,
                message = "Email không tồn tại",
            };

            var file = await _userAction.SaveOneMediaData(userRegisterModel.UrlAvatar);

            var register = await _userAction.Register(userRegisterModel, file.FileName);
            
            return new ObjectResponse
            {
                result=1,
                content=register
            };
        }
        public async Task<List<UserListModel>> GetAllUser()
        {
           var accounts = await _userQuery.QueryListUser();
            var userRoles = await _userQuery.QueryUserRoles();

            foreach (var account in accounts)
            {
                account.roles = userRoles.Where(x => x.UserID == account.id).ToList();
            }

            return accounts;
        }
        
        public async Task<string> DeleteUser(int id)
        {
            return await _userAction.DeleteUser(id);
        }
        
        public async Task<UserProfileModel> QueryFrofile(int id)
        {
            return await _userQuery.QueryFrofile(id);
        }

        public async Task<UserProfileSharingModel> QueryFrofileSharing(int id)
        {
            return await _userQuery.QueryFrofileSharing(id);
        }

        public async Task<List<UserProfileFeedback>> QueryItemFeedback(int id)
        {
            return await _userQuery.QueryItemFeedback(id);
        }
        public async Task<User> UpdateProfile(UserUpdateModel userUpdate)
        {
            var filename = "";

            if(userUpdate.UrlImage != null)
            {
                var image = await _userAction.SaveOneMediaData(userUpdate.UrlImage);
                filename = image.FileName;
            }

            return await _userAction.UpdateProfile(userUpdate, filename);
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
        //public async Task UpdatePassword()
        //{
        //    await _userAction.UpdatePassword();
        //}
        public async Task<RecipientInformationModel> RecipientInfor(int id)
        {
            return await _userQuery.QueryRecipientInfor(id);
        }
    }
}
