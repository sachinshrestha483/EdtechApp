using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Areas.Identity.ViewModels
{
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }

        // so no need ToSend It Many times We Use This  we use view Bag Here....
        // public string UserId { get; set; }

        //

        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}
