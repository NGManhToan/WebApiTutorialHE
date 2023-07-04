using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Models.CloudMedia;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IUserAction
    {
        //Task<User> AccountUpdateModels(AccountUpdateModel model);

        Task<ActionResult<string>> DeleteUser(int id);
        //Task<User> ActionFillterAccount(int id/*, string email*/);

        Task<ActionResult<User>> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<ActionResult<UserReturnRegister>>Register(UserRegisterModel userRegisterModel,string fileName);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
        Task<User> UpdateProfile(UserUpdateModel userUpdate, string filename);

        //Task UpdatePassword();
        Task<bool> IsEmailDuplicate(string email);
        Task<bool> IsPhoneDuplicate( string phoneNumber);
    }
}
