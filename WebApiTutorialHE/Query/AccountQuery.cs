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

        
    }
}
