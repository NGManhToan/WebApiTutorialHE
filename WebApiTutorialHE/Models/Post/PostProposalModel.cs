using System.ComponentModel;

namespace WebApiTutorialHE.Models.Post
{
    public class PostProposalModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string DesiredStatus { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        [DefaultValue (2)]
        public string Type { get; set; }
    }
}
