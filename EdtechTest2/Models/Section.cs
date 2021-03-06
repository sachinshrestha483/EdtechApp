using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Section
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public double SectionLength { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }


        public bool   isSectionHidden { get; set; }



    }
}
