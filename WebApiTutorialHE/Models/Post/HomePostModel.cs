namespace WebApiTutorialHE.Models.Post
{
    public class HomePostModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
    }
}
