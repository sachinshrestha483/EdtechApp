using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Default
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string Link { get; set; }

        public string ContentString { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
