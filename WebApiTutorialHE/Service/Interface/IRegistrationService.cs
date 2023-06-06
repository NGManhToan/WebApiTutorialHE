using WebApiTutorialHE.Models.Registation;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IRegistrationService
    {
        Task<List<RegistationListModel>>SeviceGetListRegistation();
    }
}
