using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TheBlueSeaResort_v2.Models;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TheBlueSeaResort_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Gallery()
        {
            var imagesModel = new ImageGallery();
            var imagesFiles = Directory.GetFiles(Server.MapPath("~/Content/Images/Gallery/"));
            foreach (var item in imagesFiles)
            {
                imagesModel.ImageList.Add(Path.GetFileName(item));
            }
            return View(imagesModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Room()
        {
            ViewBag.Message = "Your contact page.";

            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            sql = "select * from RoomType";
            com.CommandText = sql;
            SqlDataReader dr;
            SqlConnection con = new SqlConnection(pConStr);
            //   com.CommandTimeout = 60;
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            var model = new List<Customer>();
            foreach (DataRow drw in dt.Rows)
            {
                var u = new Customer();
                u.Type_Id = drw["Type_ID"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.Price = drw["Type_Price"].ToString();
                u.Type_Detail = drw["Type_Detail"].ToString();
                u.Type_Pic = drw["Type_pic"].ToString();
                u.Type_Pic_2 = drw["Type_pic_2"].ToString();

                u.Room_Amount = drw["Room_Amount"].ToString();


                model.Add(u);
            }

            return View(model);
        }
        public ActionResult Restaurant()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}