using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IAUserService
    {
        Task<List<AdminUserListModel>> GetListAccountUser(ulong userId, OSearchAdminModel oSearch);
    }
}
