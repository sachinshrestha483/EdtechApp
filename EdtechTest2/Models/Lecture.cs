using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Lecture
    {
        public int id { get; set; }

        public Section Section { get; set; }

        public int SectionId { get; set; }


        public string Name { get; set; }


        public string LectureDescription { get; set; }




        public bool isPreview { get; set; }



        public bool isLectureHidden { get; set; }
    }
}
