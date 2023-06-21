using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class User
    {
        public User()
        {
            CategoryCreatedByNavigations = new HashSet<Category>();
            CategoryLastModifiedByNavigations = new HashSet<Category>();
            CommentCreatedByNavigations = new HashSet<Comment>();
            CommentLastModifiedByNavigations = new HashSet<Comment>();
            InverseCreatedByNavigation = new HashSet<User>();
            InverseLastModifiedByNavigation = new HashSet<User>();
            ItemFeedbackCreatedByNavigations = new HashSet<ItemFeedback>();
            ItemFeedbackLastModifiedByNavigations = new HashSet<ItemFeedback>();
            NotificationCreatedByNavigations = new HashSet<Notification>();
            NotificationFromUsers = new HashSet<Notification>();
            NotificationLastModifiedByNavigations = new HashSet<Notification>();
            NotificationViews = new HashSet<NotificationView>();
            PostCreatedByNavigations = new HashSet<Post>();
            PostLastModifiedByNavigations = new HashSet<Post>();
            RegistrationCreatedByNavigations = new HashSet<Registration>();
            RegistrationLastModifiedByNavigations = new HashSet<Registration>();
            ViolationReportCreatedByNavigations = new HashSet<ViolationReport>();
            ViolationReportLastModifiedByNavigations = new HashSet<ViolationReport>();
            ViolationReportViolators = new HashSet<ViolationReport>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string StudentCode { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string Class { get; set; } = null!;
        public string? UrlAvatar { get; set; }
        public bool? IsOnline { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual Faculty Faculty { get; set; } = null!;
        public virtual User? LastModifiedByNavigation { get; set; }
        public virtual ICollection<Category> CategoryCreatedByNavigations { get; set; }
        public virtual ICollection<Category> CategoryLastModifiedByNavigations { get; set; }
        public virtual ICollection<Comment> CommentCreatedByNavigations { get; set; }
        public virtual ICollection<Comment> CommentLastModifiedByNavigations { get; set; }
        public virtual ICollection<User> InverseCreatedByNavigation { get; set; }
        public virtual ICollection<User> InverseLastModifiedByNavigation { get; set; }
        public virtual ICollection<ItemFeedback> ItemFeedbackCreatedByNavigations { get; set; }
        public virtual ICollection<ItemFeedback> ItemFeedbackLastModifiedByNavigations { get; set; }
        public virtual ICollection<Notification> NotificationCreatedByNavigations { get; set; }
        public virtual ICollection<Notification> NotificationFromUsers { get; set; }
        public virtual ICollection<Notification> NotificationLastModifiedByNavigations { get; set; }
        public virtual ICollection<NotificationView> NotificationViews { get; set; }
        public virtual ICollection<Post> PostCreatedByNavigations { get; set; }
        public virtual ICollection<Post> PostLastModifiedByNavigations { get; set; }
        public virtual ICollection<Registration> RegistrationCreatedByNavigations { get; set; }
        public virtual ICollection<Registration> RegistrationLastModifiedByNavigations { get; set; }
        public virtual ICollection<ViolationReport> ViolationReportCreatedByNavigations { get; set; }
        public virtual ICollection<ViolationReport> ViolationReportLastModifiedByNavigations { get; set; }
        public virtual ICollection<ViolationReport> ViolationReportViolators { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
