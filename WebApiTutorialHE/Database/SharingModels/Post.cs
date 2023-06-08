using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Imageposts = new HashSet<Imagepost>();
            Itemfeedbacks = new HashSet<Itemfeedback>();
            Notifications = new HashSet<Notification>();
            Registations = new HashSet<Registation>();
            Violationreports = new HashSet<Violationreport>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? VideoUrl { get; set; }
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

        public virtual Category Category { get; set; } = null!;
        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User LastModifiedByNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Imagepost> Imageposts { get; set; }
        public virtual ICollection<Itemfeedback> Itemfeedbacks { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Registation> Registations { get; set; }
        public virtual ICollection<Violationreport> Violationreports { get; set; }
    }
}
