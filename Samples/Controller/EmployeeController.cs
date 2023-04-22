using APIapps.Models;
using ITHeart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DataType;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Samples.Controller
{
    public class EmployeeController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;
        public EmployeeController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            string html = null;
            try
            {
                //if (Session["userName"] == null)
                //{
                //    return Redirect("~/Home/Login");
                //}

                int[] columnHide = { 0, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };
                DataTable dtEmployeeGet = CommonBL.Load_Employee();
                //,"employeeDataTable"
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide, "EmployeeModule", "employeeDataTable");
                return Ok(new { Flag = 0, Html = htmlTable.ToString(), Count = dtEmployeeGet.Rows.Count, List = dtEmployeeGet });
            }
            catch (Exception ex)
            {
                //html = ex.Message.ToString();
                //return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                return BadRequest();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                int[] columnHide = { 0 };
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@employeeid", ArrayLists.ToArrayList("@employeeid", id, SqlDbType.Int));
                DataSet dtEmployeeGet = CommonBL.ExecuteData(spNames.Load_Employee_forms_by_id, aList, false, false, true);
                DataSet get_EmployeeEmployementDetail = CommonBL.ExecuteData(spNames.get_EmployeeEmployementDetail, aList, false, false, true);
                DataSet get_EmployeeFamilyDetail = CommonBL.ExecuteData(spNames.get_EmployeeFamilyDetail, aList, false, false, true);
                DataSet get_EmployeeAcademicDetail = CommonBL.ExecuteData(spNames.get_EmployeeAcademicDetail, aList, false, false, true);
                DataSet get_EmployeeSalaryDetail = CommonBL.ExecuteData(spNames.get_EmployeeSalaryDetail, aList, false, false, true);
                return Ok(new
                {
                    dtEmployeeGet = dtEmployeeGet.Tables[0],
                    dtEmpddress = dtEmployeeGet.Tables[1],
                    //get_EmployeeEmployementDetail = get_EmployeeEmployementDetail.Tables[0],
                    //get_EmployeeFamilyDetail = get_EmployeeFamilyDetail.Tables[0],
                    //get_EmployeeAcademicDetail = get_EmployeeAcademicDetail.Tables[0],
                    get_EmployeeSalaryDetail = get_EmployeeSalaryDetail.Tables[0],
                    get_EmployeeSalaryItemDetail = get_EmployeeSalaryDetail.Tables[1],

                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<controller>
        [HttpPost()]
        public IHttpActionResult Post(ITHeart.BL.Models.Employee employee)
        {
            try
            {
                EmployeeInsertUpdate(employee);
                return Ok(new { Flag = 0, Html = "Employee Saved!!" });
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private void EmployeeInsertUpdate(ITHeart.BL.Models.Employee employee)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@employeeid", ArrayLists.ToArrayList("@employeeid", employee.EmployeeId, SqlDbType.Int));
            aList.Add("@Code", ArrayLists.ToArrayList("@Code", employee.Code, SqlDbType.VarChar));
            aList.Add("@Name", ArrayLists.ToArrayList("@Name", employee.Name, SqlDbType.VarChar));
            aList.Add("@Gender", ArrayLists.ToArrayList("@Gender", employee.Gender, SqlDbType.Int));
            aList.Add("@Doj", ArrayLists.ToArrayList("@Doj", employee.Doj, SqlDbType.DateTime));
            aList.Add("@MritlStatus", ArrayLists.ToArrayList("@MritlStatus", employee.MaritalStatus, SqlDbType.Int));
            aList.Add("@Nationality", ArrayLists.ToArrayList("@Nationality", employee.Nationality, SqlDbType.Int));
            aList.Add("@FatherName", ArrayLists.ToArrayList("@FatherName", employee.FatherName, SqlDbType.VarChar));
            aList.Add("@MotherName", ArrayLists.ToArrayList("@MotherName", employee.MotherName, SqlDbType.VarChar));
            aList.Add("@dateOfRel", ArrayLists.ToArrayList("@dateOfRel", employee.DateOfRelieving, SqlDbType.DateTime));
            aList.Add("@Dob", ArrayLists.ToArrayList("@Dob", employee.Dob, SqlDbType.DateTime));
            aList.Add("@HighQual", ArrayLists.ToArrayList("@HighQual", employee.HighQual, SqlDbType.Int));
            aList.Add("@dor", ArrayLists.ToArrayList("@dor", employee.Dor, SqlDbType.DateTime));
            aList.Add("@companyid", ArrayLists.ToArrayList("@companyid", employee.CompanyId, SqlDbType.VarChar));
            aList.Add("@EPFNo", ArrayLists.ToArrayList("@EPFNo", employee.EPFNo, SqlDbType.VarChar));
            aList.Add("@ESINo", ArrayLists.ToArrayList("@ESINo", employee.ESINo, SqlDbType.VarChar));
            aList.Add("@PANNo", ArrayLists.ToArrayList("@PANNo", employee.PANNo, SqlDbType.VarChar));
            aList.Add("@AdhaarNo", ArrayLists.ToArrayList("@AdhaarNo", employee.AdhaarNo, SqlDbType.VarChar));
            aList.Add("@UANNo", ArrayLists.ToArrayList("@UANNo", employee.UANNo, SqlDbType.VarChar));
            aList.Add("@StatementType", ArrayLists.ToArrayList("@StatementType", employee.StatementType, SqlDbType.VarChar));
            //if (employee.StatementType != "Update")
            //{
            //    aList.Add("@employeeid", ArrayLists.ToArrayList("@employeeid", employee.EmployeeId, SqlDbType.Int));
            //}
            aList.Add("@LicenceNo", ArrayLists.ToArrayList("@LicenceNo", employee.LicenceNo, SqlDbType.VarChar));
            aList.Add("@LicenceDateOfIssue", ArrayLists.ToArrayList("@LicenceDateOfIssue", employee.LicenceDateOfIssue, SqlDbType.DateTime));
            aList.Add("@LicenceValidity", ArrayLists.ToArrayList("@LicenceValidity", employee.LicenceValidity, SqlDbType.DateTime));
            aList.Add("@LicenceIssueAuthority", ArrayLists.ToArrayList("@LicenceIssueAuthority", employee.LicenceIssueAuthority, SqlDbType.VarChar));
            aList.Add("@PassportNo", ArrayLists.ToArrayList("@PassportNo", employee.PassportNo, SqlDbType.VarChar));
            aList.Add("@PassportDateOfIssue", ArrayLists.ToArrayList("@PassportDateOfIssue", employee.PassportDateOfIssue, SqlDbType.VarChar));
            aList.Add("@PassportValidity", ArrayLists.ToArrayList("@PassportValidity", employee.PassportValidity, SqlDbType.VarChar));
            aList.Add("@PassportPlaceIssue", ArrayLists.ToArrayList("@PassportPlaceIssue", employee.PassportPlaceIssue, SqlDbType.VarChar));
            aList.Add("@ShiftId", ArrayLists.ToArrayList("@ShiftId", employee.ShiftId, SqlDbType.VarChar));
            aList.Add("@EmployeeTypeId", ArrayLists.ToArrayList("@EmployeeTypeId", employee.EmployeeTypeId, SqlDbType.VarChar));
            aList.Add("@EmployeeCategoryId", ArrayLists.ToArrayList("@EmployeeCategoryId", employee.EmployeeCategoryId, SqlDbType.VarChar));
            aList.Add("@ProjectId", ArrayLists.ToArrayList("@ProjectId", employee.ProjectId, SqlDbType.VarChar));
            aList.Add("@DepartmentId", ArrayLists.ToArrayList("@DepartmentId", employee.DepartmentId, SqlDbType.VarChar));
            aList.Add("@DesignationId", ArrayLists.ToArrayList("@DesignationId", employee.DesignationId, SqlDbType.VarChar));
            aList.Add("@LocationId", ArrayLists.ToArrayList("@LocationId", employee.LocationId, SqlDbType.VarChar));
            aList.Add("@DivisionId", ArrayLists.ToArrayList("@DivisionId", employee.DivisionId, SqlDbType.VarChar));
            aList.Add("@BankId", ArrayLists.ToArrayList("@BankId", employee.BankId, SqlDbType.VarChar));
            aList.Add("@BankAccountNo", ArrayLists.ToArrayList("@BankAccountNo", employee.BankAccountNo, SqlDbType.VarChar));
            aList.Add("@BankIfscCode", ArrayLists.ToArrayList("@BankIfscCode", employee.BankIfscCode, SqlDbType.VarChar));

            aList.Add("@PAdd1", ArrayLists.ToArrayList("@PAdd1", employee.PAdd1, SqlDbType.VarChar));
            aList.Add("@PAdd2", ArrayLists.ToArrayList("@PAdd2", employee.PAdd2, SqlDbType.VarChar));
            aList.Add("@PDistrict", ArrayLists.ToArrayList("@PDistrict", employee.PDistrict, SqlDbType.VarChar));
            aList.Add("@PCity", ArrayLists.ToArrayList("@PCity", employee.PCity, SqlDbType.VarChar));
            aList.Add("@PState", ArrayLists.ToArrayList("@PState", employee.PState, SqlDbType.VarChar));
            aList.Add("@PPinCode", ArrayLists.ToArrayList("@PPinCode", employee.PPinCode, SqlDbType.VarChar));
            aList.Add("@PMobileNo", ArrayLists.ToArrayList("@PMobileNo", employee.PMobileNo, SqlDbType.VarChar));
            aList.Add("@PEmergencyContact", ArrayLists.ToArrayList("@PEmergencyContact", employee.PEmergencyContact, SqlDbType.VarChar));
            aList.Add("@RAdd1", ArrayLists.ToArrayList("@RAdd1", employee.RAdd1, SqlDbType.VarChar));
            aList.Add("@RAdd2", ArrayLists.ToArrayList("@RAdd2", employee.RAdd2, SqlDbType.VarChar));
            aList.Add("@RDistrict", ArrayLists.ToArrayList("@RDistrict", employee.RDistrict, SqlDbType.VarChar));
            aList.Add("@RCity", ArrayLists.ToArrayList("@RCity", employee.RCity, SqlDbType.VarChar));
            aList.Add("@RState", ArrayLists.ToArrayList("@RState", employee.RState, SqlDbType.VarChar));
            aList.Add("@RPinCode", ArrayLists.ToArrayList("@RPinCode", employee.RPinCode, SqlDbType.VarChar));
            aList.Add("@RMobileNo", ArrayLists.ToArrayList("@RMobileNo", employee.RMobileNo, SqlDbType.VarChar));
            aList.Add("@REmergencyContact", ArrayLists.ToArrayList("@REmergencyContact", employee.REmergencyContact, SqlDbType.VarChar));
            employee.IsPresentSame = true;
            aList.Add("@IsPresentSame", ArrayLists.ToArrayList("@IsPresentSame", employee.IsPresentSame, SqlDbType.Bit));
            CommonBL.ExecuteData(spNames.Insert_Employee_forms, aList, false, false, true);
            if (employee.StatementType == "Update" && !employee.isSalary)
            {
                Dictionary<string, ArrayList> aList1 = new Dictionary<string, ArrayList>();
                aList1.Add("@EmployeeId", ArrayLists.ToArrayList("@EmployeeId", employee.Salarys[0].EmployeeId, SqlDbType.Int));
                aList1.Add("@CTC", ArrayLists.ToArrayList("@CTC", employee.Salarys[0].CTC, SqlDbType.Decimal));
                aList1.Add("@Gross", ArrayLists.ToArrayList("@Gross", employee.Salarys[0].Gross, SqlDbType.Decimal));
                aList1.Add("@NetPay", ArrayLists.ToArrayList("@NetPay", employee.Salarys[0].NetPay, SqlDbType.Decimal));
                aList1.Add("@Startdate", ArrayLists.ToArrayList("@Startdate", employee.Doj, SqlDbType.DateTime));
                aList1.Add("@Enddate", ArrayLists.ToArrayList("@Enddate", null, SqlDbType.DateTime));
                aList1.Add("@templateid", ArrayLists.ToArrayList("@templateid", employee.Salarys[0].templateid, SqlDbType.Int));
                aList1.Add("@AdditionalBenefit", ArrayLists.ToArrayList("@AdditionalBenefit", employee.Salarys[0].AdditionalBenefit, SqlDbType.Decimal));
                aList1.Add("@Deduction", ArrayLists.ToArrayList("@Deduction", employee.Salarys[0].Deduction, SqlDbType.Decimal));
                aList1.Add("@Earning", ArrayLists.ToArrayList("@Earning", employee.Salarys[0].Earning, SqlDbType.Decimal));
                DataSet ds =CommonBL.ExecuteData(spNames.Employee_Salary_Insert, aList1, false, false, true);
                foreach (var item in employee.SalaryItems)
                {
                    Dictionary<string, ArrayList> aList2 = new Dictionary<string, ArrayList>();
                    aList2.Add("@SalaryId", ArrayLists.ToArrayList("@SalaryId", ds.Tables["Table"].Rows[0]["Column1"], SqlDbType.Int));
                    aList2.Add("@EmployeeId", ArrayLists.ToArrayList("@EmployeeId", employee.Salarys[0].EmployeeId, SqlDbType.Int));
                    aList2.Add("@PayrollItemId", ArrayLists.ToArrayList("@PayrollItemId", item.PayrollItemId, SqlDbType.VarChar));
                    aList2.Add("@PayrollItemName", ArrayLists.ToArrayList("@PayrollItemName", item.PayrollItemName, SqlDbType.VarChar));
                    aList2.Add("@PayrollItemType", ArrayLists.ToArrayList("@PayrollItemType", item.PayrollItemType, SqlDbType.VarChar));
                    aList2.Add("@PayrollItemTypeName", ArrayLists.ToArrayList("@PayrollItemTypeName", item.PayrollItemTypeName, SqlDbType.VarChar));
                    aList2.Add("@Amount", ArrayLists.ToArrayList("@Amount", item.Amount, SqlDbType.Decimal));
                    aList2.Add("@Startdate", ArrayLists.ToArrayList("@Startdate", employee.Doj, SqlDbType.DateTime));
                    aList2.Add("@Enddate", ArrayLists.ToArrayList("@Enddate", null, SqlDbType.DateTime));
                    CommonBL.ExecuteData(spNames.Employee_SalaryItem_Insert, aList2, false, false, true);
                }
            }
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public IHttpActionResult Put(ITHeart.BL.Models.Employee employee)
        {
            try
            {
                EmployeeInsertUpdate(employee);
                return Ok(new { Flag = 0, Html = "Employee Updated!!" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CommonBL.Delete_Employee(id);
                return Ok(new { Flag = 0, Html = "Delete Employee Successfully!!" });
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}