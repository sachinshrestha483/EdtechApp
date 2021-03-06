using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class InstructorHomePageViewModel
    {

        public List<Course> Courses { get; set; }
        public string PhotoLink { get; set; }
    }
}
