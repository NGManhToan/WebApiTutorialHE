using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Models.Admin;

namespace WebApiTutorialHE.Module.AdminManager.Action.Interface
{
    public interface IAUserAction
    {
        Task <User> AddUser(AdminCreateUserModel adminCreate,ForceInfo forceInfo);

        Task<User> AddManage(AdminCreateManagerModel adminCreate, ForceInfo forceInfo);

        Task<User> AUpdateProfile(AdminUpdateManagerModel adminUpdate, ForceInfo forceInfo);

        //Task<User> RemoveUser()
    }
}
