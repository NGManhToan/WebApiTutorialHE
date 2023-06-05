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
            var query = @"select user_id ,email,password,type,full_name,student_code,phone_number,Class
                            from accounts ac join users u on ac.account_id=u.account_id
                            where email=@Email and password=@Password;";
            return await _sharingDapper.QuerySingleAsync<LoginSuccessModel>(query, new
            {
                Email = loginModel.Email,
                Password = password,
            });
        }
    }
}
