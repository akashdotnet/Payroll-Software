using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interop.SqlServer.SQLTask;

namespace Samples
{
    public partial class NavData : System.Web.UI.Page
    {
        public ITHeart.BL.NavBL CommonBL = new ITHeart.BL.NavBL();
        public DataSet _retDs = null;
        private bool DisplayMaster = true;
        private int PrintMode = 0, iMainDescrCol = 0, NavigationHeaderHeight = 80;
        private Dictionary<string, string> DictAddOnWhere = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonBL = new ITHeart.BL.NavBL();
            if (!CommonBL.ValidateEnvironmentClient(true, "", "")) return;

            if (!Page.IsPostBack)
            {
                ViewState["FormParam"] = CommonBL.FormParam;
                ViewState["CommunityCode"] = CommonBL.CommunityAbbrv;

            }
            else
            {
                CommonBL.FormParam = (Dictionary<string, string>)ViewState["FormParam"];
                CommonBL.CommunityAbbrv = (string)ViewState["CommunityCode"];
            }
            if (CommonBL.FormParam == null)
            {
                CommonBL.MyOptionID = "";
            }
            else
            {
                CommonBL.MyOptionID = CommonBL.GetFormDictionaryParam("OptionID").Substring(0, 9);
            }
            if (!Page.IsPostBack)
            {
                hdSelection.Value = ((CommonBL.GetFormDictionaryParam("AddOnfilter") != "") ? CommonBL.GetFormDictionaryParam("AddOnfilter") : hdSelection.Value);
                BindGrid(true);
                // SetToolbar();
            }
        }

       #region " Load / Bind Data "
        public void BindGrid(bool CreateColumns)
        {

            #region " Load Data "
            // ---- Load Data -------------------------------------------------------
            try
            {
                if (ViewState["_retDs"] != null)
                    _retDs = (DataSet)ViewState["_retDs"];
                if (_retDs == null || _retDs.Tables.Count < 1)
                {
                    //if (!CommonBL.ValidateEnvironmentClient(true, CommonBL.MyOptionID, "")) return;
                    _retDs = CommonBL.LoadNavigationData(CommonBL.MyOptionID, CommonBL.GetFormDictionaryParam("GenID"), "", hdSelection.Value.ToString(),txtSearchOn.Text.Replace("'","").Replace("-",""));
                    string ss = "";
                }
                if (_retDs == null || _retDs.Tables.Count < 1)
                { FailureText.Text = CommonBL.Message; return; }
                ViewState["_retDs"] = _retDs;
            }
            catch (Exception ex)
            { FailureText.Text = ex.Message; return; }
            // ----------------------------------------------------------------------
            #endregion

             #region " Pre-Format Grid "
            // ---- Force Page Computations -----------------------------------------
            if (PrintMode == 0)
                gvNav.PageSize = Math.Max(5, (int)Convert.ToDouble(hdPageSize.Value));
            else
                gvNav.PageSize = _retDs.Tables[0].Rows.Count + 1;
            gvNav.AllowPaging = (PrintMode == 0);
            gvNav.AllowSorting = (PrintMode == 0);


            if (CreateColumns)
                CreateMasterColumns();
            // ----------------------------------------------------------------------

            // ---- Format Grid -----------------------------------------------------           
            gvNav.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            // ----------------------------------------------------------------------
            #region " Bind Data "
            // ---- Bind Data -------------------------------------------------------
            gvNav.DataSource = _retDs.Tables[0];
            gvNav.DataBind();
            // ----------------------------------------------------------------------
            #endregion
           
            #endregion
        }
        private void CreateMasterColumns()
        {
            switch (CommonBL.MyOptionID.Split('|')[0])
            {
                #region " Masters "
               
                case "202076000":       // Compliance - General Definition
                    CreateColumns_GenDefMaster(); break;

                #endregion
            }
        }
        private void CreateColumns_GenDefMaster()
        {
            int i;
            double dVal, dFieldWidth, dScreenWidth;
            string sMapField;

            // ---- Init Data -------------------------------------------------------
            iMainDescrCol = 3;
            _retDs = (DataSet)ViewState["_retDs"];
            DataTable gvColumns = _retDs.Tables[1];
            DataTable gvdata = _retDs.Tables[0];

            dScreenWidth = 800- 120 - 120;
            dFieldWidth = (int)SQLAdaptor.Compute(gvColumns, "SUM(Width)", "MapField <> 'recenable'");
            //if (dFieldWidth <= 0) dFieldWidth = dScreenWidth;
            // ----------------------------------------------------------------------

            // ---- ID Column -------------------------------------------------------           
            gvNav.Columns.Add(CommonBL.C1Grid_CreateBoundField("ID", "", "0px", HorizontalAlign.Left, false, false));
            this.gvNav.Columns.Add(CommonBL.CreateTemplateField("Sr", "30px", HorizontalAlign.Right, true, false));            

            // ---- Dynamic Columns -------------------------------------------------
            for (i = 0; i < gvColumns.Rows.Count; i++)
            {
                sMapField = Convert.ToString(gvColumns.Rows[i]["MapField"]);
                if ("__recenable__".IndexOf("_" + sMapField.ToLower() + "_") > 0) continue;

                if(sMapField.ToLower() == "mainid") sMapField = "ParentDescr";

                dVal = (Convert.ToDouble(gvColumns.Rows[i]["Width"]) * dScreenWidth) / dFieldWidth;
                int fWdth = (int)dVal;
                if (dVal - fWdth > .1)
                    dVal += 1;
                gvNav.Columns.Add(CommonBL.C1Grid_CreateBoundField(sMapField,
                   Convert.ToString(gvColumns.Rows[i]["CaptionP"]),
                   ((int)dVal).ToString() + "px", // Converts.ToString(Converts.ToDouble(gvColumns.Rows[i]["Width"]) * 2.5) + "px",
                    HorizontalAlign.Left, true, false));

                if (sMapField.ToLower() == "maindescr") iMainDescrCol = i + 3;
            }
            // ----------------------------------------------------------------------
            this.gvNav.Columns.Add(CommonBL.CreateTemplateField("Status", "60px", HorizontalAlign.Left, PrintMode == 0, false));
            // ---- Status Column ---------------------------------------------------
            this.gvNav.Columns.Add(CommonBL.CreateBoundField("recEnable", "Status", "20px", HorizontalAlign.Left, false, false,""));
            // ----------------------------------------------------------------------

            // ---- Delete Column ---------------------------------------------------
            this.gvNav.Columns.Add(CommonBL.CreateTemplateField("Action", "60px", HorizontalAlign.Center, CommonBL.ValidateUserAccess("021", false) && PrintMode == 0, false));
            // ----------------------------------------------------------------------
        }

