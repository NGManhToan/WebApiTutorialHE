﻿using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Violationreport
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ItemFeedbackId { get; set; }
        public int ViolatorId { get; set; }
        public string Description { get; set; } = null!;
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual Itemfeedback ItemFeedback { get; set; } = null!;
        public virtual User LastModifiedByNavigation { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual User Violator { get; set; } = null!;
    }
}
