using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class SectionsWithLectures
    {

        public List<Lecture> Lectures { get; set; }

        public Section section { get; set; }

        public int numberOfLectures { get; set; }


        public int LenghthOfSection { get; set; }



    }
}
