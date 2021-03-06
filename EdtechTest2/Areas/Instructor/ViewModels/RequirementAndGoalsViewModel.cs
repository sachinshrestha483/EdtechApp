using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class RequirementAndGoalsViewModel
    {
        public RequirementAndGoalsViewModel()
        {
            Requirements = new CourseRequirement[20];

            WhatWillYouLearn = new CourseWhatWillYouLearn[20];
        }
        public CourseRequirement[] Requirements { get; set; }
        public CourseWhatWillYouLearn[] WhatWillYouLearn { get; set; }

        public int CourseId { get; set; }



    }
}
