namespace WebApiTutorialHE.Models.Post
{
    public class HomeWishModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public string DesiredStatus { get; set; }
        public string ImageUrl { get; set; }
    }
}
