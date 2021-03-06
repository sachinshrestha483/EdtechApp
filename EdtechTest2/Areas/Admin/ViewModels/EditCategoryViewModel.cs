using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Admin.ViewModels
{
    public class EditCategoryViewModel
    {

        [Required]
        public int id { get; set; }
        [Required]

        public string Name { get; set; }

    }
}
