using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IRegistationAction
    {
        Task<Registration> updateRegistration(RegistationUpdateModel registationUpdate);
        Task<string> DeleteRegistration(int id);
        Task<Registration> CreateRegistration(RegistationPostModel registationPost);
        Task<List<Registration>> UpdateRegistrationStatus(UpdateStatus updateStatus);
        
    }
}
