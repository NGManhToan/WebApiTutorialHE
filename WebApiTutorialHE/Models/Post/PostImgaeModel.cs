using System.Drawing;

namespace WebApiTutorialHE.Models.Post
{
    public class PostImgaeModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public IFormFile Image { get; set; }
        public int RegistrationId { get; set; }
    }
}
