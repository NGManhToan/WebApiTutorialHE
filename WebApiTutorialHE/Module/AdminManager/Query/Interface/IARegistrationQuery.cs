using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Module.AdminManager.Model.Registration;

namespace WebApiTutorialHE.Module.AdminManager.Query.Interface
{
    public interface IARegistrationQuery
    {
        Task<List<ARegistrationModel>> GetAllRegistration();
    }
}
