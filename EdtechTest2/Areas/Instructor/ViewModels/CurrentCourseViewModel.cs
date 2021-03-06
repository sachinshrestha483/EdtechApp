using EdtechTest2.Models;
using EdtechTest2.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CurrentCourseViewModel
    {
        public CourseDto course { get; set; }

        public int numberofStudentEnrolled{ get; set; }



        public int revenueThisMonth { get; set; }


        public int totalrevenue { get; set; }

        public int totalrevenueToday { get; set; }



        public ApplicationUser ApplicationAUser { get; set; }

        public List<CourseRating> CourseRatings { get; set; }









    }
}
