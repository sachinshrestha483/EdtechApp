using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.User.ViewModels
{
    public class UserPotoUploadViewModel
    {

        [Required]
        public IFormFile Photo { get; set; }
        public string Link { get; set; }

        public bool isPotoUploaded { get; set; }

    }
}
