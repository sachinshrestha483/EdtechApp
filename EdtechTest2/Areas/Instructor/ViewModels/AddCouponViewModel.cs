using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class AddCouponViewModel
    {
        public int courseId { get; set; }
        public string couponCode { get; set; }


        public DateTime validfrom { get; set; }


        public DateTime validtill { get; set; }



        public int discountType { get; set; }


        public int  discountValue { get; set; }


        public int couponAlloted { get; set; }








    }
}
