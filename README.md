# The Blue Sea Resort
This project is Computer Practice subjects and Credit by www.tubkaakresort.com, www.captainhookresort.com Thank you for picture and content.

### Code Simple
This Code is `Admin Management` You can go to admin controller by `/admin`

```
public ActionResult Index()
   {
     ViewBag.Alert = null;
     string pConStr, sql;     
            pConStr=WebConfigurationManager.ConnectionStrings["DB"].ToString();
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            sql = "SELECT Booking.Bk_SerialCode,Customer.Cus_Id,Customer.Name,Customer.Tel as Phone,Customer.Email,CONVERT(varchar(11),Booking.Bk_Check_In,103) as CheckIn,CONVERT(varchar(11),Booking.Bk_Check_Out,103) as CheckOut,Booking.Bk_Night as Night,Booking.Bk_Total_Price as Price,Booking.Bk_Total_Room as TotalRoom,RoomType.Type_Name,Room.Room_Number FROM Customer";
            sql = sql + " INNER JOIN Booking ON Customer.Cus_Id = Booking.Cus_Id";
            sql = sql + " INNER JOIN Room ON Booking.Room_Id = Room.Room_Id";
            sql = sql + " INNER JOIN RoomType ON Room.Type_Id = RoomType.Type_Id";
            com.CommandText = sql;
            SqlDataReader dr;
            SqlConnection con = new SqlConnection(pConStr);
            com.CommandTimeout = 60;
            com.Connection = con;
            con.Open();
            dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            var model = new List<Customer>();
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
                model.Add(u);
            }
            return View(model);
        }
```
