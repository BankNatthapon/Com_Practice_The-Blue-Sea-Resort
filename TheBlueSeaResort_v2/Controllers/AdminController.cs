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
    public class AdminController : Controller
    {
        // GET: Admin
        [SystemAuthen]
        public ActionResult Index()
        {
            ViewBag.Alert = null;
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "SELECT Booking.Bk_SerialCode,Customer.Cus_Id,Customer.Name,Customer.Tel as Phone,Customer.Email,CONVERT(varchar(11),Booking.Bk_Check_In,103) as CheckIn,CONVERT(varchar(11),Booking.Bk_Check_Out,103) as CheckOut,Booking.Bk_Night as Night,Booking.Bk_Total_Price as Price,Booking.Bk_Total_Room as TotalRoom,RoomType.Type_Name,Room.Room_Number FROM Customer";
            sql = sql + " INNER JOIN Booking ON Customer.Cus_Id = Booking.Cus_Id";
            sql = sql + " INNER JOIN Room ON Booking.Room_Id = Room.Room_Id";
            sql = sql + " INNER JOIN RoomType ON Room.Type_Id = RoomType.Type_Id";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            var model = new List<Customer>(); // สร้าง model ให้รับเป็น list
            foreach (DataRow drw in dt.Rows)
            {
                var u = new Customer();
                u.Cus_Id = drw["Cus_Id"].ToString();
                u.Name = drw["Name"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();
                u.Email = drw["Email"].ToString();
                u.Phone = drw["Phone"].ToString();
                u.CheckIn = drw["CheckIn"].ToString();
                u.CheckOut = drw["CheckOut"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.SerialCode = drw["Bk_SerialCode"].ToString();
                model.Add(u); // ยัดข้อมูลของ u เข้าไปใน model เรื่ยๆ จนกลายเป็น list
            }
            return View(model);
        }

        // GET: Admin/Details/5
        [SystemAuthen]
        public ActionResult Details(string id)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "SELECT Booking.Bk_SerialCode,Customer.Cus_Id,Customer.Name,Customer.Tel as Phone,Customer.Email,CONVERT(varchar(11),Booking.Bk_Check_In,103) as CheckIn,CONVERT(varchar(11),Booking.Bk_Check_Out,103) as CheckOut,Booking.Bk_Night as Night,Booking.Bk_Total_Price as Price,Booking.Bk_Total_Room as TotalRoom,RoomType.Type_Name,Room.Room_Number FROM Customer";
            sql = sql + " INNER JOIN Booking ON Customer.Cus_Id = Booking.Cus_Id";
            sql = sql + " INNER JOIN Room ON Booking.Room_Id = Room.Room_Id";
            sql = sql + " INNER JOIN RoomType ON Room.Type_Id = RoomType.Type_Id where Customer.Cus_Id = "+"'"+id+"'";
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
                u.SerialCode = drw["Bk_SerialCode"].ToString();
                u.Name = drw["Name"].ToString();
                u.Email = drw["Email"].ToString();
                u.Phone = drw["Phone"].ToString();
                u.CheckIn = drw["CheckIn"].ToString();
                u.CheckOut = drw["CheckOut"].ToString();
                u.TotalPrice = drw["Price"].ToString();
                u.TotalRoom = drw["TotalRoom"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();
                
            }
            return View(u);
        }

        // GET: Admin/Create
        [SystemAuthen]
        public ActionResult Create(string id)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "SELECT * From RoomType";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            ViewBag.id = id;
            var model = new List<Customer>(); 
            foreach (DataRow drw in dt.Rows)
            {
                var u = new Customer();
                u.RoomType = drw["Type_Name"].ToString();
                u.Type_Id = drw["Type_Id"].ToString();
                model.Add(u);
            }
            return View(model);
        }

        // POST: Admin/Create
        [SystemAuthen]
        [HttpPost]
        public ActionResult Create()
        {
            string SerialCode = "";
            Random rand = new Random();
            for (int i = 0; i < 6; i++) {
                int Number = rand.Next(0, 10);
                SerialCode = SerialCode + Number.ToString();
            }
            // TODO: Add insert logic here
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
            sql = "select TOP 1 Room_Id from Room where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'" + Request.Form["checkin"] + "',103) and Bk_Check_Out <= CONVERT(datetime,'" + Request.Form["checkout"] + "',103)) and Room_Status = 0 and Type_Id =" + Request.Form["RoomType"];
            com.CommandText = sql;
            dr = com.ExecuteReader();
            dt.Load(dr);

            foreach (DataRow drw in dt.Rows) { 
                ViewBag.Room_Id = drw["Room_Id"].ToString();
            }
            if (ViewBag.Room_Id == null || ViewBag.Room_Id == "")
            {
                ViewBag.Alert = "This Room type not available.";
                return RedirectToAction("Create");
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
            sql = sql + "values ('" + Request.Form["name"].ToString() + "','" + Request.Form["phone"].ToString() + "','" + Request.Form["email"].ToString() + "')";
            //insert Booking
            sql = sql + " insert into Booking (Bk_SerialCode,Bk_Check_In,Bk_Check_Out,Bk_Total_Room,Cus_id)";
            sql = sql + " values ('" + SerialCode + "',CONVERT(datetime,'" + Request.Form["checkin"].ToString() + "',103),CONVERT(datetime,'" + Request.Form["checkout"].ToString() + "', 103)," + Request.Form["totalroom"].ToString() + ",(select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC))";
            //insert
            sql = sql + " Update Booking set Room_Id = "+ ViewBag.Room_Id + " where Cus_Id = (select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC)";

            com.CommandText = sql;
            dr1 = com.ExecuteReader();
            dt1.Load(dr1);
            dt.Clear();
            con.Close();
            return RedirectToAction("Index");
          
        }

        // POST: Admin/Edit/5
        [SystemAuthen]
        [HttpPost]
        public ActionResult Edit(string cusid, string name, string scode, string email, string phone, string ckin, string ckout)
        {
            try
            {
                // TODO: Add update logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = "update Customer set Name = '" + name + "',Email='" + email + "',Tel='" + phone + "' where Cus_ID = " + cusid;
                sql = sql + " update Booking set Bk_Serialcode='" + scode + "',Bk_Check_In= CONVERT(datetime,'" + ckin + "',103),Bk_Check_Out=CONVERT(datetime,'" + ckout + "',103) where Cus_Id = " + cusid;
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // POST: Admin/Delete/5
        [HttpPost]
        [SystemAuthen]
        public ActionResult Delete(string cusid3)
        {
            try
            {
                // TODO: Add delete logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = " delete from Booking where Cus_Id=" + cusid3;
                sql = sql + "delete from Customer where Cus_Id =" + cusid3;
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // ==================================================================
        // ==================== Admin Management ============================
        // ==================================================================
        [SystemAuthen]
        [AdminOnly]
        public ActionResult AdminIndex()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from AdminAccount ";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            var model = new List<Admin>(); // สร้าง model ให้รับเป็น list
            foreach (DataRow drw in dt.Rows)
            {
                var u = new Admin();
                u.AdminID = drw["AdminID"].ToString();
                u.Password = drw["Password"].ToString();
                u.FirstName = drw["FirstName"].ToString();
                u.LastName = drw["LastName"].ToString();
                u.LastLogin = drw["LastLogin"].ToString();
                u.Status = drw["Status"].ToString();
                u.Email = drw["Email"].ToString();

                model.Add(u); // ยัดข้อมูลของ u เข้าไปใน model เรื่ยๆ จนกลายเป็น list
            }
            return View(model);
        }
        // GET: Admin/Create
        [SystemAuthen]
        [AdminOnly]
        public ActionResult AdminCreate()
        {
            return View();
        }

        // POST: Admin/Create
        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult AdminCreate(FormCollection c)
        {
            try
            {
                // TODO: Add insert logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = " insert into AdminAccount (AdminID,[Password],FirstName,LastName,Email,Status)";
                sql = sql + "values ('" + c["AdminID"] + "','" + c["Password"] + "','" + c["FirstName"] + "','" + c["LastName"] + "','" + c["Email"] + "','"+ c["Status"] +"')";
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("AdminIndex");
            }
            catch
            {
                return View();
            }
        }


        // POST: Admin/Edit/5
        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult AdminEdit(string id,string firstname,string lastname,string email,string password,string status)
        {
            try
            {
                // TODO: Add update logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = "update AdminAccount set FirstName = " + "'" + firstname + "'" + ", LastName = " + "'" + lastname + "'" + ", Status= " + "'" + status + "'" + ", Email = " + "'" + email + "', Password ='"+password+"' where AdminID= "+"'" + id+"'";
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("AdminIndex");
            }
            catch
            {
                return View();
            }
        }

        

        // POST: Admin/Delete/5
        [HttpPost]
        [AdminOnly]
        [SystemAuthen]
        public ActionResult AdminDelete(string id1)
        {
            try
            {
                // TODO: Add delete logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = "delete from AdminAccount where AdminID = '" + id1 + "'";
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("AdminIndex");
            }
            catch
            {
                return View();
            }
        }       
        // ====================================================================

        // ====================================================================
        // ===================== Room Type Management =========================
        // ====================================================================
        [SystemAuthen]
        [AdminOnly]
        public ActionResult RoomTypeIndex()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from RoomType ";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            var model = new List<Customer>(); // สร้าง model ให้รับเป็น list
            foreach (DataRow drw in dt.Rows)
            {
                var u = new Customer();
                u.Type_Id = drw["Type_Id"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.Price = drw["Type_Price"].ToString();
                u.Room_Amount = drw["Room_Amount"].ToString();

                model.Add(u); // ยัดข้อมูลของ u เข้าไปใน model เรื่ยๆ จนกลายเป็น list
            }
            return View(model);
        }

        [SystemAuthen]
        [AdminOnly]
        public ActionResult DeleteRoomType(string id)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from RoomType where Type_ID = '" + id + "'";
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
                u.Type_Id = drw["Type_ID"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.Price = drw["Type_Price"].ToString();
                u.Type_Pic = drw["Type_Pic"].ToString();
                u.Type_Detail = drw["Type_Detail"].ToString();
                u.Room_Amount = drw["Room_Amount"].ToString();
            }
            return View(u);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [AdminOnly]
        [SystemAuthen]
        public ActionResult DeleteRoomType(string id, FormCollection c)
        {
            try
            {
                // TODO: Add delete logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = "delete from Room where Type_Id = " + c["Type_Id"] + "";
                sql = sql + " delete from RoomType where Type_Id = " + c["Type_Id"] + "";
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("RoomTypeIndex");
            }
            catch
            {
                return View();
            }
        }
        // GET: Admin/Create
        [SystemAuthen]
        [AdminOnly]
        public ActionResult CreateRoomType()
        {
            return View();
        }

        // POST: Admin/Create
        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult CreateRoomType(FormCollection c, HttpPostedFileBase file, HttpPostedFileBase file2)
        {
            try
            {
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
                DataTable dt = new DataTable();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;

                string pic = System.IO.Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("/Content/Images/Room/" + pic));

                string pic2 = System.IO.Path.GetFileName(file2.FileName);
                file2.SaveAs(Server.MapPath("/Content/Images/Room/" + pic2));

                sql = "insert into RoomType ([Type_Name],[Type_Price],[Type_Detail],[Room_Amount],[Type_Pic],[Type_Pic_Name],[Type_Pic_2],[Type_Pic_Name_2])";
                sql = sql + " values('" + c["RoomType"] + "', " + c["Price"] + ", '" + c["Type_Detail"];
                sql = sql + "',0" + ",  '/Content/Images/Room/" + pic + "','"+pic+ "','/Content/Images/Room/"+pic2+"','"+ pic2 + "')";

                com.CommandText = sql;
                SqlDataReader dr;
                SqlConnection con = new SqlConnection(pConStr);
                //com.CommandTimeout = 60;
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("RoomTypeIndex");
            }
            catch
            {
                return View();
            }
        }

        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult SearchRoomType()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from RoomType where Type_Name like " + "'%" + Request.Form["RoomType"].ToString()+ "%'";
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
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
                u.Room_Amount = drw["Room_Amount"].ToString();

                model.Add(u);

            }
            return View(model);
        }
        [SystemAuthen]
        [AdminOnly]
        public ActionResult RoomTypeEdit(string id)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from RoomType where Type_Id = " + id;
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
                u.Type_Id = drw["Type_ID"].ToString();
                u.RoomType = drw["Type_Name"].ToString();
                u.Price = drw["Type_Price"].ToString();
                u.Type_Pic = drw["Type_Pic"].ToString();
                ViewBag.pic = drw["Type_Pic_Name"].ToString();
                u.Type_Detail = drw["Type_Detail"].ToString();
                u.Room_Amount = drw["Room_Amount"].ToString();
            }
            return View(u);
        }

        // POST: Admin/Edit/5
        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult RoomTypeEdit(string id, FormCollection c, HttpPostedFileBase file, HttpPostedFileBase file2)
        {
            try
            {
                if (file == null) {
                    // TODO: Add update logic here
                    string pConStr, sql;
                    pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                    DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                    SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                    com.CommandType = CommandType.Text;
                    sql = "update RoomType set Type_Name = " + "'" + c["RoomType"] + "'" + ", Type_Price = " + "" + c["Price"] + "" + ", Type_Detail = " + "'" + c["Type_Detail"] + "' where Type_Id = " + id;
                    com.CommandText = sql;
                    SqlDataReader dr; // เตรียมรับผลจาก Data
                    SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                    com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                    com.Connection = con;
                    con.Open();
                    dr = com.ExecuteReader();
                    dt.Load(dr);
                    con.Close();
                }
               else if(file != null && file2 == null)
                {
                    string pConStr, sql;
                    pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                    DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                    SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                    com.CommandType = CommandType.Text;

                    string pic = System.IO.Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("/Content/Images/Room/" + pic));

                    sql = "update RoomType set Type_Name = " + "'" + c["RoomType"] + "'" + ", Type_Price = " + "" + c["Price"] + "" + ", Type_Detail = " + "'" + c["Type_Detail"] + "',Type_Pic = '/Content/Images/Room/" + pic+"' where Type_Id = " + id;
                    com.CommandText = sql;
                    SqlDataReader dr; // เตรียมรับผลจาก Data
                    SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                    com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                    com.Connection = con;
                    con.Open();
                    dr = com.ExecuteReader();
                    dt.Load(dr);
                    con.Close();
                }
                else
                {
                    string pConStr, sql;
                    pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                    DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                    SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                    com.CommandType = CommandType.Text;

                    string pic = System.IO.Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("/Content/Images/Room/" + pic));

                    string pic2 = System.IO.Path.GetFileName(file2.FileName);
                    file2.SaveAs(Server.MapPath("/Content/Images/Room/" + pic2));

                    sql = "update RoomType set Type_Name = " + "'" + c["RoomType"] + "'" + ", Type_Price = " + "" + c["Price"] + "" + ", Type_Detail = " + "'" + c["Type_Detail"] + "',Type_Pic = '/Content/Images/Room/" + pic + "',Type_Pic_Name = '"+pic+"', Type_Pic_2 = '/Content/Images/Room/"+pic2+"',Type_Pic_Name_2 = '"+pic2+"' where Type_Id = " + id;
                    com.CommandText = sql;
                    SqlDataReader dr; // เตรียมรับผลจาก Data
                    SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                    com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                    com.Connection = con;
                    con.Open();
                    dr = com.ExecuteReader();
                    dt.Load(dr);
                    con.Close();
                }
                return RedirectToAction("RoomTypeIndex");
            }
            catch
            {
                return View();
            }
        }
        // ====================================================================

        // ====================================================================
        // ========================== Room Management =========================
        // ====================================================================
        [SystemAuthen]
        [AdminOnly]
        public ActionResult RoomManagement(String id)
        {
            Session["id"] = id;
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "  select RoomType.Type_Name, Room.* from RoomType INNER JOIN Room ON RoomType.Type_ID = Room.Type_ID where Room.Type_ID=" + Session["id"];
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
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
                u.Room_Id = drw["Room_ID"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();

                model.Add(u);

            }

            return View(model);
        }
        [SystemAuthen]
        [AdminOnly]
        public ActionResult RoomManagement_2()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "  select RoomType.Type_Name, Room.* from RoomType INNER JOIN Room ON RoomType.Type_ID = Room.Type_ID where Room.Type_ID=" + Session["id"];
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
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
                u.Room_Id = drw["Room_ID"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();

                model.Add(u);

            }

            return View(model);
        }

        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult CreateRoom(FormCollection c)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString();
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;



            sql = "insert into Room ([Room_Number],[Room_Status],[Type_Id])";
            sql = sql + " values('" + Request.Form["RoomName"].ToString() + "', 0," + Session["id"] + ")";
            sql = sql + " Update RoomType set Room_Amount = (select COUNT(Room_Id) from Room where Type_Id = " + Session["id"] + ") where Type_Id = " + Session["id"] + "";

            com.CommandText = sql;
            SqlDataReader dr;
            SqlConnection con = new SqlConnection(pConStr);
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

                return RedirectToAction("RoomManagement_2");
            
        }

        [SystemAuthen]
        [AdminOnly]
        public ActionResult SearchRoom()
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = "  select RoomType.Type_Name, Room.* from RoomType INNER JOIN Room ON RoomType.Type_ID = Room.Type_ID where Room.Room_Number='" + Request.Form["RoomName"].ToString()+"' and Room.Type_Id = " + Session["id"];
            com.CommandText = sql;
            SqlDataReader dr; // เตรียมรับผลจาก Data
            SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
            com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
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
                u.Room_Id = drw["Room_ID"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();

                model.Add(u);

            }

            return View(model);
        }
        [SystemAuthen]
        [AdminOnly]
        public ActionResult DeleteRoom(string id)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from Room where Room_ID = '" + id + "'";
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
                u.Room_Id = drw["Room_ID"].ToString();
                u.RoomType = drw["Type_Id"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();
            }
            return View(u);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [AdminOnly]
        [SystemAuthen]
        public ActionResult DeleteRoom(string id, FormCollection c)
        {
            try
            {
                // TODO: Add delete logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = "delete from Room where Room_id = '" + id + "' and Type_Id = " + Session["id"];
                sql = sql + " Update RoomType set Room_Amount = (select COUNT(Room_Id) from Room where Type_Id = " + Session["id"] + ") where Type_Id = " + Session["id"] + "";
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("RoomManagement_2");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        [SystemAuthen]
        [AdminOnly]
        public ActionResult RoomEdit(string id)
        {
            string pConStr, sql;
            pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
            DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
            SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
            com.CommandType = CommandType.Text;
            sql = " select * from Room where Room_Id = " + id + " and Type_Id = " + Session["id"];
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
                u.Room_Id = drw["Room_ID"].ToString();
                u.RoomType = drw["Type_Id"].ToString();
                u.RoomNumber = drw["Room_Number"].ToString();
            }
            return View(u);
        }

        // POST: Admin/Edit/5
        [SystemAuthen]
        [AdminOnly]
        [HttpPost]
        public ActionResult RoomEdit(string id, FormCollection c)
        {
            try
            {
                // TODO: Add update logic here
                string pConStr, sql;
                pConStr = WebConfigurationManager.ConnectionStrings["DB"].ToString(); // การเชื่อมต่อ database
                DataTable dt = new DataTable();// เอาคลาส table มาประกาศเป็นชื่อ dt
                SqlCommand com = new SqlCommand();// เอาไว้ยัด sqlcommand ไว้ในนี้
                com.CommandType = CommandType.Text;
                sql = "update Room set Room_Number = " + "'" + c["RoomNumber"] + "' where Room_Id = " + id + " and Type_Id = " + Session["id"];
                com.CommandText = sql;
                SqlDataReader dr; // เตรียมรับผลจาก Data
                SqlConnection con = new SqlConnection(pConStr); // เตรียมเชื่อมต่อ
                com.CommandTimeout = 60; // กำหนดเวลาที่จะ timeout นั่นคือ 60
                com.Connection = con;
                con.Open();
                dr = com.ExecuteReader();
                dt.Load(dr);
                con.Close();

                return RedirectToAction("RoomManagement_2");
            }
            catch
            {
                return View();
            }
        }
        // ====================================================================

    }
}
