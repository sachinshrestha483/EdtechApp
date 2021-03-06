using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Course
    {
        public int   Id { get; set; }
        [Required]
        public string topicName { get; set; }


        public string subtitle { get; set; }

        public double price { get; set; }

        [NotMapped]
        public double totalLengthofCourse { get; set; }

        public string description { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        [Required]

        public string ApplicationUserId { get; set; }


        public Category Category { get; set; }
        [Required]

        public int CategoryId { get; set; }

        //public enum Levels
        //{
        //    Basic,
        //    Intermediate,
        //    Advance
        //}

        public string CourseLevel { get; set; }

        //public enum CourseStatuses
        //{
        //    Review,
        //    Approved,
        //    UnderDevelopment,
        //    Cancelled
        //}

        [Required]

        public string CourseStatus { get; set;}


        public string CourseImageLink { get; set; }
        [NotMapped]
        public string cloudImageLink { get; set; }

        public string Temp { get; set; }

        //   public string Language { get; set; }

        //     public Language Language { get; set; }
        //public Language Language { get; set; }
        public int CourseLanguageId { get; set; }



        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }


    }
}
