using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class CourseCart
    {
        // this is wrong model ......

        public int id { get; set; }
        public ApplicationUser userId { get; set; }

        public int courseId { get; set; }

        public Coupon Coupon { get; set; }

        public int? CouponId { get; set; }







    }
}
