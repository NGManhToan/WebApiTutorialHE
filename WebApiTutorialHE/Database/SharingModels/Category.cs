using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class Category
    {
        public int IdCategory { get; set; }
        public string NameCategory { get; set; } = null!;
    }
}
