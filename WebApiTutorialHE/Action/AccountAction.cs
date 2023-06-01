using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiTutorialHE.Database.SharingModels;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;


namespace WebApiTutorialHE.Action
{
    public class AccountAction:IAccountAction
    {
        private readonly SharingContext _sharingContext;

        public AccountAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }
        public static List<Account> accounts = new List<Account>();
        public async Task<Account> AccountUpdateModels(AccountUpdateModel model)
        {
            var upadateAccount = await _sharingContext.Accounts.FindAsync(model.account_id);

            if (upadateAccount != null)
            {
                upadateAccount.Email = model.email;
                upadateAccount.Password = model.password;

                _sharingContext.Accounts.Update(upadateAccount);
                await _sharingContext.SaveChangesAsync();
            }

            return upadateAccount;
        }
        public async Task<Account> ActionCreateAccount(AccountListModel model)
        {
            var createAccout = new Account()
            {
                AccountId = model.account_id,
                Email = model.email.Trim(),
                Password = Encryptor.MD5Hash(model.password.Trim()),
                Type = model.type,
            };
            _sharingContext.Add(createAccout);
            await _sharingContext.SaveChangesAsync();
            return createAccout;
        }
        public async Task<string> ActionDeleteAccount(AccountListModel model)
        {
            var deleteAccount = await _sharingContext.Accounts.FindAsync(model.account_id);
            _sharingContext.Remove(deleteAccount);
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";
        }


    }
}
