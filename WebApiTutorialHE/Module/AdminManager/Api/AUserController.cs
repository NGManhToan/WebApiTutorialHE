using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.User;
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

        
    }
}
