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
    public class PasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;//This is Already Defined Thing 
        public PasswordController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }




        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var link = "";
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    link = Url.Action("ResetPassword", "Password", new { email = model.Email, token = token, area = "Identity" }, Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"Reset Your Password By Clicking Here <a href='{HtmlEncoder.Default.Encode(link)}'>clicking here</a>.");



                }
                ViewBag.MessageHeading = "If you Are The Registered User We Have Send You the Mail.........";
                return View("Message");
            }
            return View(model);

        }



        public IActionResult ResetPassword(string email, string token)
        {
            if (email == null || token == null)
            {
                ModelState.AddModelError("", "Wrong Token");
                return View(new ResetPasswordViewModel() { });
            }
            var obj = new ResetPasswordViewModel() { Email = email, Token = token };

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(obj.Email);

                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, obj.Token, obj.Password);

                    if (result.Succeeded)
                    {
                        ViewBag.MessageHeading = "Your password reset login Password Change Done";
                        return View("Message");
                    }
                    foreach (var error in result.Errors)

                    {
                        ModelState.AddModelError("", error.Description);

                    }

                    return View(obj);
                }
                //ViewBag.MessageHeading = " Your password reset login Password Change Done";

                //return View("Message");


            }


            return View(obj);
        }




    }
}
