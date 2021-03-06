using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime JoinedDate { get; set; }

        public DateTime DateofBirth { get; set; }
        public string MobileNo { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }
        public int? ProfilePhotoId { get; set; }

      //  public string ProfilePhotoLink { get; set; }

        public bool IsInstructor { get; set; }
        public string Heading { get; set; }

        public string Description { get; set; }

        public string FacebookName { get; set; }
        public string LinkedinName { get; set; }

        public string TwitterName { get; set; }

        public string YoutubeChannelName { get; set; }

        public string WebsiteLink { get; set; }




    }
}
