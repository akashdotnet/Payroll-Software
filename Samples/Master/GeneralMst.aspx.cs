using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ITHeart.BL;
using Interop.SqlServer.SQLTask;
using System.Data;
using System.DataType;

namespace ITHeart
{
    public partial class GeneralMst : System.Web.UI.Page
    {
        public ITHeart.BL.GenMstBL CommonBL;
        protected CreatingFormControls FormControlsBL = new CreatingFormControls();
        #region "Variable Declaration"
        private ArrayList MandatoryCtrls = new ArrayList();
        private ArrayList KeyBasedCtrls = new ArrayList();
        public string _OptionID = "";
        Table tblContainer = new Table();
        private System.Web.UI.WebControls.UnitConverter f = new UnitConverter();

        public string sGenID, sHelpID, sOptionID;
        public string[] arrtemp;
        string sScript = "";

        #endregion

        #region "Properties"


        public string PrimaryID
        {
            get
            {
                if (ViewState["PrimaryID"] == null)
                    return "";
                return (string)ViewState["PrimaryID"];
            }
            set
            {
                ViewState["PrimaryID"] = value;
            }
        }



        #endregion


        protected void Page_PreInit(object sender, EventArgs e)
        {
           // Page.Theme = CommonBL.ThemeName;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BuildClientSideScript();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            #region " Initialize Environment "
            // ---- Initialize Environment ------------------------------------------

            CommonBL = new ITHeart.BL.GenMstBL();
            if (!Page.IsPostBack)
            {
                ViewState["FormParam"] = CommonBL.FormParam;
                ViewState["CommunityCode"] = CommonBL.CommunityAbbrv;
                PrimaryID = CommonBL.GetFormDictionaryParam("MID");

            }
            else
            {
                CommonBL.FormParam = (Dictionary<string, string>)ViewState["FormParam"];
                CommonBL.CommunityAbbrv = (string)ViewState["CommunityCode"];
            }



            arrtemp = (CommonBL.GetFormDictionaryParam("OptionID")).ToString().Split('|');
            sOptionID = arrtemp[0];
            if (arrtemp.Length < 2)
            {
                FailureText.Text = "You do not have access to the page you have requested for";
                HttpContext.Current.Response.Redirect(CommonBL.ProjectDetails(Common.ProjectInfoTypeEnum.pathWebError) + "?ErrorNumber="
                    + "&ErrorMessage=" + FailureText.Text + "&ComID=" + CommonBL.CommunityAbbrv);
                return;
            }
            sGenID = arrtemp[1];

            if (!CommonBL.ValidateEnvironmentClient(true, sOptionID, sGenID)) return;

            ///////if (!CommonBL.ValidateEnvironment(true, sOptionID, sGenID)) return; //today change
            //if (!CommonBL.ValidateEnvironment(true, "202076000", sGenID)) return;
            // if (CommonBL.MyOptionID != "202076000") CommonBL.MyOptionID = "202076000";
            this.Title = "General Definition for";// +CommonBL.Community.getLicenceProperties("ProductName");

            // ---- Set Form Objects ------------------------------------------------
            if (!SetFormObjects())
            {
                if (FailureText.Text == "")
                    FailureText.Text = "You do not have access to the page you have requested for";
                HttpContext.Current.Response.Redirect(CommonBL.ProjectDetails(Common.ProjectInfoTypeEnum.pathWebError) + "?ErrorNumber="
                    + "&ErrorMessage=" + FailureText.Text + "&ComID=" + CommonBL.CommunityAbbrv);
                return;
            }
            // ----------------------------------------------------------------------

            PlaceControls();
            #endregion


        }

