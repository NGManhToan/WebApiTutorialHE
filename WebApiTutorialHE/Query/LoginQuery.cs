using WebApiTutorialHE.Models.Login;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Query
{
    public class LoginQuery:ILoginQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public LoginQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }
        public async Task<LoginSuccessModel>Login(LoginModel loginModel, string password)
        {
            var query = @"SELECT Id, UrlAvatar, FullName, group_concat(ur.RoleId) Roles 
                        FROM User u 
                        	left join UserRole ur on u.id=ur.UserId
                            where Email = @Email and Password = @Password
                                    group by Id, UrlAvatar, FullName ;";
            return await _sharingDapper.QuerySingleAsync<LoginSuccessModel>(query, new
            {
                email = loginModel.Email,
                Password = password,
            });
        }
    }
}
