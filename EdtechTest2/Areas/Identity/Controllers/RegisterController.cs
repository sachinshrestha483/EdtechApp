using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EdtechTest2.Areas.Identity.ViewModels;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Identity.Controllers
{
    [Area("Identity")]

    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;//This is Already Defined Thing 
        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var num = new Random();

                String theUserName= obj.FirstName + '-' + obj.LastName;

                var tempnum = 1;

                if (await _userManager.FindByNameAsync(theUserName) == null)
                {
                   
                }
                else
                {
                    while (await _userManager.FindByNameAsync(theUserName) != null)
                    {
                        tempnum = tempnum + 1;
                        theUserName = obj.FirstName + '-' + obj.LastName + '-' + tempnum;
                    }
                }
               //var id= num.Next(1000, 9999);
               // var appendid = id.ToString() + obj.DateofBirth.Year.ToString();
                var user = new ApplicationUser { UserName = theUserName, Email = obj.Email
                    , DateofBirth=obj.DateofBirth, FirstName=obj.FirstName,LastName=obj.LastName,
                 JoinedDate=DateTime.Now, MobileNo=obj.MobileNo, IsInstructor=false };

                var result = await _userManager.CreateAsync(user, obj.Password);


                if (result.Succeeded)
                {
                    var Addtorole = await _userManager.AddToRoleAsync(user, SD.StudentUser);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("index", "ConformEmail", new { userId = user.Id, token = token }, Request.Scheme);

                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    else
                    {
                        await _emailSender.SendEmailAsync(user.Email, "Confirm Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

                        ViewBag.MessageHeading = "Email Confirm Link Has Been Sent To Your Email";
                        return View("Message");
                    }


                    // second PArameter for session cookie or the permanent cookie
                    //       await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("index", "home", new { area = "Customer" });
                }

                foreach (var error in result.Errors)
                {
                    // Add All Additional Error To ....
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(obj);
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);

            if (result == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in Use");
            }
        }


    }
}
