using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Itemfeedbacks = new HashSet<Itemfeedback>();
            Media = new HashSet<Medium>();
            Notifications = new HashSet<Notification>();
            Registrations = new HashSet<Registration>();
            Violationreports = new HashSet<Violationreport>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Status { get; set; }
        public string? Hastag { get; set; }
        public string? EdocUrl { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public string DesiredStatus { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User LastModifiedByNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Itemfeedback> Itemfeedbacks { get; set; }
        public virtual ICollection<Medium> Media { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Violationreport> Violationreports { get; set; }
    }
}
