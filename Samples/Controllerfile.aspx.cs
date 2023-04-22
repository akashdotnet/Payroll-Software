using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using ITHeart.BL;

namespace Samples
{
    public partial class Controllerfile : System.Web.UI.Page
    {
        private string sReservedParameters = "__DisplayType__ParamInfo__"; 
        private string OptionID;
        Dictionary<string, string> FormParam = new Dictionary<string, string>(); 
        Dictionary<string, string> FormParamList = new Dictionary<string, string>();

        public ITHeart.BL.Common Common = new ITHeart.BL.Common();
        private string sRedirectURL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string sVal = "", sVal2 = "", sMove2URL = "", sSession = "";
            FormParam_Build();
            OptionID = Request.QueryString["OptionID"].ToString();
            string[] sOptionID = OptionID.Split('|');
            switch (sOptionID[0].ToLower())
            {
                case "202076000":
                    if (sOptionID.Length < 2)
                        sRedirectURL = Common.ResolveUrl("Master/ListMasters.aspx");
                    else
                        sRedirectURL = Common.ResolveUrl("Master/GeneralMst.aspx");
                    break;
                case "202076000n":                   
                        sRedirectURL = Common.ResolveUrl("NavData.aspx");                   
                    break;
                case "100001000":
                    sRedirectURL = Common.ResolveUrl("Master/Company.aspx");
                    break;
                case "700001000":
                    sRedirectURL = Common.ResolveUrl("Employee1.aspx");
                    break;
                case "300008000":
                    sRedirectURL = Common.ResolveUrl("transaction/TimeAttendance.aspx");
                    break;
                case "400000000":
                    sRedirectURL = Common.ResolveUrl("Master/SalaryTemplate.aspx");
                    break;
                    
            }
            if (sRedirectURL == "") sRedirectURL= "/NotFound.aspx";

            #region " Move to URL "
            // ---- Add Token -------------------------------------------------------
            sSession = ITHeart.BL.Crypto.Encrypt( DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss"),"ITHeart-Control_");
            sSession = sSession.Replace('+', 'A').Replace("=", "%");
            Session[sSession] = FormParamList;
            sRedirectURL += ((sRedirectURL.IndexOf('?') < 0) ? "?" : "&") + "ID=" + sSession;
            // ----------------------------------------------------------------------

            // ---- Add DisplayMaster Setting ---------------------------------------
            if (Request["DisplayMaster"] != null) sRedirectURL += "&DisplayType=" + Request.QueryString["DisplayMaster"];
           // else if (bProjectMaster) sMove2URL += "&DisplayMaster=1";
            // ----------------------------------------------------------------------

            // ---- Move to URL -----------------------------------------------------
            if (sRedirectURL != "") Response.Redirect(sRedirectURL);
            // ----------------------------------------------------------------------
            #endregion
            return;
        }

        #region " Build Form Param "
        private void FormParam_Build()
        {
            int i;

            #region " Append URL Param Values "
            // ---- Append URL Param Values ------------------------------------------------
            NameValueCollection myCol = (System.Collections.Specialized.NameValueCollection)(Request.QueryString);
            for (i = 0; i < myCol.Count; i++)
            {
                if (string.IsNullOrEmpty(myCol.Keys[i])) continue;
                if (sReservedParameters.IndexOf("_" + myCol.Keys[i] + "_") < 1)
                    FormParam_AddItem(myCol.Keys[i], Common.FixURL(myCol.Get(i)));
            }
            // -----------------------------------------------------------------------------
            #endregion

            #region " Append Param Info Values "
            // ---- Append Param Info Values -----------------------------------------------
            string[] aParam = Request["ParamInfo"].ToString().Split('|');
            string[] aElement;
            for (i = 0; i < aParam.Length; i++)
            {
                aElement = aParam[i].Split('~');
                if (aElement.Length < 2 || string.IsNullOrEmpty(aElement[0])) continue;

                if (sReservedParameters.IndexOf("_" + aElement[0] + "_") < 1)
                    FormParam_AddItem(aElement[0], Common.FixURL(aElement[1]));
            }
            // -----------------------------------------------------------------------------
            #endregion
        }
        private void FormParam_AddItem(string sKey, string sValue)
        {
            try { FormParamList.Add(sKey, sValue); }
            catch { FormParamList[sKey] = sValue; }
        }
        #endregion
    }
}