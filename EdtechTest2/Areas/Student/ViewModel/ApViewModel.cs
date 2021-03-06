using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Student.ViewModel
{
    public class ApViewModel
    {
        public string name { get; set; }
        public string email { get; set; }

        public IFormFile fd { get; set; }


    }
}