        protected void gvNav_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            //if (iMainDescrCol == 0)
            iMainDescrCol = 3;

            int iCol;
            string sVal, sUrl, sCaption;
            bool bCan;

            #region " Set Row Click "
            // ---- Set Row Click ---------------------------------------------------
            if (PrintMode < 1 && Convert.ToInt16(GetMasterSetting("IsDefaultRowBound"))>0)
            {
                sVal = CommonBL.MyOptionID.Substring(0, 9);
                if (!string.IsNullOrEmpty(CommonBL.GetFormDictionaryParam("GenID"))) sVal += "|" + CommonBL.GetFormDictionaryParam("GenID");
                sUrl = CommonBL.ProjectDetails(ITHeart.BL.Common.ProjectInfoTypeEnum.pathWebFormController)
                    + "?OptionID=" + sVal + "&MID=" + e.Row.Cells[0].Text + "&DisplayMaster=2";
                
                sCaption = CommonBL.GetFormDictionaryParam("Caption") + " Details";
                if (iMainDescrCol > 0 && !String.IsNullOrEmpty(e.Row.Cells[iMainDescrCol].Text))
                    sCaption += " - " + e.Row.Cells[iMainDescrCol].Text.Replace("\n", "").Replace("\r", "");

                sVal = "javascript:ShowC1Window('" + sUrl.Replace("'", "&#39;") + "', '" + sCaption.Replace("'", "&#39;") + "', "
                    + " " + GetMasterSetting("EntryLeft") + ", " + GetMasterSetting("EntryTop") + ", "
                    + " " + GetMasterSetting("EntryWidth") + ", " + GetMasterSetting("EntryHeight") + ", 1, 1);";

                e.Row.Attributes.Add("ondblclick", sVal);
            }
            // ----------------------------------------------------------------------
            #endregion

