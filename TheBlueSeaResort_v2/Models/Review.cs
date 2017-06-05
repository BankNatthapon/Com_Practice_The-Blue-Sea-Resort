using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheBlueSeaResort_v2.Models
{
    public class Review
    {
        public string C_Cus_Id { get; set; }
        public string C_Name { get; set; }

        public string B_SerialCode { get; set; }

        public string R_Cus_Id { get; set; }
        public string R_SerialCode { get; set; }
        public string R_Id { get; set; }
        public string R_Message { get; set; }
        public string R_Rating { get; set; }
        public string R_Time { get; set; }
        public string R_d { get; set; }
        public string R_h { get; set; }
        public string R_m { get; set; }
    }
}