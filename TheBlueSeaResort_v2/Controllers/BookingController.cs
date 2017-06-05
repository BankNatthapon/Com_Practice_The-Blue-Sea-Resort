using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using TheBlueSeaResort_v2.Models;

namespace TheBlueSeaResort_v2.Controllers
{
    public class BookingController : Controller
    {
        string checkin,checkout;
        // GET: Booking
        public ActionResult Index()
        {
            
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "select DISTINCT Room.Type_Id,RoomType.* from Room INNER JOIN RoomType ON RoomType.Type_Id = Room.Type_Id  where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'" + Request.Form["checkin"] + "',103) and Bk_Check_Out <= CONVERT(datetime,'" + Request.Form["checkout"] +"',103))";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            TempData["checkin"] = Request.Form["checkin"].ToString();
            TempData["checkout"] = Request.Form["checkout"].ToString();
            var model = new List<Customer>();
            foreach (DataRow drw in dt.Rows)
            {

                var u = new Customer();
                u.Type_Id = drw["Type_ID"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.Price = drw["Type_Price"].ToString();
                u.Type_Detail = drw["Type_Detail"].ToString();
                u.Type_Pic = drw["Type_pic"].ToString();
                u.Room_Amount = drw["Room_Amount"].ToString();

                model.Add(u);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create()
        {
            string SerialCode = "";
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                int Number = rand.Next(0, 10);
                SerialCode = SerialCode + Number.ToString();
            }
            string name = Request.Form["fname"].ToString() + " " + Request.Form["lname"];
            // TODO: Add insert logic here
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            SqlDataReader dr, dr1,dr2; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            sql = "select TOP 1 Room_Id from Room where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'" + TempData["checkin"] + "',103) and Bk_Check_Out <= CONVERT(datetime,'" + TempData["checkout"] + "',103)) and Room_Status = 0 and Type_Id =" + Request.Form["Type_id"];
            com.CommandText = sql;
            dr = com.ExecuteReader();
            dt.Load(dr);

            foreach (DataRow drw in dt.Rows)
            {
                ViewBag.Room_Id = drw["Room_Id"].ToString();
            }
            if (ViewBag.Room_Id == null || ViewBag.Room_Id == "")
            {
                ViewBag.Alert = "This Room type not available.";
                return RedirectToAction("/Home/Index");
            }

            dt.Clear();
            con.Close();
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
            com.CommandType = CommandType.Text;
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            //insert Customer
            sql = " insert into Customer (Name,Tel,Email)";
            sql = sql + "values ('" + name + "','" + Request.Form["tel"].ToString() + "','" + Request.Form["email"].ToString() + "')";
            //insert Booking
            sql = sql + " insert into Booking (Bk_SerialCode,Bk_Check_In,Bk_Check_Out,Bk_Total_Room,Cus_id)";
            sql = sql + " values ('" + SerialCode + "',CONVERT(datetime,'" + TempData["checkin"] + "',103),CONVERT(datetime,'" + TempData["checkout"] + "', 103),1,(select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC))";
            //insert
            sql = sql + " Update Booking set Room_Id = " + ViewBag.Room_Id + " where Cus_Id = (select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC)";

            com.CommandText = sql;
            dr1 = com.ExecuteReader();
            dt1.Load(dr1);
            dt2.Clear();
            con.Close();

            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
            com.CommandType = CommandType.Text;
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            sql = " select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC";
            com.CommandText = sql;
            dr2 = com.ExecuteReader();
            dt2.Load(dr2);
            foreach (DataRow drw in dt2.Rows)
            {
                TempData["Cus_Id"] = drw["Cus_Id"].ToString();
            }
            dt2.Clear();
            con.Close();
            return RedirectToAction("Finish");

        }

        public ActionResult Finish()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "SELECT Booking.Bk_SerialCode,Customer.Cus_Id,Customer.Name,Customer.Tel as Phone,Customer.Email,CONVERT(varchar(11),Booking.Bk_Check_In,103) as CheckIn,CONVERT(varchar(11),Booking.Bk_Check_Out,103) as CheckOut,Booking.Bk_Night as Night,Booking.Bk_Total_Price as Price,Booking.Bk_Total_Room as TotalRoom,Room.Room_Number,RoomType.* FROM Customer";
            sql = sql + " INNER JOIN Booking ON Customer.Cus_Id = Booking.Cus_Id";
            sql = sql + " INNER JOIN Room ON Booking.Room_Id = Room.Room_Id";
            sql = sql + " INNER JOIN RoomType ON Room.Type_Id = RoomType.Type_Id where Customer.Cus_Id ="+ TempData["Cus_Id"];
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            var u = new Customer();
            foreach (DataRow drw in dt.Rows)
            {
                u.Cus_Id = drw["Cus_Id"].ToString();
                u.Name = drw["Name"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();
                u.Email = drw["Email"].ToString();
                u.Phone = drw["Phone"].ToString();
                u.CheckIn = drw["CheckIn"].ToString();
                u.CheckOut = drw["CheckOut"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.SerialCode = drw["Bk_SerialCode"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.Price = drw["Type_Price"].ToString();
                u.Type_Detail = drw["Type_Detail"].ToString();
                u.Type_Pic = drw["Type_pic"].ToString();
                u.Room_Amount = drw["Room_Amount"].ToString();
            }
            return View(u);
        }
        public ActionResult Provider()
        {
            return View();
        }

    }
}