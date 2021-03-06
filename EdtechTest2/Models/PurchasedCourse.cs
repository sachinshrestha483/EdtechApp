using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class PurchasedCourse
    {
        public int id { get; set; }
        public Course Course { get; set; }

        public int CourseId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public int price { get; set; }

        public Coupon coupon { get; set; }


        public int CouponId { get; set; }


        public DateTime broughtOndate { get; set; }


        public string paymentToken { get; set; }


        public bool isRefund { get; set; }

    }
}
