using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CourseIntroVideoUploadViewModel
    {
        //IFormFile CourseIntroductionVideo,int CourseId, int Duration

        public IFormFile CourseIntroductionVideo { get; set; }
        public int CourseId { get; set; }

        public int Duration { get; set; }
    }
}
