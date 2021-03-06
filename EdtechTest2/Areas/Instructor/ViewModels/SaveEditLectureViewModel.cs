using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class SaveEditLectureViewModel
    {

        public int Id { get; set; }
        public string EditLectureArticle  { get; set; }

        public string EditLectureDescription { get; set; }

        public IFormFile EditLectureVideo { get; set; }

        public int Length { get; set; }
    }
}
