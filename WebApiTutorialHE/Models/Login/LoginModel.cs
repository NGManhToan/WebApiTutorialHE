using System.ComponentModel;

namespace WebApiTutorialHE.Models.Login
{
    public class LoginModel
    {
        [DefaultValue("")]
        public string Email { get; set; }
        [DefaultValue("")]
        public string? Password { get; set; }
    }
}
