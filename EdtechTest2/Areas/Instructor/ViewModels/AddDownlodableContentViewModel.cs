using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class AddDownlodableContentViewModel
    {
        public int id { get; set; }

        public  IFormFile downlodableContent { get; set; }
    }
}
