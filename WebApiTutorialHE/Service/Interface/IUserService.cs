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
        Task<ObjectResponse> Register(UserRegisterModel userRegisterModel);
        Task<List<UserListModel>> GetAllUser();
        //Task<User> PutAccountUpdateModel(AccountUpdateModel model);
        //Task<User> PostAccountModel(UserListModel model);
        Task<string> DeleteUser(int id);
        //Task<User> FillterAccountModel(int id/*, string email*/);
        //Task<List<UserListModel>> FindAccountModel(string search);

        ////Lấy thông tin admin hiện tại
        //Task<List<UserListModel>> GetAccountListAdminModel();
        //Task UpdatePassword();
        void ExportDataTableToPdf(DataTable dataTable, string filePath);
        Task<UserProfileModel> QueryFrofile(int id);
        Task<UserProfileSharingModel> QueryFrofileSharing(int id);
        Task<List<UserProfileFeedback>> QueryItemFeedback(int id);
        Task<User> UpdateProfile(UserUpdateModel userUpdate);
        

    }
}
