﻿using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IUserQuery
    {
        Task<List<UserListModel>> QueryListUser();
        //Task<List<UserListModel>> QueryFindAccount(string search);

        ////Lấy thông tin admin hiện tại
        //Task<List<UserListModel>> QueryListAdminAccount();
        Task<List<UserListModel>> QueryListAdmin();
        Task<UserProfileModel> QueryFrofile(int id );
        Task<List<UserProfileSharingModel>> QueryFrofileSharing(int id);
        Task<List<UserProfileFeedback>> QueryItemFeedback(int id);
        Task<RecipientInformationModel> QueryRecipientInfor(int id);
    }
}
