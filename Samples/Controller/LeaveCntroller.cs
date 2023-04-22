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
    [RoutePrefix("api/Leave")]
    public class LeaveController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;
        public LeaveController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }

        [HttpGet]
        [Route("GetEmployeeList")]
        public IHttpActionResult Get()
        {
            try
            {
                DataTable dtGet = CommonBL.Load_Employee();
                //DataTable dt = dtGet.Tables[0];
                return Ok(new { EmployeeList = dtGet });
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }
    }
}
