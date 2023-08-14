using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Mail;
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
        public async Task<IActionResult> ChangePassword([FromForm]UserChangePasswordModel userChangePassword)
        {
            var objectResponse = await _userService.ChangePassword(userChangePassword);

            return Ok(objectResponse);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordUser([FromForm]ChangepasswordModel userChangePassword)
        {
            var objectResponse = await _userService.ChangePasswordUser(userChangePassword);

            return Ok(objectResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel userRegisterModel, IFormFile? fileName)
        {

            var register = await _userService.Register(userRegisterModel, fileName);
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
        public async Task<IActionResult> GetAdmin()
        {
            var accountList = await _userService.GetAllAdmin();

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
        public async Task<IActionResult> GetProfileByUser()
        {

            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var profile = await _userService.QueryFrofile(forceInfo.UserId);
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
        public async Task<IActionResult> QueryFrofileSharing()
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var profileSharing  = await _userService.QueryFrofileSharing(forceInfo.UserId);
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
        public async Task<IActionResult> QueryItemFeedback()
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var itemFrofile= await _userService.QueryItemFeedback(forceInfo.UserId);
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
        [HttpGet]
        public async Task<IActionResult> RecipientInfor(int id)
        {
            var account = await _userService.RecipientInfor(id);

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy danh sách thành công",
                content = new
                {
                    account=account
                }
            });
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotAsync(UserForgotPasswordModel userForgot)
        {
            var result = await _userService.ForgotPassword(userForgot);

            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUserID(int id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>IdentifyOTP(int userId, string otpCode)
        {
            var result = await _userService.IdentifyOTP(userId, otpCode);
            return Ok(result);
        }
    }
}
