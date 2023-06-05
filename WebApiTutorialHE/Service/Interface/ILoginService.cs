using WebApiTutorialHE.Models.Login;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface ILoginService
    {
        Task<ObjectResponse> Login(LoginModel loginModel);
    }
}
