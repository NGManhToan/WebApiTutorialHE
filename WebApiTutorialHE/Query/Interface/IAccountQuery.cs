using WebApiTutorialHE.Models.Account;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IAccountQuery
    {
        Task<List<AccountListModel>> QueryListAccount();
        
    }
}
