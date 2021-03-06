using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class RefundedCourse
    {
        public int id { get; set; }


        public DateTime refundgiven { get; set; }


        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }


        public string refundReason { get; set; }


        public string refundToken { get; set; }



    }
}
