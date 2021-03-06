using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Admin.ViewModels
{
    public class PhotoEditViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string Link { get; set; }

        public string ContentString { get; set; }
        
        [Display(Name ="Update Image")]
        public IFormFile UpdatePhoto { get; set; }
    }
}
