using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModel userChangePassword)
        {
            var objectResponse = await _userService.ChangePassword(userChangePassword);

            return Ok(objectResponse);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel userRegisterModel)
        {
            var register = await _userService.Register(userRegisterModel);
            return Ok(register);

        }
    }
}
