using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class CourseRequirement
    {
        public int  id { get; set; }

        public string RequirementText { get; set; }

        public Course Course { get; set; }

        public int CourseId { get; set; }




    }
}
