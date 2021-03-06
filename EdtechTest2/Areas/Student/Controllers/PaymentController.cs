using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Student.ViewModel;
using EdtechTest2.data;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace EdtechTest2.Areas.Student.Controllers
{

    [Area("Student")]
    [Authorize(Roles = SD.StudentUser + SD.InstructorUser)]


    public class PaymentController : Controller
    {
        private readonly IEmailSender _emailSender;

        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _emailSender = emailSender;

        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.isDone == false).Where(c => c.userId == user.Id).ToList();

            if (userCartItems.Count == 0)
            {
                return View("CartEmpty");
            }


            var courses = new List<Course>();

            int total = 0;


            foreach (var item in userCartItems)
            {
                var course = _db.Courses.FirstOrDefault(c => c.Id == item.courseId);

                if (course == null)
                {
                    continue;
                }
                else
                {
                    courses.Add(course);
                }

                if (item.CouponId == null)
                {
                    if (course.price > 0)
                    {
                        total = total + (int)(course.price);

                    }


                }
                else
                {
                    var coupon = _db.Coupons.FirstOrDefault(c => c.id == item.CouponId);


                    if (coupon.isCouponBlocked == false && coupon.numberofcouponUnUsed <= coupon.numberOfcouponAlloted && coupon.validTill > DateTime.Now)
                    {
                        if (coupon.discountMethod == Models.Enums.DiscountType.percent)
                        {

                            var d = course.price * coupon.discountValue / 100;


                            if (d > course.price)
                            {
                                total = total + (int)(course.price);

                            }

                            total = total + (int)(course.price - (course.price * coupon.discountValue / 100));




                        }


                        if (coupon.discountMethod == Models.Enums.DiscountType.price)
                        {

                            if (coupon.discountValue > course.price)
                            {
                                total = (int)(total + course.price);
                            }

                            total = total + (int)(course.price - coupon.discountValue);

                        }
                    }





                }



               


            }

            var checkoutobj = new CheckoutViewModel();


            checkoutobj.total = total;

            checkoutobj.User = user;


            checkoutobj.courses = courses;

            return View(checkoutobj);
        }



        [HttpPost]
        public async Task<IActionResult> checkout(string razorpay_payment_id)
        {




            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.userId == user.Id).Where(c=>c.isDone==false).ToList();

            var courses = new List<Course>();


            int total = 0;


            foreach (var item in userCartItems)
            {
                var course = _db.Courses.FirstOrDefault(c => c.Id == item.courseId);

                if (course == null)
                {
                    continue;
                }


                else
                {
                    courses.Add(course);
                }


                if (item.CouponId == null)
                {
                    if (course.price > 0)
                    {
                        total = total + (int)(course.price);

                    }


                }
                else
                {
                    var coupon = _db.Coupons.FirstOrDefault(c => c.id == item.CouponId);


                    if (coupon.isCouponBlocked == false && coupon.numberofcouponUnUsed <= coupon.numberOfcouponAlloted &&coupon.numberofcouponUnUsed!=0 &&coupon.validTill > DateTime.Now)
                    {
                        if (coupon.discountMethod == Models.Enums.DiscountType.percent)
                        {

                            var d = course.price * coupon.discountValue / 100;


                            if (d > course.price)
                            {
                                total = total + (int)(course.price);

                            }

                            total = total + (int)(course.price - (course.price * coupon.discountValue / 100));


                            coupon.numberofcouponUnUsed--;
                          

                        }


                        if (coupon.discountMethod == Models.Enums.DiscountType.price)
                        {

                            if (coupon.discountValue > course.price)
                            {
                                total = (int)(total + course.price);
                            }

                            total = total + (int)(course.price - coupon.discountValue);
                            coupon.numberofcouponUnUsed--;
                          
                        }
                    }





                }





            }

            string paymentId = razorpay_payment_id;
            var client = new RazorpayClient(SD.razorPayKey_Id, SD.razorPayKey_Secret);
            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("amount", total*100);
            // this in paise.....

            options.Add("currency", "INR");
            Razorpay.Api.Order order = client.Order.Create(options);
            var payment = new Payment(paymentId);
            Dictionary<string, object> all = new Dictionary<string, object>();
            var st = payment.Capture(options);
            string status = st.Attributes.status;
            if (status.ToLower() == "captured")
            {
               // 

                ViewBag.Message = "Paymentm  Done";


                var objs = new List<CourseTransictions>();



                foreach (var item in courses)
                {

                    var obj = new CourseTransictions();

                    obj.ApplicationUser = user;

                    obj.CourseId = item.Id;

                    obj.purchaseDate = DateTime.Now;



                  
                   obj.CouponId = userCartItems.FirstOrDefault(d => d.courseId == item.Id).CouponId;


                    obj.paymentToken = paymentId;


                    if (obj.CouponId == null)
                    {
                        obj.price = (int)(item.price);
                    }


                    else
                    {

                        var coupon = _db.Coupons.FirstOrDefault(c => c.id == obj.CourseId);

                        if(coupon==null)
                        {
                            obj.price = (int)(item.price);
                        }
                        else
                        {

                            if(coupon.isCouponBlocked == false && coupon.numberofcouponUnUsed <= coupon.numberOfcouponAlloted && coupon.numberofcouponUnUsed != 0 && coupon.validTill > DateTime.Now)
                            {


                                if (coupon.discountMethod == Models.Enums.DiscountType.percent)
                                {

                                    var d = item.price * coupon.discountValue / 100;


                                    if (d > item.price)
                                    {
                                        obj.price = (int)(item.price);

                                    }

                                    obj.price = (int)(item.price - (item.price * coupon.discountValue / 100));




                                }


                                if (coupon.discountMethod == Models.Enums.DiscountType.price)
                                {

                                    if (coupon.discountValue > item.price)
                                    {
                                        obj.price = (int)(item.price);
                                    }

                                    obj.price = (int)(item.price - coupon.discountValue);

                                }

                            }

                            else
                            {
                                obj.price = (int)(item.price);

                            }

                        }


                    }






                    //  var price=

                    //obj.price=



                    objs.Add(obj);


                }



                _db.CourseTransictionRecords.AddRange(objs);



                // change value of the  course cart


                foreach (var item in userCartItems)
                {
                    item.isDone= true;

                  




                }

                _db.SaveChanges();

                

              


                






                return RedirectToAction("myCourses","Home",new { Areas="Student"});
            }

            else
            {
                ViewBag.Message = "Payment Not  Done";
                return View("PaymentFailed");
            }



           
        }




       

        public  async Task<IActionResult> RefundPage(int id)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);


            


            // does own the course

            var coursePurchase = _db.CourseTransictionRecords.FirstOrDefault(c => c.CourseId == id&& c.ApplicationUserId==user.Id&&c.isRefund==false);




            if (coursePurchase == null)
            {
                return View("NotFound");
            }


            // 30 day validity

            if (coursePurchase.purchaseDate.AddDays(30) < DateTime.Now) {

                return View("NotRefundableCourse");
            }

            var refundModel = new RefundPageViewModel();

            refundModel.CourseId = id;






            //














            return View(refundModel);

            
        }

        
        [HttpPost]
        public async Task<IActionResult> RefundPage(RefundPageViewModel obj)
        {





            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var course = _db.Courses.FirstOrDefault(c => c.Id == obj.CourseId);





            // does own the course

            var coursePurchase = _db.CourseTransictionRecords.FirstOrDefault(c => c.CourseId == obj.CourseId && c.ApplicationUserId == user.Id && c.isRefund == false);




            if (coursePurchase == null)
            {
                return View("NotFound");
            }


            // 30 day validity

            if (coursePurchase.purchaseDate.AddDays(30) < DateTime.Now)
            {

                return View("NotRefundableCourse");
            }


            if(coursePurchase.paymentToken==null|| coursePurchase.paymentToken == "")
            {
                return View("NotRefundableCourse");
            }

            string paymentId = coursePurchase.paymentToken;
            var client = new RazorpayClient(SD.razorPayKey_Id,SD.razorPayKey_Secret);

            Payment payment = client.Payment.Fetch(paymentId);

            Refund refund = payment.Refund();

            if (refund == null)
            {
                return View("NotRefundableCourse");

            }


            await _emailSender.SendEmailAsync(user.Email, "Refund Done  For Course", "Refund Would Processed in 7 Days");




            coursePurchase.isRefund = true;


            coursePurchase.refundReason = obj.RefundReason;


            coursePurchase.refundDate = DateTime.Now;



            _db.SaveChanges();


            //














            return View("RefundDone");

        }





    }
}
