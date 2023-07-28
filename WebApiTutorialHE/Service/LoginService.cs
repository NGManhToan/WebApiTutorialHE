using WebApiTutorialHE.Models.Login;
using WebApiTutorialHE.Query;
using WebApiTutorialHE.Service.Interface;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Manager.Token;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using WebApiTutorialHE.Manager.Token.Interface;

namespace WebApiTutorialHE.Service
{
    public class LoginService:ILoginService
    {
        private readonly ILoginQuery _loginQuery;
        public LoginService(ILoginQuery loginQuery)
        {
            _loginQuery = loginQuery;
        }
        public async Task<ObjectResponse> Login(LoginModel loginModel)
        {
            loginModel.Password = Encryptor.SHA256Encode(loginModel.Password);
            var login = await _loginQuery.Login(loginModel, loginModel.Password);

            if (login == null)
            {
                return new ObjectResponse
                {
                    result = 0,
                    message = "Đăng nhập thất bại"
                };
            }
            IAuthContainerModel model = Signature.GetJWTContainerModel(login.Id.ToString(), loginModel.Email, loginModel.Password, login.Roles.ToString());
            IAuthService authService = new JWTService(model.SecretKey);

            var token = authService.GenerateToken(model);

            return new ObjectResponse
            {
                result = 1,
                message = "Đăng nhập thành công",
                content = new
                {
                    login,
                    token
                }
            };
        }
    }
}
