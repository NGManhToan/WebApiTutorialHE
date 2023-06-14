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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accountList = await _userService.GetAllUser();

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy danh sách thành công",
                content = new
                {
                    account = accountList
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileByUser(int id)
        {
            var profile = await _userService.QueryFrofile(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy người dùng thành công",
                content = new
                {
                    Profile = profile
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult> QueryFrofileSharing(int id)
        {
            var profileSharing  = await _userService.QueryFrofileSharing(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công",
                content = new
                {
                    profileSharing = profileSharing
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult> QueryItemFeedback(int id)
        {
            var itemFrofile= await _userService.QueryItemFeedback(id);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công danh sách",
                content = new
                {
                    itemFrofile = itemFrofile
                }
            });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromForm] UserUpdateModel userUpdate)
        {
            var updateUser = await _userService.UpdateProfile(userUpdate);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công danh sách",
                content = new
                {
                    userUpdate = updateUser
                }
            });
        }
        //[HttpPut]
        //public async Task<IActionResult> UpdatePassWord()
        //{
        //    await _userService.UpdatePassword();

        //    return Ok();
        //}
    }
}
