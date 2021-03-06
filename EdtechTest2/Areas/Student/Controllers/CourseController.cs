using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EdtechTest2.Areas.Instructor.DemoModels;
using EdtechTest2.Areas.Student.ViewModel;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EdtechTest2.Areas.Student.Controllers
{

    [Area("Student")]
    [Authorize(Roles = SD.StudentUser+SD.InstructorUser)]

    public class CourseController : Controller
    {



        private readonly ApplicationDbContext _db;


        private readonly GoogleFireStoreStorageSettings googleFireStoreStorageSettings;

        private readonly IMapper _mapper;


        private readonly UserManager<ApplicationUser> _userManager;


        public CourseController(ApplicationDbContext db, IMapper mapper, IOptions<GoogleFireStoreStorageSettings> settings, UserManager<ApplicationUser> userManager)
        {
            _db = db;

            googleFireStoreStorageSettings = settings.Value;

            _userManager = userManager;

            _mapper = mapper;
        }
        [AllowAnonymous]
        public async Task<string> GetLink(string fileName, string DestinationFolder)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(googleFireStoreStorageSettings.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(googleFireStoreStorageSettings.AuthEmail, googleFireStoreStorageSettings.AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = await new FirebaseStorage(
                googleFireStoreStorageSettings.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })

                .Child(DestinationFolder)
                .Child(fileName)
                .GetDownloadUrlAsync();
            //.PutAsync(stream, cancellation.Token);
            //.PutAsync(stream, cancellation.Token);

            //   task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // cancel the upload
            // cancellation.Cancel();
            string link;
            try
            {
                if (task == null)
                {
                    return null;
                }
                link = task;

                return link;


            }
            catch
            {
                return null;
            }

        }
        [AllowAnonymous]

        public async  Task<IActionResult> Index(int id)
        {

           

            //var user = await _userManager.GetUserAsync(User);

        
            var course = _db.Courses.Include(t => t.ApplicationUser).FirstOrDefault(c => c.Id == id);


            if (course == null)
            {
                return View("NotFound");
            }


            var sections = _db.Sections.Where(s => s.CourseId == course.Id).Where(s => s.isSectionHidden == false);










            string courseIntrovideolink = null;
            string courseImageLink = null;
            string courseLanguage = "Default Language";
            // Setting the introvideo And Course Image Link and The Language..
            var introvideoobj = _db.CourseIntroVideos.FirstOrDefault(c => c.CourseId == course.Id);



            if (introvideoobj != null)
            {
                courseIntrovideolink = await GetLink(introvideoobj.Name, SD.CourseIntroVideoFolderName);


            }

            if (course.CourseImageLink == null)
            {

                courseImageLink = "/images/" + _db.Defaults.FirstOrDefault(c => c.Name == SD.CourseDefaultImage).Link;


            }
            else
            {
                courseImageLink = await GetLink(course.CourseImageLink, SD.CoursePhotoFolderName);
            }

            Language language;
            if (course.CourseLanguageId != 0)
            {
                language = _db.Languages.FirstOrDefault(c => c.Id == course.Id);
                if (language != null)
                {
                    courseLanguage = language.Name;

                }
              

            }

            //



            var allContent = _db.Contents.ToList();


            var author = _db.ApplicationUsers.FirstOrDefault(a => a.Id == course.ApplicationUserId);

            


            string authorPhoto = "/images/"+_db.Defaults.FirstOrDefault(u=>u.Name==SD.UserDefaultImage).Link;


            if (author.ProfilePhotoId != null)
            {
                var photo = _db.ProfilePhoto.FirstOrDefault(p => p.Id == author.ProfilePhotoId);

                if (photo != null)
                {
                    authorPhoto = photo.PhotoLink;
                }
                
                

            }





            var lectures = _db.Lectures.Where(l => l.isLectureHidden == false).Where(l=>sections.Contains(_db.Sections.FirstOrDefault(v=>v.Id==l.SectionId))).ToList();

            var contents = _db.Contents.Where(c => c.isDownlodableContent == false).Where(c => lectures.Contains(_db.Contents.FirstOrDefault(l => l.Id == c.Id).Lecture)).ToList();

            // no downlodable here
            var videoContent = _db.Contents.Where(c => c.ContenType == Models.Content.ContentTypes.Video).ToList();

            var articles = contents.Where(c => c.ContenType == Models.Content.ContentTypes.text).ToList();

            var downlodableContents = _db.Contents.Where(c => c.isDownlodableContent == true).Where(c => lectures.Contains(_db.Contents.FirstOrDefault(l => l.Id == c.Id).Lecture)).ToList();


            var whatwillYouLearns = _db.CourseWhatWillYouLearns.Where(c => c.Id == course.Id).ToList();

            var courseRequirements = _db.CourseRequirements.Where(c=>c.CourseId==course.Id).ToList();



            List<SectionsWithLectures> SecLectures = new List<SectionsWithLectures>();


            foreach (var section in sections)
            {


                var SecLectureObj = new SectionsWithLectures();

                SecLectureObj.section = section;

                var sectionLectures = lectures.Where(l => l.SectionId == section.Id).ToList();
                SecLectureObj.Lectures = sectionLectures;

                SecLectureObj.numberOfLectures = sectionLectures.Count();


                SecLectureObj.LenghthOfSection = (int) videoContent.Where(v=>lectures.Contains(sectionLectures.FirstOrDefault(j=>j.id==v.LectureId))).Select(s=>s.Length).Sum();


                SecLectures.Add(SecLectureObj);
               











            }



            var freelectures = new List<FreeLectureModel>();


            foreach (var item in lectures.Where(l=>l.isPreview==true))
            {

                var freelectureModel = new FreeLectureModel();

                freelectureModel.lecture = item;


                var lectureContent = contents.Where(l=>l.ContenType==Models.Content.ContentTypes.Video).FirstOrDefault(c => c.LectureId == item.id);

                if (lectureContent == null)
                {
                    continue;
                }


                freelectureModel.duration = (int)lectureContent.Length;

                

                freelectureModel.VideoLink = await GetLink(lectureContent.Name, SD.CourseContentFolderName);





                freelectures.Add(freelectureModel);



            }


            var coursepageobj = new CourseHomePageViewModel()
            {
                Author = course.ApplicationUser,
                content = contents,
                CourseIntroVideoLink = courseIntrovideolink,
                CoursePhotoLink = courseImageLink,
                CourseSubtitle = course.subtitle,
                CourseTitle = course.topicName,
                CourseUploadedOn = course.CreatedOn.ToShortDateString(),
                Description = course.description,
                FreeLecModel = freelectures,
                Language = courseLanguage,
                LastUpdated = course.LastUpdatedOn.ToShortDateString(),
                NumberofArtice = articles.Count().ToString(),
                Price = course.price,
                Requirements = courseRequirements,
                SectionsWithLectures = SecLectures,
                TotalContentOfVideoHours = videoContent.Where(c=>lectures.Contains(_db.Contents.FirstOrDefault(b=>b.LectureId==c.LectureId).Lecture)).Select(c => c.Length).Sum().ToString(),
                WhatWillYouLearns = whatwillYouLearns,
                posterImageLink = courseImageLink,
                AuthorPhoto = authorPhoto,
                CourseRatings = _db.CourseRatings.Where(c => c.CourseId == course.Id).ToList(),
                ApplicationUsero = author,


            };


            coursepageobj.courseId = course.Id;



            return View(coursepageobj);
        }


       



        [HttpPost]
      

        public async Task<IActionResult> AddToCart([FromForm]  AddToCartViewModel obj)
        {

            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.id);

            if (course == null)
            {
                return NotFound();
            }


            var user = await _userManager.GetUserAsync(User);


            var isAlready = _db.UserCourseCarts.Where(c => c.courseId == obj.id && c.userId==user.Id);

            if (isAlready.Count() > 0)
            {
                return BadRequest(new { error="Course Already in Cart"});
            }




            if (user == null)
            {
                return NotFound(new { error = "User Not Found" });
            }


            var cart = new UserCourseCart()
            {
                 courseId=course.Id,
                  userId=user.Id,
                  
                   CouponId=null,

                   

            };


            if (obj.couponId != 0)
            {
                cart.CouponId = obj.couponId;
            }

            _db.UserCourseCarts.Add(cart);



            _db.SaveChanges();





            

            return Ok();

        }




        [HttpPost]
       


        public async Task<IActionResult> CheckCouponForCourse([FromForm] CheckCouponForCourseviewModel obj)
        {

            // to do apply no same string in the same course  with the different discount...

            var course = _db.Courses.FirstOrDefault(c =>c.Id==obj.courseId);



            if (course == null)
            {
                return NotFound();
            }

            var coupons = _db.Coupons.Where(c=>c.numberofcouponUnUsed!=0).Where(c => c.CourseId == course.Id).Where(c=>c.isCouponBlocked==false).Where(c=>c.validTill>DateTime.Now).ToList();


            var couponobject = coupons.FirstOrDefault(c => c.couponCode == obj.couponCode);


            if (couponobject == null)
            {
                return BadRequest(new { error = "Coupon Not Valid" });
            }


            if(couponobject.discountMethod==Models.Enums.DiscountType.percent)
            {

                int newprice = (int)(course.price * couponobject.discountValue / 100) ;

                if (newprice < 0)
                {
                    newprice = 0;
                }

                return Ok(new { newprice,id=couponobject.id });

            }


            else
            {
                int newPrice = (int)(course.price - couponobject.discountValue);

                if (newPrice < 0)
                {
                    newPrice = 0;
                }

                return Ok(new { newPrice , id = couponobject.id });

            }


           



        }


      



        public async Task<IActionResult>BuyCourse(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return View("NotFound");
            }


            var UsercourseCart = _db.UserCourseCarts.FirstOrDefault(c => c.courseId == course.Id && c.userId == user.Id);

            if (UsercourseCart != null)
            {
                return RedirectToAction("Index","Cart",new { area="Student"});
            }

            else
            {
                var userCourseObj = new UserCourseCart() { 
                
                    userId=user.Id,
                 courseId=course.Id,
                  
                
                
                };

                _db.UserCourseCarts.Add(userCourseObj);

                _db.SaveChanges();


                return RedirectToAction("Index", "Cart", new { area = "Student" });


            }




        }


    }
}
