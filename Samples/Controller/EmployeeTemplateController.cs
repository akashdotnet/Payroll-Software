using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ITHeart.BL.Models;
using System.Collections;
using System.DataType;
using ITHeart.BL;
using APIapps.Models;

namespace Samples.Controller
{
    public class EmployeeTemplateController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;

        public EmployeeTemplateController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            try
            {
                int[] columnHide = { 0 };
                DataTable dtEmployeeGet = CommonBL.Load_GmMst("029");
                DataSet ds = CommonBL.ExecuteData(spNames.salary_template_Load);
                DataTable dt = ds.Tables[0];
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide, "InitEmpSalaryTemplate", "SalaryTemplateDataTable");
                return Ok(new { Flag = 0, List = dtEmployeeGet, Html = htmlTable.ToString(), Count = dt.Rows.Count, TList = dt });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                int[] columnHide = { 0 };
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@SalaryTemplateId", ArrayLists.ToArrayList("@SalaryTemplateId", id, SqlDbType.Int));
                DataSet dtEmployeeGet = CommonBL.ExecuteData(spNames.Load_Salary_Template_forms_by_id, aList, false, false, true);
                string total = "";
                foreach (DataRow item in dtEmployeeGet.Tables[1].Rows)
                {
                    if (Convert.ToBoolean(item[7]))
                    {
                        total += Convert.ToString(item[8]);
                    }
                    else
                    {
                        decimal mainItemPercentage = Convert.ToDecimal(item[8]);
                        string ts = Convert.ToString(item[13]);
                        string[] cl = ts.Split(',').Select(sValue => sValue.Trim()).ToArray();
                        foreach (var sItems in cl)
                        {
                            foreach (DataRow items in dtEmployeeGet.Tables[1].Rows)
                            {
                                if (sItems.ToString() == Convert.ToString(items[12]))
                                {
                                    if (Convert.ToBoolean(items[7]))
                                    {
                                        var amt = (mainItemPercentage * Convert.ToDecimal(items[8]) / 100);
                                        total += Convert.ToString(amt);
                                    }
                                }
                            }
                        }
                    }
                }
                return Ok(new { dtEmployeeGet = dtEmployeeGet.Tables[0], dtEmpddress = dtEmployeeGet.Tables[1] });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<controller>
        [HttpPost()]
        public IHttpActionResult Post(SalaryTemplate salaryTemplateRequestModel)
        {
            try
            {
                TemplateInsertUpdate(salaryTemplateRequestModel);
                return Ok(new { Flag = 0, Html = "Template and template items saved." });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private void TemplateInsertUpdate(SalaryTemplate salaryTemplateRequestModel)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@Name", ArrayLists.ToArrayList("@Name", salaryTemplateRequestModel.Name, SqlDbType.VarChar));
            aList.Add("@Active", ArrayLists.ToArrayList("@Active", salaryTemplateRequestModel.Active, SqlDbType.Bit));
            aList.Add("@RecDt", ArrayLists.ToArrayList("@RecDt", DateTime.Now, SqlDbType.DateTime));
            aList.Add("@RecAddDt", ArrayLists.ToArrayList("@RecAddDt", DateTime.Now, SqlDbType.DateTime));
            aList.Add("@RecUser", ArrayLists.ToArrayList("@RecUser", "1", SqlDbType.VarChar));
            aList.Add("@RecAddUser", ArrayLists.ToArrayList("@RecAddUser", "1", SqlDbType.VarChar));
            aList.Add("@StatementType", ArrayLists.ToArrayList("@StatementType", salaryTemplateRequestModel.StatementType, SqlDbType.VarChar));
            aList.Add("@SalaryTemplateID", ArrayLists.ToArrayList("@SalaryTemplateID", salaryTemplateRequestModel.SalaryTemplateId, SqlDbType.Int));
            var salayTm = salaryTemplateRequestModel.SalaryTemplateItemRequestModels;
            DataSet ds = CommonBL.ExecuteData(spNames.Insert_Update_Salary_Template, aList, false, false, true);

            foreach (var item in salayTm)
            {
                Dictionary<string, ArrayList> aList1 = new Dictionary<string, ArrayList>();
                aList1.Add("@SalaryTemplateID", ArrayLists.ToArrayList("@SalaryTemplateID", ds.Tables["Table"].Rows[0]["Column1"], SqlDbType.Int));
                aList1.Add("@PayrollItemId", ArrayLists.ToArrayList("@PayrollItemId", item.PayrollItemId, SqlDbType.VarChar));
                aList1.Add("@DefaultValue", ArrayLists.ToArrayList("@DefaultValue", item.DefaultValue, SqlDbType.VarChar));
                aList1.Add("@ApplicableOn", ArrayLists.ToArrayList("@ApplicableOn", item.ApplicableOn, SqlDbType.VarChar));
                aList1.Add("@ISAmount", ArrayLists.ToArrayList("@ISAmount", item.ISAmount, SqlDbType.Bit));
                aList1.Add("@RecDt", ArrayLists.ToArrayList("@RecDt", DateTime.Now, SqlDbType.DateTime));
                aList1.Add("@RecAddDt", ArrayLists.ToArrayList("@RecAddDt", DateTime.Now, SqlDbType.DateTime));
                aList1.Add("@RecUser", ArrayLists.ToArrayList("@RecUser", "1", SqlDbType.VarChar));
                aList1.Add("@RecAddUser", ArrayLists.ToArrayList("@RecAddUser", "1", SqlDbType.VarChar));
                aList1.Add("@PayrollItemText", ArrayLists.ToArrayList("@PayrollItemText", item.PayrollItemText, SqlDbType.VarChar));
                aList1.Add("@ApplicableItemText", ArrayLists.ToArrayList("@ApplicableItemText", item.ApplicableItemText, SqlDbType.VarChar));
                aList1.Add("@Active", ArrayLists.ToArrayList("@Active", item.Active, SqlDbType.Bit));
                aList1.Add("@StatementType", ArrayLists.ToArrayList("@StatementType", salaryTemplateRequestModel.StatementType, SqlDbType.VarChar));
                aList1.Add("@SalaryTemplateItemID", ArrayLists.ToArrayList("@SalaryTemplateItemID", item.SalaryTemplateItemId, SqlDbType.Int));
                aList1.Add("@ValueType", ArrayLists.ToArrayList("@ValueType", item.ValueType, SqlDbType.Int));
                CommonBL.ExecuteData(spNames.Insert_Update_Salary_Template_Itm, aList1, false, false, true);
            }
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public IHttpActionResult Put(SalaryTemplate salaryTemplateRequestModel)
        {
            try
            {
                TemplateInsertUpdate(salaryTemplateRequestModel);
                return Ok(new { Flag = 0, Html = "Template Updated!!" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<controller>/5
        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@SalaryTemplateId", ArrayLists.ToArrayList("@SalaryTemplateId", id, SqlDbType.Int));
                CommonBL.ExecuteData(spNames.Delete_Salary_Template_forms, aList, false, false, true);
                return Ok(new { Flag = 0, Html = "Delete Template Successfully!!" });
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}