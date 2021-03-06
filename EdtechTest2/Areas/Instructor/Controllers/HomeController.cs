using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EdtechTest2.Areas.Identity.ViewModels;
using EdtechTest2.Areas.Instructor.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EdtechTest2.Areas.Instructor.Controllers
{
    [Area("Instructor")]

    [Authorize(Roles = SD.InstructorUser)]

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly GoogleFireStoreStorageSettings googleFireStoreStorageSettings;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ApplicationDbContext db,
            IOptions<GoogleFireStoreStorageSettings> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            googleFireStoreStorageSettings = settings.Value;

        }
        public async  Task<IActionResult> Index()
        {
            var user =await  _userManager.GetUserAsync(User);

            var courses = _db.Courses.Where(c=>c.ApplicationUserId==user.Id).ToList();

            var defaultImage = _db.Defaults.SingleOrDefault(t => t.Name == SD.CourseDefaultImage);
            var defaultImageLink = "";
            if (defaultImage!=null)
            {
                defaultImageLink = defaultImage.Link;
            }
            foreach (var course in courses)
            {
                if(course.CourseImageLink==null)
                {
                    course.cloudImageLink = "/images/" + defaultImageLink;
                }
                else
                {
                    course.cloudImageLink = "";
                    var link = await GetLink(course.CourseImageLink,SD.CoursePhotoFolderName);

                    if(link==null)
                    {
                        course.cloudImageLink = "/images/" + defaultImageLink;

                    }
                    else
                    {
                        course.cloudImageLink = link;

                    }

                }
            }
            var obj = new InstructorHomePageViewModel() { Courses = courses,  };

            return View(obj);
        }

        [Authorize(Roles ="Student")]
       

        public IActionResult BecomeInstructor()
        {
            return View();
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BecomeInstructor(string a)
        {
            var user =await  _userManager.GetUserAsync(User);
            await _userManager.AddToRoleAsync(user, SD.InstructorUser);
            return RedirectToAction(nameof(Index));
        }


        public async Task<string> GetLink(string fileName,string DestinationFolder)
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


    }
}
