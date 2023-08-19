using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public sbyte? Status { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
