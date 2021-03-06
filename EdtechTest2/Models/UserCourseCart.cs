using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class UserCourseCart
    {
        //this is the real model 
        public int id { get; set; }
        public ApplicationUser user { get; set; }
        public string userId { get; set; }


        public int courseId { get; set; }

        public Coupon Coupon { get; set; }

        public int? CouponId { get; set; }


        public bool isDone { get; set; }

    }
}
