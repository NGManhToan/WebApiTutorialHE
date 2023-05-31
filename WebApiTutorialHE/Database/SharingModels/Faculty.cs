using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Faculty
    {
        public Faculty()
        {
            Users = new HashSet<User>();
        }

        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
