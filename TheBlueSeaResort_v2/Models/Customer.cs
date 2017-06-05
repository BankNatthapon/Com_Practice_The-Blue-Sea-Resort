using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheBlueSeaResort_v2.Models
{
    public class Customer
    {
        public string Cus_Id { get; set; }
        public string SerialCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Night { get; set; }
        public string TotalPrice { get; set; }
        public string TotalRoom { get; set; }
        public string RoomNumber { get; set; }
        public string Email { get; set; }

        public string RoomType { get; set; }
        public string Bk_Id { get; set; }
        public string Room_Id { get; set; }

        public string Price { get; set; }
        public string Type_Id { get; set; }
        public string Type_Detail { get; set; }
        public string Room_Amount { get; set; }
        public string Type_Pic { get; set; }
        public string Type_Pic_Name { get; set; }
        public string Type_Pic_2 { get; set; }
        public string Type_Pic_Name_2 { get; set; }
    }

}