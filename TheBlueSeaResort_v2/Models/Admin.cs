using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheBlueSeaResort_v2.Models
{
    public class Admin
    {
        [Required]
        public string AdminID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        [DataType(DataType.DateTime)]
        public string LastLogin { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}