using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IAccountAction
    {
        Task<Account> AccountUpdateModels(AccountUpdateModel model);
        Task<Account> ActionCreateAccount(AccountListModel model);
        Task<string> ActionDeleteAccount(AccountListModel model);
    }
}
