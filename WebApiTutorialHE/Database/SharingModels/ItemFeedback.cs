using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Itemfeedback
    {
        public Itemfeedback()
        {
            Violationreports = new HashSet<Violationreport>();
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User LastModifiedByNavigation { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<Violationreport> Violationreports { get; set; }
    }
}
