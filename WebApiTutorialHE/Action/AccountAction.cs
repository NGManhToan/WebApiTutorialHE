using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiTutorialHE.Database.SharingModels;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.UtilsProject;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        public async Task<string> ActionDeleteAccount(int id)
        {
            var deleteAccount = await _sharingContext.Accounts.SingleOrDefaultAsync(x => x.AccountId == id);
            _sharingContext.Remove(deleteAccount);
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";
        }

        public async Task<Account> ActionFillterAccount(int id/*, string email*/)
        {
            var fillterAccount = await _sharingContext.Accounts.SingleOrDefaultAsync(x => x.AccountId.Equals(id));
            if (fillterAccount == null)
            {
                // Xử lý khi không tìm thấy tài khoản
                // Ví dụ: throw một ngoại lệ hoặc trả về giá trị mặc định
                throw new Exception("Không tìm thấy tài khoản."); // Ví dụ sử dụng ngoại lệ
                                                                  // return null; // Ví dụ trả về giá trị mặc định
            }

            return fillterAccount;
        }

        

    }
}
