using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.User.ViewModels
{
    public class UserAccountSettingViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = " Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The New Password And Conformation Do Not Match ")]
        public string ConfirmNewPassword { get; set; }



    }
}
