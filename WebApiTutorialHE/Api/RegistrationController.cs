using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service;
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
        [HttpDelete]
        public async Task<IActionResult> DeleteRegistrationID(int id)
        {
            var delete = await _registrationService.DeleteRegistation(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Xóa thành công",
                content = new
                {
                    registrationDelete = delete
                }
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegistration(RegistationPostModel registationPost)
        {
            var create = await _registrationService.CreateRegistation(registationPost);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Thêm thành công",
                content = new
                {
                    registrationCreate = create
                }
            });
        }
    }
}
