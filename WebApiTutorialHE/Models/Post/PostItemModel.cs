namespace WebApiTutorialHE.Models.Post
{
    public class PostItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public string Content { get; set; }
        public string UrlImage { get; set; }
        public string Type { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
