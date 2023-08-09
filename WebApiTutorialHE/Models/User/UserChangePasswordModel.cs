using System.ComponentModel.DataAnnotations;

namespace WebApiTutorialHE.Models.User
{
    public class UserChangePasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}
