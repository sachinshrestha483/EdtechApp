using EdtechTest2.Models;
using EdtechTest2.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CoursePreviewViewModel
    {


        public int sectionId { get; set; }
        public string sectionName { get; set; }

        public List<CoursePreviewLecture> LecturesData { get; set; }

        //public int sectionId { get; set; }
        //public string sectionName { get; set; }

        //public   List<Lecture>Lectures { get; set; }

        //public List<Content> LectureMainContent { get; set; }


        //public List<Content> DownlodableContents { get; set; }

    }
}
