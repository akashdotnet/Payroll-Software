using APIapps.Models;
using ITHeart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DataType;
using System.Web.Http;

namespace Samples.Controller
{
    [RoutePrefix("api/PIS")]
    public class PISController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;

        public PISController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }
        [HttpPost]
        [Route("GetEmployment/{employeeId}")]
        public IHttpActionResult GetEmployment(int employeeId)
        {
            try
            {
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@employeeid", ArrayLists.ToArrayList("@employeeid", Convert.ToInt32(employeeId), SqlDbType.Int));                
                DataSet dtGet = CommonBL.ExecuteData(spNames.get_EmployeeEmployementDetail, aList, false, false, true);
                DataTable dt = dtGet.Tables[0];
                return Ok(new { GetEmployment = dt });
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }

        [HttpGet]
        [Route("GetAcademic")]
        public IHttpActionResult GetAcademic(List<string> lstFollowupDate)
        {
            try
            {
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@employeeid", ArrayLists.ToArrayList("@employeeid", Convert.ToInt32(lstFollowupDate[0]), SqlDbType.Int));
                DataSet dtGet = CommonBL.ExecuteData(spNames.get_EmployeeAcademicDetail, aList, false, false, true);
                DataTable dt = dtGet.Tables[0];
                return Ok(new { Lst = dt });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

          [HttpGet]
        [Route("GetFamily")]
        public IHttpActionResult GetFamily(List<string> lstFollowupDate)
        {
            try
            {
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@employeeid", ArrayLists.ToArrayList("@employeeid", Convert.ToInt32(lstFollowupDate[0]), SqlDbType.Int));
                DataSet dtGet = CommonBL.ExecuteData(spNames.get_EmployeeFamilyDetail, aList, false, false, true);
                DataTable dt = dtGet.Tables[0];
                return Ok(new { Lst = dt });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
