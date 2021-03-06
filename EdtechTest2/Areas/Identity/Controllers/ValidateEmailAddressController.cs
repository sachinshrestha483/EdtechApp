using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EdtechTest2.Areas.Identity.ViewModels;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Identity.Controllers
{

    [Area("Identity")]
    public class ValidateEmailAddressController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;//This is Already Defined Thing 


        public ValidateEmailAddressController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View(new ValidateEmailAddressViewModel());
        }

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ValidateEmailAddressViewModel obj)
        {
            var user = await _userManager.FindByEmailAsync(obj.Email);

            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("index", "ConformEmail", new { userId = user.Id, token = token }, Request.Scheme);


                await _emailSender.SendEmailAsync(user.Email, "Confirm Password", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

            }

            ViewBag.MessageHeading = "If Your Email is Registered We Have Send the Email  Confirm Link To Your Registered Email";
            return View("Message");


        }
    }
}
