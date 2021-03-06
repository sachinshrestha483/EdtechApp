using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class AddSectionViewModel
    {
        [Required]
        public string sectionName { get; set; }
        [Required]

        public int courseId { get; set; }


    }
}
