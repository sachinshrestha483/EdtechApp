using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Admin.ViewModels
{
    public class AdminHomeViewModel
    {
        public int NumberOfStudents { get; set; }

        public int NumberOfInstructor { get; set; }


        public int NumberOfCourse { get; set; }

        public int CoursesSelledToday { get; set; }


        public int TotalNumberOfCoursesSelled { get; set; }



        public int TotalrevenueToday { get; set; }



        public int RevenueTillNow { get; set; }

        public int CoursesPendingForApproval { get; set; }


    }
}
