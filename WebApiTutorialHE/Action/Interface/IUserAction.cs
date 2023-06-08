using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IUserAction
    {
        //Task<User> AccountUpdateModels(AccountUpdateModel model);
        
        Task<string> DeleteUser(int id);
        //Task<User> ActionFillterAccount(int id/*, string email*/);

        Task<User> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<User> Register(Models.User.UserRegisterModel userRegisterModel);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
    }
}
