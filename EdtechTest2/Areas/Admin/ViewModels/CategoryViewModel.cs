using EdtechTest2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; }


        public List<Category> Categories { get; set; }
    }
}
