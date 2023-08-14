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

        Task<User> DeleteUser(int id);
        //Task<User> ActionFillterAccount(int id/*, string email*/);

        Task<ActionResult<User>> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<ActionResult<string>>Register(UserRegisterModel userRegisterModel,IFormFile fileName);
        Task<CloudOneMediaModel> SaveOneMediaData(IFormFile avata);
        Task<User> UpdateProfile(UserUpdateModel userUpdate, string filename);

        //Task UpdatePassword();
        Task<bool> IsEmailDuplicate(string email);
        Task<bool> IsPhoneDuplicate( string phoneNumber);
        Task<string> IdentifyOTP(int userId, string otpCode);

        Task ChangePasswordUser(int id, string newPassword);
    }
}
