using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EdtechTest2.Areas.Identity.ViewModels;
using EdtechTest2.Areas.User.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EdtechTest2.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.StudentUser + SD.InstructorUser)]

    public class HomeController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;//This is Already Defined Thing 
        private readonly ApplicationDbContext _db;
        private readonly GoogleFireStoreStorageSettings googleFireStoreStorageSettings;


        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,ApplicationDbContext db, IOptions<GoogleFireStoreStorageSettings> settings)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            googleFireStoreStorageSettings = settings.Value;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var obj = new UserHomepageViewModel { FirstName = user.FirstName, LastName = user.LastName, Description = user.Description,
                Email = user.Email, FacebookName = user.FacebookName, LinkedInName = user.LinkedinName, 
                MobileNo = user.MobileNo, Title = user.Heading, Website = user.WebsiteLink, 
                YoutubeChannelName = user.YoutubeChannelName ,uniqueUserName=user.UserName, TwitterName=user.TwitterName};
            if(TempData["GlobalMessage"]==null)
            {

            }
            else
            {
                ViewBag.GlobalMessage = TempData["GlobalMessage"].ToString();

            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Index(UserHomepageViewModel obj)
        {
            var user = await _userManager.GetUserAsync(User);

            user.Heading = obj.Title;
            user.Description = obj.Description;
            user.FacebookName = obj.FacebookName;
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.LinkedinName = obj.LinkedInName;
            user.MobileNo = obj.MobileNo;
            user.TwitterName = obj.TwitterName;
            user.WebsiteLink = obj.Website;
            user.YoutubeChannelName = obj.YoutubeChannelName;
            var UpdateUser = await _userManager.UpdateAsync(user);

            if(UpdateUser.Succeeded)
            {
                TempData["GlobalMessage"] = "User Updated SucessFully";

            }
            else
            {
                TempData["GlobalMessage"] = "User Not Updated Due To Some Error";

            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            if (user == null)
            {
                ViewBag.Message = "User With Given Id Not found";
                return View("NotFound");
             }
            var picObj = _db.UserPhotos.FirstOrDefault(p => p.UserId == user.Id);
            string address = null;
            if(picObj!=null)
            {
                address = picObj.PhotoLink;
            }
            else
            {
                var pic = _db.Defaults.FirstOrDefault(t => t.Name == SD.UserDefaultImage).Link;
                address = "/images/" + pic;
            }
           
            var obj = new ProfileViewModel
            {
                Descreption = user.Description,
                email = user.Email,
                facebook = user.FacebookName,
                fname = user.FirstName,
                lname = user.LastName,
                linkedin = user.LinkedinName,
                joinedOn = user.JoinedDate.ToShortDateString(),
                title = user.Heading,
                twitter = user.TwitterName,
                 PersonalSite=user.WebsiteLink,
                  PhotoLink=address

            };
            return View(obj);
        }
        public async Task<IActionResult> Photo()
        {
            var user =await  _userManager.GetUserAsync(User);

            

            var obj = new UserPotoUploadViewModel { };


            if(await _db.UserPhotos.FirstOrDefaultAsync(t=>t.UserId==user.Id)==null)
            {
                obj.Link = _db.Defaults.SingleOrDefault(p => p.Name == SD.UserDefaultImage).Link;
                obj.isPotoUploaded = false;
            }


            
            else
            {
                obj.Link =  _db.UserPhotos.FirstOrDefault(p=>p.UserId==user.Id).PhotoLink;
                obj.isPotoUploaded = true;
            }

            return View(obj);
        }


        


        [HttpPost]
        public async Task<IActionResult>Photo(UserPotoUploadViewModel obj)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Photo));
            }

            if(!SD.IsImage(obj.Photo))
            {

                return RedirectToAction(nameof(Photo));
            }
           
            var user = await _userManager.GetUserAsync(User);
            var userPhotoName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;

            if ((await _db.UserPhotos.FirstOrDefaultAsync(t => t.UserId == user.Id) == null))
            {
                var operation=await Upload(obj.Photo.OpenReadStream(), userPhotoName);

                if(operation==null)
                {
                  

                    var obj1 = new UserPotoUploadViewModel { };

                    if (user.ProfilePhotoId == null)
                    {
                        obj1.Link = _db.Defaults.SingleOrDefault(p => p.Name == SD.UserDefaultImage).Link;
                    }
                    else
                    {
                        obj1.Link = _db.ProfilePhoto.FirstOrDefault(p => p.Id == user.ProfilePhotoId).PhotoLink;
                    }
                    ModelState.AddModelError("","Photo Not Able To Uploaded Due To Some Error");
                    return View(obj1);
                }

                // var photo = new ProfilePhoto() {  PhotoLink =operation, UserId=user.Id};
                //await  _db.ProfilePhoto.AddAsync(photo);

                var UserPhoto = new UserPhoto() { PhotoLink = operation, PhotoName = userPhotoName, UserId = user.Id };
                _db.UserPhotos.Add(UserPhoto);
               await  _db.SaveChangesAsync();



            }
            else
            {
            //    var a = _db.UserPhotos.FirstOrDefault(t => t.UserId == user.Id).PhotoName;

            //   await  Delete(deletefilename);
              
                var a = _db.UserPhotos.FirstOrDefault(t => t.UserId == user.Id);
                var deletefilename = a.PhotoName;

                _db.UserPhotos.Remove(a);
                _db.SaveChanges();



                var operation = await Upload(obj.Photo.OpenReadStream(), userPhotoName);

                if (operation == null)
                {


                    var obj1 = new UserPotoUploadViewModel { };

                    if (await _db.UserPhotos.FirstOrDefaultAsync(t => t.UserId == user.Id) == null)
                    {
                        obj1.Link = _db.Defaults.SingleOrDefault(p => p.Name == SD.UserDefaultImage).Link;
                    }
                    else
                    {
                        obj1.Link = _db.ProfilePhoto.FirstOrDefault(p => p.Id == user.ProfilePhotoId).PhotoLink;
                    }
                    ModelState.AddModelError("", "Photo Not Able To Uploaded Due To Some Error");
                    return View(obj1);
                }

                // var photo = new ProfilePhoto() {  PhotoLink =operation, UserId=user.Id};
                //await  _db.ProfilePhoto.AddAsync(photo);

                var UserPhoto = new UserPhoto() { PhotoLink = operation, PhotoName = userPhotoName, UserId = user.Id };
                _db.UserPhotos.Add(UserPhoto);
                await _db.SaveChangesAsync();

                await Delete(deletefilename);

            }


            return RedirectToAction(nameof(Photo));
        }


        public async Task<string> Upload(Stream stream, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(googleFireStoreStorageSettings.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(googleFireStoreStorageSettings.AuthEmail,googleFireStoreStorageSettings.AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                googleFireStoreStorageSettings.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })

                .Child(SD.ProfilePhotoFolderName)
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


        public async Task Delete(string fileName)
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

                .Child(SD.ProfilePhotoFolderName)
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


        [HttpGet]
        public async  Task<ActionResult> GetUserPhoto()
        {
            var user = await _userManager.GetUserAsync(User);

            string link=null;
           var obj = _db.UserPhotos.FirstOrDefault(p => p.UserId == user.Id);
            if(obj==null)
            {
                var pic = _db.Defaults.FirstOrDefault(t => t.Name == SD.UserDefaultImage).Link;
                link = "/images/" + pic;
            }
            // return String(link, "image/jpeg");
                    
            return Content("<img  src="+link+" height='35' width='35' style='border-radius: 50%;' />");
        }

        //public FirebaseStorageReference Delete()
        //{
        //    FirebaseStorageReference images_ref = FirebaseStorageReference.Child("images");

        //    // Child references can also take paths delimited by '/' such as:
        //    // "images/space.jpg".
        //    Firebase.Storage.StorageReference space_ref = images_ref.Child("space.jpg");
        //}

        public IActionResult Account()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Account(UserAccountSettingViewModel model)
        {
            if (ModelState.IsValid)
            {


            



                //User Represt currently logged in user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                ViewBag.GlobalMessage = "Password Changed Sucesfully";
                return View();
            }
            return View();

        }
    }
}
