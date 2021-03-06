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

    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext db,UserManager<ApplicationUser>userManager)
        {

            _db = db;
            _userManager = userManager;


        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CartData()
        {
            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.userId == user.Id).Where(c=>c.isDone==false).ToList();



            var cartItemsList = new List<CartDataViewModel>();



            foreach (var cartItem in userCartItems)
            {

                var cartItemobj = new CartDataViewModel();
                cartItemobj.courseId = cartItem.courseId;//



                int newPrice = 0;//
                int discount = 0;//




                var course = _db.Courses.FirstOrDefault(c=>c.Id== cartItem.courseId);



                cartItemobj.courseName = course.topicName;//

                newPrice = (int)course.price;

                if (cartItem.CouponId == null)
                {
                    cartItemobj.couponCode = "";//
                }

                else
                {
                    var coupon = _db.Coupons.Where(c=>c.validTill>DateTime.Now).Where(c=>c.numberofcouponUnUsed<=c.numberOfcouponAlloted).Where(c=>c.isCouponBlocked==false).FirstOrDefault(c => c.id == cartItem.CouponId);



                    if (coupon == null)
                    {

                    }
                    else
                    {
                        cartItemobj.couponCode = coupon.couponCode;


                        if (coupon.discountMethod == Models.Enums.DiscountType.percent)
                        {
                            discount = coupon.discountValue;

                            newPrice = (int)(course.price - (course.price * discount / 100));

                            if (newPrice < 0)
                            {
                                newPrice = 0;
                            }



                        }


                        else if (coupon.discountMethod == Models.Enums.DiscountType.price)
                        {
                            discount = coupon.discountValue;

                            newPrice = (int)(course.price - discount);

                            if (newPrice < 0)
                            {
                                newPrice = 0;
                            }



                        }
                    }
                    

                }


                cartItemobj.price = newPrice;
                cartItemobj.discount = discount;




                cartItemsList.Add(cartItemobj);

               
               


                



            }









            return Json(cartItemsList);











        }

       [HttpPost]
        public async Task<IActionResult> DeleteCartItems(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.userId == user.Id).Where(c => c.isDone == false).ToList();

            var course = _db.Courses.FirstOrDefault(c => c.Id == id);
            
            if (course == null)
            {
                return NotFound();
            }



            var usercartobject = userCartItems.FirstOrDefault(c => c.courseId == course.Id);

            if (usercartobject == null)
            {
                return BadRequest();

            }


            if (usercartobject != null)
            {
                _db.UserCourseCarts.Remove(usercartobject);
            }



            _db.SaveChanges();



            return Ok();

        }



        public async Task<IActionResult> CartTotalData()
        {
            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.userId == user.Id).Where(c => c.isDone == false).ToList();


            int total = 0;


            foreach (var item in userCartItems)
            {
                var course = _db.Courses.FirstOrDefault(c => c.Id == item.courseId);

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

                            total =total+ (int)(course.price - (course.price * coupon.discountValue / 100));




                        }


                        if (coupon.discountMethod == Models.Enums.DiscountType.price)
                        {

                            if (coupon.discountValue > course.price)
                            {
                                total =(int) (total + course.price);
                            }

                            total = total + (int)(course.price - coupon.discountValue);

                        }
                    }
                    
                        
                       


                }





            }

            return Ok(total);


        }




        public async Task<IActionResult> PurchaseFreeCourses()
        {
            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.userId == user.Id).Where(c => c.isDone == false).ToList();

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


                    if (coupon.isCouponBlocked == false && coupon.numberofcouponUnUsed <= coupon.numberOfcouponAlloted && coupon.numberofcouponUnUsed != 0 && coupon.validTill > DateTime.Now)
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


            if (total == 0)
            {

                ViewBag.Message = "Paymentm  Done";


                var objs = new List<CourseTransictions>();



                foreach (var item in courses)
                {

                    var obj = new CourseTransictions();

                    obj.ApplicationUser = user;

                    obj.CourseId = item.Id;

                    obj.purchaseDate = DateTime.Now;




                    obj.CouponId = userCartItems.FirstOrDefault(d => d.courseId == item.Id).CouponId;


                    obj.paymentToken = null;


                    if (obj.CouponId == null)
                    {
                        obj.price = (int)(item.price);
                    }


                    else
                    {

                        var coupon = _db.Coupons.FirstOrDefault(c => c.id == obj.CourseId);

                        if (coupon == null)
                        {
                            obj.price = (int)(item.price);
                        }
                        else
                        {

                            if (coupon.isCouponBlocked == false && coupon.numberofcouponUnUsed <= coupon.numberOfcouponAlloted && coupon.numberofcouponUnUsed != 0 && coupon.validTill > DateTime.Now)
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
                    item.isDone = true;






                }

                _db.SaveChanges();













                return RedirectToAction("myCourses", "Home", new { Areas = "Student" });
            }

            else
            {
                return RedirectToAction("Index");
            }
        }




        [HttpPost]
        public async Task<IActionResult> PurchaseCourses(PurchaseCoursesViewModel obj)
        {

            var user = await _userManager.GetUserAsync(User);

            var userCartItems = _db.UserCourseCarts.Where(c => c.userId == user.Id).Where(c => c.isDone == false).ToList();


            int total = 0;


            foreach (var item in userCartItems)
            {
                var course = _db.Courses.FirstOrDefault(c => c.Id == item.courseId);

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

            string paymentId = obj.razorpay_payment_id;
            var client = new RazorpayClient(SD.razorPayKey_Id, SD.razorPayKey_Secret);
            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("amount", total);
            // this in paise.....

            options.Add("currency", "INR");
            Razorpay.Api.Order order = client.Order.Create(options);
            var payment = new Payment(paymentId);
            Dictionary<string, object> all = new Dictionary<string, object>();
            var st = payment.Capture(options);
            string status = st.Attributes.status;
            if (status.ToLower() == "captured")
            {
                ViewBag.Message = "Paymentm  Done";
                return View();
            }

            else
            {
                ViewBag.Message = "Payment Not  Done";
                return View("PaymentFailed");
            }



            return Ok(total);

        }


    }
}
