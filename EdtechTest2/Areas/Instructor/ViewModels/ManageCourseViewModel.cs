using EdtechTest2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class ManageCourseViewModel
    {
        
        [Required]
        public string CourseTitle { get; set; }
        [Required]

        public string CourseSubtitle { get; set; }
        [Required]

        public string Level { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public int Category { get; set; }
        [Required]

        public int Language { get; set; }


        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> LevelList { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get; set; }




        public List<CourseRequirement> CourseRequirements { get; set; }

        public List<CourseWhatWillYouLearn> WhatWillYouDos { get; set; }


        [Display(Name ="Course Photo")]
        public IFormFile CoursePhoto { get; set; }


        [Display(Name ="Course Intro Video")]

        public IFormFile CourseIntroductionVideo { get; set; }
        public CourseIntroVideo CourseIntroVideo { get; set; }

        public string Duration { get; set; }

       // public introVideoViewModel IntroVideoViewModel { get; set; }

     //   public string coursePhotoName { get; set; }
        public string coursePhotoLink { get; set; }

        public int CourseId { get; set; }

        [Required]
        [Range(0, 5000)]
        public double CoursePrice { get; set; }
        public string IntroVideoPlayableLink { get; set; }
    }
}
