using WebApiTutorialHE.Models.Login;
using WebApiTutorialHE.Module.AdminManager.Model;
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
                          LEFT JOIN UserRole ur ON u.Id = ur.UserId
                          WHERE u.Email = @Email AND u.Password = @Password
                          GROUP BY Id, UrlAvatar, FullName";
            return await _sharingDapper.QuerySingleAsync<LoginSuccessModel>(query, new
            {
                email = loginModel.Email,
                Password = password,
            });
        }
    }
}
