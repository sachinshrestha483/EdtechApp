using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Identity.Controllers
{
    [Area("Identity")]

    public class AcessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
