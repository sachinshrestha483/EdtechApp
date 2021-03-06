using EdtechTest2.Models;
using EdtechTest2.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class LectureWithContentViewModel
    {

        
        public int Id { get; set; }
        public string LectureName { get; set; }

        public int contentType { get; set; }
        public string Video { get; set; }

        public double VideoLength { get; set; }
        public string Article { get; set; }

        public string Description { get; set; }


        public List<DownlodableContentViewModel> Downlodablecontents { get; set; }




    }
}
