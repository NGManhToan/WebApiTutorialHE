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
        //public async Task<List<UserListModel>> QueryFindAccount(string search)
        //{
        //    var query = @"SELECT * FROM user WHERE id LIKE @search";
        //    var parameters = new { Search = "%" + search + "%" };
        //    return await _sharingDapper.QueryAsync<UserListModel>(query, parameters);
        //}

        ////Lấy thông tin admin hiện tại
        //public async Task<List<UserListModel>> QueryListAdminAccount()
        //{
        //    var query = @"select *
        //                from `user` u join userrole ur on u.Id = ur.UserId
        //                where RoleId=1";
        //    return await _sharingDapper.QueryAsync<UserListModel>(query);
        //}

    }
}