            #region " Set Sr No. Column "
            // ---- Set Sr No. Column -----------------------------------------------
            e.Row.Cells[1].Text = ((gvNav.PageIndex * gvNav.PageSize) + e.Row.RowIndex + 1).ToString();
            // ----------------------------------------------------------------------
            #endregion

            #region " Set Status Column "
            // ---- Set Status Column -----------------------------------------------
            if (PrintMode < 5 && Convert.ToInt16(GetMasterSetting("IsDefaultRowBound")) > 0)
            {
                iCol = e.Row.Cells.Count - 1;

                //if (!Convert.ToBoolean(e.Row.Cells[iCol].Text == "" ? "false" : e.Row.Cells[iCol].Text))
                //{
                //    e.Row.Cells[iMainDescrCol].Font.Strikeout = true;
                //    sUrl = "";
                //    if (CommonBL.MyOptionID.Split('|')[0] == "202068000")
                //    { sVal = "~/Images/disable.gif"; sCaption = "User Account is locked"; }
                //    else
                //    { sVal = "~/Images/disable1.gif"; sCaption = CommonBL.GetFormDictionaryParam("Caption") + " is deactivated"; }
                //    e.Row.Cells[2].Controls.Add(CommonBL.PutHtmlImage(sVal, sCaption, sUrl));
                //}
            }
            // ----------------------------------------------------------------------
            #endregion

            #region " Set Master Specific Cells "
            // ---- Set Each Cell ---------------------------------------------------
            RowDataBoundMaster(e);
            // ----------------------------------------------------------------------
            #endregion

