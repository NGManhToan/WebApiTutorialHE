using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationViews = new HashSet<NotificationView>();
        }

        public int Id { get; set; }
        public int? FromUserId { get; set; }
        public int RegistrationId { get; set; }
        public int NotificationTypeId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual User? FromUser { get; set; }
        public virtual User? LastModifiedByNavigation { get; set; }
        public virtual NotificationType NotificationType { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual Registration Registration { get; set; } = null!;
        public virtual ICollection<NotificationView> NotificationViews { get; set; }
    }
}
