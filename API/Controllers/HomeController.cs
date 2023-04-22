using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        public ITHeart.BL.GenMstBL CommonBL;

        [HttpPost]
        public ActionResult EmployeeCreate()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
            string html = null;
            string id = null;
            try
            {
                //if (Session["userName"] == null)
                //{
                //    return Redirect("~/Home/Login");
                //}

                
                return Json(new { Flag = flag, Html = htmlTable.ToString(), id = model.EmployeeId }, JsonRequestBehavior.AllowGet);
                //}
                else
                {
                    return Json(new { Flag = 2, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
