using WebApiTutorialHE.Models.Account;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IAccountQuery
    {
        Task<List<AccountListModel>> QueryListAccount();
        Task<List<AccountListModel>> QueryFindAccount(string search);

        //Lấy thông tin admin hiện tại
        Task<List<AccountListModel>> QueryListAdminAccount();

    }
}
