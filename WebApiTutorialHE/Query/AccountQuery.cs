using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Query
{
    public class AccountQuery:IAccountQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public AccountQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<AccountListModel>> QueryListAccount()
        {
            var query =
                @"SELECT * 
                FROM sharingtogether.accounts";
            return await _sharingDapper.QueryAsync<AccountListModel>(query);
        }
        public async Task<List<AccountListModel>> QueryFindAccount(string search)
        {
            var query = @"SELECT * FROM sharingtogether.accounts WHERE account_id LIKE @search";
            var parameters = new { Search = "%" + search + "%" };
            return await _sharingDapper.QueryAsync<AccountListModel>(query, parameters);
        }

        //Lấy thông tin admin hiện tại
        public async Task<List<AccountListModel>> QueryListAdminAccount()
        {
            var query = @"SELECT * FROM sharingtogether.accounts WHERE type=1";
            return await _sharingDapper.QueryAsync<AccountListModel>(query);
        }

    }
}
