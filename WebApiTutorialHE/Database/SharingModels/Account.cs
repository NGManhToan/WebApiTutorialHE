using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Account
    {
        public Account()
        {
            Users = new HashSet<User>();
        }

        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Type { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
