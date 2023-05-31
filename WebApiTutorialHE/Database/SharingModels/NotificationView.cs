using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class NotificationView
    {
        public int Id { get; set; }
        public int? NotificationId { get; set; }
        public int? UserIdView { get; set; }
        public DateTime? ViewedAt { get; set; }
        public int? StatusView { get; set; }

        public virtual User? UserIdViewNavigation { get; set; }
    }
}
