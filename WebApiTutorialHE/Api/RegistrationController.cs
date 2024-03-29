﻿using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Itemfeedback;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service;
using WebApiTutorialHE.Service.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> GetRegisterByIdUser()
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var registration=await _registrationService.GetListRegistation(forceInfo.UserId);
            if (registration != null)
            {
                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Lấy danh sách thành công",
                    content = new
                    {
                        registration = registration
                    }
                });
            }
            return Ok(new ObjectResponse
            {
                result=0,
                message="Lấy danh sách không công",
            });
        }
        [HttpPut]
        public async Task<IActionResult>UpdateRegistration(RegistationUpdateModel registationUpdate)
        {
            var update= await _registrationService.UpdateRegistation(registationUpdate);
            return Ok(update);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRegistrationID()
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };

            var delete = await _registrationService.DeleteRegistation(forceInfo.UserId);
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
            var forceInfo = new ForceInfo
            {
                DateNow = Utils.DateNow(),
                UserId = Utils.GetUserIdFromToken(Request),
            };
            var create = await _registrationService.CreateRegistation(registationPost, forceInfo);
            if(create != null)
            {
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
            return Ok(new ObjectResponse
            {
                result = 0,
                message = "Thêm không thành công",
                
            });

        }
        [HttpPut]
        public async Task<IActionResult> UpdateStatus(UpdateStatus updateStatus)
        {
            var update = await _registrationService.UpdateStatus(updateStatus);

            if(update != null)
            {
                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Đổi thành công",
                    content = new
                    {
                        update = update
                    }
                });
            }
            return Ok(new ObjectResponse
            {
                result = 0,
                message = "Đổi không thành công",
                
            });
        }
        [HttpGet]
        public async Task<IActionResult> CountNumRegistation(int postId, int createdBy)
        {
            var count= await _registrationService.NumRegistation(postId, createdBy);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công",
                content = new
                {
                    count = count
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetListRegistrationHaveProposer(int postId)
        {
			var getList = await _registrationService.GetListRegistrationHaveProposer(postId);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Lấy thành công",
                content = new
                {
                    getList = getList
                }
            });
        }
    }
}
