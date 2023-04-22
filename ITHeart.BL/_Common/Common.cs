using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interop.SqlServer.SQLTask;
using System.Collections;
using System.Data;
using System.DataType;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace ITHeart.BL
{
    public partial class Common : System.Web.UI.Page
    {
        protected int _Status;
        public int Status { get { return _Status; } }

        /// <summary>
        /// Gets the message.
        /// </summary>
        protected string _Message;
        public string Message { get { return _Message; } }
        private Dictionary<string, string> _FormParam = (Dictionary<string, string>)null;
        private Dictionary<string, string> _TempParam = (Dictionary<string, string>)null;
        public Dictionary<string, string> FormParam { get { return _FormParam; } set { _FormParam = value; } }
        public Dictionary<string, string> TempParam { get { return _TempParam; } set { _TempParam = value; } }
        private string _CommunityID = "";
        public string CommunityID { get { return _CommunityID; } set { _CommunityID = value; } }
        private string _CommunityAbbrv = "";
        public string CommunityAbbrv { get { return _CommunityAbbrv; } set { _CommunityAbbrv = value; } }
        public Dictionary<string, string> DashBoardGlobalValues = new Dictionary<string, string>();
        public int DisplayMaster
        { get { return (int)Converts.ToDouble(HttpContext.Current.Request.QueryString["DisplayMaster"]); } }
        public string FormParamToUrl = "";
        public string LocID { get { return "DEL"; } }
        public string UserID 
        { get 
            {
                return ""; // Converts.ToString(GetUserHeaderProperty(PropertyType.UserInfo, "ID"));
            } 
        }
        public string LoginID
        {
            get
            {
                return "admin"; // Converts.ToString(GetUserHeaderProperty(PropertyType.UserInfo, "ID"));
            }
        }
        public string Password
        {
            get
            {
                return "admin"; // Converts.ToString(GetUserHeaderProperty(PropertyType.UserInfo, "ID"));
            }
        }
       

        #region " Dictionary - Dashboard (Default Values) Or Form Param "
        public string GetFormDictionaryParam(string sKey)
        {
            try { return _FormParam[sKey]; }
            catch { return ""; }
        }

        public string GetDashboardGlobal(string sKey)
        {
            try { return DashBoardGlobalValues[sKey]; }
            catch { return ""; }
        }
        //public void SetDashboardGlobal(string sKey, string sVal)
        //{
        //    try { DashBoardGlobalValues[sKey] = sVal; }
        //    catch (Exception e2) { DashBoardGlobalValues.Add(sKey, sVal); }
        //    HttpContext.Current.Session["DefaultSelection"] = DashBoardGlobalValues;
        //}
        public bool IsSingleDB //IsSingleDB~SingleDataBase|NoN SingleDB! MultipleDatabse
        {
            get
            {
                return 0 < 1;
            }
        }


        #endregion

        
        #region " Constructor "
        public Common()
        {
            try
            {
              SQLAdaptor.New("DESKTOP-N2KO9ID", "", "", "Pay_DB1", 1);

             // SQLAdaptor.New("103.117.156.104,2499", "sa", "Hgfhd%$!612bvvd", "Pay_DB", 0);
                _FormParam = (Dictionary<string, string>)HttpContext.Current.Session[HttpContext.Current.Request.QueryString["ID"]];
                BuildFormParamToURL();
            }
            catch { }
            string sVal = "";
            try
            {
                if (HttpContext.Current.Request.QueryString["ComID"] != null)
                    sVal = HttpContext.Current.Request.QueryString["ComID"];
                else
                    sVal = GetFormDictionaryParam("ComID");
            }
            catch { }
            if (sVal != "") _CommunityAbbrv = sVal;

            try
            {
                if (HttpContext.Current.Session["DefaultSelection"] != null)
                { DashBoardGlobalValues = (Dictionary<string, string>)HttpContext.Current.Session["DefaultSelection"]; }
            }
            catch { }

        }
        #endregion

        #region " Project Information "
        /// <summary>
        /// Enum which has the Information project installation
        /// </summary>
        public enum ProjectInfoTypeEnum
        {
            ProjectName = 1,
            ProjectVerson = 2,
            VendorName = 11,

            RegistrationNumber = 51,
            ClientCode = 52,
            CompanyName = 56,

            EmailAddress = 92,
            InfoEmailAddress = 94,

            // Folder Available on Server to contain shared files like Documents, Images, NAV Files, etc.
            pathServerPath = 101,
            pathUserImages = 102,
            pathEDIFiles = 103,
            pathNAVFiles = 104,

            // Folder Available on client Machine as a part of Application or Temporary Folders
            pathApplicationPath = 111,
            pathSysImages = 112,
            pathSystemFiles = 113,
            pathApplicationOutput = 121,

            // Folder Available in the browser over web on http
            pathWebPath = 141,
            pathWebFormController = 146,
            pathWebReportViewer = 148,
            pathWebSelectionCritera = 149,
            pathWebLogin = 151,
            pathWebLogout = 152,
            pathWebError = 150,
            pathWebDashboard = 156,

            // Community Root
            pathWebCommunityRoot = 181,
            pathApplicationCommunityRoot = 171
        }

        /// <summary>
        /// Projects the details.
        /// </summary>
        /// <param name="ProjectInfoIndex">Index of the project info.</param>
        /// <returns>string</returns>
        public string ProjectDetails(ProjectInfoTypeEnum ProjectInfoIndex)
        {
            string ReturnValue = "";
            switch (ProjectInfoIndex)
            {
                #region " License Related "
                case ProjectInfoTypeEnum.ProjectName:
                    ReturnValue ="";// _Community.getLicenceProperties("ProductName").Trim();
                    break;
                case ProjectInfoTypeEnum.ProjectVerson:
                    ReturnValue ="";// _Community.getLicenceProperties("Version").Trim();
                    break;
                case ProjectInfoTypeEnum.VendorName:
                    ReturnValue ="";// _Community.getLicenceProperties("VendorName").Trim();
                    break;
                case ProjectInfoTypeEnum.RegistrationNumber:
                    ReturnValue ="";// _Community.getLicenceProperties("RegistrationKey").Trim();
                    break;

                case ProjectInfoTypeEnum.ClientCode:
                    ReturnValue ="";// _Community.getLicenceProperties("ClientCode").Trim();
                    break;
                case ProjectInfoTypeEnum.CompanyName:
                    ReturnValue = "";//_Community.getLicenceProperties("CompanyName").Trim();
                    break;
                #endregion

                #region " Hard Coded Values "
                case ProjectInfoTypeEnum.EmailAddress:
                    ReturnValue = "support@itheart.com";
                    break;
                case ProjectInfoTypeEnum.InfoEmailAddress:
                    ReturnValue = "info@itheart.com";
                    break;
                #endregion

                #region " Path Related "
                case ProjectInfoTypeEnum.pathServerPath:
                    ReturnValue = ConfigurationSettings.AppSettings["ApplicationPhysicalPath"].ToString(); //_Community.DBInfo.ApplicationPhysicalPath;
                    break;
                case ProjectInfoTypeEnum.pathUserImages:
                    ReturnValue = ConfigurationSettings.AppSettings["ApplicationPhysicalPath"].ToString() + "\\userFile\\Images\\"; //_Community.DBInfo.ApplicationPhysicalPath + "\\userFile\\Images\\";
                    break;

                case ProjectInfoTypeEnum.pathEDIFiles:
                    ReturnValue = ConfigurationSettings.AppSettings["ApplicationPhysicalPath"].ToString() + "userFile\\EDIFiles\\"; //_Community.DBInfo.ApplicationPhysicalPath + "userFile\\EDIFiles\\";
                    break;

                case ProjectInfoTypeEnum.pathNAVFiles:
                    ReturnValue = ConfigurationSettings.AppSettings["ApplicationPhysicalPath"].ToString() + "userFile\\NavFiles\\"; //_Community.DBInfo.ApplicationPhysicalPath + "userFile\\NavFiles\\";
                    break;

                case ProjectInfoTypeEnum.pathApplicationPath:
                    ReturnValue = this.ResolveClientUrl("Project.Master").Replace("Project.Master", "");//_Community.DBInfo.ApplicationWebPath;
                    break;

                case ProjectInfoTypeEnum.pathSysImages:
                    ReturnValue = this.ResolveClientUrl("Project.Master").Replace("Project.Master", "") + "sysImages\\";//_Community.DBInfo.ApplicationWebPath + "sysImages\\";
                    break;
                case ProjectInfoTypeEnum.pathSystemFiles:
                    ReturnValue = this.ResolveClientUrl("Project.Master").Replace("Project.Master", "") + "sysFiles\\"; //_Community.DBInfo.ApplicationWebPath + "sysFiles\\";
                    break;
                case ProjectInfoTypeEnum.pathApplicationOutput:
                    ReturnValue = ConfigurationSettings.AppSettings["ApplicationPhysicalPath"].ToString() + "\\output\\"; //_Community.DBInfo.ApplicationPhysicalPath + "\\output\\";
                    break;

                case ProjectInfoTypeEnum.pathWebPath:
                    ReturnValue = ConfigurationSettings.AppSettings["ApplicationWebPath"].ToString() + "\\";//_Community.DBInfo.ApplicationWebPath + "\\";
                    break;

                case ProjectInfoTypeEnum.pathWebFormController:
                    ReturnValue = this.ResolveUrl("Controllerfile.aspx"); //this.ResolveClientUrl("BuyerEase.Master").Replace("BuyerEase.Master", "") + "/Login/LoginPage.aspx"; // _Community.DBInfo.ApplicationWebPath + "/Login/LoginPage.aspx";
                    break;

                case ProjectInfoTypeEnum.pathWebSelectionCritera:
                    ReturnValue = this.ResolveUrl("Utilities/SelectionCriteria.aspx"); //this.ResolveClientUrl("BuyerEase.Master").Replace("BuyerEase.Master", "") + "/Login/LoginPage.aspx"; // _Community.DBInfo.ApplicationWebPath + "/Login/LoginPage.aspx";
                    break;

                case ProjectInfoTypeEnum.pathWebReportViewer:
                    ReturnValue = this.ResolveUrl("Reports/ReportViewer.aspx"); //this.ResolveClientUrl("BuyerEase.Master").Replace("BuyerEase.Master", "") + "/Login/LoginPage.aspx"; // _Community.DBInfo.ApplicationWebPath + "/Login/LoginPage.aspx";
                    break;

                case ProjectInfoTypeEnum.pathWebLogin:
                    ReturnValue = this.ResolveUrl("Login/LoginPage.aspx"); //this.ResolveClientUrl("BuyerEase.Master").Replace("BuyerEase.Master", "") + "/Login/LoginPage.aspx"; // _Community.DBInfo.ApplicationWebPath + "/Login/LoginPage.aspx";
                    break;

                case ProjectInfoTypeEnum.pathWebLogout:
                    ReturnValue = this.ResolveUrl("Login/LoggedOut.aspx"); // this.ResolveClientUrl("BuyerEase.Master").Replace("BuyerEase.Master", "") + "/Login/LoggedOut.aspx"; // _Community.DBInfo.ApplicationWebPath + "/Login/LoggedOut.aspx";
                    break;

                case ProjectInfoTypeEnum.pathWebError:
                    ReturnValue = this.ResolveUrl("_Admin/ErrorPage.aspx"); // _Community.DBInfo.ApplicationWebPath + "/ErrorPage.aspx";
                    break;

                case ProjectInfoTypeEnum.pathWebDashboard:                    
                        ReturnValue = this.ResolveUrl("DashBoard.aspx");// this.ResolveUrl("DashBoard/FinOrbDashBoard.aspx");  //_Community.DBInfo.ApplicationWebPath + "/DashBoard/FinOrbDashBoard.aspx";
                        break;

                case ProjectInfoTypeEnum.pathApplicationCommunityRoot:
                    ReturnValue = ""; // _Community.DBInfo.ApplicationFilePhysicalPath + "\\";
                    break;
                case ProjectInfoTypeEnum.pathWebCommunityRoot:
                    ReturnValue = this.ResolveClientUrl("Project.Master").Replace("Project.Master", "") + "/";   //_Community.DBInfo.ApplicationFileWebPath + "/";
                    break;
                #endregion
            }
            return ReturnValue;
        }

        public string ResolveUrl(string relativeUrl)
        {
            if (relativeUrl == null) throw new ArgumentNullException("relativeUrl");

            if (relativeUrl.Length == 0 || relativeUrl[0] == '/' || relativeUrl[0] == '\\')
                return relativeUrl;

            int idxOfScheme = relativeUrl.IndexOf(@"://", StringComparison.Ordinal);
            if (idxOfScheme != -1)
            {
                int idxOfQM = relativeUrl.IndexOf('?');
                if (idxOfQM == -1 || idxOfQM > idxOfScheme) return relativeUrl;
            }

            StringBuilder sbUrl = new StringBuilder();
            sbUrl.Append(HttpRuntime.AppDomainAppVirtualPath);
            if (sbUrl.Length == 0 || sbUrl[sbUrl.Length - 1] != '/') sbUrl.Append('/');

            // found question mark already? query string, do not touch!
            bool foundQM = false;
            bool foundSlash; // the latest char was a slash?
            if (relativeUrl.Length > 1
                && relativeUrl[0] == '~'
                && (relativeUrl[1] == '/' || relativeUrl[1] == '\\'))
            {
                relativeUrl = relativeUrl.Substring(2);
                foundSlash = true;
            }
            else foundSlash = false;
            foreach (char c in relativeUrl)
            {
                if (!foundQM)
                {
                    if (c == '?') foundQM = true;
                    else
                    {
                        if (c == '/' || c == '\\')
                        {
                            if (foundSlash) continue;
                            else
                            {
                                sbUrl.Append('/');
                                foundSlash = true;
                                continue;
                            }
                        }
                        else if (foundSlash) foundSlash = false;
                    }
                }
                sbUrl.Append(c);
            }

            return sbUrl.ToString();
        }
        #endregion


        #region " DB Methods "
        /// <summary>
        /// Executes the SQL Query
        /// </summary>
        /// <param name="spName">Store Proc Name</param>
        /// <returns>Dataset </returns>
        public DataSet ExecuteData(string spName)
        {
            //return ExecuteData(spName, new Dictionary<string, ArrayList>(), false, false, true);
            return SQLAdaptor.ExecuteSP(spName, new Dictionary<string, ArrayList>(), null);
        }

        /// <summary>
        /// Executes the SQL Query
        /// </summary>
        /// <param name="spName">Store Proc Name</param>
        /// <param name="SP_params">Passes Dictionary for SP Parameter List.</param>
        /// <returns>Dataset </returns>
        public DataSet ExecuteData(string spName, Dictionary<string, ArrayList> SP_params)
        {
            DataSet ds = SQLAdaptor.ExecuteSP(spName, SP_params, null);
            //return ExecuteData(spName, SP_params, false, false, true);
            _Message = SQLAdaptor.ErrorMessage;
            return ds;

        }

        /// <summary>
        /// Executes the SQL Query
        /// </summary>
        /// <param name="spName">Store Proc Name</param>
        /// <param name="SP_params">Passes Dictionary for SP Parameter List.</param>
        /// <param name="onFileServer">if set to <c>true</c> [on file server].</param>
        /// <param name="ExecuteInTransaction">if set to <c>true</c> [execute in transaction].</param>
        /// <param name="CloseTransaction">if set to <c>true</c> [close transaction].</param>
        /// <returns>Dataset </returns>
        public DataSet ExecuteData(string spName, Dictionary<string, ArrayList> SP_params, bool onFileServer, bool ExecuteInTransaction, bool CloseTransaction)
        {
            DataSet ds = SQLAdaptor.ExecuteSP(spName, SP_params, null);
            _Message = SQLAdaptor.ErrorMessage;

            return ds;
        }

        #region " Quick DB Retrival / Updation "
               /// <summary>
        /// Load & Validate DB Info
        /// Load Community Properties
        /// </summary>
        /// <returns>Error </returns>
        public bool ValidateDBInfo()
        {
            DataSet ds = new DataSet(); ds = null;
           // return FSL.Web.Community.CommunityClass.ValidateDBInfo(0, ds);
            return true;
        }
        #endregion

        #region " Database - Misc Functions "
        /// <summary>
        /// Initializes a new instance of the FrontCommon class.
        /// </summary>

        /// <summary>
        /// Generates the new prow ID.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        public string GenerateNewpRowID(string TableName)
        {

            return GenerateNewpRowID(TableName, "");

        }
        /// <summary>
        /// Initializes a new instance of the FrontCommon class.
        /// </summary>

        /// <summary>
        /// Generates the new prow ID.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns></returns>
        public string GenerateNewpRowID(string TableName, string FieldName)
        {
            string gLocation = Converts.ToString("DEL");
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@LocId", ArrayLists.ToArrayList("@LocID", gLocation, SqlDbType.VarChar));
            spParam.Add("@Tablename", ArrayLists.ToArrayList("@Tablename", TableName, SqlDbType.VarChar));
            if (FieldName != "")
                spParam.Add("@FieldName", ArrayLists.ToArrayList("@FieldName", FieldName, SqlDbType.VarChar));
            DataSet dsNewPrimaryKey = ExecuteData(spNames.Generate_A_New_Primary_Key, spParam, false, false, true);
            if (Status != 0 && Message != "")
            {
                _Status = Status;
                _Message = Message;
                return "";
            }

            return dsNewPrimaryKey.Tables[0].Rows[0][0].ToString();

        }
        public string PadZeros(string Key, Int32 PrefixLength)
        {
            string RequiredLength = Key.Substring(PrefixLength);
            return Key.Substring(0, PrefixLength) + "0000000000".Substring(PrefixLength + RequiredLength.Length) + RequiredLength;

            //return Key.Substring(0, PrefixLength) + "0000000000" + Key.Substring(PrefixLength + 1, DesiredLength - PrefixLength).Substring(PrefixLength, DesiredLength - PrefixLength);
            //            PadZeros = Left(Key, PrefixLength) & Right(Replace(Space(DesiredLength), " ", "0") & Mid(Key, PrefixLength + 1), DesiredLength - PrefixLength)
        }
        public string RemoveZeros(string Key, Int32 PrefixLength)
        {
            string strValue = "";
            int len = Key.Length;
            if (PrefixLength < 1) PrefixLength = 2;
            if (Key.Trim() == "" || Key.Length < PrefixLength)
                return Key;
            //strValue = key.Substring(0, PrefixLength) + key.Substring((len - PrefixLength), PrefixLength + 1);
            strValue = Key.Substring(0, PrefixLength) + Convert.ToInt32(Key.Substring(PrefixLength + 1)).ToString();
            return strValue;
            //if (key.Trim() == "" || key.Length < PrefixLength)
            //    return key;

            //return Key.Substring(0, PrefixLength) + Converts.ToDouble(Key.Substring(PrefixLength + 1, Key.Length - PrefixLength)).ToString("0");
        }
        public string GetMenuString(string strLinkID)
        {
            string strQry;
            DataSet ds;

            // ' ------ Load Option Parameters -------------------------------------------
            strQry = " Select mnu.MenuID, mnu.OptionName, mnu.ParentID, mnu.OptionID, opt.FormName, opt.OpenMode " +
                " From sysMenu mnu " +
                " Inner Join OptionTable opt on opt.OptionID = mnu.OptionID " +
                " Where Substring(mnu.OptionID,1,6) = '" + strLinkID.Substring(0, 6) + "' ";

            ArrayList sParam = new ArrayList();
            sParam.Add(spNames.ExecuteQry);
            sParam.Add("@Qry," + strQry);
            ds = SQLAdaptor.ExecuteSP(sParam);

            //ds = ExecuteQry(strQry);
            if (ds.Tables[0].Rows.Count < 1) return "";
            //  ' -------------------------------------------------------------------------
            //
            //  ' ------ Build Fx String --------------------------------------------------
            return "OpenFormParam('" + ds.Tables[0].Rows[0]["FormName"].ToString().Replace("'", "&#39;") + "', " +
                 ds.Tables[0].Rows[0]["OpenMode"].ToString() + ", " +
                 "'" + "', " +
                 "'" + ds.Tables[0].Rows[0]["OptionID"].ToString().Replace("'", "&#39;") + "', " +
                 "'" + "O" + "'" +
                 ")";
            // ' -------------------------------------------------------------------------
        }
        #endregion

        #endregion

        public bool ValidateEnvironmentClient(bool bLogInReqd, string sOptID, string ExtraParam)
        {
            //Added By Niraj for Merge SingleDB Application or MultiDB Application Date : 06-July-2015
            if (IsSingleDB)
            {
                if (ValidateEnvironment(bLogInReqd, sOptID, ExtraParam))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return true;
        }
        public bool ValidateEnvironment(bool bLogInReqd, string sOptID, string ExtraParam)
        {
            return true;
        }

        #region " User Rights "
        public enum Modules { None = 0, Compliance = 1, CRM = 2, ShipmentTrack = 4 };
        public bool IsModule(Modules mod)
        {
            if ((mod & Modules.Compliance)

                == Modules.Compliance)
                if (ValidateCommunityAccess("207035", "", true)) return true;

            return false;
        }
        private string _myOptionID, _OptionId, _GenId = "";
        public int RecStatus
        { get { return (int)ViewState["RecStatus"]; } set { ViewState["RecStatus"] = value; } }
        public string MyOptionID
        {
            get { return _myOptionID; }
            set
            {
                _myOptionID = value;
                string[] arrIds = value.Split('|');
                _OptionId = arrIds[0];
                if (arrIds.Length > 1) _GenId = arrIds[1];
                //if (_myOptionID.Trim() != "") LoadUserRights();
            }
        }

        public bool ValidateUserAccess(string OptID3, bool bUseDashboardGlobalCommunity)
        {
            return ValidateUserAccess(_myOptionID.Substring(0, 6) + OptID3, _GenId, bUseDashboardGlobalCommunity);
        }
        public bool ValidateUserAccess(string OptionID, string ExtraParam, bool bUseDashboardGlobalCommunity)
        {
            string sCommunityID = "";
            //if (bUseDashboardGlobalCommunity) sCommunityID = GetDashboardGlobal("CommunityID");

            //if (OptionID.StartsWith("202020000") && SaaS) ExtraParam = "";
            //if (!ValidateUserAccess(OptionID, ExtraParam)) return false;
            //if (!SaaS || !Strings.IsNullOrEmpty(_UserInfo.CommunityID) || Strings.IsNullOrEmpty(sCommunityID))
            //    return true;

            ////if (!SaaS || !Strings.IsNullOrEmpty(_UserInfo.CommunityID) || Strings.IsNullOrEmpty(sCommunityID)) 
            ////    return ValidateUserAccess(OptionID, ExtraParam);

            //if (_CommunityUserInfo == null && Session["CommunityUser"] == null)
            //    return false;

            //if (_CommunityUserInfo == null && Session["CommunityUser"] != null)
            //    _CommunityUserInfo = (FSL.Web.User.UserClass)Session["CommunityUser"];
            //if (_CommunityUserInfo.dtRights == null) return false;

            //string sVal = "OptionID Like '" + OptionID.Trim() + "%' ";
            //if (!string.IsNullOrEmpty(ExtraParam))
            //    sVal += ((Strings.IsNullOrEmpty(sVal)) ? "" : " And ") + "ExtraParam ='" + ExtraParam + "'";

            //try { return (_CommunityUserInfo.dtRights.Select(sVal).Length > 0); }
            //catch
            //{ return false; }
           return true;
        }
        private bool ValidateUserAccess(string OptionID, string ExtraParam)
        {
            //if (_UserInfo == null && Session["User"] == null)
            //    return false;

            //if (_UserInfo == null && Session["User"] != null)
            //    _UserInfo = (FSL.Web.User.UserClass)Session["User"];
            //if (_UserInfo.dtRights == null) return false;

            //string sVal = "OptionID Like '" + OptionID.Trim() + "%' ";
            //if (!string.IsNullOrEmpty(ExtraParam))
            //    sVal += ((Strings.IsNullOrEmpty(sVal)) ? "" : " And ") + "ExtraParam ='" + ExtraParam + "'";

            //try { return (_UserInfo.dtRights.Select(sVal).Length > 0); }
            //catch
            //{ return false; }
            return true;
        }
        public bool ValidateCommunityAccess(string OptionID, string ExtraParam, bool bUseDashboardGlobalCommunity)
        {
            string sCommunityID = "";
            //if (bUseDashboardGlobalCommunity) sCommunityID = GetDashboardGlobal("CommunityID");

            //if (_UserInfo == null && Session["User"] == null)
            //    return false;
            //if (_UserInfo == null && Session["User"] != null)
            //    _UserInfo = (FSL.Web.User.UserClass)Session["User"];

            //if (!SaaS || !Strings.IsNullOrEmpty(_UserInfo.CommunityID) || Strings.IsNullOrEmpty(sCommunityID))
            //    return ValidateCommunityAccess(OptionID, ExtraParam);

            //if (_CommunityUserInfo == null && Session["CommunityUser"] == null)
            //    return false;

            //if (_CommunityUserInfo == null && Session["CommunityUser"] != null)
            //    _CommunityUserInfo = (FSL.Web.User.UserClass)Session["CommunityUser"];

            //if (_CommunityUserInfo.dtFeatures == null) return ValidateUserAccess(OptionID, ExtraParam, bUseDashboardGlobalCommunity);

            //string sVal = "OptionID Like '" + OptionID.Trim() + "%' ";
            //if (!string.IsNullOrEmpty(ExtraParam))
            //    sVal += ((string.IsNullOrEmpty(sVal)) ? "" : " And ") + "ExtraParam ='" + ExtraParam + "'";
            //try { return (_CommunityUserInfo.dtFeatures.Select(sVal).Length > 0); }
            //catch
            //{ return false; }
            return true;
        }
        private bool ValidateCommunityAccess(string OptionID, string ExtraParam)
        {
            //if (_Community.getLicenceProperties("FeatureList").IndexOf(OptionID.Trim()) < 0)
            //    return false;

            //if (_UserInfo == null && Session["User"] == null)
            //    return false;

            //if (_UserInfo == null && Session["User"] != null)
            //    _UserInfo = (FSL.Web.User.UserClass)Session["User"];

            //if (_UserInfo.dtFeatures == null) return ValidateUserAccess(OptionID, ExtraParam);

            //string sVal = "OptionID Like '" + OptionID.Trim() + "%' ";
            //if (!string.IsNullOrEmpty(ExtraParam))
            //    sVal += ((string.IsNullOrEmpty(sVal)) ? "" : " And ") + "ExtraParam ='" + ExtraParam + "'";
            //try { return (_UserInfo.dtFeatures.Select(sVal).Length > 0); }
            //catch
            //{ return false; }
            return true;
        }
        #endregion

        #region "Generate new primary Id"
        public string GetNewId(string TableName)
        {
            DataSet dsNewID;
            Dictionary<string, ArrayList> sParam = new Dictionary<string, ArrayList>();

            sParam.Add("@LocId", ArrayLists.ToArrayList("@LocId", LocID, SqlDbType.VarChar));
            sParam.Add("@Tablename", ArrayLists.ToArrayList("@Tablename", TableName, SqlDbType.VarChar));
            dsNewID = ExecuteData(spNames.Generate_A_New_Primary_Key, sParam);
            if (Status != 0 && Message != "")
            {
                _Status = Status;
                _Message = Message;
                return string.Empty;
            }

            return dsNewID.Tables[0].Rows[0].ItemArray[0].ToString();


        }
        #endregion

        #region " Fiscal Year "
        public string gblFiscalYearID = "";
        public string gblFiscalYearName = "";
        public string gblFiscalStartDate = "";
        public string gblFiscalEndDate = "";

        public string gblFromDate = "";
        public string gblToDate = "";
        public string gblFromDate2 = "";
        public string gblToDate2 = "";
        public event FiscalYearChangedHandler GlobalFiscalYearChanged;
        public delegate void FiscalYearChangedHandler(String ID);

        public string FiscalYearID
        {
            get { return gblFiscalYearID; }
            set
            {
                if (gblFiscalYearID != value)
                {
                    gblFiscalYearID = value;
                    SetFiscalYear(value);
                    if (GlobalFiscalYearChanged != null)
                        GlobalFiscalYearChanged(value);
                    gblFromDate2 = "";
                    gblToDate2 = "";
                }
            }
        }
        public void SetFiscalYear()
        {
            if (Convert.ToString(gblFiscalYearID) != "") return;
            //// gblValues.GetCustomProperties("20")
            //Community.GetLicenseValidationValue("20")  
            string sQry = "Select Id, Description, StartDate, EndDate ,0 as rectype "
                + " From FinancialYear "
                + " Where DateAdd(d,-" + 0 + ",getdate()) Between StartDate and DateAdd(d,1,EndDate) "
                + " UNION select Top 1 Id, Description, StartDate, EndDate, 1 as rectype From FinancialYear"
                + " order by rectype ";

            Dictionary<string, ArrayList> SP_paramsd = new Dictionary<string, ArrayList>();
            SP_paramsd.Add("@Qry", ArrayLists.ToArrayList("@Qry", sQry, SqlDbType.VarChar));

            DataSet dsFisYear = ExecuteData(spNames.ExecuteQry, SP_paramsd, false, false, true);

            if (Status != 0 && Message != "")
            {
                _Status = Status;
                _Message = Message;
                return;
            }


            if (dsFisYear.Tables[0].Rows.Count > 0)
            {
                SetFiscalYearValues(Convert.ToString(dsFisYear.Tables[0].Rows[0]["ID"]),
                    Convert.ToString(dsFisYear.Tables[0].Rows[0]["Description"]),
                    Convert.ToDateTime(dsFisYear.Tables[0].Rows[0]["StartDate"].ToString()).ToString("dd/MMM/yyyy"),
                    Convert.ToDateTime(dsFisYear.Tables[0].Rows[0]["EndDate"].ToString()).ToString("dd/MMM/yyyy"));
                return;
            }
        }
        private void SetFiscalYear(string sID)
        {
            if (sID == null || sID.ToLower() == "null" || sID.Trim() == "")
            {
                SetFiscalYear();
                SetFiscalYearValues("", "", "", "");
                return;
            }

            Dictionary<string, ArrayList> SP_paramsd = new Dictionary<string, ArrayList>();
            SP_paramsd.Add("@Qry", ArrayLists.ToArrayList("@Qry", "select Id;Description;StartDate;EndDate from FinancialYear where ID='" + sID + "'", SqlDbType.VarChar));

            DataSet dsFisYear = ExecuteData(spNames.ExecuteQry, SP_paramsd, false, false, true);

            if (Status != 0 && Message != "")
            {
                _Status = Status;
                _Message = Message;
                return;
            }

            if (dsFisYear.Tables[0].Rows.Count > 0)
            {
                SetFiscalYearValues(Convert.ToString(dsFisYear.Tables[0].Rows[0]["ID"]),
                    Convert.ToString(dsFisYear.Tables[0].Rows[0]["Description"]),
                    Convert.ToDateTime(dsFisYear.Tables[0].Rows[0]["StartDate"].ToString()).ToString("dd/MMM/yyyy"),
                    Convert.ToDateTime(dsFisYear.Tables[0].Rows[0]["EndDate"].ToString()).ToString("dd/MMM/yyyy"));
            }
        }
        private void SetFiscalYearValues(string FYID, string FYName, string FYFrom, string FYTo)
        {
            gblFiscalYearID = FYID.Trim();
            gblFiscalYearName = FYName;
            gblFiscalStartDate = FYFrom.Trim();
            gblFiscalEndDate = FYTo.Trim();
            gblFromDate = FYFrom.Trim();
            if (Convert.ToDateTime(FYTo) > DateTime.Now)
                gblToDate = DateTime.Now.ToString("dd-MMM-yyyy");
            else
                gblToDate = FYTo.Trim();
        }

        #endregion

        #region " ArrayList Manipulations "
        public DataSet ExecuteSP(ArrayList Sp_Params)
        {


            DataSet ds = SQLAdaptor.ExecuteSP(Sp_Params);
            _Message = SQLAdaptor.ErrorMessage;
            return ds;
        }
        public ArrayList CopyArrayList(ArrayList sList)
        {
            ArrayList tList = new ArrayList();
            for (int i = 0; i < sList.Count; i++)
                tList.Add(sList[i]);
            return tList;
        }
        public ArrayList Build_SP_Param(ArrayList oList, string sParam)
        {
            return Build_SP_Param(oList, sParam, 0);
        }
        public ArrayList Build_SP_Param(ArrayList oList, string sParam, int iStart)
        {
            int i, m;
            string[] sALRow;
            string sKey, sVal;
            ArrayList aParamList = new ArrayList();
            string strParam = "|" + sParam;
            // ----- Replace Values --------------------------------------
            for (i = iStart; i < oList.Count; i++)
            {
                sALRow = Convert.ToString(oList[i]).Split(',');
                sVal = ""; sKey = sALRow[0].Replace("@", "");
                if (sALRow.Length > 1) sVal = sALRow[1];
                if (sALRow.Length > 1 && strParam.ToUpper().IndexOf("|" + sKey.ToUpper() + "~") >= 0)
                {
                    m = strParam.ToUpper().IndexOf("|" + sKey.ToUpper() + "~") + sKey.Length + 1;
                    sVal = sParam.Substring(m);
                    sVal = sVal.Split('|')[0];
                }
                if (sALRow.Length > 1)
                    aParamList.Add("@" + sKey + "," + sVal);
                else aParamList.Add(sKey);
            }
            // -----------------------------------------------------------

            // ----- Insert Values ---------------------------------------
            string[] aParam = sParam.Split('|');
            for (i = 0; i < aParam.Length; i++)
            {
                sALRow = Convert.ToString(aParam[i]).Split('~');
                if (sALRow.Length < 2 || sALRow[0].Trim() == "") continue;
                if (DoesArrayListHaveValue(aParamList, "@" + sALRow[0])) continue;
                aParamList.Add("@" + sALRow[0] + "," + sALRow[1].Replace(Convert.ToChar(7), '~'));
            }
            // -----------------------------------------------------------
            return aParamList;
        }
        public static bool DoesArrayListHaveValue(ArrayList aList, string sKey)
        {
            for (int ijk = 0; ijk < aList.Count; ijk++)
            {
                string[] xarr = aList[ijk].ToString().Split(',');
                if (xarr[0].ToUpper() == sKey.ToUpper())
                    return true;
            }
            return false;

        }
        public string GetArrayListValue(ArrayList aList, string sKey)
        {
            if (aList == null) return "";
            for (int ijk = 0; ijk < aList.Count; ijk++)
            {
                string[] xarr = aList[ijk].ToString().Split(',');
                if (xarr[0].ToUpper() == sKey.ToUpper())
                    return xarr[1];
            }
            return "";
        }
        public int GetArrayListNumValue(ArrayList aList, string sKey)
        {
            for (int ijk = 0; ijk < aList.Count; ijk++)
            {
                string[] xarr = aList[ijk].ToString().Split(',');
                if (xarr[0].ToUpper() == sKey.ToUpper())
                {
                    if (xarr[1].Trim() == "" || xarr[1].Trim().ToUpper() == "NULL") return 0;
                    else
                        return Convert.ToInt32(xarr[1]);
                }
            }
            return 0;
        }
        #endregion

        #region " Dictionary Manipulations "
        public Dictionary<string, ArrayList> Build_SP_ParamDictionary(Dictionary<string, ArrayList> oList, string sParam)
        {
            return Build_SP_ParamDictionary(oList, sParam, 0);
        }
        public Dictionary<string, ArrayList> Build_SP_ParamDictionary(Dictionary<string, ArrayList> oList, string sParam, int iStart)
        {
            string[] strVals = sParam.Split('|');

            for (int i = 0; i < strVals.Length; i++)
            {
                string[] strtemps = strVals[i].Split('~');

                if (strtemps.Length < 2 || strtemps[0].Trim() == "") continue;



                if (oList.ContainsKey("@" + strtemps[0]))
                {
                    ArrayList atemp = oList["@" + strtemps[0]];
                    if (strtemps.Length < 3)
                    {
                        atemp[1] = strtemps[1];
                    }
                    else
                    {
                        string sVal = "";
                        for (int iLoop = 1; iLoop < strtemps.Length; iLoop++)
                        {
                            sVal += ((sVal == "") ? "" : "~") + strtemps[iLoop];
                        }
                        atemp[1] = sVal;
                    }
                    oList["@" + strtemps[0]] = atemp;
                    //continue;
                }
                else
                {
                    ArrayList atemp;
                    if (strtemps.Length < 3)
                    {
                        atemp = ArrayLists.ToArrayList("@" + strtemps[0], strtemps[1], SqlDbType.VarChar);
                    }
                    else
                    {
                        string sVal = "";
                        for (int iLoop = 1; iLoop < strtemps.Length; iLoop++)
                        {
                            sVal += ((sVal == "") ? "" : "~") + strtemps[iLoop];
                        }
                        atemp = ArrayLists.ToArrayList("@" + strtemps[0], sVal, SqlDbType.VarChar);
                    }
                    oList["@" + strtemps[0]] = atemp;
                }

            }
            return oList;
        }
        public Dictionary<string, ArrayList> CopyDictionary(Dictionary<string, ArrayList> sList)
        {
            // Dictionary<string, ArrayList> tList = new Dictionary<string, ArrayList>();

            Dictionary<string, ArrayList> tList = new Dictionary<string, ArrayList>();
            if (sList == null)
                return tList;

            foreach (KeyValuePair<string, ArrayList> kvp in sList)
            {
                ArrayList paramvals = CopyArrayList((ArrayList)kvp.Value);
                tList.Add(paramvals[0].ToString(), paramvals);
            }
            return tList;
        }
        public Dictionary<string, string> CopyDictionary(Dictionary<string, string> sList)
        {

            Dictionary<string, string> tList = new Dictionary<string, string>();
            if (sList == null)
                return tList;

            foreach (KeyValuePair<string, string> kvp in sList)
            {
                tList.Add(kvp.Key.ToString(), kvp.Value);
            }
            return tList;
        }
        public string GetDictionaryValue(Dictionary<string, string> aDict, string sKey)
        {
            try { return Converts.ToString(aDict[sKey]); }
            catch { return ""; }
        }
        public int GetDictionaryNumValue(Dictionary<string, string> aDict, string sKey)
        { return (int)Converts.ToDouble(GetDictionaryValue(aDict, sKey)); }
        public string GetDictionaryValue(Dictionary<string, ArrayList> aDict, string sKey)
        {
            if (aDict.ContainsKey(sKey))
            {
                ArrayList temp = aDict[sKey];
                return Converts.ToString(temp[1]);
            }
            else
                return "";

        }
        public int GetDictionaryNumValue(Dictionary<string, ArrayList> aDict, string sKey)
        {
            if (aDict.ContainsKey(sKey))
            {
                ArrayList temp = aDict[sKey];
                return Convert.ToInt32(temp[1]);
            }
            else return 0;


        }
        public Dictionary<string, ArrayList> SetDictionaryValue(Dictionary<string, ArrayList> aDict, string sKey, string sValue)
        {
            return SetDictionaryValue(aDict, sKey, sValue, SqlDbType.VarChar);
            //ArrayList aList = ArrayLists.ToArrayList("@" + sKey, sValue, SqlDbType.VarChar);

            //try { aDict.Add("@" + sKey, aList); }
            //catch { aDict["@" + sKey] = aList; }

            //return aDict;
        }
        public Dictionary<string, ArrayList> SetDictionaryValue(Dictionary<string, ArrayList> aDict, string sKey, object sValue, SqlDbType _SqlDbType)
        {
            ArrayList aList = ArrayLists.ToArrayList("@" + sKey, sValue, _SqlDbType);

            try { aDict.Add("@" + sKey, aList); }
            catch { aDict["@" + sKey] = aList; }

            return aDict;
        }

        public Dictionary<string, string> SetDictionaryValue(Dictionary<string, string> aDict, string sKey, string sValue)
        {
            try { aDict.Add("@" + sKey, sValue); }
            catch { aDict["@" + sKey] = sValue; }

            return aDict;
        }
        public Dictionary<string, ArrayList> AddDictionaryValues(Dictionary<string, ArrayList> aDict, string AddOnCriteria)
        {
            if (Strings.IsNullOrEmpty(AddOnCriteria)) return aDict;

            string[] arrStr = AddOnCriteria.Split('&');
            for (int i = 0; i < arrStr.Length; i++)
            {
                string[] arrStr1 = arrStr[i].Split('=');
                if (arrStr1.Length > 1 && arrStr1[1].Trim() != "")
                { aDict = SetDictionaryValue(aDict, arrStr1[0].Trim(), arrStr1[1].Trim()); }
            }
            return aDict;
        }

        public Dictionary<string, string> MakeDictionary(string sString)
        {
            int i;
            string[] aRow, aCol;
            Dictionary<string, string> dParam = new Dictionary<string, string>();

            aRow = sString.Split('|');
            for (i = 0; i < aRow.Length; i++)
            {
                aCol = aRow[i].Split('~');
                if (aCol.Length > 1 && !Strings.IsNullOrEmpty(aCol[0]) && !Strings.IsNullOrEmpty(aCol[1]))
                    try { dParam.Add(aCol[0].ToLower(), aCol[1]); }
                    catch { dParam[aCol[0].ToLower()] = aCol[1]; }
            }
            return dParam;
        }

        public string Dictionary2String(Dictionary<string, string> aDict)
        {
            string sString = "";

            foreach (KeyValuePair<string, string> kvp in aDict)
            {
                sString += Convert.ToChar(27).ToString() + kvp.Key.Replace("@", "") + Convert.ToChar(7).ToString() + kvp.Value;
            }

            return sString;
        }
        public Dictionary<string, string> String2Dictionary(string sString)
        {
            Dictionary<string, string> aDict = new Dictionary<string, string>();
            if (Strings.IsNullOrEmpty(sString)) return aDict;

            string[] arrStr = sString.Split(Convert.ToChar(27));
            for (int i = 0; i < arrStr.Length; i++)
            {
                string[] arrStr1 = arrStr[i].Split(Convert.ToChar(7));
                if (arrStr1.Length > 1 && arrStr1[1].Trim() != "")
                { aDict.Add(arrStr1[0].Trim(), arrStr1[1].Trim()); }
            }
            return aDict;
        }

        public string Dictionary2String(Dictionary<string, ArrayList> aDict)
        {
            string sString = "";

            foreach (KeyValuePair<string, ArrayList> kvp in aDict)
            {
                sString += Convert.ToChar(27).ToString() + kvp.Key + Convert.ToChar(7).ToString() + kvp.Value[1];
            }

            return sString;
        }
        public Dictionary<string, ArrayList> String2ALDictionary(string sString)
        {
            Dictionary<string, ArrayList> aDict = new Dictionary<string, ArrayList>();
            if (Strings.IsNullOrEmpty(sString)) return aDict;

            string[] arrStr = sString.Split(Convert.ToChar(27));
            for (int i = 0; i < arrStr.Length; i++)
            {
                string[] arrStr1 = arrStr[i].Split(Convert.ToChar(7));
                if (arrStr1.Length > 1 && arrStr1[1].Trim() != "")
                {
                    aDict.Add(arrStr1[0].Trim(), ArrayLists.ToArrayList(arrStr1[0], arrStr1[1].Trim(), SqlDbType.VarChar));
                }
            }
            return aDict;
        }
        #endregion

        #region " Fill Dropdown List "
        //public void FillRadioList(System.Web.UI.WebControls.RadioButtonList rdl, string sFillString)
        //{
        //    FillRadioList(rdl, sFillString, null);
        //}
        public void FillRadioList(System.Web.UI.WebControls.RadioButtonList rdl, string sFillString, System.Web.UI.WebControls.HiddenField hd)
        {
            FillRadioList(rdl, sFillString, "SetRadioList(this.value,'" + hd.ClientID + "')");
        }
        public void FillRadioList(System.Web.UI.WebControls.RadioButtonList rdl, string sFillString, string jsOnClick)
        {
            DataTable dtc = new DataTable();
            dtc.Columns.Add("DisplayMember");
            dtc.Columns.Add("ValueMember");

            string[] cboArr = sFillString.Split('|');
            for (int j = 0; j < cboArr.Length; j++)
            {
                string[] cboItem = cboArr[j].ToString().Split('~');
                if (cboItem.Length > 1)
                    dtc.Rows.Add(cboItem[1], cboItem[0]);
                else
                    dtc.Rows.Add(cboItem[0], j.ToString());
            }
            dtc.AcceptChanges();

            rdl.DataSource = dtc;
            rdl.DataValueField = "ValueMember";
            rdl.DataTextField = "DisplayMember";
            rdl.DataBind();
            for (int i = 0; i < rdl.Items.Count; i++)
            {
                rdl.Items[i].Attributes.Add("onclick", jsOnClick);
            }
        }
        public void FillCheckBoxList(System.Web.UI.WebControls.CheckBoxList chkl, string sFillString)
        {
            DataTable dtc = new DataTable();
            dtc.Columns.Add("DisplayMember");
            dtc.Columns.Add("ValueMember");

            string[] cboArr = sFillString.Split('|');
            for (int j = 0; j < cboArr.Length; j++)
            {
                string[] cboItem = cboArr[j].ToString().Split('~');
                if (cboItem.Length > 1)
                    dtc.Rows.Add(cboItem[1], cboItem[0]);
                else
                    dtc.Rows.Add(cboItem[0], j.ToString());
            }
            dtc.AcceptChanges();

            chkl.DataSource = dtc;
            chkl.DataValueField = "ValueMember";
            chkl.DataTextField = "DisplayMember";
            chkl.DataBind();


        }
        public void FillDropDown(System.Web.UI.WebControls.DropDownList ddl, string sFillString)
        {
            DataTable dtc = new DataTable();
            dtc.Columns.Add("DisplayMember");
            dtc.Columns.Add("ValueMember");

            string[] cboArr = sFillString.Split('|');
            for (int j = 0; j < cboArr.Length; j++)
            {
                string[] cboItem = cboArr[j].ToString().Split('~');
                if (cboItem.Length > 1)
                    dtc.Rows.Add(cboItem[1], cboItem[0]);
                else
                    dtc.Rows.Add(cboItem[0], j.ToString());
            }
            dtc.AcceptChanges();

            ddl.DataSource = dtc;
            ddl.DataValueField = "ValueMember";
            ddl.DataTextField = "DisplayMember";
            ddl.DataBind();


        }


        public void FillDropDownSQL(System.Web.UI.WebControls.DropDownList ddl, string sQry)
        {
            if (sQry.Trim() == "") return;


            Dictionary<string, ArrayList> SP_paramsd = new Dictionary<string, ArrayList>();

            SP_paramsd.Add("@Qry", ArrayLists.ToArrayList("@Qry", sQry, SqlDbType.VarChar));

            DataSet dscombo = ExecuteData(spNames.ExecuteQry, SP_paramsd);

            if (!string.IsNullOrEmpty(Message))
            {
                _Status = Status;
                _Message = Message;
                return;
            }

            ddl.DataSource = dscombo.Tables[0];
            ddl.DataValueField = "MainID";
            ddl.DataTextField = "MainDescr";
            ddl.DataBind();
        }
        #endregion

        #region " Fix URL "
        public string FixJSText(string sVal)
        {
            sVal = sVal.Replace("\\", "\\\\");
            sVal = sVal.Replace("'", "\\'");
            sVal = sVal.Replace("'", "\\'");
            sVal = sVal.Replace("\r\n", "\\n");
            return sVal;
        }

        public string FixURL(string sVal, bool bRetreive = true)
        {
            if (string.IsNullOrEmpty(sVal))
                return "";
            int iLoop = 0;
            string sList = null;
            string[] aRow = null;
            string[] aVal = null;

            sList = "')COL()Q(" + ")ROW(" + "')COL()DQ(" + ")ROW(`)COL()RQ(" + ")ROW(" + "\\)COL()BS(" + ")ROW(" + "/)COL()FS(" + ")ROW(" + "#)COL()H(";
            // & ")ROW(" & "?)COL()QS("

            if (!bRetreive)
                sVal = sVal.Replace("\r\n", " ");

            aRow = sList.Split(new[] { ")ROW(" }, StringSplitOptions.None);
            for (iLoop = 0; iLoop <= aRow.Length - 1; iLoop++)
            {
                aVal = aRow[iLoop].Split(new[] { ")COL(" }, StringSplitOptions.None);
                if (aVal.Length < 2 || string.IsNullOrEmpty(aVal[0]) || string.IsNullOrEmpty(aVal[1]))
                    continue;
                if (bRetreive)
                {
                    sVal = sVal.Replace(aVal[1], aVal[0]);
                }
                else
                {
                    sVal = sVal.Replace(aVal[0], aVal[1]);
                }
            }
            return sVal;
        }
        #endregion

        #region " HTML Functions "
        public HtmlTableCell MakeTD(string sInnerHtml, bool bNoWrap, int iRowSpan, int iColSpan, string sWidth, string sAlign, string sVAlign, string sClass, string sAttribute, string sStyle)
        {
            HtmlTableCell td = new HtmlTableCell();

            if (sInnerHtml != "") td.InnerHtml = sInnerHtml;

            if (iRowSpan > 1) td.RowSpan = iRowSpan;
            if (iColSpan > 1) td.ColSpan = iColSpan;
            td.NoWrap = bNoWrap;

            if (sAlign != "") td.Align = sAlign;
            if (sVAlign != "") td.VAlign = sVAlign;

            if (sClass != "") td.Attributes.Add("class", sClass);

            int iLoop;
            string[] sRow, sCol;
            if (sStyle != "")
            {
                sRow = sStyle.Split(';');
                for (iLoop = 0; iLoop < sRow.Length; iLoop++)
                {
                    sCol = sRow[iLoop].Split(':');
                    if (sCol.Length < 2 || sCol[0] == "" || sCol[1] == "") continue;
                    try { td.Style.Add(sCol[0], sCol[1]); }
                    catch { }
                }
            }

            if (sAttribute != "")
            {
                sRow = sAttribute.Split(Convert.ToChar(27));
                for (iLoop = 0; iLoop < sRow.Length; iLoop++)
                {
                    sCol = sRow[iLoop].Split(Convert.ToChar(7));
                    if (sCol.Length < 2 || sCol[0] == "" || sCol[1] == "") continue;
                    try { td.Attributes.Add(sCol[0], sCol[1]); }
                    catch { }
                }
            }

            return td;
        }
        public HtmlTableCell MakeControlTD(Control Cntrl, bool bNoWrap, int iRowSpan, int iColSpan, string sWidth, string sAlign, string sVAlign, string sClass, string sAttribute, string sStyle)
        {
            HtmlTableCell td = new HtmlTableCell();

            if (Cntrl == null) td.InnerHtml = "";
            td.Controls.Add(Cntrl);

            if (iRowSpan > 1) td.RowSpan = iRowSpan;
            if (iColSpan > 1) td.ColSpan = iColSpan;
            td.NoWrap = bNoWrap;

            if (sAlign != "") td.Align = sAlign;
            if (sVAlign != "") td.VAlign = sVAlign;

            if (sClass != "") td.Attributes.Add("class", sClass);

            int iLoop;
            string[] sRow, sCol;
            if (sStyle != "")
            {
                sRow = sStyle.Split(';');
                for (iLoop = 0; iLoop < sRow.Length; iLoop++)
                {
                    sCol = sRow[iLoop].Split(':');
                    if (sCol.Length < 2 || sCol[0] == "" || sCol[1] == "") continue;
                    try { td.Style.Add(sCol[0], sCol[1]); }
                    catch { }
                }
            }

            if (sAttribute != "")
            {
                sRow = sAttribute.Split(Convert.ToChar(27));
                for (iLoop = 0; iLoop < sRow.Length; iLoop++)
                {
                    sCol = sRow[iLoop].Split(Convert.ToChar(7));
                    if (sCol.Length < 2 || sCol[0] == "" || sCol[1] == "") continue;
                    try { td.Attributes.Add(sCol[0], sCol[1]); }
                    catch { }
                }
            }

            return td;
        }

        public void ShowTableRow(string trID, bool bShow)
        {
            if (trID == "") return;
            HtmlTableRow tr = (HtmlTableRow)this.FindControl(trID);
            if (tr != null) tr.Visible = bShow;
        }
        public HtmlImage PutHtmlImage(string sSrc, string sAlt, string sOnClick)
        { return PutHtmlImage("", sSrc, sAlt, sOnClick, "", ""); }
        public HtmlImage PutHtmlImage(string sID, string sSrc, string sAlt, string sOnClick, string sStyle)
        { return PutHtmlImage(sID, sSrc, sAlt, sOnClick, sStyle, ""); }
        public HtmlImage PutHtmlImage(string sID, string sSrc, string sAlt, string sOnClick, string sStyle, string sAttribute)
        {
            HtmlImage img = new HtmlImage();
            int i;
            string[] aArr, aRow;

            img.ID = sID;
            img.Src = ResolveUrl(sSrc);
            if (!Strings.IsNullOrEmpty(sAlt))
            {
                img.Alt = sAlt;
                img.Attributes.Add("title", sAlt);
            }

            if (!Strings.IsNullOrEmpty(sOnClick))
            {
                img.Attributes.Add("onclick", sOnClick);
                img.Style.Add("cursor", "pointer");
            }

            if (!Strings.IsNullOrEmpty(sStyle))
            {
                aArr = sStyle.Split('|');
                for (i = 0; i < aArr.Length; i++)
                {
                    if (Strings.IsNullOrEmpty(aArr[i])) continue;
                    aRow = aArr[i].Split('~');
                    if (!Strings.IsNullOrEmpty(aRow[0]) && !Strings.IsNullOrEmpty(aRow[1]))
                        img.Style.Add(aRow[0], aRow[1]);
                }
            }

            if (!Strings.IsNullOrEmpty(sAttribute))
            {
                aArr = sAttribute.Split(Convert.ToChar(27));
                for (i = 0; i < aArr.Length; i++)
                {
                    if (Strings.IsNullOrEmpty(aArr[i])) continue;
                    aRow = aArr[i].Split(Convert.ToChar(7));
                    if (!Strings.IsNullOrEmpty(aRow[0]) && !Strings.IsNullOrEmpty(aRow[1]))
                        img.Attributes.Add(aRow[0], aRow[1]);
                }
            }

            return img;
        }
        public HtmlInputCheckBox CreateHtmlCheckbox(string sID, string sValue, bool bChecked, string sAttribute)
        {
            HtmlInputCheckBox chk = new HtmlInputCheckBox();
            chk.ID = sID;
            chk.Value = sValue;
            chk.Checked = bChecked;
            if (!Strings.IsNullOrEmpty(sAttribute))
                chk.Attributes.Add("onclick", sAttribute);
            return chk;
        }
        public HtmlInputHidden CreateHtmlHiddenField(string sID, string sValue)
        {
            HtmlInputHidden hd = new HtmlInputHidden();
            hd.ID = sID;
            hd.Value = sValue;

            return hd;
        }
        public HtmlSelect CreateHtmlSelect(string sID, string sItemList, string sSelectedValue, string sAttribute, string sClass, string sWidth, bool bEnable)
        {
            HtmlSelect cbo = new HtmlSelect();
            int i;
            string[] sArr = sItemList.Split(';');
            string[] sEle;
            cbo.ID = sID;
            ListItem lItem;
            for (i = 0; i < sArr.Length; i++)
            {
                sEle = sArr[i].Split('~');
                if (Strings.IsNullOrEmpty(sEle[0])) continue;
                if (sEle.Length < 2)
                { cbo.Items.Add(sArr[i]); continue; }

                lItem = new ListItem();
                lItem.Text = sEle[0];
                lItem.Value = sEle[1];
                if (!Strings.IsNullOrEmpty(sSelectedValue) && sEle[1] == sSelectedValue)
                    lItem.Selected = true;
                cbo.Items.Add(lItem);
            }

            if (cbo.Items.Count > 1 && cbo.SelectedIndex < 0) cbo.SelectedIndex = 0;
            cbo.Disabled = !bEnable || cbo.Items.Count < 2;
            if (!Strings.IsNullOrEmpty(sAttribute) && bEnable && cbo.Items.Count > 1) cbo.Attributes.Add("onchange", sAttribute);
            if (!Strings.IsNullOrEmpty(sClass)) cbo.Attributes.Add("class", sClass);
            if (!Strings.IsNullOrEmpty(sWidth)) cbo.Style.Add("width", sWidth);

            return cbo;
        }
        
        #endregion

        #region " Grid View  "
        public BoundField CreateBoundField(string DataField, string HeaderText, string ColWidth, HorizontalAlign Align, bool IsVisible, bool IsWrappable, string GroupHeaderText)
        {
            System.Web.UI.WebControls.UnitConverter C = new UnitConverter();

            BoundField field = new BoundField();
            field.DataField = DataField;
            field.HeaderText = HeaderText;
            field.ItemStyle.Width = (System.Web.UI.WebControls.Unit)C.ConvertFromString(ColWidth);
            field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            field.ItemStyle.HorizontalAlign = Align;
            field.SortExpression = DataField;
            field.Visible = IsVisible;
            field.ItemStyle.Wrap = IsWrappable;

            return (BoundField)field;
        }
        public BoundField C1Grid_CreateBoundField(string DataField, string HeaderText, string ColWidth, HorizontalAlign Align, bool IsVisible, bool IsWrappable)
        {
            return CreateBoundField(DataField, HeaderText, ColWidth, Align, IsVisible, IsWrappable,  null);
        }

        public TemplateField CreateTemplateField(string HeaderText, string ColWidth, HorizontalAlign Align, bool IsVisible, bool IsWrappable)
        {

            System.Web.UI.WebControls.UnitConverter C = new UnitConverter();


            TemplateField field = new TemplateField();

            field.HeaderText = HeaderText;
            field.HeaderStyle.Wrap = false;
            field.ItemStyle.Width = (System.Web.UI.WebControls.Unit)C.ConvertFromString(ColWidth);
            field.ItemStyle.HorizontalAlign = Align;
            field.ItemStyle.Wrap = IsWrappable;
            field.Visible = IsVisible;
            return (TemplateField)field;

        }
        #endregion
        public string BuildFormParamToURL()
        {
            FormParamToUrl = "";

            foreach (KeyValuePair<string, string> kvp in _TempParam)
            {
                if ("__MyOptionID__".IndexOf(kvp.Key) < 0)
                {
                    FormParamToUrl += "&" + kvp.Key + "=" + kvp.Value;
                }
            }
            return FormParamToUrl;
        }

    }
}
