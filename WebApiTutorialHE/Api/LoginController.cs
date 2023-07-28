using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.Login;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResponse = await _loginService.Login(loginModel);

            if (loginResponse.result == 0)
            {
                return NotFound();
            }
            return Ok(loginResponse);
        }

    }
}
