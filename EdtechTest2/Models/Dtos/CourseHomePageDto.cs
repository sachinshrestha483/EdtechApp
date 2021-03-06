using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models.Dtos
{
    public class CourseHomePageDto
    {


        public int Id { get; set; }
        public string topicName { get; set; }
        public string subtitle { get; set; }
        public double price { get; set; }
        public string CourseLevel { get; set; }
        public string CourseImageLink { get; set; }
        public string CourseLanguage { get; set; }
        public DateTime CreatedOn { get; set; }


        public string authorName { get; set; }

    }
}
