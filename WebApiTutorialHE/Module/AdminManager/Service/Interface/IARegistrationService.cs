using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Module.AdminManager.Model.Registration;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IARegistrationService
    {
        Task<List<ARegistrationModel>> GetAll();
        Task<List<string>>AUpdateStatus(AUpdateStatusModel aUpdate);
    }
}
