using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using P2N_Pet_API.Module.AdminManager.Models.Admin;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Manager.FilterAttr;
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
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var objectResponse = await _userService.ChangePasswordUser(userChangePassword,forceInfo);

            return Ok(objectResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel userRegisterModel, IFormFile? fileName)
        {

            var register = await _userService.Register(userRegisterModel, fileName);
            return Ok(register);

        }
        [ManagerAccess]
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

        [ManagerAccess]
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
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var updateUser = await _userService.UpdateProfile(userUpdate, forceInfo);
            return Ok(updateUser);
        }


        [HttpPost]
        public async Task<IActionResult> IdentifyOTPUpdate( string otpCode)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var result = await _userService.IdentifyOTPUpdate(forceInfo, otpCode);
            var profile = await _userService.QueryFrofile(forceInfo.UserId);
            if(result != null)
            {
                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Thanh cong",
                    content=profile
                });
            }
            return Ok(new ObjectResponse
            {
                result = 0,
                message = "That bai",
            });
        }



        [HttpGet]
        public async Task<IActionResult> RecipientInfor()
        {
			var forceInfo = new ForceInfo
			{
				UserId = Utils.GetUserIdFromToken(Request),
				DateNow = Utils.DateNow()
			};
			var account = await _userService.RecipientInfor(forceInfo.UserId);

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
        [ManagerAccess]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserID(AdminDeleteModel adminDelete)
        {
            if(adminDelete.UserId == 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Vui lòng nhập UserId"
                });
            }
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            try
            {
                var result = await _userService.DeleteUser(forceInfo, adminDelete);

                if (result == null)
                {
                    return Ok(new ObjectResponse
                    {
                        result = 0,
                        message = "Delete thất bại. Vui lòng thử lại"
                    });
                }

                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Delete thành công.",
                    content = new
                    {
                        result
                    }
                });
            }
            catch (Exception e)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = e.Message.ToString()
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult>IdentifyOTP(int userId, string otpCode)
        {
            var result = await _userService.IdentifyOTP(userId, otpCode);
            return Ok(result);
        }
    }
}
