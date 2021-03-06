using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EdtechTest2.Areas.Instructor.DemoModels;
using EdtechTest2.Areas.Instructor.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Models.Dtos;
using EdtechTest2.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

namespace EdtechTest2.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles =  SD.InstructorUser)]

    public class CourseController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GoogleFireStoreStorageSettings googleFireStoreStorageSettings;
        private readonly IMapper _mapper;

        public CourseController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IOptions<GoogleFireStoreStorageSettings> settings, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            googleFireStoreStorageSettings = settings.Value;
            _mapper = mapper;


        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            var obj = new CourseCreateViewModel()
            {
                CategoryList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.id.ToString(),
                }),
            };

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);


                var category = _db.Categories.FirstOrDefault(c => c.id == obj.CategoryId);
                if (category == null)
                {
                    return RedirectToAction(nameof(Create));
                }

                var course = new Course()
                {
                    topicName = obj.Name,
                    CategoryId = category.id,
                    CourseStatus = SD.CourseStatusDevelopment,
                    ApplicationUserId = user.Id,
                };



                _db.Courses.Add(course);
                _db.SaveChanges();
                return RedirectToAction(nameof(ManageCourse), new { id = course.Id });
            }

            return RedirectToAction(nameof(Create));
        }


        public async Task<IActionResult> ManageCourse(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == id);

            if (isAuthorOfCourse == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");

            }


            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                ViewBag.Message = "CourseWithGivenIdNotFound";
                return View("NotFound");
            }


            var courseIntrovideo = _db.CourseIntroVideos.FirstOrDefault(c => c.CourseId == course.Id);
            var courseRequirements = _db.CourseRequirements.Where(r => r.CourseId == course.Id).ToList();
            var WhatWillyouLearns = _db.CourseWhatWillYouLearns.Where(r => r.CourseId == course.Id).ToList();

            var obj = new ManageCourseViewModel()
            {
                CourseId = course.Id,
                CourseTitle = course.topicName,
                CourseSubtitle = course.subtitle,
                Category = course.CategoryId,
                Level = course.CourseLevel,
                Description = course.description,
                Language = course.CourseLanguageId,
                CoursePrice = course.price,

                CourseIntroVideo = courseIntrovideo,
                CourseRequirements = courseRequirements,
                WhatWillYouDos = WhatWillyouLearns,
                CategoryList = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.id.ToString(),

                }),
                LanguageList = _db.Languages.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }),
                LevelList = SD.CourseLevels.Select(x => new SelectListItem

                {
                    Text = x,
                    Value = x,
                }),


            };

            // Check For Video Video  view model to it 


            var courseIntrovideoobj = _db.CourseIntroVideos.FirstOrDefault(v => v.CourseId == course.Id);

            if (courseIntrovideoobj == null)
            {
                // puth their image only
                obj.IntroVideoPlayableLink = null;


            }
            else
            {
                // put their video
                if (courseIntrovideoobj.Name == null)
                {
                    // put their image only
                    obj.IntroVideoPlayableLink = null;

                }

                else
                {
                    var IVlink = await GetLink(courseIntrovideoobj.Name, SD.CourseIntroVideoFolderName);
                    if (IVlink == null)
                    {
                        // photo
                        obj.IntroVideoPlayableLink = null;

                    }
                    else
                    {
                        obj.IntroVideoPlayableLink = IVlink;
                    }
                }



            }






            // Attach Image To It

            var imgobj = _db.Defaults.FirstOrDefault(d => d.Name == SD.CourseDefaultImage);


            if (course.CourseImageLink == null)
            {

                if (imgobj == null)
                {
                    obj.coursePhotoLink = "";
                }

                else
                {
                    obj.coursePhotoLink = "/images/" + imgobj.Link;
                }
                //  obj.coursePhotoName = "k";

            }


            else
            {
                var link = await GetLink(course.CourseImageLink, SD.CoursePhotoFolderName);

                if (link == null)
                {
                    obj.coursePhotoLink = "/images/" + imgobj.Link;

                }
                else
                {
                    obj.coursePhotoLink = link;

                }
            }


            return View(obj);
        }




        [HttpPost]
        public async Task<IActionResult> ManageCourse(ManageCourseViewModel obj)
        {

            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == obj.CourseId);

            if (isAuthorOfCourse == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");
            }

            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.CourseId);

            course.topicName = obj.CourseTitle;
            course.subtitle = obj.CourseSubtitle;
            course.description = obj.Description;
            course.CourseLanguageId = obj.Language;
            course.CourseLevel = obj.Level;
            course.CategoryId = obj.Category;
            course.price = obj.CoursePrice;

            _db.Courses.Update(course);
            _db.SaveChanges();




            return RedirectToAction("ManageCourse", new { id = course.Id });
        }


        [HttpPost]
        public async Task<IActionResult> UCourseIntroVideo([FromForm] CourseIntroVideoUploadViewModel IvVm)
        {
            //var ffProbe = new NReco.VideoInfo.FFProbe();
            //var videoInfo = ffProbe.GetMediaInfo("https://www.youtube.com/watch?v=9jd2l8eFRbs");
            //  ViewBag.Message(videoInfo.Duration+"kkk");
            //return View("NotFound");
            // var d = videoInfo.Duration;

            //ViewBag.Message = Duration;
            //return View("NotFound");

            if (IvVm == null || IvVm.Duration == 0)
            {
                return RedirectToAction(nameof(ManageCourse), new { id = IvVm.CourseId });
            }





            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == IvVm.CourseId);

            if (isAuthorOfCourse == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");
            }
            var course = _db.Courses.FirstOrDefault(c => c.Id == IvVm.CourseId);
            var courseVideoName = Guid.NewGuid().ToString() + "_" + IvVm.CourseIntroductionVideo.FileName;

            var courseIntroVideo = _db.CourseIntroVideos.FirstOrDefault(c => c.CourseId == course.Id);

            if (courseIntroVideo == null)
            {
                var operation = await Upload(IvVm.CourseIntroductionVideo.OpenReadStream(), courseVideoName, SD.CourseIntroVideoFolderName);

                if (operation == null)
                {
                    return RedirectToAction("ManageCourse", new { id = course.Id });
                }

                // to create a intro video
                var introvedeo = new CourseIntroVideo() { CourseId = course.Id, CourseIntroVideoLength = IvVm.Duration, Name = courseVideoName };
                _db.CourseIntroVideos.Add(introvedeo);
                _db.SaveChanges();
            }
            else
            {

                if (courseIntroVideo.Name == null)
                {
                    var operation = await Upload(IvVm.CourseIntroductionVideo.OpenReadStream(), courseVideoName, SD.CourseIntroVideoFolderName);

                    if (operation == null)
                    {
                        return RedirectToAction("ManageCourse", new { id = course.Id });
                    }
                    courseIntroVideo.Name = courseVideoName;
                    _db.CourseIntroVideos.Update(courseIntroVideo);
                    _db.SaveChanges();
                }
                else
                {
                    var operation = await Upload(IvVm.CourseIntroductionVideo.OpenReadStream(), courseVideoName, SD.CourseIntroVideoFolderName);
                    if (operation == null)
                    {
                        return RedirectToAction("ManageCourse", new { id = course.Id });
                    }
                    var oldIntroVideo = courseIntroVideo.Name;
                    courseIntroVideo.Name = courseVideoName;
                    _db.CourseIntroVideos.Update(courseIntroVideo);
                    _db.SaveChanges();



                    await Delete(oldIntroVideo, SD.CourseIntroVideoFolderName);

                }
                // To Edit intro Video


            }
            return RedirectToAction("ManageCourse", new { id = course.Id });

        }

        [HttpPost]
        public async Task<IActionResult> UCoursePhoto(IFormFile coursePhoto, int courseId)
        {
            if (coursePhoto == null)
            {
                return RedirectToAction(nameof(ManageCourse), new { id = courseId });
            }

            if (!SD.IsImage(coursePhoto))
            {
                ViewBag.Message = "Photo Not Correct";
                return View("NotFound");
            }

            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == courseId);


            if (isAuthorOfCourse == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");
            }


            var course = _db.Courses.FirstOrDefault(c => c.Id == courseId);
            var userPhotoName = Guid.NewGuid().ToString() + "_" + coursePhoto.FileName;

            if (course.CourseImageLink == null)
            {
                var operation = await Upload(coursePhoto.OpenReadStream(), userPhotoName, SD.CoursePhotoFolderName);

                if (operation != null)
                {
                    course.CourseImageLink = userPhotoName;
                    _db.Courses.Update(course);
                }
                else
                {
                    return RedirectToAction(nameof(ManageCourse), new { id = course.Id });
                }
                // operation gives the link

                _db.SaveChanges();
            }

            else
            {
                var oldPhotoName = course.CourseImageLink;
                var operation = await Upload(coursePhoto.OpenReadStream(), userPhotoName, SD.CoursePhotoFolderName);

                if (operation != null)
                {
                    course.CourseImageLink = userPhotoName;
                    _db.Courses.Update(course);

                }
                else
                {
                    return RedirectToAction(nameof(ManageCourse), new { id = course.Id });
                }
                await Delete(oldPhotoName, SD.CoursePhotoFolderName);
                _db.SaveChanges();

            }





            return RedirectToAction("ManageCourse", new { id = course.Id });



        }


        public async Task<IActionResult> Requirementandgoals(int id)
        {
            var course = _db.Courses.FirstOrDefault(i => i.Id == id);
            if (course == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");

            }
            var obj = new RequirementAndGoalsViewModel() { CourseId = course.Id };

            // check if it already have what you will learn
            var whatlList = _db.CourseWhatWillYouLearns.Where(t => t.CourseId == course.Id).ToList();


            if (whatlList.Count > 0)
            {
                int i = 0;
                foreach (var item in whatlList)
                {
                    obj.WhatWillYouLearn[i] = item;
                    i++;
                    if (i == 21)
                    {
                        break;
                    }
                }
            }



            // checkif  requiremen already done

            var reqList = _db.CourseRequirements.Where(c => c.CourseId == course.Id).ToList();


            if (reqList.Count > 0)
            {
                int i = 0;
                foreach (var item in reqList)
                {
                    obj.Requirements[i] = item;
                    i++;
                    if (i == 21)
                    {
                        break;
                    }
                }
            }



            return View(obj);
        }





        [HttpPost]
        public async Task<IActionResult> Requirementandgoals(RequirementAndGoalsViewModel obj)
        {

            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == obj.CourseId);

            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.CourseId);


            if (isAuthorOfCourse == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");
            }






            var UWhatwillYouLearnList = new List<CourseWhatWillYouLearn>();
            var CWhatwillYouLearnList = new List<CourseWhatWillYouLearn>();

            var URequirementList = new List<CourseRequirement>();
            var CRequirementList = new List<CourseRequirement>();


            foreach (var item in obj.Requirements)
            {
                if (item.id == 0)
                {
                    item.CourseId = course.Id;
                    CRequirementList.Add(item);
                }
                else
                {
                    item.CourseId = course.Id;

                    URequirementList.Add(item);
                }
            }



            foreach (var item in obj.WhatWillYouLearn)
            {
                if (item.Id == 0)
                {
                    item.CourseId = course.Id;

                    CWhatwillYouLearnList.Add(item);
                }
                else
                {
                    item.CourseId = course.Id;

                    UWhatwillYouLearnList.Add(item);
                }
            }


            //foreach (var item in obj.WhatWillYouLearn)
            //{
            //    if(item!=null)
            //    {
            //        var temp = new CourseWhatWillYouLearn() { CourseId = obj.CourseId, 
            //            WhatWillYouLearnText = item.WhatWillYouLearnText,
            //            Id=item.Id };
            //        WhatwillYouLearnList.Add(temp);

            //    }
            //}

            //foreach (var item in obj.Requirements)
            //{
            //    if(item!=null)
            //    {
            //        var temp = new CourseRequirement() { CourseId = obj.CourseId, 
            //            RequirementText = item.RequirementText,
            //             id=item.id,

            //        };
            //        RequirementList.Add(temp);

            //    }
            //}

            _db.CourseWhatWillYouLearns.AddRange(CWhatwillYouLearnList);
            _db.CourseRequirements.AddRange(CRequirementList);
            _db.CourseWhatWillYouLearns.UpdateRange(UWhatwillYouLearnList);
            _db.CourseRequirements.UpdateRange(URequirementList);
            _db.SaveChanges();

            return RedirectToAction(nameof(ManageCourse), new { id = course.Id });


        }


        public async Task<IActionResult> CoursePreview(int id)
        {

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return View("NotFound");
            }

            // var sections = _db.Sections.Where(s => s.CourseId == course.Id).ToList();
            //var lectures = _db.Lectures.Where(l => sections.Contains(_db.Lectures.FirstOrDefault(k=>k.SectionId==l.SectionId).Section)).ToList();

            //var contents = _db.Contents.Where(c => lectures.Contains(_db.Contents.FirstOrDefault(v=>v.LectureId==c.LectureId).Lecture)).ToList();


            //List<CoursePreviewViewModel> previewObjs = new List<CoursePreviewViewModel>();


            ViewBag.courseId = course.Id;




















            return View();
        }


        public async Task<IActionResult> CoursePreviewAPI(int id)
        {

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return View("NotFound");
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

            // return View(previewObjs );
        }



        public async Task<IActionResult> CourseLandingPagePreview(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == id);
            if (isAuthorOfCourse == null)
            {
                return View("Notfound");
            }

            var course = _db.Courses.Include(t => t.ApplicationUser).FirstOrDefault(c => c.Id == id);

            var csections = _db.Sections.Where(s => s.CourseId == course.Id).Where(s => s.isSectionHidden == false);

            string courseIntrovideolink = null;
            string courseImageLink = null;
            string courseLanguage = "Default Language";

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


            if (course.CourseLanguageId != 0)
            {
                var language = _db.Languages.FirstOrDefault(c => c.Id == course.Id);
                if (language != null)
                {
                    courseLanguage = language.Name;

                }

            }




            List<SectionsWithLectures> SecLectures = new List<SectionsWithLectures>();

            var allContent = _db.Contents.ToList();



            var lectures = _db.Lectures.ToList().Where(l => l.isLectureHidden == false);


            foreach (var item in csections)
            {
                SectionsWithLectures sl = new SectionsWithLectures();
                sl.section = item;

                // finding the numbers of lecture
                int secLectures = 0;
                int duration = 0;

                foreach (var item3 in lectures)
                {
                    if (item3.SectionId == item.Id)
                    {

                        secLectures++;


                    }
                }

                sl.numberOfLectures = secLectures;



                foreach (var item3 in allContent)
                {

                    if (item3.ContenType == Models.Content.ContentTypes.Video && item3.isDownlodableContent == false && _db.Lectures.Where(l => l.Section.Id == item.Id).ToList().Contains(item3.Lecture))
                    {
                        if (item3.Length == 0)
                        {
                            duration = duration + 1;
                        }

                        else
                        {
                            duration = duration + (int)item3.Length;
                        }

                    }

                }



                sl.LenghthOfSection = duration;


                SecLectures.Add(sl);
            }



            var allLectures = new List<Lecture>();
            foreach (var item in SecLectures)
            {

                var sLectures = _db.Lectures.Where(l => l.SectionId == item.section.Id).ToList();
                allLectures.AddRange(sLectures);
                item.Lectures = sLectures;


                //   item.content = _db.Contents.FirstOrDefault(c=>c.LectureId==);


            }



            var aSections = _db.Sections.Where(s => s.CourseId == course.Id).ToList();

            var freeLectures = _db.Lectures.Where(l => aSections.Contains(_db.Lectures.FirstOrDefault(s => s.SectionId == l.SectionId).Section)).Where(l => l.isPreview == true).ToList();

            var freeContent = _db.Contents.Where(c => freeLectures.Contains(_db.Contents.FirstOrDefault(s => s.LectureId == c.LectureId).Lecture)).Where(j => j.isDownlodableContent == false && j.ContenType == Models.Content.ContentTypes.Video).ToList();

            List<FreeLectureModel> freeLecModel = new List<FreeLectureModel>();



            foreach (var item in freeLectures)
            {

                FreeLectureModel model = new FreeLectureModel();
                model.lecture = item;


                var content = freeContent.FirstOrDefault(c => c.LectureId == item.id);

                if (content != null)
                {
                    var link = await GetLink(content.Name, SD.CourseContentFolderName);
                    model.VideoLink = link;
                    model.duration = (int)content.Length;
                    freeLecModel.Add(model);





                }



















            }






            var obj = new CourseLandingPagePreviewViewModel()
            {
                Author = user,
                FreeLecModel = freeLecModel,




                CourseIntroVideoLink = courseIntrovideolink,
                CoursePhotoLink = courseImageLink,
                CourseSubtitle = course.subtitle,
                CourseTitle = course.topicName,
                CourseUploadedOn = DateTime.Now.ToShortDateString(),
                Description = course.description,
                Language = courseLanguage,
                LastUpdated = DateTime.Now.ToShortDateString(),
                NumberofArtice = allContent.Where(
                    c => c.ContenType == Models.Content.ContentTypes.text
                    && c.isDownlodableContent == false
                    && allLectures.Contains(c.Lecture)).Count().ToString(),
                Requirements = _db.CourseRequirements.Where(c => c.CourseId == course.Id).ToList(),
                Price = course.price,
                TotalContentOfVideoHours = _db.Contents.Where(c => c.ContenType == Models.Content.ContentTypes.Video && c.isDownlodableContent == false && allLectures.Contains(c.Lecture)).Select(c => c.Length).Sum().ToString(),
                WhatWillYouLearns = _db.CourseWhatWillYouLearns.Where(c => c.CourseId == course.Id).ToList(),
                SectionsWithLectures = SecLectures,

                content = allContent.Where(c => c.ContenType == Models.Content.ContentTypes.Video || c.ContenType == Models.Content.ContentTypes.text).ToList(),


                // Lectures = _db.Lectures.Where(l=>l.).ToList();



            };


            obj.UserPhotoLink = _db.Defaults.FirstOrDefault(d => d.Name == SD.UserDefaultImage).Link;


            if (_db.ProfilePhoto.FirstOrDefault(d => d.UserId == user.Id) != null)
            {
                obj.UserPhotoLink = _db.ProfilePhoto.FirstOrDefault(d => d.UserId == user.Id).PhotoLink;

            }

            return View(obj);
        }


        public async Task<string> Upload(Stream stream, string fileName, string DestinationFolder)
        {


            var auth = new FirebaseAuthProvider(new FirebaseConfig(googleFireStoreStorageSettings.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(googleFireStoreStorageSettings.AuthEmail, googleFireStoreStorageSettings.AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                googleFireStoreStorageSettings.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })

                .Child(DestinationFolder)
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);
            //.PutAsync(stream, cancellation.Token);

            //   task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // cancel the upload
            // cancellation.Cancel();
            string link;
            try
            {
                link = await task;
                return link;
            }
            catch
            {
                link = null;
                return link;
            }



        }

        public async Task Delete(string fileName, string DestinationFolder)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(googleFireStoreStorageSettings.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(googleFireStoreStorageSettings.AuthEmail, googleFireStoreStorageSettings.AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                googleFireStoreStorageSettings.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })

                .Child(DestinationFolder)
                .Child(fileName)
                .DeleteAsync();
            //.PutAsync(stream, cancellation.Token);
            //.PutAsync(stream, cancellation.Token);

            //   task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // cancel the upload
            // cancellation.Cancel();

            try
            {
                //string link = await task;

            }
            catch
            {

            }

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


        public async Task<IActionResult> Curriculum(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == id);
            if (isAuthorOfCourse == null)
            {
                return View("NotFound");
            }


            return View(new AddSectionViewModel() { courseId = id });
        }



        public async Task<IActionResult> GetAllSections(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == id);
            if (isAuthorOfCourse == null)
            {
                return NotFound();
            }

            var sections = _db.Sections.Where(s => s.CourseId == id).ToList();
            //  var c = new Course() { topicName = "ded" };
            return Ok(sections);
        }


        [HttpPost]
        public async Task<IActionResult> AddSection(AddSectionViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == obj.courseId
                );
                if (isAuthorOfCourse == null)
                {
                    return NotFound();
                }

                var section = new Section() { Name = obj.sectionName, CourseId = obj.courseId };

                _db.Sections.Add(section);
                _db.SaveChanges();
                return Ok();

            }
            else
            {

                return StatusCode(404, ModelState);

            }

        }


        public async Task<IActionResult> EditSection(EditSectionViewModel obj)
        {
            var section = _db.Sections.FirstOrDefault(s => s.Id == obj.sectionId);
            if (section == null || obj.sectionName == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == obj.sectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);


            if (isAuthorOfCourse == null)
            {
                return NotFound();
            }

            section.Name = obj.sectionName;
            _db.Sections.Update(section);
            _db.SaveChanges();
            return Ok();


        }

        public async Task<IActionResult> GetSection(int id)
        {
            var section = _db.Sections.FirstOrDefault(s => s.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == id).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);


            if (isAuthorOfCourse == null)
            {
                return NotFound();
            }



            return Ok(new { section.Name });
        }


        // get all the article  for the 
        public async Task<IActionResult> GetArticles(int id)
        {

            var section = _db.Sections.FirstOrDefault(s => s.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == section.Id).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);


            if (isAuthorOfCourse == null)
            {
                return NotFound();
            }

            if (section == null)
            {
                return NotFound();
            }




            var contents = _db.Contents.Where(c => c.Lecture.SectionId == section.Id).ToList().Where(t => t.LectureArticleText != null);
            //var dtoObj= new { string Article,lecture }
            List<LectureArticleViewModel> dtoObj = new List<LectureArticleViewModel>();

            foreach (var item in contents)
            {
                dtoObj.Add(new LectureArticleViewModel { Article = item.LectureArticleText, LectureId = item.LectureId });
                //dtoObj.LectureId = item.LectureId;
                //dtoObj.Article = item.LectureArticleText; 

            }

            return Ok(dtoObj);


        }


        // get content for the lectureId...
        public async Task<IActionResult> GetContent(int id)
        {
            // lecture Id

            var lecture = _db.Lectures.FirstOrDefault(t => t.id == id);
            var section = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId);
            if (section == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == section.Id).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);


            if (isAuthorOfCourse == null)
            {
                return NotFound();
            }

            if (lecture == null)
            {
                return NotFound();
            }

            var contents = _db.Contents.Where(c => c.LectureId == lecture.id).ToList();

            return Ok(contents);





            //var content = _db.Contents.FirstOrDefault(d => d.Id == id);

            //if(content==null)
            //{
            //    return NotFound();
            //}

            //var lecture = _db.Lectures.FirstOrDefault(s => s.id == content.LectureId);
            //var section = _db.Sections.FirstOrDefault(l =>l.Id==lecture.SectionId);
            //if (section == null)
            //{
            //    return NotFound();
            //}
            //var user = await _userManager.GetUserAsync(User);

            //var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == section.Id).CourseId;
            //var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);


            //if (isAuthorOfCourse == null)
            //{
            //    return NotFound();
            //}


            //return Ok(content);


        }

        public async Task<IActionResult> Section(int id)
        {

            var section = _db.Sections.FirstOrDefault(s => s.Id == id);
            if (section == null)
            {
                return View("NotFound");
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == id).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);


            if (isAuthorOfCourse == null)
            {
                return View("NotFound");
            }


            var obj = new SectionViewModel() { SectionName = section.Name, SectionId = section.Id };
            return View(obj);
        }

        public async Task<IActionResult> GetLectures(int id)
        {
            var section = _db.Sections.FirstOrDefault(s => s.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == id).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);
            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }

            var lectures = _db.Lectures.Where(t => t.SectionId == section.Id).ToList();

            return Ok(lectures);
        }



        public async Task<IActionResult> GetLecture(int id)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.id == id);
            if (lecture == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);

            return Ok(lecture);


        }
        public async Task<IActionResult> GetLectureVideo(int id)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.id == id);
            if (lecture == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);

            var content = _db.Contents.Where(d => d.LectureId == lecture.id).FirstOrDefault(d => d.isDownlodableContent == false);

            if (content == null)
            {
                return Ok(null);
            }

            return Ok(content);


        }


        [HttpPost]
        public async Task<IActionResult> AddDownlodableContent([FromForm] AddDownlodableContentViewModel obj)
        {
            int lectureId = obj.id;

            var lecture = _db.Lectures.FirstOrDefault(l => l.id == obj.id);

            if (lecture == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);

            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }
            if (obj.downlodableContent != null)
            {
                var LectureVideoName = Guid.NewGuid().ToString() + "_" + obj.downlodableContent.FileName;







                var operation = await Upload(obj.downlodableContent.OpenReadStream(), LectureVideoName, SD.CourseContentFolderName);

                if (operation == null)
                {
                    ModelState.AddModelError("", "Not Able To Upload Content");
                    return StatusCode(500, ModelState);
                }

                // to create a intro video
                //   var introvedeo = new CourseIntroVideo() { CourseId = course.Id, CourseIntroVideoLength = IvVm.Duration, Name = courseVideoName };
                //_db.CourseIntroVideos.Add(introv);
                //_db.SaveChanges();





                var content = new Content()
                {
                    LectureId = lecture.id,
                    isDownlodableContent = true,
                    ContenType = Models.Content.ContentTypes.DownlodableItem,
                    Name = LectureVideoName,
                    UploadedFileName = obj.downlodableContent.FileName,



                };

                _db.Contents.Add(content);
                _db.SaveChanges();


                return Ok();


            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddLecture([FromForm] AddLectureViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var section = _db.Sections.FirstOrDefault(s => s.Id == obj.SectionId);
                if (section == null)
                {
                    return BadRequest();
                }
                var user = await _userManager.GetUserAsync(User);

                var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == obj.SectionId).CourseId;
                var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);
                if (isAuthorOfCourse == null)
                {
                    return BadRequest();
                }

                var lecture = new Lecture()
                {
                    LectureDescription = obj.LectureDescription,
                    Name = obj.LectureName,
                    SectionId = obj.SectionId
                };

                _db.Lectures.Add(lecture);
                _db.SaveChanges();



                //Add Lecture with Article with downlodable content..
                if (obj.LectureVideo == null)
                {

                    var content = new Content()
                    {
                        ContenType = Models.Content.ContentTypes.text,
                        LectureId = lecture.id,
                        LectureArticleText = obj.LectureArticle,

                    };
                    _db.Contents.Add(content);
                    _db.SaveChanges();



                }

                //Add Lecture with Video with downlodable content..

                else
                {
                    if (!SD.IsVideo(obj.LectureVideo))
                    {
                        return BadRequest();
                    }



                    var LectureVideoName = Guid.NewGuid().ToString() + "_" + obj.LectureVideo.FileName;

                    if (obj.LectureVideoLength == '0' || obj.LectureVideoLength == 0)
                    {
                        return BadRequest();
                    }

                    if (!SD.IsVideo(obj.LectureVideo))
                    {
                        return BadRequest();
                    }



                    var operation = await Upload(obj.LectureVideo.OpenReadStream(), LectureVideoName, SD.CourseContentFolderName);

                    if (operation == null)
                    {
                        ModelState.AddModelError("", "Not Able To Upload Video");
                        return StatusCode(500, ModelState);
                    }

                    // to create a intro video
                    //   var introvedeo = new CourseIntroVideo() { CourseId = course.Id, CourseIntroVideoLength = IvVm.Duration, Name = courseVideoName };
                    //_db.CourseIntroVideos.Add(introv);
                    //_db.SaveChanges();





                    var content = new Content()
                    {
                        LectureId = lecture.id,
                        Length = obj.LectureVideoLength,
                        ContenType = Models.Content.ContentTypes.Video,
                        Name = LectureVideoName,
                        UploadedFileName = obj.LectureVideo.FileName,



                    };

                    _db.Contents.Add(content);
                    _db.SaveChanges();




                }


                if (obj.DownlodableContent != null)
                {



                    var downlodableContentName = Guid.NewGuid().ToString() + "_" + obj.DownlodableContent.FileName;

                    var operation = await Upload(obj.DownlodableContent.OpenReadStream(), downlodableContentName, SD.CourseContentFolderName);

                    if (operation == null)
                    {
                        ModelState.AddModelError("", "Not Able To Upload Video");
                        return StatusCode(500, ModelState);
                    }


                    var downlodableContent = new Content()
                    {
                        ContenType = Models.Content.ContentTypes.DownlodableItem,

                        LectureId = lecture.id,
                        isDownlodableContent = true,



                        Name = downlodableContentName,
                        UploadedFileName = obj.DownlodableContent.FileName,


                    };
                    _db.Contents.Add(downlodableContent);
                    _db.SaveChanges();
                }


                return Ok();

            }
            else
            {
                return BadRequest();
            }



        }




        [HttpDelete]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.id == id);

            if (lecture == null)
            {
                return BadRequest();

            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);
            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }


            var deleteableContents = _db.Contents.Where(l => l.Id == lecture.id);

            _db.Contents.RemoveRange(deleteableContents);
            _db.SaveChanges();


            _db.Lectures.Remove(lecture);

            _db.SaveChanges();



            return Ok();


        }


        [HttpPost]
        public async Task<IActionResult> SaveEditedLecture(SaveEditLectureViewModel obj)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.id == obj.Id);

            if (lecture == null)
            {
                return BadRequest();

            }


            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);
            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }


            if (obj.EditLectureVideo != null)
            {
                // have article

                // Check The Video
                if (!SD.IsVideo(obj.EditLectureVideo))
                {
                    return BadRequest();
                }

                bool haveVideo = false;
                bool haveArticle = false;



                var oldArticle = _db.Contents.FirstOrDefault(a => a.LectureId == lecture.id && a.ContenType == Models.Content.ContentTypes.text);

                if (oldArticle != null)
                {
                    haveArticle = true;
                }

                var oldVideo = _db.Contents.FirstOrDefault(c => c.LectureId == lecture.id && c.ContenType == Models.Content.ContentTypes.Video);


                if (oldVideo != null)
                {
                    haveVideo = true;
                }




                //  Upload Video

                var NewVideoName = Guid.NewGuid().ToString() + "_" + obj.EditLectureVideo.FileName;

                var operation = await Upload(obj.EditLectureVideo.OpenReadStream(), NewVideoName, SD.CourseContentFolderName);

                if (operation == null)
                {
                    ModelState.AddModelError("", "Not Able To Upload Video");
                    return StatusCode(500, ModelState);
                }

                var editedVideo = new Content()
                {
                    ContenType = Models.Content.ContentTypes.Video,

                    LectureId = lecture.id,
                    isDownlodableContent = false,
                    Name = NewVideoName,
                    UploadedFileName = obj.EditLectureVideo.FileName,
                    Length = obj.Length

                };
                _db.Contents.Add(editedVideo);
                _db.SaveChanges();


                // Delete Previous Content


                if (haveArticle == true)
                {
                    _db.Contents.Remove(oldArticle);
                }

                if (haveVideo == true)
                {
                    await Delete(oldVideo.Name, SD.CourseContentFolderName);

                    _db.Contents.Remove(oldVideo);
                }

                _db.SaveChanges();



                // Update description


                lecture.LectureDescription = obj.EditLectureDescription;
                _db.SaveChanges();



                return Ok();


            }
            else
            {

                if (obj.EditLectureArticle == null || obj.EditLectureArticle == "null")
                {
                    lecture.LectureDescription = obj.EditLectureDescription;


                    _db.SaveChanges();


                    return Ok();
                }



                bool haveVideo = false;
                bool haveArticle = false;



                var oldArticle = _db.Contents.FirstOrDefault(a => a.LectureId == lecture.id && a.ContenType == Models.Content.ContentTypes.text);

                if (oldArticle != null)
                {
                    haveArticle = true;
                }

                var oldVideo = _db.Contents.FirstOrDefault(c => c.LectureId == lecture.id && c.ContenType == Models.Content.ContentTypes.Video);


                if (oldVideo != null)
                {
                    haveVideo = true;
                }



                // Adding new Article 
                var newArticle = new Content()
                {
                    ContenType = Models.Content.ContentTypes.text,

                    LectureId = lecture.id,
                    isDownlodableContent = false,

                    LectureArticleText = obj.EditLectureArticle,


                };

                _db.Contents.Add(newArticle);
                _db.SaveChanges();


                // Delete old content

                if (haveArticle == true)
                {

                    _db.Contents.Remove(oldArticle);



                }

                if (haveVideo == true)
                {
                    await Delete(oldVideo.Name, SD.CourseContentFolderName);
                    _db.Contents.Remove(oldVideo);
                    _db.SaveChanges();
                }


                // update Description

                lecture.LectureDescription = obj.EditLectureDescription;


                _db.SaveChanges();





                return Ok();


            }


            return BadRequest();

        }


        public async Task<IActionResult> LectureDetailsContent(int id)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.id == id);

            if (lecture == null)
            {
                return BadRequest();

            }


            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);
            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }

            var contents = _db.Contents.Where(c => c.LectureId == lecture.id);


            foreach (var item in contents)
            {
                if (item.Name != null) {
                    item.Link = await GetLink(item.Name, SD.CourseContentFolderName);
                }
            }



            var contentDtos = new List<ContentDto>();


            string lArticle = "";
            string lVideoLink = " ";
            double lVideoLength = 0;
            int type = -1;
            List<DownlodableContentViewModel> DownlodableContentList = new List<DownlodableContentViewModel>();
            foreach (var item in contents)
            {
                if (item.ContenType == Models.Content.ContentTypes.text)
                {
                    lArticle = item.LectureArticleText;
                    type = 2;
                    break;
                }

                if (item.ContenType == Models.Content.ContentTypes.Video)
                {
                    lVideoLink = item.Link;
                    lVideoLength = item.Length;
                    type = 0;
                    break;
                }
                // ContentDto conte = _mapper.Map<ContentDto>(item);
                //contentDtos.Add(conte);
            }

            foreach (var item in contents)
            {
                if (item.isDownlodableContent)
                {
                    DownlodableContentViewModel dfile = new DownlodableContentViewModel() { name = item.UploadedFileName, Link = item.Link, downlodableContentId = item.Id };
                    DownlodableContentList.Add(dfile);
                }
            }


            var lectureDto = _mapper.Map<LectureDto>(lecture);

            var obj = new LectureWithContentViewModel() { Id = lecture.id, Article = lArticle, Video = lVideoLink
             , contentType = type, LectureName = lecture.Name, Description = lecture.LectureDescription, VideoLength = lVideoLength, Downlodablecontents = DownlodableContentList };


            return Ok(obj);



        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDownlodableContent(int id)
        {
            var obj = _db.Contents.FirstOrDefault(l => l.Id == id);


            if (obj == null)
            {
                return BadRequest();

            }

            var lecture = _db.Lectures.FirstOrDefault(l => l.id == obj.LectureId);

            var section = _db.Sections.FirstOrDefault(s => s.Id == lecture.SectionId);

            if (lecture == null)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);

            //  var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == obj.Lecture.SectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == section.CourseId);
            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }


            var content = _db.Contents.FirstOrDefault(c => c.Id == id);
            await Delete(content.Name, SD.CourseContentFolderName);
            if (content == null)
            {
                return BadRequest();
            }

            _db.Contents.Remove(content);

            _db.SaveChanges();



            return Ok(new { id = lecture.id });


        }

        public async Task<IActionResult> LecturesList(int id)
        {
            int sectionId = id;
            var section = _db.Sections.FirstOrDefault(s => s.Id == sectionId);
            if (section == null)
            {
                return BadRequest();
            }
            var user = await _userManager.GetUserAsync(User);

            var CourseOfSection = _db.Sections.FirstOrDefault(s => s.Id == sectionId).CourseId;
            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == CourseOfSection);
            if (isAuthorOfCourse == null)
            {
                return BadRequest();
            }

            var lectures = _db.Lectures.Where(l => l.SectionId == section.Id);
            //     var o = _mapper.Map<NationalParkDto>(obj);
            List<LectureDto> lecturesDtoList = new List<LectureDto>();

            foreach (var item in lectures)
            {
                var lec = _mapper.Map<LectureDto>(item);
                lecturesDtoList.Add(lec);
            }

            return Ok(lecturesDtoList);


        }




        public async Task<IActionResult> CourseSetting(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var isAuthorOfCourse = _db.Courses.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.Id == id);

            if (isAuthorOfCourse == null)
            {
                ViewBag.Message = "Course Not Found";
                return View("NotFound");
            }

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            var sections = _db.Sections.Where(c => c.CourseId == id).ToList();

            var lectures = _db.Lectures.Where(l => sections.Contains(_db.Sections.FirstOrDefault(k => k.Id == l.SectionId))).ToList();


            var sectionWithLectures = new List<SectionsWithCourse>();







            foreach (var item in sections)
            {
                var sectionWithLectureItem = new SectionsWithCourse();

                sectionWithLectureItem.section = item;

                var lecturesList = new List<Lecture>();

                foreach (var item1 in lectures)
                {


                    if (item1.SectionId == item.Id)
                    {
                        Console.WriteLine(item1.Name);

                        lecturesList.Add(item1);

                        //sectionWithLectureItem.Lectures.Add(item1);



                    }


                }

                sectionWithLectureItem.Lectures = lecturesList;


                sectionWithLectures.Add(sectionWithLectureItem);


                // lecturesList.Clear();


            }



            return View(sectionWithLectures);

        }



        [HttpPost]
        public async Task<IActionResult> SetFreeLectures([FromForm] SetFreeLectures obj)
        {

            obj.id = 1009;
            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.id);



            if (course == null)
            {
                return BadRequest();
            }


            List<Lecture> lectures = new List<Lecture>();





            foreach (var item in obj.lectureIds)
            {
                var lec = _db.Lectures.FirstOrDefault(l => l.id == item);



                lec.isPreview = true;

                if (lec != null)
                {

                    lectures.Add(lec);

                }

            }

            _db.SaveChanges();

            return Ok();



        }






        [HttpPost]

        public async Task<IActionResult> SetUnsetPreview(SUPreview previewobj)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.id == previewobj.lectureId);

            if (lecture == null)
            {
                return NotFound();
            }

            lecture.isPreview = !lecture.isPreview;


            int done = _db.SaveChanges();


            if (done == 1)
            {
                return Ok();
            }


            else
            {
                return BadRequest();
            }








        }


        [HttpPost]
        public async Task<IActionResult> ShowHideSection(SHSection sectionobj)
        {
            var section = _db.Sections.FirstOrDefault(s => s.Id == sectionobj.sectionId);


            if (section == null)
            {
                return NotFound();
            }


            section.isSectionHidden = !section.isSectionHidden;

            int done = _db.SaveChanges();


            if (done == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> ShowHideLecture(SHLecture lectureObj)
        {

            var lecture = _db.Lectures.FirstOrDefault(l => l.id == lectureObj.lectureId);


            if (lecture == null)
            {
                return NotFound();
            }


            lecture.isLectureHidden = !lecture.isLectureHidden;

            int done = _db.SaveChanges();

            if (done == 1)
            {
                return Ok();
            }

            else
            {
                return BadRequest();

            }











        }




        public async Task<IActionResult> PreviewCourse(int id)
        {
            return View();
        }


        public async Task<IActionResult> CurrentCourseStatus(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Id == id);


            if (course == null)
            {
                return NotFound();
            }

            var currentCourseObject = new CurrentCourseViewModel();


            currentCourseObject.course =  _mapper.Map<CourseDto>(course);
            currentCourseObject.numberofStudentEnrolled = _db.CourseTransictionRecords.Where(c => c.CourseId == course.Id && c.isRefund == false).Count();
            currentCourseObject.revenueThisMonth = _db.CourseTransictionRecords.Where(c => c.CourseId == course.Id && c.isRefund == false&&c.purchaseDate.Month==DateTime.Now.Month && c.purchaseDate.Year == DateTime.Now.Year).Select(s => s.price).Sum();
            currentCourseObject.totalrevenue = _db.CourseTransictionRecords.Where(c => c.CourseId == course.Id && c.isRefund == false).Select(s => s.price).Sum();
            currentCourseObject.totalrevenueToday = _db.CourseTransictionRecords.Where(c => c.CourseId == course.Id && c.isRefund == false && c.purchaseDate==DateTime.Now).Select(s => s.price).Sum();
            currentCourseObject.CourseRatings = _db.CourseRatings.Where(c => c.CourseId == course.Id).Include(c=>c.ApplicationUser).ToList();
            
            return View(currentCourseObject);
            

           

           
        }



        public async Task<IActionResult> CurrentCourseStatusApi(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Id == id);


            if (course == null)
            {
                return NotFound();

            }

            var currentCourseObject = new CurrentCourseViewModel();


            currentCourseObject.course = _mapper.Map<CourseDto>(course);
            

           
                return Json(currentCourseObject);
           
        }






        [HttpPost]
        public async Task<IActionResult>SendCourseForApproval(int id)
        {


            // check For If requested By Author or Not 

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            course.CourseStatus = SD.CourseStatusReview;

           int done=_db.SaveChanges();

            if (done == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();

            }






        }




        [HttpPost]

        public async Task<IActionResult>PublishCourse(int id)
        {
            // check For If requested By Author or Not 

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            course.CourseStatus = SD.CourseStatusPublished;

            int done = _db.SaveChanges();

            if (done == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();

            }



        }




        public async Task<IActionResult> UnPublishCourse(int id)
        {

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            course.CourseStatus = SD.CourseStatusUnPublished;

            int done = _db.SaveChanges();

            if (done == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();

            }


        }




        public async Task<IActionResult> AddCouponPage(int id)
        {
            var course = _db.Courses.FirstOrDefault(c=>c.Id==id);

            if (course == null)
            {
                return View("NotFound");
            }



            return View(course);
        }


        public async Task<IActionResult> CouponsList(int id)
        {


            var course = _db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var coupons = _db.Coupons.Where(c => c.CourseId == course.Id).ToList();



            var couponDtosList = new List<CouponDto>();



            foreach (var item in coupons)
            {

                var couponDtoObject = _mapper.Map<CouponDto>(item);
                couponDtosList.Add(couponDtoObject);



            }


            return Json(couponDtosList);



        }



        [HttpPost]
        public async Task<IActionResult> AddCoupon(AddCouponViewModel Couponobj)
        {

            // Check Author For The Course ...
            
            var course = _db.Courses.FirstOrDefault(c => c.Id == Couponobj.courseId);

            if (course == null)
            {
                return NotFound();
            }

            var coupon = new Coupon();

            coupon.couponCode = Couponobj.couponCode;
            coupon.coupounCreatedOn = DateTime.Now;
            coupon.CourseId = course.Id;

            if (Couponobj.discountType == 0)
            {
                coupon.discountMethod = Models.Enums.DiscountType.price;
            }
            else if(Couponobj.discountType == 1)
            {
                coupon.discountMethod = Models.Enums.DiscountType.percent;
            }

            else
            {
                return BadRequest();
            }


            coupon.discountValue = Couponobj.discountValue;

            coupon.isCouponBlocked = false;


            coupon.numberOfcouponAlloted = Couponobj.couponAlloted;

            coupon.numberofcouponUnUsed = Couponobj.couponAlloted;

            coupon.validTill = Couponobj.validtill;




            _db.Coupons.Add(coupon);


            _db.SaveChanges();


            return Ok();














         
        }


        [HttpPost]

        public async Task<IActionResult> IncreaseCoupon(int id)
        {

            var coupon = _db.Coupons.FirstOrDefault(c => c.id == id);

            if (coupon == null)
            {
                return NotFound();
            }

            if (DateTime.Compare(coupon.validTill,DateTime.Now )<0)
            {
                return BadRequest();
            }

            coupon.numberOfcouponAlloted++;
            coupon.numberofcouponUnUsed++;



            _db.SaveChanges();

            return Ok();


        }


        [HttpPost]
        public async Task<IActionResult> DecreaseCoupon(int id)
        {
            var coupon = _db.Coupons.FirstOrDefault(c => c.id == id);

            if (coupon == null)
            {
                return NotFound();
            }

            if (DateTime.Compare(coupon.validTill, DateTime.Now) < 0)
            {
                return BadRequest();
            }

            if ((coupon.numberOfcouponAlloted - coupon.numberofcouponUnUsed)<0)
            {
                return BadRequest();
            }

            coupon.numberOfcouponAlloted--;
            coupon.numberofcouponUnUsed--;

            _db.SaveChanges();

            return Ok();



        }



        [HttpPost]

        public async Task<IActionResult> BlockUnblockCoupon(int id)
        {

            var coupon = _db.Coupons.FirstOrDefault(c => c.id == id);

            if (coupon == null)
            {

                return NotFound();

            }


            coupon.isCouponBlocked = !coupon.isCouponBlocked;


           var d= _db.SaveChanges();


            if (d == 1)
            {
                return Ok();

            }

            else
            {
                return BadRequest();

            }









        }




    }
}