            #region " Set Delete Column "
            // ---- Set Delete Column -----------------------------------------------
            if (PrintMode < 5 && Convert.ToInt16(GetMasterSetting("IsDefaultRowBound")) > 0)
            {
                iCol = e.Row.Cells.Count - 1;
                sVal = e.Row.Cells[0].Text;
                bCan = (e.Row.Cells[iCol].Visible && !sVal.StartsWith("SY"));   // ()
                bCan = bCan && CommonBL.ValidateUserAccess("021", true);
                
                if (CommonBL.MyOptionID.Split('|')[0] == "202078000")
                {
                    if (bCan) bCan = (String.IsNullOrEmpty(CommonBL.GetDashboardGlobal("CommunityID"))
                        || Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - 4].Text) < 1);     // Is System
                }
                if (bCan)
                {
                    //sVal = "javascript:OpenDeleteRecord('" + e.Row.Cells[0].Text + "','Delete " + CommonBL.GetFormDictionaryParam("Caption") + " : " + e.Row.Cells[iMainDescrCol].Text + " ');";

                    sUrl = CommonBL.ProjectDetails(ITHeart.BL.Common.ProjectInfoTypeEnum.pathWebFormController)
                        + "?OptionID=deleterecord&ContextType=" + CommonBL.MyOptionID.Split('|')[0] + "&ContextID=" + e.Row.Cells[0].Text + "&DisplayMaster=2"
                        + "&Caption=" + CommonBL.GetFormDictionaryParam("Caption") + "";
                    if (!String.IsNullOrEmpty(CommonBL.GetFormDictionaryParam("GenID"))) sUrl += "&GenID=" + CommonBL.GetFormDictionaryParam("GenID");
                    sCaption = "Delete " + CommonBL.GetFormDictionaryParam("Caption") + ": " + e.Row.Cells[iMainDescrCol].Text.Replace("\n", "").Replace("\r", "") + "";

                    sVal = "javascript:ShowC1Window('" + sUrl.Replace("'", "&#39;") + "', '" + sCaption.Replace("'", "&#39;") + "', 180, 40, 550, 550, 1, 1);";

                    e.Row.Cells[iCol].Controls.Add(CommonBL.PutHtmlImage("~/Images/trash.gif", sCaption, sVal));
                }
            }
            // ----------------------------------------------------------------------
            #endregion

            #region " Set Each Cell "
            // ---- Set Each Cell ---------------------------------------------------
            for (iCol = 1; iCol < e.Row.Cells.Count; iCol++)
            {
                if (PrintMode < 1) e.Row.Cells[iCol].Style.Add("cursor", "pointer");
                if (!string.IsNullOrEmpty(e.Row.Cells[iCol].Text))
                    e.Row.Cells[iCol].Text = e.Row.Cells[iCol].Text.Replace("<br />", " ").Replace("\n", "").Replace("\r", "");
            }
            // ----------------------------------------------------------------------
            #endregion

        }
        private string GetMasterSetting(string sKey)
        {
            string sVal = "";
            switch (CommonBL.MyOptionID.Split('|')[0])
            {
                #region " Masters "
               
                case "202076000":       // General Definition
                    sVal = GetSetting_GenDefMaster(sKey); break;

                
                #endregion
               

            }
            if (string.IsNullOrEmpty(sVal)) sVal = GetDefaultMasterSetting(sKey);
            return sVal;
        }
        private string GetDefaultMasterSetting(string sKey)
        {
            switch (sKey.ToLower())
            {
                case "caption": return "Master Navigation";
                case "entryleft": return "180";
                case "entrytop": return "20";
                case "entrywidth": return "750";
                case "entryheight": return "580";
                case "total_licences_allowed": return "0";
                case "total_licences_consumed": return _retDs.Tables[0].Rows.Count.ToString();
                case "isdefaultrowbound": return "1";
                case "isdefaultstatus": return "1";
                case "isdefaultdelete": return "1";
            }
            return "";
        }
        private string GetSetting_GenDefMaster(string sKey)
        {

            switch (sKey.ToLower())
            {
                case "caption": return "General Definition"; break;
            }
            return "";
        }
       #endregion

        
        private void RowDataBoundMaster(GridViewRowEventArgs e)
        {
            switch (CommonBL.MyOptionID.Split('|')[0])
            {
                #region " Masters "
                
                case "202076000":       // General Definition
                    RowDataBoundGenDefMaster(e); break;
                
                #endregion


                default:
                    RowDataBoundDefaultMaster(e); break;
            }
        }
        private void RowDataBoundDefaultMaster(GridViewRowEventArgs e)
        {
        }
        private void RowDataBoundGenDefMaster(GridViewRowEventArgs e)
        {
            string sGenID = CommonBL.GetFormDictionaryParam("GenID");
            switch (sGenID)
            {
                case "020":
                    int iIndexchk = 0;
                    for (int i = 0; i < gvNav.Columns.Count; i++)
                    {
                        if (gvNav.Columns[i].ToString().ToLower() == "numval1")
                        {
                            iIndexchk = i;
                        }
                    }
                    e.Row.Cells[iIndexchk].Text = ((e.Row.Cells[iIndexchk].Text.StartsWith("1")) ? "Yearly" : "");
                    break;
                case "190":
                    iIndexchk = 0;
                    for (int i = 0; i < gvNav.Columns.Count; i++)
                    {
                        if (gvNav.Columns[i].ToString().ToLower() == "numval1")
                        {
                            iIndexchk = i;
                        }
                    }
                    int iVal = (int)Convert.ToDouble(e.Row.Cells[iIndexchk].Text);

                    e.Row.Cells[iIndexchk].Text = SQLAdaptor.RetriveInfo("Sysdata22", "GenID=35 AND MainID='" + iVal + "' ", "MainDescr");
                    iIndexchk = 0;
                    for (int i = 0; i < gvNav.Columns.Count; i++)
                    {
                        if (gvNav.Columns[i].ToString().ToLower() == "chrval1")
                        {
                            iIndexchk = i;
                        }
                    }
                    string sVal = Convert.ToString(e.Row.Cells[iIndexchk].Text);

                    e.Row.Cells[iIndexchk].Text = SQLAdaptor.RetriveInfo("Sysdata22", "GenID=37 AND MainID='" + sVal + "' ", "MainDescr");
                    break;
            }
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            string sRedirectURL="";
            sRedirectURL = CommonBL.ProjectDetails(ITHeart.BL.Common.ProjectInfoTypeEnum.pathWebFormController) + "?OptionID=" + CommonBL.MyOptionID + "|" + CommonBL.GetFormDictionaryParam("GenID") + "&GenID=" + CommonBL.GetFormDictionaryParam("GenID") + "&MID=&DisplayType=1&ParamInfo=";
            Response.Redirect(sRedirectURL);
        }
    }
}