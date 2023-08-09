using System.ComponentModel.DataAnnotations;

namespace WebApiTutorialHE.Models.User
{
    public class ChangepasswordModel
    {
        public int  id { get; set; }
        [Required, Display(Name = "Current password")]
        public string CurrentPassword { get; set; }
        [Required, Display(Name = "New password")]
        public string NewPassword { get; set; }
        [Required, Display(Name = "Confirm new password")]
        public string ConfirmNewPassword { get; set; }
    }
}
