using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Admin.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.InstructorUser)]

    public class LanguageController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LanguageController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj = new LanguageViewModel() {  Languages=_db.Languages.ToList(),    };
            return View(obj);
        }

        [HttpPost]

        public IActionResult Index(LanguageViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var language = new Language() { Name = obj.Name };
                _db.Languages.Add(language);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Edit(int id)
        {
            var language = _db.Languages.FirstOrDefault(n => n.Id == id);
            if (language == null)
            {
                ViewBag.Message = "Category With This Id Not Found";
                return View("Notfound");
            }
            var obj = new LanguageViewModel()
            {
                Name = language.Name,
                id = language.Id,
            };


            //return RedirectToAction(nameof(Index));
            return View(obj);

        }

        [HttpPost]

        public IActionResult Edit(LanguageViewModel obj)
        {
            var language = _db.Languages.FirstOrDefault(n => n.Id == obj.id);

            if (language == null)
            {
                ViewBag.Message = "Category With This Id Not Found";
                return View("Notfound");
            }

            language.Name = obj.Name;
            _db.Languages.Update(language);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




    }
}
