namespace WebApiTutorialHE.Models.User
{
    public class UserRegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Class { get; set; }
        public string StudentCode { get; set; }
        public int FacultyId { get; set; }
        public IFormFile UrlAvatar { get; set; }
    }
}
