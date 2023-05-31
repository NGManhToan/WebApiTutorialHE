using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Item
    {
        public Item()
        {
            ItemFeedbacks = new HashSet<ItemFeedback>();
            Registations = new HashSet<Registation>();
        }

        public int ItemId { get; set; }
        public string NameItem { get; set; } = null!;
        public string ImageItem { get; set; } = null!;
        public string? NoteItem { get; set; }
        public DateTime PostDate { get; set; }
        public int? UserId { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; } = null!;
        public string? FileEdoc { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<ItemFeedback> ItemFeedbacks { get; set; }
        public virtual ICollection<Registation> Registations { get; set; }
    }
}
