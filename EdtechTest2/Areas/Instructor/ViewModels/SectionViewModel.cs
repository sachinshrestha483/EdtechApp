using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Instructor.ViewModels
{
    public class SectionViewModel
    {

        [Required]
        [Display(Name ="Section Name")]
        public string  SectionName{ get; set; }



       
        public int SectionId { get; set; }


        public List<Lecture>Lectures { get; set; }
    }
}
