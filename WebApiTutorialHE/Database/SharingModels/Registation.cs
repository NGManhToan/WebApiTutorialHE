using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Registation
    {
        public int RegistationId { get; set; }
        public int? ItemId { get; set; }
        public int? RegisterId { get; set; }
        public DateTime RegistationDate { get; set; }
        public string? Content { get; set; }
        public int RegisterStatus { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? RegisterNotifi { get; set; }

        public virtual Item? Item { get; set; }
        public virtual User? Register { get; set; }
    }
}
