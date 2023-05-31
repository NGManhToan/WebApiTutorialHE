using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class ItemFeedback
    {
        public int ItemFeedbackId { get; set; }
        public int? ItemId { get; set; }
        public int? UserId { get; set; }
        public string FeedbackContent { get; set; } = null!;
        public DateTime FeedbackDate { get; set; }

        public virtual Item? Item { get; set; }
        public virtual User? User { get; set; }
    }
}
