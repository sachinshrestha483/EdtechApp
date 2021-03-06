using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Admin.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.InstructorUser)]

    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var obj = new AdminHomeViewModel();

            obj.CoursesPendingForApproval=_db.Courses.Where(c=>c.CourseStatus==SD.CourseStatusReview).Count();
            obj.CoursesSelledToday = _db.CourseTransictionRecords.Where(c => c.purchaseDate == DateTime.Now).Count();
            obj.NumberOfCourse = _db.Courses.Where(c => c.CourseStatus == SD.CourseStatusPublished).Count();
            obj.NumberOfInstructor = _db.ApplicationUsers.Where(a => a.IsInstructor == true).Count();
            obj.NumberOfStudents = _db.ApplicationUsers.Where(a => a.IsInstructor == false).Count();
            obj.RevenueTillNow = _db.CourseTransictionRecords.Where(c => c.isRefund == false).Select(c => c.price).Sum();
            obj.TotalNumberOfCoursesSelled = _db.CourseTransictionRecords.Count();
            obj.TotalrevenueToday = _db.CourseTransictionRecords.Where(c => c.purchaseDate == DateTime.Now).Count();
            

            return View(obj);


        }
    }
}
