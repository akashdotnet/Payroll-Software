using APIapps.Models;
using ITHeart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DataType;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Samples.Controller
{
    public class MonthlyAttendanceController : ApiController
    {       
        public ITHeart.BL.GenMstBL CommonBL;
        public MonthlyAttendanceController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }
        
        [HttpPost()]
        public IHttpActionResult Post(List<EmpMonthlyAttendanceEntryViewModel> empMonthlyAttendanceEntryViewModel)
        {
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(empMonthlyAttendanceEntryViewModel);
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source =DESKTOP-N2KO9ID; Initial Catalog = pay_db1;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            int? status = null;
            SqlCommand cmd = new SqlCommand("dbo.emp_attendance_entry_Create", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = cmd.Parameters.AddWithValue("@EmpAttendanceEntry", dt);
            prm.SqlDbType = SqlDbType.Structured;
            prm.TypeName = "dbo.EmpAttendanceEntry";
            status = cmd.ExecuteNonQuery();
            cnn.Close();
            return Ok(new { Html = "Attendance Inserted Sucessfully" });
        }      
    }
    public class EmpMonthlyAttendanceEntryViewModel
    {
        public int emp_attendance_entry_id { get; set; }
        public int paytype_id { get; set; }
        public int employee_id { get; set; }
        public int pay_period { get; set; }
        public int pay_year { get; set; }
        public int work_unit { get; set; }
        public decimal days_worked { get; set; }
        public decimal days_overtime { get; set; }
        public int notes { get; set; }
        public int attendance_method { get; set; }
        public int local_day { get; set; }
        public int non_local_day { get; set; }

    }

    public class ListtoDataTableConverter

    {

        public DataTable ToDataTable<T>(List<T> items)

        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)

            {

                //Setting column names as Property names

                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)

            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)

                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }

    }
}