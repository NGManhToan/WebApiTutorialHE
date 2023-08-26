using Microsoft.AspNetCore.Mvc;
using P2N_Pet_API.Module.AdminManager.Models.Admin;
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
        Task<List<UserListModel>> GetAllAdmin();
        Task<ObjectResponse> ForgotPassword(UserForgotPasswordModel userForgot);
        Task<User>DeleteUser(ForceInfo forceInfo,AdminDeleteModel adminDelete);
        void ExportDataTableToPdf(DataTable dataTable, string filePath);
        Task<UserProfileModel> QueryFrofile(int id);
        Task<List<UserProfileSharingModel>> QueryFrofileSharing(int id);
        Task<List<UserProfileFeedback>> QueryItemFeedback(int id);
        Task<ObjectResponse> UpdateProfile(UserUpdateModel userUpdate, ForceInfo forceInfo);
        Task<RecipientInformationModel> RecipientInfor(int id);

        Task<string> IdentifyOTP(int userId, string otpCode);
        Task<string> IdentifyOTPUpdate(ForceInfo forceInfo, UserUpdateModel userUpdate, string otpCode);

        Task<ObjectResponse> ChangePasswordUser(ChangepasswordModel changepassword, ForceInfo forceInfo);

    }
}
