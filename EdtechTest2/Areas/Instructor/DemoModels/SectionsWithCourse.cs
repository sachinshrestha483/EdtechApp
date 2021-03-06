using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.DemoModels
{
    public class SectionsWithCourse
    {
        public Section section { get; set; }

        
        public List<Lecture> Lectures { get; set; }
    }
}
