using DataAnnotationsExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Identity.ViewModels
{
    public class RegisterViewModel
    {

       


        [Required]
        [EmailAddress]
       [Remote(action: "IsEmailInUse", controller: "Register", areaName: "Identity")]

        public string Email { get; set; }
      
        [Display(Name = "First Name")]
        [Required]
        public string  FirstName { get; set; }
        
        
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]

        public DateTime DateofBirth { get; set; }

        [Required]
        [Phone]
        public string MobileNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password And Confirm Password Do Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
