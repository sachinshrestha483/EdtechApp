using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Identity.ViewModels
{
    public class UserHomepageViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string MobileNo { get; set; }
        [Required]

        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string FacebookName { get; set; }
        public string LinkedInName { get; set; }
        public string YoutubeChannelName { get; set; }

        public string TwitterName { get; set; }

        [Url]
        public string Website { get; set; }
        [Display(Name ="Unique User Name")]
        public string uniqueUserName { get; set; }


    }
}
