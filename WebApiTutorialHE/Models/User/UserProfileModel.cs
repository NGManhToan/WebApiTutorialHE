namespace WebApiTutorialHE.Models.User
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public string StudentCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public string Faculty { get; set;}
        public string UrlAvatar { get; set; }
        public string Session { get; set; }
        public int Items { get; set; }
        public int Shared { get ; set; }

    }
}
