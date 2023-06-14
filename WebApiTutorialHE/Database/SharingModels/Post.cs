using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            ItemFeedbacks = new HashSet<ItemFeedback>();
            Media = new HashSet<Medium>();
            Notifications = new HashSet<Notification>();
            Registrations = new HashSet<Registration>();
            ViolationReports = new HashSet<ViolationReport>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public float? Price { get; set; }
        public string? Status { get; set; }
        public string? EdocUrl { get; set; }
        public string DesiredStatus { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User LastModifiedByNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ItemFeedback> ItemFeedbacks { get; set; }
        public virtual ICollection<Medium> Media { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<ViolationReport> ViolationReports { get; set; }
    }
}
