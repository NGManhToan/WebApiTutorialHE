using WebApiTutorialHE.Models.User;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Query
{
    public class UserQuery:IUserQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public UserQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<List<UserListModel>> QueryListUser()
        {
            var query =
                @"SELECT * 
                FROM user";
            return await _sharingDapper.QueryAsync<UserListModel>(query);
        }
        public async Task<List<UserRoleModel>> QueryUserRoles()
        {
            var query = @"SELECT r.Id, r.Name, ur.UserId
                           from userrole ur 
	                        left join role r on ur.RoleId = r.Id";
            return await _sharingDapper.QueryAsync<UserRoleModel>(query);
        }

    }
}
