using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Manager.FilterAttr;
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

        [ManagerAccess]
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

        [ManagerAccess]
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

        [ManagerAccess]
        [HttpGet]
        public async Task<IActionResult> CountRegistrationByCategory()
        {
            var result = await _registrationService.CountRegistrationByCategory();
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Success",
                content = result
            });
        }

        [ManagerAccess]
        [HttpGet]
        public async Task<IActionResult> CountRegistrationByFaculty()
        {
            var result = await _registrationService.CountRegistrationByFaculty();
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Success",
                content = result
            });
        }
    }
}
