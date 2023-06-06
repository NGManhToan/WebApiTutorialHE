﻿using System.Data;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IAccountService
    {
        Task<List<AccountListModel>> GetAccountListModels();
        Task<Account> PutAccountUpdateModel(AccountUpdateModel model);
        Task<Account> PostAccountModel(AccountListModel model);
        Task<string> DeleteAccountModel(int id);
        Task<Account>FillterAccountModel(int id/*, string email*/);
        Task<List<AccountListModel>> FindAccountModel(string search);

        //Lấy thông tin admin hiện tại
        Task<List<AccountListModel>> GetAccountListAdminModel();
        DataTable GetDataTable();
    }
}