        private bool SetFormObjects()
        {
            if (Page.IsPostBack) return true;

            bool bCanEdit = false;

            GenMst_AdhocHelpSelected(sGenID);

            PlaceControls();

            //PrimaryID =Converts.ToString(CommonBL.GetFormDictionaryParam("MID"));

            //--------------- Handle Add Mode --------------------------------------
            if (string.IsNullOrEmpty(PrimaryID))
            {
                FormControlsBL.ResetControlValues(tblContainer);
                FormControlsBL.SetMSHelpFilter(tblContainer, "PrimaryKeyID", this.PrimaryID);
            }
            //----------------------------------------------------------------------


            ///Show in Edit Mode

            // ---- Load Data -------------------------------------------------------
            if (!string.IsNullOrEmpty(PrimaryID))
            {

                DataSet ds;
                ds = CommonBL.Load_GenMstforMaster(this.PrimaryID);

                if (ds != null)
                {
                    string[] arrIds = (CommonBL.GetFormDictionaryParam("OptionID")).ToString().Split('|');
                    if (arrIds.Length == 1) NoGenIdSelected();
                    sGenID = arrIds[1].ToString();
                    sHelpID = SQLAdaptor.RetriveInfo("sysGenDef", "GenID='" + sGenID + "'", "SearchForm");

                }
                if (tblContainer.Controls.Count < 1)
                { PlaceControls(); }

                FormControlsBL.SetControlValues(tblContainer, ds.Tables[0].Rows[0]);
                tblContainer.Enabled = true;
                FormControlsBL.SetMSHelpFilter(tblContainer, "PrimaryKeyID", this.PrimaryID);



            }

            // ---- Check User Access Rights ----------------------------------------
            if (string.IsNullOrEmpty(PrimaryID))
            {
               // if (!CommonBL.ValidateUserAccess("001", false)) return false;
                bCanEdit = true;
            }
            else
            {
                bCanEdit = !PrimaryID.StartsWith("SY") && CommonBL.ValidateUserAccess("011", false);
            }
            // ----------------------------------------------------------------------

            // ---- Set Objects -----------------------------------------------------
            if (bCanEdit)
                EnableControls(tblContainer);
            else
                DisableControls(tblContainer);


            return true;
        }


