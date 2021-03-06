using EdtechTest2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Default> Defaults { get; set; }

        public DbSet<ProfilePhoto> ProfilePhoto { get; set; }

        public DbSet<UserPhoto> UserPhotos { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseRequirement> CourseRequirements { get; set; }
        public DbSet<CourseWhatWillYouLearn> CourseWhatWillYouLearns { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Content> Contents { get; set; }

        public DbSet<CourseIntroVideo> CourseIntroVideos { get; set; }

        public DbSet<Annoucement> Annoucements { get; set; }

        public DbSet<Language> Languages { get; set; }


        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<CourseCart> CourseCarts { get; set; }


        public DbSet<UserCourseCart> UserCourseCarts { get; set; }


        //public DbSet<PurchasedCourse> PurchasedCourses { get; set; }
    //    public DbSet<RefundedCourse> RefundedCourses { get; set; }

        public DbSet<CourseTransictions> CourseTransictionRecords { get; set; }

        public DbSet<CourseRating> CourseRatings { get; set; }






    }

}
