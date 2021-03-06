using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class CourseTransictions
    {

        public int id { get; set; }
       
        public string paymentToken { get; set; }


        public bool isRefund { get; set; }

        public string refundReason { get; set; }


        public DateTime refundDate { get; set; }

        public DateTime purchaseDate { get; set; }

        public  int price { get; set; }

        // this can create a problem
        public Coupon  Coupon { get; set; }

        public int? CouponId { get; set; }

        // 

        public Course Course { get; set; }
        public int CourseId { get; set; }


        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }




    }
}
