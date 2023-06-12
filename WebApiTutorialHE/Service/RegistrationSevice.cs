using Microsoft.EntityFrameworkCore.Internal;
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
        public async Task<List<RegistationListModel>> GetListRegistation()
        {
            return await _registrationQuery.QueryGetListRegistation();
        }

        public async Task<Registation> updateRegistation(RegistationUpdateModel registationUpdate)
        {
            return await _registationAction.updateRegistration(registationUpdate);
        }
    }
}
