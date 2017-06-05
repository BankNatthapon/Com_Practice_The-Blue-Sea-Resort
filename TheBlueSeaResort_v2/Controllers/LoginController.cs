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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                string pConstr, sql;
                pConstr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
                DataTable dt = new DataTable();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                sql = "select FirstName + ' ' + LastName as UserName,Status,AdminID,FirstName,LastLogin from AdminAccount  where AdminID = '" + collection["usr"] + "' and Password='" + collection["pwd"] + "'";
                com.CommandText = sql;
                SqlDataReader dr;
                SqlConnection con = new SqlConnection(pConstr);

                com.CommandTimeout = 60;
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0) // ไม่มี user แลพ password ที่ match กัน จะเด้งกลับไปหน้าเดิม
                {
                    return View("Index");
                }
                foreach (DataRow drw in dt.Rows)    // เอา username เข้าไปเก็บใน session
                {
                    Session["FirstName"] = drw["UserName"].ToString();
                    Session["Status"] = drw["Status"].ToString();
                    Session["AdminID"] = drw["AdminID"].ToString();
                    Session["LastLogin"] = drw["LastLogin"].ToString();
                    Session["RealFirstName"] = drw["FirstName"].ToString();
                }
                Session["usr"] = collection["usr"];
                return RedirectToAction("Index", "Admin"); // ถ้าทำสำเร็จแล้วให้ไปที่หน้า userlogin
            }
            catch
            {
                return View("Index");
            }

        }

        public ActionResult Logoff()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "update AdminAccount set LastLogin = CONVERT(VARCHAR(24),GETDATE(),121) where AdminID = '" + Session["AdminID"] + "' and FirstName = '" + Session["RealFirstName"] + "'";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}