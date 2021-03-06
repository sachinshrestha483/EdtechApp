using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Student.ViewModel
{
    public class CheckoutViewModel
    {
        public ApplicationUser User { get; set; }

        public int total { get; set; }


        public List<Course> courses { get; set; }



    }
}
