using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class CourseCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}
