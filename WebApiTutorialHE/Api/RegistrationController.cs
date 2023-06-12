using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController:ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var registration=await _registrationService.GetListRegistation();
            return Ok(new ObjectResponse
            {
                result=1,
                message="Lấy danh sách thành công",
                content = new
                {
                    registration= registration
                }
            });
        }
        [HttpPut]
        public async Task<IActionResult>UpdateRegistration(RegistationUpdateModel registationUpdate)
        {
            var update= await _registrationService.updateRegistation(registationUpdate);
            return Ok(new ObjectResponse
            {
                result=1,
                message="Cập nhật thành công",
                content=new
                {
                    registationUpdate= update
                }
            });
        }
    }
}
