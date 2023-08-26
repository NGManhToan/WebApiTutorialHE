using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Models.Admin;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IAUserService
    {
        Task<List<AdminUserListModel>> GetListAccountUser(ulong userId, OSearchAdminModel oSearch);

        Task<ObjectResponse> AddUserByAdmin(AdminCreateUserModel adminCreate, ForceInfo forceInfo);

        Task<ObjectResponse> AddAdminByAdmin(AdminCreateManagerModel adminCreate, ForceInfo forceInfo);

        Task<ObjectResponse> AdminUpdateProfile(AdminUpdateManagerModel adminUpdate, ForceInfo forceInfo);
    }
}
