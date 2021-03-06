using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Identity.ViewModels
{
    public class ValidateEmailAddressViewModel
    {

        [Required]
        [Email]
        public string Email { get; set; }
    }
}
