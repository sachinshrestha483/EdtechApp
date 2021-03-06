using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CourseSettingViewModel
    {
        public int courseId { get; set; }

        public string name { get; set; }



        public List<Lecture> Lectures { get; set; }


        public List<Section> Sections { get; set; }





    }
}
