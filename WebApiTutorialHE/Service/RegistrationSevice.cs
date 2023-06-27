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

        public async Task<ObjectResponse> UpdateRegistation(RegistationUpdateModel registationUpdate)
        {
            var updated = await _registationAction.UpdateRegistration(registationUpdate);
            if (updated)
            {
                return new ObjectResponse
                {
                    result = 1,
                    message = "Update thành công",
                };
            }
            else
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Update thất bại"
                };
            }
        }
        public async Task<string> DeleteRegistation(int id)
        {
            return await _registationAction.DeleteRegistration(id);
        }
        public async Task<Registration> CreateRegistation(RegistationPostModel registationPost)
        {
            return await _registationAction.CreateRegistration(registationPost);
        }
        public async Task<List<Registration>> UpdateStatus(UpdateStatus updateStatus)
        {
            return await _registationAction.UpdateRegistrationStatus(updateStatus);
        }
        public async Task<int> NumRegistation(int postId, int createdBy)
        {
            return await _registrationQuery.QueryNumRegistation(postId, createdBy);
        }
        public async Task<List<RegistrationProserModel>> GetListRegistation(int id, int postId)
        {
            return await _registrationQuery.GetListRegistrationHaveProposer(postId, id);
        }
    }
}
