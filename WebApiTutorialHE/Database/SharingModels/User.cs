using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class User
    {
        public User()
        {
            ItemFeedbacks = new HashSet<ItemFeedback>();
            Items = new HashSet<Item>();
            NotificationViews = new HashSet<NotificationView>();
            Notifications = new HashSet<Notification>();
            Proposals = new HashSet<Proposal>();
            Registations = new HashSet<Registation>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string StudentCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string? UrlAvartar { get; set; }
        public int? YearAdmission { get; set; }
        public int? AccountId { get; set; }
        public int? FacultyId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<ItemFeedback> ItemFeedbacks { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<NotificationView> NotificationViews { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public virtual ICollection<Registation> Registations { get; set; }
    }
}
