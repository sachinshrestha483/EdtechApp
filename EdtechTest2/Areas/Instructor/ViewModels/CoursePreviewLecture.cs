using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CoursePreviewLecture
    {

        public int lecId { get; set; }

        public string name { get; set; }

        public string lectureDescription { get; set; }


        public double length { get; set; }


        public string  lectureArticle { get; set; }


        public string lectureContentLink { get; set; }


        public List<CoursePreviewContentViewModel> downlodableContents { get; set; }

    }
}
