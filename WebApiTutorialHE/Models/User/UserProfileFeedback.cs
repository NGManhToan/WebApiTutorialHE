namespace WebApiTutorialHE.Models.User
{
    public class UserProfileFeedback
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Item { get; set; }
        public string Content { get; set; }
    }
}
