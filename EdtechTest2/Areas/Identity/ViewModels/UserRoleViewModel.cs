using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Identity.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        //  public string RoleId { get; set; }
        // To Not Dulicate The Role Id We Use View Bag For This


        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}
