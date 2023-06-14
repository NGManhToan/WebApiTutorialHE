using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class NotificationView
    {
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public int NotificationId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ViewedAt { get; set; }

        public virtual Notification Notification { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
    }
}
