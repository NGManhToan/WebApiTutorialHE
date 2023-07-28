using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class VerificationCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime? ExpirationTime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
