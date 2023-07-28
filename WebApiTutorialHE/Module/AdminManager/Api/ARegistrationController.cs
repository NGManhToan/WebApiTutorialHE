using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model;
using WebApiTutorialHE.Module.AdminManager.Model.Registration;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ARegistrationController:ControllerBase
    {
        private readonly IARegistrationService _registrationService;

        public ARegistrationController(IARegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getRegistration = await _registrationService.GetAll();
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công",
                content = getRegistration
            });
        }

        [HttpPut]
        public async Task<IActionResult>UpdateStatus(AUpdateStatusModel aUpdateStatus)
        {
            var updateStatus = await _registrationService.AUpdateStatus(aUpdateStatus);

            var statusList = Enum.GetNames(typeof(StatusEnumModel)).ToList();
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Cập nhật thành công",
                content = statusList,
            });
        }
    }
}
