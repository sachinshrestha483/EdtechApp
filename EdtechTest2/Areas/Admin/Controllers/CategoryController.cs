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
    [Authorize(Roles =  SD.InstructorUser)]

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj = new CategoryViewModel() { Categories = _db.Categories.ToList() };
            return View(obj);
        }

        [HttpPost]
        public IActionResult Index(CategoryViewModel obj)
        {
            if(ModelState.IsValid)
            {
                var cat = new Category() { Name = obj.Name };
                _db.Categories.Add(cat);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var name = _db.Categories.FirstOrDefault(n => n.id == id);
            if(name==null)
            {
                ViewBag.Message = "Category With This Id Not Found";
                return View("Notfound");
            }
            var obj = new EditCategoryViewModel()
            { 
                Name= name.Name , 
                 id=name.id,
            };


            //return RedirectToAction(nameof(Index));
            return View(obj);

        }




        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel obj)
        {
            var language = _db.Categories.FirstOrDefault(n => n.id == obj.id);

            if(language==null)
            {
                ViewBag.Message = "Category With This Id Not Found";
                return View("Notfound");
            }

            language.Name = obj.Name;
            _db.Categories.Update(language);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
 }