        #region "Button Events"
        protected bool ValidateDuplicacy(Dictionary<string, ArrayList> SP_params)
        {

            string sFilter, sVal, sDescr = "", sMainDescr = "";   

            sFilter = "GenID = '" + sGenID + "' And ID <>'" + PrimaryID + "' "
                + " And ISNULL(CommunityID,'') = '" + CommonBL.GetDashboardGlobal("CommunityID") + "' ";

            // ---- Additional Validations ------------------------------------------------
            switch (sGenID)
            {
                case "020":         // Holiday Master - Check Date
                    sDescr = CommonBL.GetDictionaryValue(SP_params, "@dtVal1").Trim();

                    if (string.IsNullOrEmpty(sDescr)) { FailureText.Text = " Please specify Date"; return false; }

                    sMainDescr = CommonBL.GetDictionaryValue(SP_params, "@MainDescr").Trim();      // Added By Amit Tiwari On 13-Jan-2015

                    // --- By Sanjeev ---
                    sVal = sFilter + " And Year(dbo.CDate(dtVal1)) = '" + DateTime.Parse(sDescr.Replace("'", "''")).Year + "' And MainDescr = '" + sMainDescr.Replace("'", "''") + "' ";
                    sVal = SQLAdaptor.RetriveInfo("GenMst", sVal, "MainDescr");

                    if (!string.IsNullOrEmpty(sVal))
                        sVal = lblGenMstNm.Text + ", '" + sMainDescr + "', is already defined"; // for " + sVal; // I did Changed sMainDescr on the place of sDescr // Niraj 31-Jan-2015
                    else
                    {
                        sVal = sFilter + "AND MainDescr = '" + sMainDescr.Replace("'", "''") + "' And numVal1 = 1 ";
                        sVal = SQLAdaptor.RetriveInfo("GenMst", sVal, "MainDescr");

                        if (!string.IsNullOrEmpty(sVal))
                            sVal = lblGenMstNm.Text + ", '" + sDescr + "', is already defined for " + sVal;
                    }

                    break;
                default:
                    // ---- Added By Amit Tiwari on 13-Jan-2015 Start -------------------------
                    // ---- Check Master Name -----------------------------------------------------
                    sDescr = CommonBL.GetDictionaryValue(SP_params, "@MainDescr").Trim();
                    sVal = sFilter + " And MainDescr = '" + sDescr.Replace("'", "''") + "' ";
                    sVal = SQLAdaptor.RetriveInfo("GenMst", sVal, "MainDescr");
                    if (!string.IsNullOrEmpty(sVal))
                    {
                        FailureText.Text = lblGenMstNm.Text + " Master, '" + sDescr + "', is already defined ";
                        SuccessText.Text = "";
                        return false;
                    }
                    break;
                // ---- Added By Amit Tiwari on 13-Jan-2015 End -------------------------
                //break;    // Commented By Amit Tiwari on 13-Jan-2015
            }
            if (!string.IsNullOrEmpty(sVal))
            { FailureText.Text = sVal; SuccessText.Text = ""; return false; }
            // ----------------------------------------------------------------------------
            return true;

            //string sFilter, sVal, sDescr = "";

            //sFilter = "GenID = '" + sGenID + "' And ID <>'" + PrimaryID + "' "
            //    + " And ISNULL(CommunityID,'') = '" + CommonBL.GetDashboardGlobal("CommunityID") + "' ";

            //// ---- Check Master Name -----------------------------------------------------
            //sDescr = CommonBL.GetDictionaryValue(SP_params, "@MainDescr").Trim();
            //sVal = sFilter + " And MainDescr = '" + sDescr.Replace("'", "''") + "' ";
            //sVal = SQLAdaptor.RetriveInfo("GenMst", sVal, "MainDescr");
            //if (!string.IsNullOrEmpty(sVal))
            //{
            //    FailureText.Text = lblGenMstNm.Text + " Master, '" + sDescr + "', is already defined ";
            //    SuccessText.Text = "";
            //    return false;
            //}
            //// ----------------------------------------------------------------------------

            //// ---- Additional Validations ------------------------------------------------
            //switch (sGenID)
            //{
            //    case "020":         // Holiday Master - Check Date
            //        sDescr = CommonBL.GetDictionaryValue(SP_params, "@dtVal1").Trim();
            //        if(Converts.ToDouble(CommonBL.GetDictionaryValue(SP_params, "@numVal1")) > 0)
            //            sVal = sFilter + " And Left(dbo.CDate(dtVal1),6) = Left('" + sDescr.Replace("'", "''") + "',6) And numVal1 = 1 ";
            //        else
            //            sVal = sFilter + " And  dbo.CDate(dtVal1) = '" + sDescr.Replace("'", "''") + "' ";

            //        sVal = SQLAdaptor.RetriveInfo("GenMst", sVal, "MainDescr");
            //        if (!string.IsNullOrEmpty(sVal))
            //            sVal = lblGenMstNm.Text + ", '" + sDescr + "', is already defined for " + sVal;
            //        break;
            //    default:
            //        break;
            //}
            //if (!string.IsNullOrEmpty(sVal))
            //{ FailureText.Text = sVal; SuccessText.Text = ""; return false; }
            //// ----------------------------------------------------------------------------
            //return true;
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            FailureText.Text = "";

            if (!FormControlsBL.CheckMandatoryControls(MandatoryCtrls))
            {
                FailureText.Text = FormControlsBL.Message;
                return;
            }

            string ProcedureName = "";

            Dictionary<string, ArrayList> SP_params = new Dictionary<string, ArrayList>();
            DataSet ds;
            string returnId = "";

            try
            {

                if (string.IsNullOrEmpty(this.PrimaryID))
                {
                    ProcedureName = spNames.Add_General_Master;
                    returnId = CommonBL.GetNewId("GmData");
                }
                else
                {
                    ProcedureName = spNames.Update_General_Master;
                    returnId = this.PrimaryID;
                }
                SP_params.Add("@pGenRowID", ArrayLists.ToArrayList("@pGenRowID", returnId, SqlDbType.Char));

                SP_params.Add("@GenID", ArrayLists.ToArrayList("@GenID", sGenID, SqlDbType.VarChar));
                SP_params.Add("@LocID", ArrayLists.ToArrayList("@LocID", CommonBL.LocID, SqlDbType.Char));
                SP_params.Add("@recDirty", ArrayLists.ToArrayList("@recDirty", 0, SqlDbType.TinyInt));
                SP_params.Add("@recUser", ArrayLists.ToArrayList("@recUser", CommonBL.UserID, SqlDbType.Char));
                SP_params.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", "DEL0000001", SqlDbType.VarChar)); //CommonBL.DashBoardGlobalValues["CommunityID"]

                FormControlsBL.GetControlValues(tblContainer, SP_params);
                if (!ValidateDuplicacy(SP_params)) return;

                ds = CommonBL.ExecuteData(ProcedureName, SP_params, false, false, true);     //StoredP.ExecuteSP(SP_params, this.ActiveTransaction);

                //// ---- Finalize Save ---------------------------------------------------
                if (!string.IsNullOrEmpty(CommonBL.Message) || ds == null)
                {
                    FailureText.Text = CommonBL.Message; return;
                }
                // ----------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message; return;
            }


            #region " Refresh underlying Form "
            /// ---- Reload Navigation -----------------------------------------------          
            if (CommonBL.DisplayMaster == 2)
            { sScript += "parent.CallRefreshReport();" + "\n"; }
            else if (CommonBL.DisplayMaster == 1)
            { sScript += "if(window.opener != null) window.opener.CallRefreshReport();" + "\n"; }
            if (CommonBL.DisplayMaster != 1 && !string.IsNullOrEmpty(PrimaryID))
                sScript += "FormClose();" + "\n";
            /// ----------------------------------------------------------------------
            #endregion

            /// ---- Finalize Save ---------------------------------------------------
            this.PrimaryID = returnId; SuccessText.Text = "Saved successfully";
            /// ----------------------------------------------------------------------
        }



