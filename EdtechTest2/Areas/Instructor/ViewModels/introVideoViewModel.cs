using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class introVideoViewModel
    {
        public IFormFile CourseIntroductionVideo { get; set; }

        public int CourseId { get; set; }


        public string VideoDuration { get; set; }
    }
}
