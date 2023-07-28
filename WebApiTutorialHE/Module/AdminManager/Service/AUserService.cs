using WebApiTutorialHE.Module.AdminManager.Model.User;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service
{
    public class AUserService: IAUserService
    {
        private readonly IAUserQuey _userQuey;
        public AUserService (IAUserQuey userQuey)
        {
            _userQuey = userQuey;
        }

        public async Task<List<AdminUserListModel>> GetListAccountUser(ulong userId, OSearchAdminModel oSearch)
        {
            var getAccount = await _userQuey.QueryGetAllUser(userId, oSearch);
            return getAccount;
        }
    }
}
