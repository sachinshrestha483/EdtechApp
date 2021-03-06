using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Identity.ViewModels;
using EdtechTest2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Identity.Controllers
{
    [Area("Identity")]

    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
                
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Index(LoginViewModel obj, string returnUrl)
        {
            if (ModelState.IsValid)
            // if check box check then persitent cookie or Session Cookie...
            // fourth parameter is account locked in faliure
            {
                var user = await _userManager.FindByEmailAsync(obj.Email);


                if (user != null && user.EmailConfirmed == false
                  && await _userManager.CheckPasswordAsync(user, obj.Password)
                  )
                {
                    ModelState.AddModelError("", "Email Not confirmed Yet");
                    return View(obj);
                }


//                obj.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Login Attept");


                    return View(obj);

                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, obj.Password, obj.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("index", "home", new { area = "Student" });
                }


                ModelState.AddModelError("", "Invalid Login Attept");


                return View(obj);

            }
            return View(obj);
        }
    }
}