        #endregion

        #region "GenMst Function"
        private void NoGenIdSelected()
        {
            FailureText.Text = "Invalid Selection. This form is closing";


        }

        private void PlaceControls()
        {

            ///Set table property
            tblContainer.ID = "tblContainer";
            tblContainer.Width = (Unit)f.ConvertFromString("100%");
            //tblContainer.Controls.Clear();
            pnlMain.Controls.Add(tblContainer);
            pnlMain.Height = tblContainer.Height;
            pnlMain.Width = tblContainer.Width;

            if (tblContainer.Controls.Count > 0) return;

            // ---- Load Configured Field List ---------------------------------

            Dictionary<string, ArrayList> sParam = new Dictionary<string, ArrayList>();

            sParam.Add("@GenId", ArrayLists.ToArrayList("@GenId", sGenID, SqlDbType.VarChar));

            DataSet dsControls = CommonBL.ExecuteData(spNames.Load_GenDef_Objects, sParam);

            if (!string.IsNullOrEmpty(CommonBL.Message))
            {

                FailureText.Text = CommonBL.Message;
                return;
            }
            DataRow[] drCtrls = dsControls.Tables[0].Select("tabControlObj is null");

            // -----------------------------------------------------------------

            // ---- Create Objects ---------------------------------------------
            for (int i = 0; i < drCtrls.Length; i++)
            {
                FormControlsBL.AddControls(tblContainer, drCtrls[i], MandatoryCtrls, KeyBasedCtrls, i, Converts.ToInt32(drCtrls[i]["Mandatory"]) < 1, false, false);
            }
            Panel pnl = new Panel();
            pnl.ID = "pnlgap";
            pnl.Height = 0;

            // -----------------------------------------------------------------

            BindEvents(tblContainer);


        }
        public void BindEvents(Control pCtrl)
        {

            Table tbl = null;

            if (pCtrl.GetType().Name != "Table" && pCtrl.Controls.Count > 0)
            {
                for (int i = 0; i < pCtrl.Controls.Count; i++)
                {
                    BindEvents(pCtrl.Controls[i]);
                }
            }

            tbl = (Table)pCtrl;

            foreach (TableRow tr in tbl.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    foreach (Control ctrl in tc.Controls)
                    {
                        if (ctrl.ID == "") continue;
                        switch (ctrl.ID.Substring(0, 3).ToLower())
                        {


                            case ("txt"):
                                break;
                            case ("tnt"):
                                break;
                            case ("cbo"):
                                break;
                            case ("dtp"):
                                break;
                            case ("rdo"):
                                break;
                            case ("chk"):
                                break;
                            case ("hlp"):
                                //((UserControls.HelpBox)Ctrl).ValueChosenWithObj += new FAS.net.Win.UserControls.HelpBox.ChosenClickedHandlerwithObject(frmPlaceControl_ValueChosenWithObj);
                                break;
                            case ("pnl"):
                            case ("tab"):
                            case ("tbc"):
                                BindEvents(ctrl);
                                break;
                        }
                    }
                }
            }
        }

