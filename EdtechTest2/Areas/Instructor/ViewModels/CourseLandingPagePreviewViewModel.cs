using EdtechTest2.Areas.Instructor.DemoModels;
using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CourseLandingPagePreviewViewModel
    {
        public string CourseTitle { get; set; }





        public List<SectionsWithLectures >SectionsWithLectures { get; set; }

        public string CourseSubtitle { get; set; }

        public string CoursePhotoLink { get; set; }

        public string CourseIntroVideoLink { get; set; }


        public double  Price { get; set; }

       

        public string Language { get; set; }

        public string TotalContentOfVideoHours { get; set; }

        public string NumberofArtice { get; set; }

        public string LastUpdated { get; set; }


        public string CourseUploadedOn { get; set; }

        public List<CourseWhatWillYouLearn> WhatWillYouLearns { get; set; }


        public List<CourseRequirement> Requirements { get; set; }

        public string Description { get; set; }


      //  public List<Lecture> Lectures { get; set; }

        public ApplicationUser Author { get; set; }


        public List<Content> content { get; set; }



        public string UserPhotoLink { get; set; }



        public List<FreeLectureModel> FreeLecModel { get; set; }


       




    }
}
