using WebApiTutorialHE.Module.AdminManager.Model.User;

namespace WebApiTutorialHE.Module.AdminManager.Query.Interface
{
    public interface IAUserQuey
    {
        Task<List<AdminUserListModel>>QueryGetAllUser(ulong userId,OSearchAdminModel oSearch);
    }
}
