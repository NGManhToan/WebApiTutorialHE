using Microsoft.EntityFrameworkCore.Internal;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class RegistrationSevice : IRegistrationService
    {
        private readonly IRegistrationQuery _registrationQuery;
        public RegistrationSevice(IRegistrationQuery registrationQuery)
        {
            _registrationQuery = registrationQuery;
        }
        public async Task<List<RegistationListModel>> GetListRegistation()
        {
            return await _registrationQuery.QueryGetListRegistation();
        }
    }
}
