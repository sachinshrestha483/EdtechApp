using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class CourseIntroVideo
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public string Link { get; set; }


        public double CourseIntroVideoLength { get; set; }

        [NotMapped]
        public double CourseLength { get; set; }
        public string CourseIntroImageLink { get; set; }

        public Course Course { get; set; }

        public int CourseId { get; set; }

    }
}
