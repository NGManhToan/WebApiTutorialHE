using System;
using System.Collections.Generic;

namespace WebApiTutorialHE.Database.SharingModels
{
    public partial class ImgItem
    {
        public int IdImgItem { get; set; }
        public int IdItem { get; set; }
        public string Image { get; set; } = null!;
    }
}
