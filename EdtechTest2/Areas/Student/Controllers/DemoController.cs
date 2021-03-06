using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Student.ViewModel;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = Utility.SD.StudentUser + SD.InstructorUser)]


    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Data([FromForm] ApViewModel obj)
        {
            if(obj==null)
            {
                return Ok(obj);

            }
            return Ok("working"+obj.email+obj.name+obj.fd.FileName);
        }
    }
}
