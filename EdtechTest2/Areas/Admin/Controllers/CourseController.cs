using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EdtechTest2.Areas.Admin.ViewModels;
using EdtechTest2.data;
using EdtechTest2.Models.Dtos;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdtechTest2.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.InstructorUser)]

    public class CourseController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CourseController(ApplicationDbContext db, IEmailSender emailSender, IMapper mapper)
        {
            _db = db;
            _emailSender = emailSender;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult PendingCourse()
        {
            return View();
        }


        public IActionResult PendingCoursesList()
        {
            var courses = _db.Courses.Where(c => c.CourseStatus == SD.CourseStatusReview);


            var coursesDto = new List<CourseDto>();

            foreach (var item in courses)
            {

                var courseDtoObj= _mapper.Map<CourseDto>(item);

                coursesDto.Add(courseDtoObj);

            }


            return Json(coursesDto);



        }

        [HttpPost]
        public async Task<IActionResult> PendingCourseOperation(PendingCourseviewModel obj)
        {
            var course = _db.Courses.Include(c=>c.ApplicationUser).FirstOrDefault(c => c.Id == obj.coursesId);

            if(course==null)
            {

                return BadRequest();
            }



            if (obj.approved == 1)
            {
                course.CourseStatus = SD.CourseStatusApproved;
            }

            else if(obj.rejected==1)
            {
                course.CourseStatus = SD.CourseStatusRejected;

                string email=course.ApplicationUser.Email;
                string subject=" Regarding Rejection of your  Course Approval Request ";
                string message="Your Course Has Been Rejected  as It Does Not Stand On Our Standards there are Reasons Why Your Application Rejected  "+obj.reasonsForRejection;
                await _emailSender.SendEmailAsync(email, subject, message);



            }


            _db.SaveChanges();





            return Ok();
        }

        

       




       
    }
}
