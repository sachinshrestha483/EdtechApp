using EdtechTest2.Models;
using EdtechTest2.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Student.ViewModel
{
    public class HomePageViewModel
    {

        public Category  Category { get; set; }


        public List<CourseHomePageDto> Courses{ get; set; }
    }
}
