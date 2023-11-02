using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.Registration;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service
{
    public class ARegistrationService: IARegistrationService
    {
        private readonly IARegistrationQuery _registrationQuey;
        private readonly IARegistrationAction _registrationAction;
        public ARegistrationService(IARegistrationQuery registrationQuery, IARegistrationAction registrationAction)
        {
            _registrationQuey = registrationQuery;
            _registrationAction = registrationAction;
        }

        public async Task<List<ARegistrationModel>> GetAll()
        {
            return await _registrationQuey.GetAllRegistration();
        }

        public async Task<List<string>> AUpdateStatus(AUpdateStatusModel aUpdate)
        {
            return await _registrationAction.UpdateStatus(aUpdate);
        }

        public async Task<List<CountRegistration>> CountRegistrationByCategory()
        {
            return await _registrationQuey.CountRegistrationByCategory();
        }

        public async Task<List<CountFaculty>> CountRegistrationByFaculty()
        {
            return await _registrationQuey.CountRegistrationByFaculty();
        }
    }
}
