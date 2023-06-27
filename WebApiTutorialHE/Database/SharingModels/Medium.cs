using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Medium
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int? RegistrationId { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Registration? Registration { get; set; }
    }
}
