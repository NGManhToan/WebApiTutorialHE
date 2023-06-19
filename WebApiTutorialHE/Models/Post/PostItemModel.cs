using System.ComponentModel;

namespace WebApiTutorialHE.Models.Post
{
    public class PostItemModel
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public string Content { get; set; }
        public IFormFile UrlImage { get; set; }
        [DefaultValue(1)]
        public string Type { get; set; }
        public int CreatedBy { get; set; }
        
    }
}
