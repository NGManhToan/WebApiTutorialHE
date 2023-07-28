using WebApiTutorialHE.Module.AdminManager.Model.Registration;

namespace WebApiTutorialHE.Module.AdminManager.Action.Interface
{
    public interface IARegistrationAction
    {
        Task<List<string>> UpdateStatus(AUpdateStatusModel aUpdateStatus);
    }
}
