using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class CourseRating
    {

        public int id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public string RatingComment { get; set; }


        public int fullstar { get; set; }

        public int halfstar { get; set; }



        public Course Course { get; set; }

        public int CourseId { get; set; }







    }
}
