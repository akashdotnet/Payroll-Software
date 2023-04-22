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
    [RoutePrefix("api/Master")]
    public class MasterController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;

        public MasterController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }

        [HttpGet]
        [Route("GetMaster")]
        public IHttpActionResult Get()
        {
            try
            {
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                DataSet dtGet = CommonBL.ExecuteData(spNames.GenMasterAll_Load, aList, false, false, true);
                DataTable dt = dtGet.Tables[0];
                DataTable dtm = CommonBL.Load_Employee();

                DataSet ds= CommonBL.ExecuteData(spNames.salary_template_Load);
                DataTable dt1 = ds.Tables[0];

                return Ok(new { Lst = dt , EmployeeList = dtm, Salarytemplate = dt1 });
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }
    }
}
