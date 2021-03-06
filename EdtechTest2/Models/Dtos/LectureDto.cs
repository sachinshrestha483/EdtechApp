using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models.Dtos
{
    public class LectureDto
    {
        public int id { get; set; }

        //public Section Section { get; set; }

        public int SectionId { get; set; }


        public string Name { get; set; }


        public string LectureDescription { get; set; }
    }
}
