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
    }
}
