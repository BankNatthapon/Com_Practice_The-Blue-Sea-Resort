using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using TheBlueSeaResort_v2.Models;
using TheBlueSeaResort_v2.Filter;

namespace TheBlueSeaResort_v2.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "Select *,CONVERT(varchar(20),Time,106) as timemm from Review";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
      
            var model = new List<Review>();
            foreach (DataRow drw in dt.Rows)
            {

                var u = new Review();
                u.C_Name = drw["Name"].ToString();
                u.R_Message = drw["Message"].ToString();
                u.R_Rating = drw["Rating"].ToString();
                u.R_Time = drw["timemm"].ToString();

                model.Add(u);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMessage()
        {
            try
            {
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                DataTable dt1 = new DataTable();
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                SqlDataReader dr, dr1; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                sql = "select * from Booking where Bk_SerialCode ='"+Request.Form["serial"]+"'";
                com.CommandText = sql;
                dr = com.ExecuteReader();
                dt.Load(dr);

                foreach (DataRow drw in dt.Rows)
                {
                    ViewBag.Cus_Id = drw["Cus_Id"].ToString();
                }
                if (ViewBag.Cus_Id == null || ViewBag.Cus_Id == "")
                {
                    ViewBag.Alert = "This Room type not available.";
                    return RedirectToAction("Index");
                }

                dt.Clear();
                con.Close();
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
                com.CommandType = CommandType.Text;
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                //insert Customer
                sql = " insert into Review (Cus_Id,Rating,Message,SerialCode,Name,Time)";
                sql = sql + "values (" + ViewBag.Cus_Id + ",'" + Request.Form["input-2"].ToString() + "','" + Request.Form["message"].ToString() + "','"+ Request.Form["serial"].ToString() + "','" + Request.Form["name"].ToString() + "',(Select CONVERT(varchar(20),GETDATE(),106)))";
                com.CommandText = sql;
                dr1 = com.ExecuteReader();
                dt1.Load(dr1);
                dt.Clear();
                con.Close();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

    }
}