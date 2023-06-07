namespace WebApiTutorialHE.Models.User
{
    public class UserRegisterModel
    {
        public string full_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }
        public string RepeatPassword { get; set; }
        public string Class{ get;set;}
        public string student_code { get; set; }
        public IFormFile url_avatar { get; set; }
    }
}
