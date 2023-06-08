using WebApiTutorialHE.Models.UtilsProject;
using System.Security.Claims;
using WebApiTutorialHE.Manager.Token.Interface;
using WebApiTutorialHE.Database.SharingModels;

namespace WebApiTutorialHE.Manager.Token
{
    public class Signature
    {
        public static JWTContainerModel GetJWTContainerModel(string Id, string email, string password, string role)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Id),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.AuthenticationMethod, password),
                    new Claim(ClaimTypes.Role, role)
                }
            };
        }

        public static bool CheckTokenValid(string token)
        {
            IAuthService authService = new JWTService(Utils.KeyToken);

            if (authService.IsTokenValid(token))
            {
                return true;
            }

            return false;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
