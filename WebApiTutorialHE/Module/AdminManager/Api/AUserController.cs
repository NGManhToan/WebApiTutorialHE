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

        [HttpPost]
        public async Task<IActionResult>GetListAccountUser(OSearchAdminModel oSearchAdmin)
        {

            ulong userId = Utils.GetUserIdFromToken(Request);

            var userList = await _userService.GetListAccountUser(userId, oSearchAdmin);
            try
            {
                if (userList == null)
                {
                    return Ok(new ObjectResponse
                    {
                        result = 0,
                        message = "Lay du lieu that bai"
                    });
                }

                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Lay du lieu thanh cong",
                    content = new
                    {
                        Users = userList,
                       
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = ex.Message.ToString()
                });
            }
        }
    }
}
