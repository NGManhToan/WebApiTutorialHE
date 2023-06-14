using WebApiTutorialHE.Models.User;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IUserQuery
    {
        Task<List<UserListModel>> QueryListUser();
        //Task<List<UserListModel>> QueryFindAccount(string search);

        ////Lấy thông tin admin hiện tại
        //Task<List<UserListModel>> QueryListAdminAccount();
        Task<List<UserRoleModel>> QueryUserRoles();
        Task<UserProfileModel> QueryFrofile(int id);
        Task<UserProfileSharingModel> QueryFrofileSharing(int id);
        Task<List<UserProfileFeedback>> QueryItemFeedback(int id);
    }
}
