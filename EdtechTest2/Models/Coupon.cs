using EdtechTest2.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Coupon
    {
        public  int id{ get; set; }

        public Course Course { get; set; }


        public int CourseId { get; set; }

        public  string couponCode { get; set; }


        public int  numberOfcouponAlloted { get; set; }


        public int numberofcouponUnUsed { get; set; }




        public DateTime validTill { get; set; }


        public DiscountType discountMethod { get; set; }



        public int discountValue { get; set; }

        public DateTime coupounCreatedOn { get; set; }



        public bool isCouponBlocked { get; set; }

    }
}
