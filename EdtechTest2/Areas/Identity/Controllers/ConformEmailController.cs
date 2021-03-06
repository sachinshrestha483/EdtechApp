using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Identity.Controllers
{
    [Area("Identity")]

    public class ConformEmailController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ConformEmailController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMHeading = "Error While Conforming Email Id";

                ViewBag.EMBody = "User Does Not Exixt With This User Id";
                return View("CError");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            return View("CError");
        }

    }
}
