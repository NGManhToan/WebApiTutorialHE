using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Manager.FilterAttr;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.ViolationReport;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;


namespace WebApiTutorialHE.Module.AdminManager.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AViolationReportController:ControllerBase
    {
        private readonly IAViolationReportService _aViolationReportService;

        public AViolationReportController(IAViolationReportService aViolationReportService)
        {
            _aViolationReportService = aViolationReportService;
        }
        [ManagerAccess]
        [HttpGet]
        public async Task<IActionResult> ListReport()
        {
            try
            {
                var result = await _aViolationReportService.ListReport();
                if (result != null)
                {
                    return Ok(new ObjectResponse
                    {
                        result = 1,
                        message = "Lấy thành công danh sách",
                        content = result
                    });
                }
                else
                {
                    return Ok(new ObjectResponse
                    {
                        result = 0,
                        message = "Lấy thất bại"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        [ManagerAccess]
        [HttpGet]
        public async Task<IActionResult> ListReportIsActiveFalse()
        {
            try
            {
                var result = await _aViolationReportService.ListReportIsFalse();
                if (result != null)
                {
                    return Ok(new ObjectResponse
                    {
                        result = 1,
                        message = "Lấy thành công danh sách",
                        content = result
                    });
                }
                else
                {
                    return Ok(new ObjectResponse
                    {
                        result = 0,
                        message = "Lấy thất bại"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        [ManagerAccess]
        [HttpPut]
        public async Task<IActionResult>EditReport(int id)
        {
            var forceInfo = new ForceInfo
            {
                DateNow = Utils.DateNow(),
                UserId = Utils.GetUserIdFromToken(Request)
            };
            var result = await _aViolationReportService.EditViolationReport(id, forceInfo);
            if (result != null)
            {
                return Ok(new ObjectResponse { result = 1, message = "Cập nhật thành công",content = result });
            }
            return Ok(new ObjectResponse { result = 0, message = "Cập nhật thất bại" });
        }
        [ManagerAccess]
        [HttpDelete]
        public async Task<IActionResult>Deletereport(int id)
        {
            var forceInfo = new ForceInfo
            {
                DateNow = Utils.DateNow(),
                UserId = Utils.GetUserIdFromToken(Request)
            };

            var result = await _aViolationReportService.RemoveReport(id, forceInfo);
            if (result != null)
            {
                return Ok(new ObjectResponse
                {
                    result = 1,
                    message = "Delete success",
                    content = result
                });
            }
            return Ok(new ObjectResponse
            {
                result = 0,
                message = "Delete failure"
            });
        }
    }
}
