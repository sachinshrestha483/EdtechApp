using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class CourseWhatWillYouLearn
    {
        public int Id { get; set; }

        public string WhatWillYouLearnText { get; set; }
        public Course Course { get; set; }

        public int CourseId { get; set; }
    }
}
