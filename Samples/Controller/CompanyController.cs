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
    public class CompanyController : ApiController
    {
        public ITHeart.BL.GenMstBL CommonBL;
        public CompanyController()
        {
            CommonBL = new ITHeart.BL.GenMstBL();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                int[] columnHide = { 0, 1, 2, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62,63,64,65,66,67 };
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                DataSet dtGet = CommonBL.ExecuteData(spNames.Get_Company, aList, false, false, true);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtGet.Tables[0], columnHide, "CompanyModule", "CompanyDataTable");
                DataTable Load_Country = CommonBL.Load_Country();
                DataTable Load_State = CommonBL.Load_State();
                return Ok(new { Flag = 0, Html = htmlTable.ToString(), Count = dtGet.Tables[0].Rows.Count, List = dtGet.Tables[0], Load_State= Load_State, Load_Country = Load_Country });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@id", ArrayLists.ToArrayList("@id", id, SqlDbType.Char));
                DataSet dtGet = CommonBL.ExecuteData(spNames.Delete_Company_Details, aList, false, false, true);
                return Ok(new { Flag = 0, Html = "Delete  Successfully!!" });
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
        public IHttpActionResult Get(string id)
        {
            try
            {
                int[] columnHide = { 0 };
                Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
                aList.Add("@id", ArrayLists.ToArrayList("@id", id, SqlDbType.VarChar));
                DataSet dt = CommonBL.ExecuteData(spNames.Get_Company_by_id, aList, false, false, true);
                return Ok(new
                {
                    dtCompanyMasterGet = dt.Tables[0],
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost()]
        public IHttpActionResult Post(CompanyMaster CompanyMaster)
        {
            try
            {
                
                CompanyMasterInsertUpdate(CompanyMaster);
                return Ok(new { Flag = 0, Html = "Company Master Saved!!" });
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPut()]
        public IHttpActionResult Put(CompanyMaster CompanyMaster)
        {
            try
            {
                CompanyMasterInsertUpdate(CompanyMaster);
                return Ok(new { Flag = 0, Html = "CompanyMaster Updated!!" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        private void CompanyMasterInsertUpdate(CompanyMaster CompanyMaster)
        {
     
            Dictionary <string, ArrayList> aList = new Dictionary<string, ArrayList>();
           // if (CompanyMaster.StatementType != "Update")
           // {
                aList.Add("@pRowID", ArrayLists.ToArrayList("@pRowID", CompanyMaster.pRowID, SqlDbType.Char));
           // }
            aList.Add("@LocID", ArrayLists.ToArrayList("@LocID", CompanyMaster.LocID, SqlDbType.Char));
            aList.Add("@AssoID", ArrayLists.ToArrayList("@AssoID", CompanyMaster.AssoID, SqlDbType.Char));
            aList.Add("@Name", ArrayLists.ToArrayList("@Name", CompanyMaster.Name, SqlDbType.VarChar));
            aList.Add("@DisplayName", ArrayLists.ToArrayList("@DisplayName", CompanyMaster.DisplayName, SqlDbType.VarChar));
            aList.Add("@Address", ArrayLists.ToArrayList("@Address", CompanyMaster.Address, SqlDbType.VarChar));
            aList.Add("@City", ArrayLists.ToArrayList("@City", CompanyMaster.City, SqlDbType.VarChar));
            aList.Add("@Zip", ArrayLists.ToArrayList("@Zip", CompanyMaster.Zip, SqlDbType.VarChar));
            aList.Add("@State", ArrayLists.ToArrayList("@State", CompanyMaster.State, SqlDbType.VarChar));
            aList.Add("@CountryID", ArrayLists.ToArrayList("@CountryID", CompanyMaster.CountryID, SqlDbType.Char));
            aList.Add("@PhoneNo1", ArrayLists.ToArrayList("@PhoneNo1", CompanyMaster.PhoneNo1, SqlDbType.VarChar));
            aList.Add("@PhoneNo2", ArrayLists.ToArrayList("@PhoneNo2", CompanyMaster.PhoneNo2, SqlDbType.VarChar));
            aList.Add("@PhoneNo3", ArrayLists.ToArrayList("@PhoneNo3", CompanyMaster.PhoneNo3, SqlDbType.VarChar));
            aList.Add("@Fax", ArrayLists.ToArrayList("@Fax", CompanyMaster.Fax, SqlDbType.VarChar));
            aList.Add("@Email", ArrayLists.ToArrayList("@Email", CompanyMaster.Email, SqlDbType.VarChar));
            aList.Add("@WebURL", ArrayLists.ToArrayList("@WebURL", CompanyMaster.WebURL, SqlDbType.VarChar));
            aList.Add("@LogoImage", ArrayLists.ToArrayList("@LogoImage", CompanyMaster.LogoImage, SqlDbType.VarChar));

            aList.Add("@LegalName", ArrayLists.ToArrayList("@LegalName", CompanyMaster.LegalName, SqlDbType.VarChar));
            aList.Add("@LegalShortName", ArrayLists.ToArrayList("@LegalShortName", CompanyMaster.LegalShortName, SqlDbType.VarChar));
            aList.Add("@LegalFax", ArrayLists.ToArrayList("@LegalFax", CompanyMaster.LegalFax, SqlDbType.VarChar));
            aList.Add("@LegalAddress", ArrayLists.ToArrayList("@LegalAddress", CompanyMaster.LegalAddress, SqlDbType.VarChar));
            aList.Add("@LegalEmail", ArrayLists.ToArrayList("@LegalEmail", CompanyMaster.LegalEmail, SqlDbType.VarChar));
            aList.Add("@LegalPhone", ArrayLists.ToArrayList("@LegalPhone", CompanyMaster.LegalPhone, SqlDbType.VarChar));
            aList.Add("@LegalWebsite", ArrayLists.ToArrayList("@LegalWebsite", CompanyMaster.LegalWebsite, SqlDbType.VarChar));
            aList.Add("@LegalPincode", ArrayLists.ToArrayList("@LegalPincode", CompanyMaster.LegalPincode, SqlDbType.VarChar));
            aList.Add("@LegalLocationId", ArrayLists.ToArrayList("@LegalLocationId", CompanyMaster.LegalLocationId, SqlDbType.VarChar));
            aList.Add("@LegalCountryId", ArrayLists.ToArrayList("@LegalCountryId", CompanyMaster.LegalCountryId, SqlDbType.Char));
            aList.Add("@LegalStateId", ArrayLists.ToArrayList("@LegalStateId", CompanyMaster.LegalStateId, SqlDbType.Char));
            aList.Add("@LegalCityId", ArrayLists.ToArrayList("@LegalCityId", CompanyMaster.LegalCityId, SqlDbType.Char));
            aList.Add("@FiscalYearMonth", ArrayLists.ToArrayList("@FiscalYearMonth", CompanyMaster.FiscalYearMonth, SqlDbType.VarChar));
            aList.Add("@Year", ArrayLists.ToArrayList("@Year", CompanyMaster.Year, SqlDbType.VarChar));
            aList.Add("@TaxMonth", ArrayLists.ToArrayList("@TaxMonth", CompanyMaster.TaxMonth, SqlDbType.VarChar));
            aList.Add("@NoofYear", ArrayLists.ToArrayList("@NoofYear", CompanyMaster.NoofYear, SqlDbType.VarChar));

            aList.Add("@Fein", ArrayLists.ToArrayList("@Fein", CompanyMaster.Fein, SqlDbType.VarChar));
            aList.Add("@Ssn", ArrayLists.ToArrayList("@Ssn", CompanyMaster.Ssn, SqlDbType.VarChar));
            aList.Add("@PayrollStartDate", ArrayLists.ToArrayList("@PayrollStartDate", CompanyMaster.PayrollStartDate, SqlDbType.DateTime));
            aList.Add("@StartDate", ArrayLists.ToArrayList("@StartDate", CompanyMaster.StartDate, SqlDbType.DateTime));
            aList.Add("@Enddate", ArrayLists.ToArrayList("@Enddate", CompanyMaster.Enddate, SqlDbType.DateTime));

            aList.Add("@EPF", ArrayLists.ToArrayList("@EPF", CompanyMaster.EPF, SqlDbType.VarChar));
            aList.Add("@ESIC", ArrayLists.ToArrayList("@ESIC", CompanyMaster.ESIC, SqlDbType.VarChar));
            aList.Add("@PAN", ArrayLists.ToArrayList("@PAN", CompanyMaster.PAN, SqlDbType.VarChar));
            aList.Add("@TAN", ArrayLists.ToArrayList("@TAN", CompanyMaster.TAN, SqlDbType.VarChar));
            aList.Add("@Premises", ArrayLists.ToArrayList("@Premises", CompanyMaster.PREMISES, SqlDbType.VarChar));
            aList.Add("@GST", ArrayLists.ToArrayList("@GST", CompanyMaster.GST , SqlDbType.VarChar));
            aList.Add("@ServiceTax", ArrayLists.ToArrayList("@ServiceTax", CompanyMaster.ServiceTax, SqlDbType.VarChar));
            aList.Add("@DefaultCurrency", ArrayLists.ToArrayList("@DefaultCurrency", CompanyMaster.DefaultCurrency, SqlDbType.VarChar));
             if (CompanyMaster.StatementType == "Update")
            {
                CommonBL.ExecuteData(spNames.Update_Company_Details, aList, false, false, true);
            }
            else
            {
                CommonBL.ExecuteData(spNames.Save_Company_Details, aList, false, false, true);
            }
        }
    }

    public class CompanyMaster
    {
        public string StatementType { get; set; }
        public string EPF { get; set; }
        public string ESIC { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string PREMISES { get; set; }
        public string GST { get; set; }
        public string ServiceTax { get; set; }

        public string DefaultCurrency { get; set; }
        public string pRowID { get; set; }
        public string LocID { get; set; }
        public string AssoID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Abbrv { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string CountryID { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PhoneNo3 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebURL { get; set; }
        public string LogoImage { get; set; }
        public string EmailSuffix { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPwd { get; set; }
        public string EnableSSL { get; set; }
        public int Copy2Self { get; set; }
        public int PwdExpiryDays { get; set; }
        public int GraceLogin { get; set; }
        public int recDirty { get; set; }
        public int recEnable { get; set; }
        public string recUser { get; set; }
        public DateTime recAddDt { get; set; }
        public DateTime recDt { get; set; }
        public DateTime ediDt { get; set; }
        public int PageSize { get; set; }
        public int DateFormat { get; set; }
        public string CompanyLetterHead { get; set; }
        public string LegalName { get; set; }
        public string LegalShortName { get; set; }
        public string LegalFax { get; set; }
        public string LegalAddress { get; set; }
        public string LegalEmail { get; set; }
        public string LegalPhone { get; set; }
        public string LegalWebsite { get; set; }
        public string LegalPincode { get; set; }
        public string LegalLocationId { get; set; }
        public string LegalCountryId { get; set; }
        public string LegalStateId { get; set; }
        public string LegalCityId { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
        public string FiscalYearMonth { get; set; }
        public string Year { get; set; }
        public string TaxMonth { get; set; }
        public string NoofYear { get; set; }
        public string Fein { get; set; }
        public string Ssn { get; set; }
        public DateTime? PayrollStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Enddate { get; set; }
    }
}
