using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IUserService
    {
        Task<ObjectResponse> ChangePassword(UserChangePasswordModel userForgotPassword);
        Task<ObjectResponse> Register(UserRegisterModel userRegisterModel,IFormFile fileName);
        Task<List<UserListModel>> GetAllUser();
        Task<ObjectResponse> ForgotPassword(UserForgotPasswordModel userForgot);
        Task<ActionResult<string>>DeleteUser(int id);
        void ExportDataTableToPdf(DataTable dataTable, string filePath);
        Task<UserProfileModel> QueryFrofile(int id);
        Task<List<UserProfileSharingModel>> QueryFrofileSharing(int id);
        Task<List<UserProfileFeedback>> QueryItemFeedback(int id);
        Task<User> UpdateProfile(UserUpdateModel userUpdate);
        Task<RecipientInformationModel> RecipientInfor(int id);

        Task<string> IdentifyOTP(int userId, string otpCode);

    }
}
