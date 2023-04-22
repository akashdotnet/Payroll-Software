using API.Models;
using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class EmployeeController : Controller
    {
        public ITHeart.BL.GenMstBL CommonBL;

        BusinessLayer obj = new BusinessLayer();

        public ActionResult Employee()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult PropertyTypeInsert(PropertyTypeDetail model)
        //{
        //    string html = null;
        //    try
        //    {
        //        int a = obj.FunExecuteNonQuery("exec dbo.PropertyTypeDetail_Create '" + model.OrderNo + "','" + model.PropertyTypeName + "'");
        //        if (a > 0)
        //        {
        //            //return RedirectToAction("PropertyTypeGet");
        //            DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
        //            if (dt.Rows.Count > 0)
        //            {
        //                int[] columnHide = { 0 };
        //                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
        //                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                html = "<div class='alert alert-danger'>'" + Resources.Resource1.norecord + "'</div>";
        //                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
        //            }

        //        }
        //        else
        //        {
        //            html = Resources.Resource1.insertfailed;
        //            return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        html = "Insert Failed !!!";
        //        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult PropertyTypeUpdate(PropertyTypeDetail model)
        //{
        //    string html = null;
        //    try
        //    {
        //        int a = obj.FunExecuteNonQuery("exec dbo.PropertyTypeDetail_Update '" + model.PropertyTypeId + "','" + model.OrderNo + "','" + model.PropertyTypeName + "'");
        //        if (a > 0)
        //        {
        //            //return RedirectToAction("PropertyTypeGet");
        //            DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
        //            if (dt.Rows.Count > 0)
        //            {
        //                int[] columnHide = { 0 };
        //                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
        //                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                html = "<div class='alert alert-danger'>'" + Resources.Resource1.norecord + "'</div>";
        //                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
        //            }

        //        }
        //        else
        //        {
        //            html = Resources.Resource1.updatefailed;
        //            return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        html = Resources.Resource1.updatefailed;
        //        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult PropertyTypeDelete(PropertyTypeDetail model)
        //{
        //    string html = null;
        //    try
        //    {
        //        int a = obj.FunExecuteNonQuery("exec dbo.PropertyTypeDetail_Delete '" + model.PropertyTypeId + "'");
        //        if (a > 0)
        //        {
        //            //return RedirectToAction("PropertyTypeGet");
        //            DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
        //            if (dt.Rows.Count > 0)
        //            {
        //                int[] columnHide = { 0 };
        //                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
        //                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                html = "<div class='alert alert-danger'>'" + Resources.Resource1.norecord + "'</div>";
        //                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
        //            }

        //        }
        //        else
        //        {
        //            html = Resources.Resource1.deletefailed;
        //            return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        html = Resources.Resource1.deletefailed;
        //        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult EmployeeGet()
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>'no records'</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = "Fatch Result Failed !!!";
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
