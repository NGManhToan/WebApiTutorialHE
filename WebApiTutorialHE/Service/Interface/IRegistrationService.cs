using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IRegistrationService
    {
        Task<List<RegistationListModel>>GetListRegistation(int id);
        Task<Registration> updateRegistation(RegistationUpdateModel registationUpdate);
        Task<string> DeleteRegistation(int id);
    }
}
