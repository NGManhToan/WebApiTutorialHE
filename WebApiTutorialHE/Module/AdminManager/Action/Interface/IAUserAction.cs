using WebApiTutorialHE.Database.SharingModels;

namespace WebApiTutorialHE.Module.AdminManager.Action.Interface
{
    public interface IAUserAction
    {
        Task<User> UpdateUser();
    }
}
