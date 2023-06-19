namespace WebApiTutorialHE.Models.Post
{
    public class CreateCommentModel
    {
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public int CreateBy { get; set; }
        public string Content { get; set; }
    }
}
