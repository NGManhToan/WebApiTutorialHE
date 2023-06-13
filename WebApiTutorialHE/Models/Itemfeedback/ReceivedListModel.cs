using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiTutorialHE.Models.Itemfeedback
{
    public class ReceivedListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }
}
