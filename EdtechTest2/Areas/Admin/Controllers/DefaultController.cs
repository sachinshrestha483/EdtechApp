using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Admin.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = SD.InstructorUser)]

    public class DefaultController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;


        public DefaultController(ApplicationDbContext db,IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
       
        public IActionResult Index()
        {
            var obj = _db.Defaults.ToList();
            
             return View(obj);
        }

        public IActionResult sit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult sit(Default model)
        {
            if (ModelState.IsValid)
            {
                if(model.Photo==null)
                {
                    var def = new Default
                    {
                        Name = model.Name,
                        ContentString = model.ContentString,
                         
                       
                    };
                    _db.Defaults.Add(def);
                    _db.SaveChanges();
                }
                else
                {
                    var isFormatCorrect = SD.IsImage(model.Photo);
                    if (isFormatCorrect == true)
                    {
                        string uniqueFileName = ProcessUploadedFile(model);
                        var def = new Default
                        {
                            Name = model.Name,
                            ContentString = model.ContentString,

                            Link = uniqueFileName,
                        };
                        _db.Defaults.Add(def);
                        _db.SaveChanges();

                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Format File Uploaded");
                        return View(model);
                    }

                }







            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int id)
        {
            var obj = _db.Defaults.FirstOrDefault(t=>t.id==id);

            if (obj == null)
            {
                ViewBag.Message = "Object Not Found Wit Give Id";
                return View("NotFound");
            }
            else
            {
                var vm = new PhotoEditViewModel { id=obj.id, Link=obj.Link, ContentString=obj.ContentString, Name=obj.Name };
                return View(vm);
            }


            return View(new { id=id});
        }

        private string ProcessUploadedFile(Default model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


        private string EProcessUploadedFile(IFormFile model)
        {
            string uniqueFileName = null;
            if (model != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            var obj = _db.Defaults.FirstOrDefault(t=>t.id==id);
            if(obj==null)
            {
                ViewBag.Message("Object Not Found With The Give Id");
                return View("NotFound"); 
            }
            _db.Defaults.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }




        [HttpPost]
        public IActionResult Update(PhotoEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UpdatePhoto == null)
                {
                    var obj = _db.Defaults.FirstOrDefault(d => d.id == model.id);

                    if (obj == null)
                    {
                        return RedirectToAction("NotFound");
                    }
                    else
                    {
                        obj.Name = model.Name;
                        obj.ContentString = model.ContentString;
                        _db.Update(obj);
                        _db.SaveChanges();
                    }

                }
                else
                {
                    var obj = _db.Defaults.FirstOrDefault(d => d.id == model.id);
                    if (obj == null)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                                   "images", obj.Link);
                        System.IO.File.Delete(filePath);
                    }

                   var isFormatCorrect = SD.IsImage(model.UpdatePhoto);
                    if (isFormatCorrect == true)
                    {
                        string uniqueFileName = EProcessUploadedFile(model.UpdatePhoto);
                        obj.Link = uniqueFileName;
                        obj.Name = model.Name;
                        obj.ContentString = model.ContentString;

                        _db.Defaults.Update(obj);
                        _db.SaveChanges();

                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Format File Uploaded");
                        return View(model);
                    }



                }
            }
            return RedirectToAction("Update",model.id);
        }



    }
}
