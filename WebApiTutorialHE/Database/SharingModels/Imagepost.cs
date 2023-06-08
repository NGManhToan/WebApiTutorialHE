using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Imagepost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
