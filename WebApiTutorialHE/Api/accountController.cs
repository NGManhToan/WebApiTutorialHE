using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public accountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public static  List<Account> accounts = new List<Account>();
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var accountList = await _accountService.GetAccountListModels();
            return Ok(accountList);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(AccountUpdateModel model)
        {
            var updateAccount = await _accountService.PutAccountUpdateModel(model);
            return Ok(updateAccount);
        }
        [HttpPost]
        public async Task<IActionResult>CreateAccount(AccountListModel model)
        {
            var createAccount=await _accountService.PostAccountModel(model);
            return Ok(createAccount);
        }
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