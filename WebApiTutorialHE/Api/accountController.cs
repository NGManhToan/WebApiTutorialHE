using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        private readonly IUserService _accountService;

        public accountController(IUserService accountService)
        {
            _accountService = accountService;
        }

        

        //[HttpPut]
        //public async Task<IActionResult> UpdateAccount(AccountUpdateModel model)
        //{
        //    var updateAccount = await _accountService.PutAccountUpdateModel(model);
        //    return Ok(updateAccount);
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateAccount(UserListModel model)
        //{
        //    var createAccount = await _accountService.PostAccountModel(model);
        //    return Ok(createAccount);
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult>DeleteAccount( int id)
        //{
        //    var deleteAccount = await _accountService.DeleteAccountModel(id);
        //    return Ok(deleteAccount);
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> FillterAccount(int id/*,string email*/)
        //{

        //    var fillterAccount = await _accountService.FillterAccountModel(id/*,email*/);
        //    return Ok(fillterAccount);
        //}
        //[HttpGet("{search}")]///Tìm kiếm theo ID
        //public async Task<IActionResult> FindAccount(string search)
        //{
        //    var findAccount = await _accountService.FindAccountModel(search);
        //    return Ok(findAccount);
        //}
        ////Lấy thông tin admin hiện tại
        //[HttpGet("admin")]
        //public async Task<IActionResult> ListAdminAccount()
        //{
        //    var listAdmin = await _accountService.GetAccountListAdminModel();
        //    return Ok(listAdmin);
        //}
        //[HttpGet("export-excel")]
        //public ActionResult ExportExcel()
        //{
        //    var _empdata = _accountService.GetDataTable();
        //    using(XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.AddWorksheet(_empdata, "Emloyee Records");
        //        using(MemoryStream ms = new MemoryStream())
        //        {
        //            wb.SaveAs(ms);
        //            return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Sample.xlsx");
        //        }
        //    }
        //}
        //[HttpGet("export-pdf")]
        //public IActionResult ExportPdf()
        //{
        //    var dataTable = _accountService.GetDataTable(); // Lấy dữ liệu từ DataTable

        //    var filePath = "accounts.pdf"; // Đường dẫn và tên file PDF đầu ra

        //    _accountService.ExportDataTableToPdf(dataTable, filePath); // Xuất DataTable ra file PDF

        //    // Trả về file PDF đính kèm
        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    return File(fileBytes, "application/pdf", "accounts.pdf");
        //}
        //[HttpPost]
        //public IActionResult Create(AccountListModel model)
        //{

        //    if (model == null)
        //    {
        //        return BadRequest("Dữ liệu không hợp lệ");
        //    }

        //    var account = new Account()
        //    {
        //        Email = model.email,
        //        Password = model.password,
        //        Type = model.type,
        //    };

        //    _sharingContext.Accounts.Add(account);
        //    _sharingContext.SaveChanges();

        //    return Ok(account);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var account = _sharingContext.Accounts.FirstOrDefault(a => a.AccountId == id);

        //    if (account == null)
        //    {
        //        return NotFound("Không tìm thấy tài khoản");
        //    }

        //    _sharingContext.Accounts.Remove(account);
        //    _sharingContext.SaveChanges();

        //    return Ok("Xóa tài khoản thành công");
        //}
        //[HttpPut]
        //public IActionResult Put(AccountListModel model)
        //{
        //    var updateAccount = _sharingContext.Accounts.FirstOrDefault(a => a.AccountId == model.accounts_id);
        //    if (model == null)
        //    {
        //        return NotFound("Không tìm thấy tài khoản");
        //    }
        //    updateAccount.Email = model.email;
        //    updateAccount.Password = model.password;

        //    _sharingContext.Accounts.Update(updateAccount);
        //    _sharingContext.SaveChanges();
        //    return Ok(updateAccount);
        //}

    }
}