using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class RegistrationSevice : IRegistrationService
    {
        private readonly IRegistrationQuery _registrationQuery;
        private readonly IRegistationAction _registationAction;
        public RegistrationSevice(IRegistrationQuery registrationQuery, IRegistationAction registationAction)
        {
            _registrationQuery = registrationQuery;
            _registationAction = registationAction;
        }
        public async Task<List<RegistationListModel>> GetListRegistation(int id)
        {
            return await _registrationQuery.QueryGetListRegistation(id);
        }

        public async Task<Registration> updateRegistation(RegistationUpdateModel registationUpdate)
        {
            return await _registationAction.updateRegistration(registationUpdate);
        }
        public async Task<string> DeleteRegistation(int id)
        {
            return await _registationAction.DeleteRegistration(id);
        }
        public async Task<Registration> CreateRegistation(RegistationPostModel registationPost)
        {
            return await _registationAction.CreateRegistration(registationPost);
        }
    }
}
