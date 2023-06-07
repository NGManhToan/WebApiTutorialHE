using Microsoft.EntityFrameworkCore;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;
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

        public async Task<Account> ChangePassword(UserChangePasswordModel userForgotPassword)
        {
            var user = await _sharingContext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(userForgotPassword.Email));
            if (user != null)
            {
                user.Password = Encryptor.MD5Hash(userForgotPassword.NewPassword.Trim());
                user.AccountId = user.AccountId;
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
        public async Task<Tuple<Account,User>> Register(UserRegisterModel userRegisterModel)
        {



            var user = new User
            {
                FullName = userRegisterModel.full_name,
                PhoneNumber = userRegisterModel.phone_number,
                Class = userRegisterModel.Class,
                StudentCode = userRegisterModel.student_code,
                UrlAvartar = userRegisterModel.url_avatar.FileName
            };

            var account = new Account
            {
                Email=userRegisterModel.email,
                Password=Encryptor.MD5Hash(userRegisterModel.password.Trim()),
            };
            _sharingContext.Accounts.Add(account);
            _sharingContext.Users.Add(user);
            await _sharingContext.SaveChangesAsync();
            return Tuple.Create(account, user);
        }
    }
}
