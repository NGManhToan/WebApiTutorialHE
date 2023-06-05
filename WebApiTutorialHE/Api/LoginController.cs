using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.Login;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        private readonly ILoginService _loginService;
        public  LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginModel loginModel)
        {
            var login= await _loginService.Login(loginModel);
            return Ok(login);
        }
    }
}
