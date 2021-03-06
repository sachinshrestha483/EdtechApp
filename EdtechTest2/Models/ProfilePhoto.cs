using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class ProfilePhoto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string PhotoLink { get; set; }

        public string  PhotoName { get; set; }
    }
}
