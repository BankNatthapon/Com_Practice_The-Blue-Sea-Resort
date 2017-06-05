using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace TheBlueSeaResort_v2.Filter
{
    public class AdminOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RouteValueDictionary Rd = new RouteValueDictionary { { "controller", "Admin" }, { "action", "index" } }; // ถ้าไม่เป็นไปตามเงื่อนไขก็ให้เข้าไปตามนี้
            if (filterContext.HttpContext.Session["Status"].ToString() != "admin")
            {
                filterContext.Result = new RedirectToRouteResult(Rd); // ให้ตามไปยัง Rd ที่ได้สร้างไว้
            }
            base.OnActionExecuting(filterContext);
        }
    }
}