using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class AccountService:IAccountService
    {
        private readonly IAccountQuery _accountQuery;
        private readonly IAccountAction _accountAction;
        public AccountService(IAccountQuery accountQuery,IAccountAction accountAction)
        {
            _accountQuery = accountQuery;
            _accountAction = accountAction;
        }
        public async Task<List<AccountListModel>> GetAccountListModels()
        {
            return await _accountQuery.QueryListAccount();
        }
        public async Task<Account> PutAccountUpdateModel(AccountUpdateModel model)
        {
            return await _accountAction.AccountUpdateModels(model);
        }
        public async Task<Account> PostAccountModel(AccountListModel model)
        {
            return await _accountAction.ActionCreateAccount(model);
        }
        public async Task<string>DeleteAccountModel(int id)
        {
            return await _accountAction.ActionDeleteAccount(id);
        }
        public async Task<Account> FillterAccountModel(int id/*,string email*/)
        {
            return await _accountAction.ActionFillterAccount(id/*,email*/);
        }
        public async Task<List<AccountListModel>> FindAccountModel(string search)
        {
            return await _accountQuery.QueryFindAccount(search);
        }

        //Lấy thông tin admin hiện tại
        public async Task<List<AccountListModel>> GetAccountListAdminModel()
        {
            return await _accountQuery.QueryListAdminAccount();
        }
    }
}
