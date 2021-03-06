using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EdtechTest2.Areas.Instructor.Controllers;
using EdtechTest2.Areas.Instructor.ViewModels;
using EdtechTest2.Areas.Student.ViewModel;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Models.Dtos;
using EdtechTest2.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EdtechTest2.Areas.Student.Controllers
{
    [Area("Student")]
   // [Authorize(Roles = SD.StudentUser+SD.InstructorUser)]

    public class HomeController : Controller
    {


        private readonly ApplicationDbContext _db;
        private readonly GoogleFireStoreStorageSettings googleFireStoreStorageSettings;
        private readonly UserManager<ApplicationUser> _userManager;



        private readonly IMapper _mapper;



        public HomeController(ApplicationDbContext db,IMapper mapper, IOptions<GoogleFireStoreStorageSettings> settings, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            googleFireStoreStorageSettings = settings.Value;
            _userManager = userManager;
            _mapper = mapper;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {

            // All the Courses With The price photo and its Categories

         




            return View();
        }


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
        public async Task <IActionResult>  HomePageApi()
        {

            var categories = _db.Categories.ToList();

            var categoriesObjects = new List<HomePageViewModel>();


            foreach (var item in categories)
            {
                var categoryobject = new HomePageViewModel();


                categoryobject.Category = item;


                var coursesDtos = new List<CourseHomePageDto>();


                var courses = _db.Courses.Where(c => c.CategoryId == item.id && c.CourseStatus==SD.CourseStatusPublished).ToList();



                foreach (var course in courses)
                {

                    var courseDtoObj = _mapper.Map<CourseHomePageDto>(course);





                    if (course.CourseImageLink == null)
                    {
                        var link = _db.Defaults.FirstOrDefault(p => p.Name == SD.CourseDefaultImage);

                        courseDtoObj.CourseImageLink = "/images/"+link.Link;
                    }
                    else
                    {
                        courseDtoObj.CourseImageLink = await GetLink(course.CourseImageLink, SD.CoursePhotoFolderName);

                    }






                    var language = _db.Languages.FirstOrDefault(l => l.Id == course.CourseLanguageId);

                    if (language != null)
                    {
                        courseDtoObj.CourseLanguage = language.Name;
                    }
                    else
                    {
                        courseDtoObj.CourseLanguage = "";
                    }

                    var author = _db.ApplicationUsers.FirstOrDefault(a => a.Id == course.ApplicationUserId);

                    courseDtoObj.authorName = author.FirstName + " " + author.LastName;


                    coursesDtos.Add(courseDtoObj);

                }


                categoryobject.Courses = coursesDtos;



                categoriesObjects.Add(categoryobject);















            }




            return Json(categoriesObjects);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        public async Task<IActionResult> Course(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Id == id);


            return View();

        }




        public async Task<IActionResult> myCourses()
        {


            var user = await _userManager.GetUserAsync(User);

            var purchasedtable = _db.CourseTransictionRecords.Where(c => c.ApplicationUserId == user.Id && c.isRefund == false).ToList();

            var purchasedCourses = _db.CourseTransictionRecords.Where(c => c.ApplicationUserId == user.Id && c.isRefund == false).Select(c=>c.Course).ToList();




            var myCourseObjects = new List<MyCourseViewModel>();



            foreach (var item in purchasedCourses)
            {


                var myCourseObject= new MyCourseViewModel();


                myCourseObject.CouseName = item.topicName;

                var link = item.CourseImageLink;

                if (link == null)
                {

                    var imgobj = _db.Defaults.FirstOrDefault(i => i.Name == SD.CourseDefaultImage);

                    if (imgobj == null)
                    {
                        link = item.CourseImageLink;
                    }


                    link = "/images/" + imgobj.Link;
                }

                else
                {
                 link= await   GetLink(item.CourseImageLink, SD.CoursePhotoFolderName);
                }


                myCourseObject.CoursePhotoLink = link;



                myCourseObject.PurchasedOn = purchasedtable.FirstOrDefault(p => p.CourseId == item.Id).purchaseDate;

                myCourseObject.courseId = item.Id;

                myCourseObjects.Add(myCourseObject);










            }



        


         














            return View(myCourseObjects);
        }




        public async Task<IActionResult> learn(int id)
        {

            ViewBag.courseId = id;
            return View();
        }

        public async Task<IActionResult>learnApi(int id)
        {
            var user = await _userManager.GetUserAsync(User);


            var course = _db.Courses.FirstOrDefault(c => c.Id == id);


            if (course == null)
            {
                return NotFound();
            }


            var isPurchasedCourse = _db.CourseTransictionRecords.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.CourseId == course.Id);


            if (isPurchasedCourse == null)
            {
                return NotFound();
            }







            var sections = _db.Sections.Where(s => s.CourseId == course.Id).Where(s => s.isSectionHidden == false).ToList();
            var lectures = _db.Lectures.Where(l => sections.Contains(_db.Lectures.FirstOrDefault(k => k.SectionId == l.SectionId).Section)).Where(l => l.isLectureHidden == false).ToList();

            var contents = _db.Contents.Where(c => lectures.Contains(_db.Contents.FirstOrDefault(v => v.LectureId == c.LectureId).Lecture)).ToList();


            List<CoursePreviewViewModel> previewObjs = new List<CoursePreviewViewModel>();



            foreach (var section in sections)
            {
                var obj = new CoursePreviewViewModel();


                // Section Name and The Section Id
                obj.sectionName = section.Name;


                obj.sectionId = section.Id;

                //



                //Lecture Dto's

                var lecturesobj = lectures.Where(l => l.SectionId == section.Id).ToList();

                //


                // section Content Dto's Content

                var LectureMainContents = contents.Where(c => lectures.Contains(_db.Contents.FirstOrDefault(j => j.LectureId == c.LectureId).Lecture)).Where(c => c.ContenType == Models.Content.ContentTypes.Video || c.ContenType == Models.Content.ContentTypes.text).Where(c => c.isDownlodableContent == false).ToList();

                //





                // Section Downlodable Content Dto 
                var DownlodableContents = contents.Where(c => lectures.Contains(_db.Contents.FirstOrDefault(j => j.LectureId == c.LectureId).Lecture)).Where(c => c.isDownlodableContent == true).ToList();

                //


                var lectureDatasObjs = new List<CoursePreviewLecture>();


                foreach (var item in lecturesobj)
                {
                    var lectureDataObj = new CoursePreviewLecture();

                    lectureDataObj.lecId = item.id;

                    lectureDataObj.name = item.Name;
                    lectureDataObj.lectureDescription = item.LectureDescription;
                    lectureDataObj.length = 1;

                    lectureDataObj.lectureArticle = (LectureMainContents.FirstOrDefault(c => c.LectureId == item.id) == null ? null : LectureMainContents.FirstOrDefault(c => c.LectureId == item.id).LectureArticleText);

                    var file = LectureMainContents.FirstOrDefault(c => c.LectureId == item.id);

                    if (file != null)
                    {
                        if (file.ContenType == Models.Content.ContentTypes.Video)
                        {
                            var contentlink = await GetLink(file.Name, SD.CourseContentFolderName);

                            lectureDataObj.lectureContentLink = contentlink;
                            lectureDataObj.length = file.Length;

                        }

                    }



                    var lecDContents = DownlodableContents.Where(l => l.LectureId == item.id);

                    var dcobjects = new List<CoursePreviewContentViewModel>();

                    foreach (var item1 in lecDContents)
                    {
                        var contentsobj = new CoursePreviewContentViewModel();

                        contentsobj.Name = item1.UploadedFileName;

                        contentsobj.Link = await GetLink(item1.Name, SD.CourseContentFolderName);

                        if (contentsobj != null)
                        {
                            dcobjects.Add(contentsobj);



                        }


                    }

                    lectureDataObj.downlodableContents = dcobjects;

                    lectureDatasObjs.Add(lectureDataObj);




                }





                obj.LecturesData = lectureDatasObjs;












                previewObjs.Add(obj);



            }

            return Json(new { contents = previewObjs, courseId = course.Id });
        }





        public async Task<IActionResult> HasRating(int id)
        {



            var course = _db.Courses.FirstOrDefault(c => c.Id == id);


            if (course == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest();
            }
           

            var ratingobj = _db.CourseRatings.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.CourseId == id);

            if (ratingobj == null)
            {
             
                return Ok(new { rating= 0 });
            }

            else
            {
                
                return Ok(new {rating= 1,ratingObject=new {fullStar=ratingobj.fullstar,halfStar=ratingobj.halfstar,ratingComment=ratingobj.RatingComment }  });
            }



        }


        [HttpPost]

        public async Task<IActionResult> SetRating([FromForm] SetRatingViewModel obj)
        {
            
            


            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.courseId);

            if (course == null)
            {
                return NotFound();
            }

            var purchasedCourse = _db.CourseTransictionRecords.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.CourseId == course.Id);


            if (purchasedCourse == null)
            {
                return NotFound();
            }
            var ratingObject = _db.CourseRatings.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.CourseId == course.Id);


            if (ratingObject != null)
            {
                return NotFound();
            }

            var review = new CourseRating();

            review.ApplicationUserId = user.Id;
            review.CourseId = course.Id;
            if (obj.fullstar > 5)
            {
                review.fullstar = 5;
                review.halfstar = 0;
            }
            else if (obj.halfStar > 1)
            {
                review.fullstar = 1;
                review.halfstar = 0;
            }

            review.fullstar = obj.fullstar;
            review.halfstar = obj.halfStar;


            review.RatingComment = obj.ratingComment;


            _db.CourseRatings.Add(review);




            _db.SaveChanges();




           
            //








            return Ok();

        }



        [HttpPost]

        public async Task<IActionResult> UpdateRating([FromForm] UpdateRatingViewModel obj)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.courseId);

            if (course == null)
            {
                return NotFound();
            }

            var purchasedCourse = _db.CourseTransictionRecords.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.CourseId == course.Id);


            if (purchasedCourse == null)
            {
                return NotFound();
            }


            var ratingObject = _db.CourseRatings.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.CourseId == course.Id);


            if (ratingObject == null)
            {
                return NotFound();
            }



            ratingObject.fullstar = obj.fullStar;

            ratingObject.halfstar = obj.halfStar;

            ratingObject.RatingComment = obj.ratingText;


            _db.SaveChanges();

            return Ok();






        }








        //public async Task<IActionResult> getVideolink(int id)
        //{

        //    var content = _db.Contents.FirstOrDefault(c => c.Id == 6098);
        //    var link = await GetLink(content.Name, SD.CourseContentFolderName);
        //    //A stream of bytes that represents the binary file  
        //    FileStream fs = new FileStream(link, FileMode.Open, FileAccess.Read);

        //    //The reader reads the binary data from the file stream  
        //    BinaryReader reader = new BinaryReader(fs);

        //    //Bytes from the binary reader stored in BlobValue array  
        //    byte[] BlobValue = reader.ReadBytes((int)fs.Length);




        //    return Ok(BlobValue);

        //}







    }
}
