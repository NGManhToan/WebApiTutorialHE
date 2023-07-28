using System.ComponentModel;

namespace WebApiTutorialHE.Module.AdminManager.Model.Login
{
    public class ALoginModel
    {
        [DefaultValue("")]
        public string Email { get; set; }
        [DefaultValue("")]
        public string? Password { get; set; }
    }
}
