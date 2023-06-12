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


namespace WebApiTutorialHE.Action
{
    public class UserAction:IUserAction
    {
        private readonly SharingContext _sharingContext;
        private readonly ICloudMediaService _cloudMediaService;

        public UserAction(SharingContext sharingContext,ICloudMediaService cloudMediaService)
        {
            _sharingContext = sharingContext;
            _cloudMediaService = cloudMediaService;
        }

        //public async Task<User> AccountUpdateModels(AccountUpdateModel model)
        //{
        //    var upadateAccount = await _sharingContext.Users.FindAsync(model.account_id);

        //    if (upadateAccount != null)
        //    {
        //        upadateAccount.Email = model.email;
        //        upadateAccount.Password = model.password;

        //        _sharingContext.Users.Update(upadateAccount);
        //        await _sharingContext.SaveChangesAsync();
        //    }

        //    return upadateAccount;
        //}
        public async Task<User> ActionCreateAccount(UserListModel model)
        {
            var createAccout = new User()
            {
                Id = model.id,
                Email = model.email.Trim(),
                Password = Encryptor.MD5Hash(model.password.Trim()),
                //Roles = List<>.,
            };
            _sharingContext.Add(createAccout);
            await _sharingContext.SaveChangesAsync();
            return createAccout;
        }
        public async Task<string> DeleteUser(int id)
        {
            var deleteAccount = await _sharingContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            deleteAccount.IsDeleted = true;
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";
        }

        //public async Task<User> ActionFillterAccount(int id/*, string email*/)
        //{
        //    var fillterAccount = await _sharingContext.Users.SingleOrDefaultAsync(x => x.Id.Equals(id));
        //    if (fillterAccount == null)
        //    {
        //        // Xử lý khi không tìm thấy tài khoản
        //        // Ví dụ: throw một ngoại lệ hoặc trả về giá trị mặc định
        //        throw new Exception("Không tìm thấy tài khoản."); // Ví dụ sử dụng ngoại lệ
        //                                                          // return null; // Ví dụ trả về giá trị mặc định
        //    }

        //    return fillterAccount;
        //}

        public async Task<User> ChangePassword(UserChangePasswordModel userForgotPassword)
        {
            var user = await _sharingContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(userForgotPassword.Email));
            if (user != null)
            {
                user.Password = Encryptor.MD5Hash(userForgotPassword.NewPassword.Trim());
                user.Id = user.Id;
                _sharingContext.Update(user);
                _sharingContext.SaveChanges();
            }
            return user;
        }

        public async Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata)
        {
            var cloudOneMediaConfig = new CloudOneMediaConfig
            {
                Folder = "Upload/Avatar/Avatar_",
                FileName = "Image_Avatar",
                FormFile = avata,
            };
            return await _cloudMediaService.SaveOneFileData(cloudOneMediaConfig);
        }
        public async Task<User> Register(UserRegisterModel userRegisterModel)
        {
            var user = new User
            {
                Email = userRegisterModel.Email,
                Password = Encryptor.MD5Hash(userRegisterModel.Password.Trim()),
                FullName = userRegisterModel.FullName ?? string.Empty,
                PhoneNumber = userRegisterModel.PhoneNumber,
                Class = userRegisterModel.Class,
                StudentCode = userRegisterModel.StudentCode,
                FacultyId = userRegisterModel.FacultyId,
                UrlAvatar = userRegisterModel.UrlAvatar.FileName,
                
                //Roles = new List<Role> { new Role {  Id=userRegisterModel.RoleID } }
            };

            var role = await _sharingContext.Roles.FindAsync(userRegisterModel.RoleID);
            user.Roles.Add(role);


            _sharingContext.Users.Add(user);
            await _sharingContext.SaveChangesAsync();
            
            //_sharingContext.Roles.Add(role);
            //foreach (var Userrole in roles)
            //{
            //    Userrole.Id = user.Id;
            //    _sharingContext.Roles.Add(Userrole);
            //    await _sharingContext.SaveChangesAsync();
            //}

            //var rolesToAdd = roles.Select(role => new Role
            //{
            //    Id = user.Id,
            //    // Các thuộc tính khác của Role
            //}).ToList();

            //using (var transaction = _sharingContext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        _sharingContext.Users.Add(user);
            //        await _sharingContext.SaveChangesAsync();

            //        var userRole = new Role
            //        {
            //            Id = user.Id,
            //            Name=user.FullName

            //            // Các thuộc tính khác của Role
            //        };

            //        _sharingContext.Roles.Add(userRole);
            //        await _sharingContext.SaveChangesAsync();

            //        transaction.Commit();
            //    }
            //    catch (Exception)
            //    {
            //        transaction.Rollback();
            //        throw;
            //    }
            //}

            return user;
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
