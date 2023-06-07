using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IUserService
    {
        Task<ObjectResponse> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<Tuple<Account, User>> Register(UserRegisterModel userRegisterModel);
    }
}
