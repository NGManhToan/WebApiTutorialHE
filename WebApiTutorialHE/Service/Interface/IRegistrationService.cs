using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IRegistrationService
    {
        Task<List<RegistationListModel>>GetListRegistation();
        Task<Registration> updateRegistation(RegistationUpdateModel registationUpdate);
    }
}
