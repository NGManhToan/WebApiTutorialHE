using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTutorialHE.Module.AdminManager.Models.Admin
{
    public class AdminCreateManagerModel
    {
        [DefaultValue("")]
        public string Name { get; set; }

        [DefaultValue("")]
        public string Email { get; set; }

        [DefaultValue("")]
        public string Phone { get; set; }

        [DefaultValue("")]
        public string Password { get; set; }

        [DefaultValue("")]
        public string RepeatPassword { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