        public void EnableControls(Control pCtrl)
        {

            Table tbl = null;

            if (pCtrl.GetType().Name != "Table" && pCtrl.Controls.Count > 0)
            {
                for (int i = 0; i < pCtrl.Controls.Count; i++)
                {
                    EnableControls(pCtrl.Controls[i]);
                }
            }

            tbl = (Table)pCtrl;

            foreach (TableRow tr in tbl.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    foreach (Control ctrl in tc.Controls)
                    {
                        if (ctrl.ID == "") continue;
                        switch (ctrl.ID.Substring(0, 3).ToLower())
                        {


                            case ("txt"):

                            case ("tnt"):

                            case ("cbo"):

                            case ("dtp"):

                            case ("rdo"):

                            case ("chk"):
                                tbl.Enabled = true;
                                pnlMain.Enabled = true;
                                break;
                            case ("hlp"):
                                ((UserControl.HelpControl)ctrl).Enabled = true;
                                break;
                            case ("pnl"):
                            case ("tab"):
                            case ("tbc"):
                                EnableControls(ctrl);
                                break;
                        }
                    }
                }
            }
        }


        public void DisableControls(Control pCtrl)
        {


            Table tbl = null;

            if (pCtrl.GetType().Name != "Table" && pCtrl.Controls.Count > 0)
            {
                for (int i = 0; i < pCtrl.Controls.Count; i++)
                {
                    DisableControls(pCtrl.Controls[i]);
                }
            }

            tbl = (Table)pCtrl;

            foreach (TableRow tr in tbl.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    foreach (Control ctrl in tc.Controls)
                    {
                        if (ctrl.ID == "") continue;
                        switch (ctrl.ID.Substring(0, 3).ToLower())
                        {


                            case ("txt"):

                            case ("tnt"):

                            case ("cbo"):

                            case ("dtp"):

                            case ("rdo"):

                            case ("chk"):
                                tbl.Enabled = false;
                                pnlMain.Enabled = false;
                                break;
                            case ("hlp"):
                                ((UserControl.HelpControl)ctrl).Enabled = false;
                                break;
                            case ("pnl"):
                            case ("tab"):
                            case ("tbc"):
                                DisableControls(ctrl);
                                break;
                        }
                    }
                }
            }
        }

        public void PlaceControl_ValueChosenWithObj(ITHeart.UserControl.HelpControl sender, string ID)
        {
            FormControlsBL.SetMSHelpFilter(tblContainer, sender.Attributes["Tag"].ToString(), ID);


        }

        public string returnAddValue(string val)
        {
            string[] valr = val.Split(',');
            return (valr.Length > 1 ? valr[1] : val);
        }
        #endregion



        public void GenMst_AdhocHelpSelected(string _genID)
        {
            if (_genID == "")
                return;


            sHelpID = SQLAdaptor.RetriveInfo("sysGmDef", "GenID='" + _genID + "'", "SearchForm");
            this.Title = SQLAdaptor.RetriveInfo("sysGmDef", "GenID='" + _genID + "'", "DetailForm");
            lblGenMstNm.Text = SQLAdaptor.RetriveInfo("sysGmDef", "GenID='" + _genID + "'", "GenDescr");

        }

        #region " Build Client-side Script "
        private void BuildClientSideScript()
        {
            // ---- Include Script Files --------------------------------------------
            // NONE
            // ----------------------------------------------------------------------

            System.Text.StringBuilder sbScript = new System.Text.StringBuilder("");

            sbScript.Append("<script language='javascript' type='text/javascript'>" + "\n");

            #region "Open Help"
            // ---- ----------------------------------------------------------
            sbScript.Append("function OpenGenMstHelp(Url, helpId, Helptext, selectedHelpID, hdBtnID, OptionID) {" + "\n");
            //sbScript.Append("    var AdditionalWhere ='&CommunityID=" + CommonBL.DashBoardGlobalValues["CommunityID"] + "'" + "\n");
            sbScript.Append("   window.open(Url + '?OptionID=help&ID=' + helpId + '&Helptext=' + Helptext + '&ReturnValueBoxID=' + selectedHelpID + '&hdButton=' + hdBtnID + '&BoundOptionID=' + OptionID + AdditionalWhere + '', 'SelectGeneralDefinition', 'toolbars=0, scrollbars=1, status=1');" + "\n");
            sbScript.Append("}" + "\n");
            // ----------------------------------------------------------------------
            #endregion

            #region " Page Load "
            sbScript.Append("function contentPageLoad() {" + "\n");
            //sbScript.Append("   var wWidth = 330, wHeight = " + tblContainer.Rows.Count * 43 + " + (24*4) ;" + "\n");
            //if (CommonBL.DisplayMaster == 2)
            //{
            //    sbScript.Append("   window.parent.SetC1WindowProperties('Size', wWidth, wHeight );" + "\n");
            //    //sbScript.Append("   alert(parseInt(173.45));" + "\n");
            //    sbScript.Append("   window.parent.SetC1WindowProperties('Pos', parseInt((screen.availWidth - wWidth) / 2), parseInt((screen.availHeight - wHeight) / 2));" + "\n");
            //}
            //else
            //{
            //    sbScript.Append("   window.resizeTo(wWidth, wHeight);" + "\n");
            //    sbScript.Append("   window.moveTo((screen.availWidth - wWidth) / 2, (screen.availHeight - wHeight) / 2);" + "\n");
            //}
            sbScript.Append("}" + "\n");
            #endregion


            sbScript.Append("</script>" + "\n");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "@JSScript" + this.ClientID, sbScript.ToString(), false);

            #region " Additional Dynamic Script "
            // ---- Additional Script -----------------------------------------------
            if (!string.IsNullOrEmpty(sScript))
                ScriptManager.RegisterStartupScript(this, this.GetType(), "@JSScriptOnSave" + this.ClientID, "<script language='javascript' type='text/javascript'>" + sScript + "</script>", false);
            // ----------------------------------------------------------------------
            #endregion
        }
        #endregion
    }
}