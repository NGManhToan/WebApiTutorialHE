namespace WebApiTutorialHE.Models.User
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentCode { get; set; }
        public string Faculty { get; set;}
        public string Session { get; set; }
        public int Items { get; set; }
        public int Shared { get ; set; }

    }
}
