using EdtechTest2.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Admin.ViewModels
{
    public class PendingCourseviewModel
    {
        public int coursesId { get; set; }

        public int approved { get; set; }


        public int rejected { get; set; }


        public string  reasonsForRejection { get; set; }


    }
}
