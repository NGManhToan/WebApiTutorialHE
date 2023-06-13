namespace WebApiTutorialHE.Models.User
{
    public class UserForgotPasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}
