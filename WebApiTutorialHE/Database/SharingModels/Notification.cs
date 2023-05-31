using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Notification
    {
        public int NotifyId { get; set; }
        public int? ItemId { get; set; }
        public int? FromUserId { get; set; }
        public DateTime NotifyDate { get; set; }
        public string NotifyContent { get; set; } = null!;
        public string? Url { get; set; }
        public string? Type { get; set; }
        public int? IdRegistation { get; set; }

        public virtual User? FromUser { get; set; }
    }
}
