using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class AddLectureViewModel
    {
        [Required]
        public string LectureName { get; set; }

        [Required]

        public int SectionId { get; set; }

        
        public string LectureDescription { get; set; }

        public string LectureArticle { get; set; }


        public IFormFile LectureVideo { get; set; }

        public int LectureVideoLength { get; set; }


        public IFormFile DownlodableContent { get; set; }


    }
}
