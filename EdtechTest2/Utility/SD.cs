using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Utility
{
    public static class SD
    {
        // User Roles
        public const string AdminUser = "Admin";
        public const string StudentUser = "Student";
        public const string InstructorUser = "Instructor";
        
        
        // Default Values
        public const string UserDefaultImage = "UserDefaultImage";
        public const string CourseDefaultImage = "CourseDefaultImage";

        //  Course Statuses

        public const string CourseStatusReview = "Review";
        public const string CourseStatusApproved = "Approved";
        public const string CourseStatusDevelopment = "Development";
        public const string CourseStatusPublished = "Published";
        public const string CourseStatusRejected = "Rejected";
        public const string CourseStatusUnPublished = "UnPublished";


        //Cloud Folder Name

        public const string ProfilePhotoFolderName = "UserProfilePhoto";
        public const string CoursePhotoFolderName = "CoursePhoto";
        public const string CourseIntroVideoFolderName = "CourseIntroVideo";
        public const string CourseContentFolderName = "CourseContent";






        // Course Levels


        public const string CourseBegineerLevel = "BegineerLevel";
        public const string CourseIntermediateLevel = "IntermediateLevel";
        public const string CourseAdvanceLevel = "AdvanceLevel";
        public const string CourseAllLevel = "AllLevel";



        public static readonly List<string> CourseLevels = new List<string>() {
            CourseAdvanceLevel,
            CourseBegineerLevel, 
            CourseIntermediateLevel,
            CourseAllLevel 
        };




        //RazorPay

        public const string razorPayKey_Id = "rzp_test_6pWr7K9xz2LCTh";
        public const string razorPayKey_Secret = "0ry25jNtdfLVX1mR1X5xPQZx";


        //


        // fumnction to check  Check  uploaded file  Is Image

        public static bool IsImage(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types

            if (!string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
           !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;



        }



        public static bool IsVideo(IFormFile postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types

            if (!string.Equals(postedFile.ContentType, "video/mp4", StringComparison.OrdinalIgnoreCase) )
         
            {
                return false;
            }
            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".mp4", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;



        }



    }
}
