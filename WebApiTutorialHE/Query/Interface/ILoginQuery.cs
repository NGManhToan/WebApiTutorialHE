using WebApiTutorialHE.Models.Login;

namespace WebApiTutorialHE.Query.Interface
{
    public interface ILoginQuery
    {
        Task<LoginSuccessModel>Login(LoginModel loginModel, string password);
    }
}
