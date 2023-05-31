﻿using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IAccountService
    {
        Task<List<AccountListModel>> GetAccountListModels();
        Task<Account> PutAccountUpdateModel(AccountUpdateModel model);
        Task<Account> PostAccountModel(AccountListModel model);
    }
}