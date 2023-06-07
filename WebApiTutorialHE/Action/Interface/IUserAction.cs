using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IUserAction
    {
        Task<Account> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<Tuple<Account, User>> Register(UserRegisterModel userRegisterModel);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
    }
}
