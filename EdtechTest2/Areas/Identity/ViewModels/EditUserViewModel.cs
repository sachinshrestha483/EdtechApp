using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Identity.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {

            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateofBirth { get; set; }
        public string JoinedOn { get; set; }

        public string PhoneNumber { get; set; }

        [Display(Name ="Unique User Name")]
        public string UniqueUserNameId { get; set; }




        [Required]
        [EmailAddress]
        public string Email { get; set; }



        public IList<string> Roles { get; set; }
    }
}
