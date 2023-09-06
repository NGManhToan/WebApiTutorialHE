using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Manager.FilterAttr;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Models.Admin;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AUserController:ControllerBase
    {
        private readonly IAUserService _userService;
        
        public AUserController (IAUserService userService)
        {
            _userService = userService;
        }

        [ManagerAccess]
        [HttpPost]
        public async Task<IActionResult> CreateUserByAdmin([FromForm] AdminCreateUserModel adminCreate)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var result = await _userService.AddUserByAdmin(adminCreate, forceInfo);
            return Ok(result);
        }

        [ManagerAccess]
        [HttpPost]
        public async Task<IActionResult> CreateAdminByAdmin([FromForm] AdminCreateManagerModel adminCreate)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var result = await _userService.AddAdminByAdmin(adminCreate, forceInfo);
            return Ok(result);
        }

        [ManagerAccess]
        [HttpPut]
        public async Task<IActionResult> AdminUpdate([FromForm] AdminUpdateManagerModel adminUpdate)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var result = await _userService.AdminUpdateProfile(adminUpdate, forceInfo);
            return Ok(result);
        }
        
    }
}
