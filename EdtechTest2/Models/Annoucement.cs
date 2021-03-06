using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Annoucement
    {
        public int id { get; set; }

        public string heading { get; set; }
        public string Message { get; set; }


        public Course Course { get; set; }

        public int CourseId { get; set; }

    }
}
