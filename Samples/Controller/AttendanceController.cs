using APIapps.Models;
using ITHeart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DataType;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Samples.Controller
{
    public class AttendanceController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;

        public AttendanceController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }
       [HttpPost()]
        public IHttpActionResult Post(List<string> lstData)
        {
            try
            {
                DataTable dt = new DataTable();
                List<employeeDetails> dataList = new List<employeeDetails>();
                DataSet dtGet = new DataSet();
                StringBuilder htmlTable = new StringBuilder();
                if (!(Convert.ToBoolean(lstData[3])))
                {
                    Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                    aList.Add("@STARTDATE", ArrayLists.ToArrayList("@STARTDATE", lstData[1], SqlDbType.Int));
                    aList.Add("@ENDDATE", ArrayLists.ToArrayList("@ENDDATE", lstData[2], SqlDbType.Int));
                    aList.Add("@EmpID", ArrayLists.ToArrayList("@EmpID", Convert.ToInt32(lstData[0]), SqlDbType.Int));
                    dtGet = CommonBL.ExecuteData(spNames.Load_att_by_date, aList, false, false, true);
                    dt = dtGet.Tables[0];
                    return Ok(new { Flag = 0, List = dt, Html = htmlTable.ToString(), Count = dt.Rows.Count, TList = dt, dataList = dataList });

                }
                else
                {
                    if (Convert.ToInt32(lstData[11]) == 0)
                    {
                        int[] columnHide = { 0, 1 };
                        Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                        aList.Add("@pay_month", ArrayLists.ToArrayList("@pay_month", Convert.ToInt32(lstData[9]), SqlDbType.Int));
                        aList.Add("@pay_year", ArrayLists.ToArrayList("@pay_year", Convert.ToInt32(lstData[10]), SqlDbType.Int));
                        aList.Add("@employee_id", ArrayLists.ToArrayList("@employee_id", lstData[0], SqlDbType.Int));
                        aList.Add("@department_id", ArrayLists.ToArrayList("@department_id", lstData[5], SqlDbType.VarChar));
                        aList.Add("@designation_id", ArrayLists.ToArrayList("@designation_id", lstData[2], SqlDbType.VarChar));
                        aList.Add("@entry_lock", ArrayLists.ToArrayList("@entry_lock", lstData[12].ToString() == "AttendanceApproval" ? 1 : Convert.ToInt32(lstData[11]), SqlDbType.Int));
                        aList.Add("@location_id", ArrayLists.ToArrayList("@location_id", lstData[4], SqlDbType.VarChar));
                        aList.Add("@division_id", ArrayLists.ToArrayList("@division_id", lstData[8], SqlDbType.VarChar));
                        aList.Add("@emp_type_id", ArrayLists.ToArrayList("@emp_type_id", lstData[1], SqlDbType.VarChar));
                        aList.Add("@shift_id", ArrayLists.ToArrayList("@shift_id", lstData[7], SqlDbType.VarChar));
                        dtGet = CommonBL.ExecuteData(spNames.Monthly_Attendance_Entry_Get, aList, false, false, true);
                        dt = dtGet.Tables[0];
                        htmlTable = CommonUtil.htmlTableAttendance1(dt, columnHide, "AttendanceModule", lstData[12].ToString());
                        DataTable dt1 = new DataTable();
                        DataSet dts = new DataSet();
                        Dictionary<string, ArrayList> aList1 = new Dictionary<string, ArrayList>();
                        dts = CommonBL.ExecuteData(spNames.MaleFemaleGrah, aList1, false, false, true);
                        dt1 = dts.Tables[0];
                        foreach (DataRow item in dt1.Rows)
                        {
                            employeeDetails employeeDetails = new employeeDetails();
                            employeeDetails.Gender = item[0].ToString();
                            employeeDetails.Total = Convert.ToInt32(item[1]);
                            dataList.Add(employeeDetails);
                        }
                        return Ok(new { Flag = 0, List = dt1, Html = htmlTable.ToString(), Count = dt1.Rows.Count, TList = dt1, dataList = dataList });

                    }
                    else
                    {
                   
                        int[] columnHide = { 0,1};
                        Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                        aList.Add("@employee_id", ArrayLists.ToArrayList("@employee_id", lstData[0], SqlDbType.Int));
                        aList.Add("@department_id", ArrayLists.ToArrayList("@department_id", lstData[5], SqlDbType.VarChar));
                        aList.Add("@designation_id", ArrayLists.ToArrayList("@designation_id", lstData[2], SqlDbType.VarChar));
                        aList.Add("@location_id", ArrayLists.ToArrayList("@location_id", lstData[4], SqlDbType.VarChar));
                        aList.Add("@project_id", ArrayLists.ToArrayList("@project_id", null, SqlDbType.VarChar));
                        aList.Add("@emp_type_id", ArrayLists.ToArrayList("@emp_type_id", lstData[1], SqlDbType.VarChar));
                        aList.Add("@shift_id", ArrayLists.ToArrayList("@shift_id", lstData[7], SqlDbType.VarChar));
                        aList.Add("@attendance_date", ArrayLists.ToArrayList("@attendance_date", lstData[13], SqlDbType.Date));
                        var t = CommonBL.ExecuteData(spNames.emp_daily_attendance_get);
                        var ts = CommonBL.ExecuteData(spNames.emp_daily_attendance_get, aList);

                        dtGet = CommonBL.ExecuteData(spNames.emp_daily_attendance_get, aList, false, false, true);
                        dt = dtGet.Tables[0];
                        htmlTable = CommonUtil.htmlTableAttendance(dt, columnHide, "AttendanceModule", lstData[12].ToString());
                       
                       
                        return Ok(new { Flag = 0, Html = htmlTable.ToString(), Count = dt.Rows.Count,  dataList = dataList });

                    }
                }

            }
            catch (Exception x)
            {
                return BadRequest();
            }
        }

        //[HttpPut()]
        //public IHttpActionResult Put(List<Attendanceentry> attendanceentry)
        //{
        //    try
        //    { 
        //        string connetionString;
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add(new DataColumn("EmployeeId", typeof(int)));
        //        dt.Columns.Add(new DataColumn("PRESENT_STATUS", typeof(string)));
        //        dt.Columns.Add(new DataColumn("DATE", typeof(DateTime)));
        //        foreach (var item in attendanceentry)
        //        {
        //            if (item.PRESENT_STATUS != "NA") {
        //                DataRow dr = dt.NewRow();
        //                dr["EmployeeId"] = Convert.ToInt32(item.EmployeeId);
        //                dr["PRESENT_STATUS"] = item.PRESENT_STATUS;
        //                dr["DATE"] = Convert.ToDateTime(item.DATE);
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //        SqlConnection cnn;
        //        connetionString = @"Data Source =DESKTOP-N2KO9ID; Initial Catalog = pay_db1;Integrated Security=True";
        //        cnn = new SqlConnection(connetionString);
        //        SqlBulkCopy sbc = new SqlBulkCopy(cnn);
        //        sbc.DestinationTableName = "dbo.Attendance";
        //        sbc.ColumnMappings.Add("EmployeeId", "EmployeeId");
        //        sbc.ColumnMappings.Add("PRESENT_STATUS", "PRESENT_STATUS");
        //        sbc.ColumnMappings.Add("DATE", "DATE");
        //        cnn.Open();
        //        sbc.WriteToServer(dt);
        //        cnn.Close();
        //        return Ok(new { Html = "Attendance Inserted Sucessfully" });

        //    }
        //    catch (Exception x)
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPut()]
        public IHttpActionResult Put(Attendance Attendance)
        {
            try
            {
                string connetionString;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("EmployeeId", typeof(int)));               
                dt.Columns.Add(new DataColumn("DATE", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("PRESENT_STATUS", typeof(string)));
              if(Attendance.AttendanceType== "DailyAttendance")
                {
                    dt.Columns.Add(new DataColumn("INTIME", typeof(TimeSpan)));
                    dt.Columns.Add(new DataColumn("OUTTIME", typeof(TimeSpan)));
                }
                foreach (var item in Attendance.Attendanceentry)
                {
                    if (item.PRESENT_STATUS != "NA" && Attendance.AttendanceType != "DailyAttendance")
                    {
                        DataRow dr = dt.NewRow();
                        dr["EmployeeId"] = Convert.ToInt32(item.EmployeeId);                        
                        dr["DATE"] = Convert.ToDateTime(item.DATE);
                        dr["PRESENT_STATUS"] = item.PRESENT_STATUS;
                        dt.Rows.Add(dr);
                    }
                    if (Attendance.AttendanceType == "DailyAttendance")
                    {
                        DataRow dr = dt.NewRow();
                        dr["EmployeeId"] = Convert.ToInt32(item.EmployeeId);
                        dr["DATE"] = Convert.ToDateTime(item.DATE);
                        dr["PRESENT_STATUS"] = item.PRESENT_STATUS;
                        dr["INTIME"] = item.INTIME;
                        dr["OUTTIME"] = item.OUTTIME;
                        dt.Rows.Add(dr);
                    }
                }
                SqlConnection cnn;
                connetionString = @"Data Source =DESKTOP-N2KO9ID; Initial Catalog = pay_db1;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                SqlBulkCopy sbc = new SqlBulkCopy(cnn);
                sbc.DestinationTableName = "dbo.Attendance";
                sbc.ColumnMappings.Add("EmployeeId", "EmployeeId");               
                sbc.ColumnMappings.Add("DATE", "DATE");
                sbc.ColumnMappings.Add("PRESENT_STATUS", "PRESENT_STATUS");
                if (Attendance.AttendanceType == "DailyAttendance")
                {
                    sbc.ColumnMappings.Add("INTIME", "INTIME");
                    sbc.ColumnMappings.Add("OUTTIME", "OUTTIME");
                }
                cnn.Open();
                sbc.WriteToServer(dt);
                cnn.Close();
                return Ok(new { Html = "Attendance Inserted Sucessfully" });

            }
            catch (Exception x)
            {
                return BadRequest();
            }
        }

        public class employeeDetails
        {
            public string Gender { get; set; }
            public int Total { get; set; }
        }
        public class Attendanceentry
        {
            public int AttendanceId { get; set; }
            public int EmployeeId { get; set; }
            public string NAME { get; set; }

            public DateTime DATE { get; set; }
            public string PRESENT_STATUS { get; set; }
            public TimeSpan? INTIME { get; set; }
            public TimeSpan? OUTTIME { get; set; }
        }

        public class Attendance
        {
            public string AttendanceType { get; set; }
            public List<Attendanceentry> Attendanceentry { get; set; }

        }
    }

}
