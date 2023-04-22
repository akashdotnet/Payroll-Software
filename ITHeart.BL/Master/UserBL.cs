using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.DataType;
using Interop.SqlServer.SQLTask;

namespace ITHeart.BL 
{
    public class UserBL: Common
    {

        #region " User Properties "
        /// <summary>
        /// Loads User Properties 
        /// <param name="UserID">The user ID.</param>
        /// <param name="Password">The password.</param>
        /// </summary>
        /// <returns></returns>
        public bool UserLogIn(string LoginID, string Password, string CommunityCode)
        {
            string sVal, sLoginID, sUserID = "";
            sLoginID = Community.Encrypt("", LoginID);   //Community.Encrypt("FSL Software Technologies Ltd.", LoginID);
            
            // --- Load User Info ----------------------------------------------
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam = SetDictionaryValue(spParam, "LoginID", sLoginID);
            spParam = SetDictionaryValue(spParam, "Password", Community.Encrypt(LoginID, Password));
            if(SaaS) spParam = SetDictionaryValue(spParam, "CommunityAbbrv", CommunityCode);
            spParam = SetDictionaryValue(spParam, "SaaS", (SaaS ? "1" : "0"));
            spParam = SetDictionaryValue(spParam, "TimeDiff", TimeDiff, SqlDbType.Float);
            DataSet ds = ExecuteData(spNames.Validate_User_logging_in, spParam);
            // ------------------------------------------------------------------

            // --- If Load has Error --------------------------------------------
            if (_Status != 0 || _Message != "" || ds == null || ds.Tables.Count < 1)
            { if(Strings.IsNullOrEmpty(_Message)) _Message = "Incorrect UserID and/or Password"; return false; }
            // ------------------------------------------------------------------

            // --- If UserID / Password Incorrect -------------------------------
            if (ds.Tables[0].Rows.Count < 1)
            { _Status = 2001; _Message = "Incorrect UserID and/or Password"; return false; }
            // ------------------------------------------------------------------


            //For Validate IP FOR LOGIN Added By Niraj : 03-July-2015

            string strSysIp = "", strAssignedIP="";
            //strSysIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //strSysIp = GetUserHostAddress(); //Get Current System
            strSysIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //strSysIp = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            if (strSysIp == null)
            {
                strSysIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            strAssignedIP = ds.Tables[0].Rows[0]["AssignedIP"].ToString();

            if (!string.IsNullOrEmpty(strAssignedIP)) // If AssignIP is Null that means user Can access his account from every where.
            {
                //if (strSysIp != ds.Tables[0].Rows[0]["AssignIP"].ToString())// Validating IP
                //{
                //    _Status = 2001; _Message = "Invalid IP - System IP are not matching to your Assigned IP -Please contact your administrator"; return false;
                //}
                if (!strAssignedIP.Contains(strSysIp))
                {
                    _Status = 2001; _Message = "Invalid IP - System IP are not Matching to your Assigned IP - Please contact your System Administrator"; return false;
                }

            }

            //-------------------------End IP Validation-------------------------------------------------------

            // --- Initialize User Info -----------------------------------------
            if (HttpContext.Current.Session["User"] == null)
            { _UserInfo = new FSL.Web.User.UserClass(); }
            else
            { _UserInfo = (FSL.Web.User.UserClass)HttpContext.Current.Session["User"] ; }
            sUserID = Converts.ToString(ds.Tables[0].Rows[0]["ID"]);
            // ------------------------------------------------------------------

            // -- Set Global Variables ------------------------------------------
            _UserInfo.LogonMessage = "";
            _UserInfo.SessionID = HttpContext.Current.Session.SessionID;
            // ------------------------------------------------------------------

            // --- Initialize User & Community Properties -----------------------
            _UserInfo.Initialize(ds, SaaS);
            // ------------------------------------------------------------------

            // --- Check User & Community Lease; & Pwd Expiry -------------------
            sVal = CheckLease(ds.Tables[0].Rows[0]);
            if (!string.IsNullOrEmpty(sVal))
            { _UserInfo = (FSL.Web.User.UserClass)null; _Status = 2011; _Message = sVal; return false; }
            // ------------------------------------------------------------------

            // -- Create Login Log ----------------------------------------------
            UpdateLoginDatabaseLog(GetUserHostAddress(), false, true);
            UpdateLoginApplicationLog();
            // ------------------------------------------------------------------

            HttpContext.Current.Session["User"] = UserInfo;

            return true;
        }
        #endregion

        #region " Lease Expiry & Deactivations -  Users & Communities "
        /// <summary>
        /// Lease Expiry & Deactivations -  Users & Communities
        /// </summary>
        /// <param name="drUser">The dr user.</param>
        /// <returns></returns>
        private string CheckLease(DataRow drUser)
        {
            string strMessage = "", strSuffix = "";

            #region " Check User License "
            // == Check User License =========================================================================================
            // -- Build Global Message for User --------------------------------------
            strSuffix = "Please contact your Administrator at "
                + Converts.ToString(GetCommunityHeaderProperty(FrontCommon.PropertyType.UserInfo | FrontCommon.PropertyType.SystemInfo, "CompanyName"))
                + " to re-activate your account for "
                + base.ProjectDetails(FSL.Web.Business.FrontCommon.ProjectInfoTypeEnum.ProjectName);
            // -----------------------------------------------------------------------

            // -- Check If User id Disabled ------------------------------------------
            if (!Converts.ToBoolean(drUser["recEnable"]))
            {
                strMessage += "Account is Locked-out." + Convert.ToChar(13).ToString() + Convert.ToChar(13).ToString() + strSuffix;
                return strMessage;
            }
            // ----------------------------------------------------------------------------

            // -- Check If User Lease is not Expired ---------------------------------
            DateTime dt = DateTime.Now.AddYears(1);
            try { dt = Convert.ToDateTime(drUser["UserLease"]); }
            catch { }
            if (dt.Subtract(DateTime.Now).Days < 0)
            {
                strMessage += "Lease is expired for the Account on " + dt.ToString("dd-MMM-yyyy")
                    + Convert.ToChar(13).ToString() + Convert.ToChar(13).ToString() + strSuffix;
                return strMessage;
            }
            // -----------------------------------------------------------------------

            // -- Check If User can work on WEB --------------------------------------
            //if (!Converts.ToBoolean(drUser["WebLogon"])) 
            //{
            //    strMessage += "Dear " + Converts.ToString(drUser["UserName"]) + " you don't have right to access ComplianceMantra on Web." + Convert.ToChar(13).ToString() + Convert.ToChar(13).ToString();

            //    return strMessage;
            //}
            // -----------------------------------------------------------------------
            // ===============================================================================================================
            #endregion

            #region " Check Password Expiry "
            // == Check Password Expiry ======================================================================================
            strMessage = CheckUserPasswordExpiry(true);
            if (!string.IsNullOrEmpty(strMessage)) return strMessage;
            // ===============================================================================================================
            #endregion

            #region " Check Community License "
            if (!SaaS || UserInfo.CommunityID == "") return "";

            // == Check Community License ====================================================================================
            // -- Check If Community is Disabled -------------------------------------
            if (!Converts.ToBoolean(GetCommunityHeaderProperty(PropertyType.UserInfo | PropertyType.SystemInfo, "recEnable"))
                || Converts.ToDouble(GetCommunityHeaderProperty(PropertyType.UserInfo | PropertyType.SystemInfo, "Status")) > 0)
            {
                strSuffix = "Please contact Web Master at "
                    + base.ProjectDetails(FSL.Web.Business.FrontCommon.ProjectInfoTypeEnum.CompanyName)
                    + " to re-activate your account for "
                    + base.ProjectDetails(FSL.Web.Business.FrontCommon.ProjectInfoTypeEnum.ProjectName);

                strMessage += "Your Community, " + Converts.ToString(GetCommunityHeaderProperty(PropertyType.UserInfo | PropertyType.SystemInfo, "CompanyName")) + ","
                    + " is Locked." + Convert.ToChar(13).ToString() + Convert.ToChar(13).ToString()
                    + strSuffix;
                return strMessage;
            }
            // -----------------------------------------------------------------------

            // -- Check If Community Payment Details ---------------------------------
            try { dt = Convert.ToDateTime(GetCommunityPaymentDueDate(FrontCommon.PropertyType.UserInfo | FrontCommon.PropertyType.LicenseInfo, true)); }
            catch { }
            if (dt.Subtract(DateTime.Now).Days < 0)
            {
                try { dt = Convert.ToDateTime(GetCommunityPaymentDueDate(FrontCommon.PropertyType.UserInfo | FrontCommon.PropertyType.LicenseInfo, false)); }
                catch { }
                strSuffix = "Please contact Web Master at "
                    + base.ProjectDetails(FSL.Web.Business.FrontCommon.ProjectInfoTypeEnum.CompanyName)
                    + " to renew your license for "
                    + base.ProjectDetails(FSL.Web.Business.FrontCommon.ProjectInfoTypeEnum.ProjectName);

                strMessage += "Payment for the Community, " + Converts.ToString(GetCommunityHeaderProperty(PropertyType.UserInfo | PropertyType.SystemInfo, "CompanyName")) + ","
                    + " is overdue since " + dt.ToString("dd-MMM-yyyy") + Convert.ToChar(13).ToString() + Convert.ToChar(13).ToString()
                    + strSuffix;
                return strMessage;
            }
            // -----------------------------------------------------------------------
            // ===============================================================================================================
            #endregion

            return "";
        }
        #endregion

        #region " Change Password "

        public bool SetUserPassword(string sUserID, string sCurrentPwd, string sNewPwd, bool bAskCurrentPassword)
        {
            DataSet ds;
            string sLogin, sVal;

            #region " Validate Current Password "
            // --- Load User Info ----------------------------------------------
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@UserID", ArrayLists.ToArrayList("@UserID", sUserID, SqlDbType.VarChar));
            ds = ExecuteData(spNames.Load_UserInfoForPassword, spParam);
            // ------------------------------------------------------------------

            // --- If Load has Error --------------------------------------------
            if (_Status != 0 || _Message != "" || ds == null || ds.Tables.Count < 1)
            { return false; }
            // ------------------------------------------------------------------

            // ---- Decrypt the Password -------------------------------------
            sLogin = Converts.ToString(ds.Tables[0].Rows[0]["LoginName"]);
            sVal = Converts.ToString(ds.Tables[0].Rows[0]["Password"]);
            sLogin = Community.Decrypt("", sLogin);
            // ------------------------------------------------------------------
            #endregion


            #region " Validate Current Password "
            // --- Password Incorrect -------------------------------
            if (bAskCurrentPassword && sCurrentPwd != Community.Decrypt(sLogin, sVal))
            { _Message = "Incorrect Old Password!"; return false; }
            //---------------------------------------------------------
            #endregion

            #region " Save New Password "
            // ---- Encrypt the Password ------------------------------
            sNewPwd = Community.Encrypt(sLogin, sNewPwd);
            //---------------------------------------------------------

            // ---- Update Password -----------------------------------
            sVal = "Password = '" + sNewPwd.Replace("'", "''") + "', ";
            int iPwdExpiryDays = (int)Converts.ToDouble(GetCommunityHeaderProperty(FrontCommon.PropertyType.CommunityInfo | FrontCommon.PropertyType.UserInfo | FrontCommon.PropertyType.SystemInfo, "PwdExpiryDays"));
            if (iPwdExpiryDays <= 0) sVal += " PwdExpiryDate = Null, ";
            else if (sUserID != UserID)
                sVal += " PwdExpiryDate = getdate(), ";
            else
                sVal += " PwdExpiryDate = DateAdd(d, " + iPwdExpiryDays.ToString() +", getdate()), ";
            sVal += " GraceLogin=0, recUser='" + UserID + "', recDt=getdate() ";
            
            sVal = SQLAdaptor.UpdateInfo("UserMaster", sVal, "ID = '" + sUserID + "' ");
            if (sVal != "")
            { _Message = "Password could not be changed!"; return false; }
            SetUserInfoGlobalValue("PwdExpiryDate", null);
            SetUserInfoGlobalValue("GraceLogin", 0);
            //---------------------------------------------------------
            #endregion
            _Message = ""; 
            return true;
        }
        #endregion
        
        #region "User Profile"
        public bool Save_UserProfile(string sUserID, string sUserName, string sDesg, string sEmail, string sMobile, string sPhone,
            string LoginText, string Role, string recEnable, string Reporting, string Group, string Company, string Department, string password, 
            string LeaseTill, string sAssignedIP, int ReProcessRights)
        {

            //------------ Update User Info -----------------------------------------------
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam = SetDictionaryValue(spParam, "ID", sUserID);
            spParam = SetDictionaryValue(spParam, "LocId", LocID);

            spParam = SetDictionaryValue(spParam, "UserName", sUserName);
            spParam = SetDictionaryValue(spParam, "Email", sEmail);
            spParam = SetDictionaryValue(spParam, "Desg", ((sDesg == "") ? null : sDesg));
            spParam = SetDictionaryValue(spParam, "Mobile", ((sMobile == "") ? null : sMobile));
            spParam = SetDictionaryValue(spParam, "Phone", ((sPhone == "") ? null : sPhone));

            spParam = SetDictionaryValue(spParam, "CommunityID", ((GetDashboardGlobal("CommunityID") == "") ? null : GetDashboardGlobal("CommunityID")));

            spParam = SetDictionaryValue(spParam, "LoginName", ((LoginText == "") ? null : LoginText));
            spParam = SetDictionaryValue(spParam, "RoleId", ((Role == "") ? null : Role));
            spParam = SetDictionaryValue(spParam, "CompGroup", ((Group == "") ? null : Group));
            spParam = SetDictionaryValue(spParam, "CompanyApplicable", ((Company == "") ? null : Company));
            spParam = SetDictionaryValue(spParam, "DepartmentApplicable", ((Department == "") ? null : Department));
            spParam = SetDictionaryValue(spParam, "UserLease", ((LeaseTill == "") ? null : LeaseTill));
            spParam = SetDictionaryValue(spParam, "ParentID", ((Reporting == "") ? null : Reporting));
            spParam = SetDictionaryValue(spParam, "Password", ((password == "") ? null : password));
            spParam = SetDictionaryValue(spParam, "AssignedIP", ((sAssignedIP == "") ? null : sAssignedIP));

            spParam = SetDictionaryValue(spParam, "recEnable", ((recEnable == "") ? null : recEnable));
            spParam = SetDictionaryValue(spParam, "ReprocessUserRight", ReProcessRights.ToString());


            spParam = SetDictionaryValue(spParam, "recUser", UserID);
            spParam = SetDictionaryValue(spParam, "EditionID", Converts.ToString(GetCommunityHeaderProperty(PropertyType.CommunityInfo | PropertyType.UserInfo | PropertyType.LicenseInfo, "edition")));

            DataSet ds = ExecuteData(spNames.UserProfile_Save, spParam, false, false, true);
            if (_Status != 0 || _Message != "" || ds == null)
            { return false ; }
            // ------------------------------------------------------------------

            return true;
        }

        #endregion

        #region " Recover Forgotten Password "
        public void RecoverForgottenPassword(string CommunityCode, string UserID, string Email, string Mobile, Int16 iMode)
        {
            _Status = 0; _Message = "";

            string sTable, sFilter, sFields, sLogin, sPassword;
            DataTable dtInfo;

            #region " Validate User Info "
            // --- Validate User Info ------------------------------------------
            sTable = "UserMaster u Left Join CM_CommunityHeader c on c.ID = u.CommunityID"; 
            sFilter = "1=1"; 
            sFields = "u.ID, u.UserName, u.LoginName, u.[Password], ISNULL(u.Email,'') As Email, ISNULL(u.Mobile,'') As Mobile, u.recEnable, "
                + " u.CommunityID, ISNULL(c.Abbrv,'') As CommunityAbbrv, ISNULL(c.CompanyName,'') As CommunityName, ISNULL(c.recEnable,0) As CommunityEnable";
            if (!string.IsNullOrEmpty(UserID))
                sFilter += "And u.LoginName = '" + Community.Encrypt("", UserID.Trim()).Replace("'", "''") + "' ";
            else if (!string.IsNullOrEmpty(Email))
                sFilter += "And u.Email = '" + Email.Trim().Replace("'", "''") + "' ";
            else if (!string.IsNullOrEmpty(Mobile))
                sFilter += "And u.Mobile = '" + Mobile.Trim().Replace("'", "''") + "' ";
            else
            { _Status = -1; _Message = "Please specify either your user id, email address, or registered phone number."; return; }

            if (!string.IsNullOrEmpty(CommunityCode))
            { sFilter += " And ISNULL(c.Abbrv,'')='" + CommunityCode + "'"; }

            dtInfo = SQLAdaptor.RetriveTableInfo(sTable, sFilter, sFields);
            if (dtInfo == null || dtInfo.Rows.Count < 1)
            { _Status = -10; _Message = "User is not defined for the provided credentials."; return; }
            // -----------------------------------------------------------------

            // --- Validate ----------------------------------------------------
            if (!Converts.ToBoolean(dtInfo.Rows[0]["recEnable"]))
            { _Status = -11; _Message = "User account is locked."; return; }
            if(Converts.ToString(dtInfo.Rows[0]["CommunityID"]) != "" && !Converts.ToBoolean(dtInfo.Rows[0]["CommunityEnable"]))
            { _Status = -11; _Message = "Community is locked."; return; }
            // -----------------------------------------------------------------
            
            sLogin = Converts.ToString(dtInfo.Rows[0]["LoginName"]);
            sPassword = Converts.ToString(dtInfo.Rows[0]["Password"]);
            sLogin = Community.Decrypt("", sLogin).Trim();
            sPassword = Community.Decrypt(sLogin, sPassword).Trim();
            #endregion

            #region " Send SMS "
            // --- Send SMS ----------------------------------------------------
            if (iMode > 0)
            {
                if (string.IsNullOrEmpty(Converts.ToString(dtInfo.Rows[0]["Mobile"])))
                { _Status = -51; _Message = "The associated user account does not have a registed mobile number.<br />Cannot recover password via SMS.<br />Please contact your Administrator."; return; }

                sFields = "ComplianceMantra";
                sFilter = Converts.ToString(dtInfo.Rows[0]["CommunityAbbrv"]).Trim();
                if (!string.IsNullOrEmpty(sFilter))
                { sFields += (string.IsNullOrEmpty(sFields) ? "" : "\n") + " Code: " + sFilter; }

                sFields += (string.IsNullOrEmpty(sFields) ? "" : "\n") + " Login ID: " + sLogin;
                sFields += (string.IsNullOrEmpty(sFields) ? "" : "\n") + " Password: " + sPassword;

                sTable = Converts.ToString(dtInfo.Rows[0]["Mobile"]).Trim();

                if (!SendSMS(Converts.ToString(dtInfo.Rows[0]["CommunityID"]), sTable, sFields))
                { _Status = -56; _Message = "SMS could not be sent to the registed mobile number." + "<br />" + _Message; return; }

                _Status = 0; _Message = "SMS has been sent to the registed mobile number."; return;
            }
            // -----------------------------------------------------------------
            #endregion

            #region " Send via Email "
            // --- Send via Email ----------------------------------------------
            if (string.IsNullOrEmpty(Converts.ToString(dtInfo.Rows[0]["Email"])))
            { _Status = -41; _Message = "The associated user account does not have a registed email address.<br />Cannot recover password via Email.<br />Please contact your Administrator."; return; }

            if (!SendForgottenPasswordMail(Converts.ToString(dtInfo.Rows[0]["CommunityAbbrv"]), Converts.ToString(dtInfo.Rows[0]["CommunityName"]), 
                Converts.ToString(dtInfo.Rows[0]["UserName"]), sLogin, sPassword, Converts.ToString(dtInfo.Rows[0]["Email"])))
            { _Status = -46; return; }

            UpdateCommunityConsumption(Converts.ToString(dtInfo.Rows[0]["CommunityID"]), 0, 1);
            _Status = 0; _Message = "Email has been sent to the registed Email address.";
            return;
            // -----------------------------------------------------------------
            #endregion

        }
        public bool SendForgottenPasswordMail(string sCommunityCode, string sCommunityName, string sUserName, string sLoginName, string sPassword, string sEmail)
        {
            string strSubject, strMessage, sAttachment;

            strSubject = "Lost your way at " + Community.getLicenceProperties("ProductName");

            #region " Build Mail Text "
            // --- Send via Email ----------------------------------------------
            strMessage = "<html>";
            strMessage += "<body>";
            strMessage += "<TABLE cellSpacing='0' cellPadding='1' width='100%' border='0'>";
            strMessage += "<TR>";
            strMessage += "<TD align='left' valign='top'>";
            strMessage += "<P align='left'>";
            strMessage += "<Font face='Arial' size='2'>";
            strMessage += "Dear <strong>" + sUserName + "</strong>,";
            if (!string.IsNullOrEmpty(sCommunityName))
            { strMessage += "<br /><strong>" + sCommunityName + "</strong>"; }
            strMessage += "</Font></p>";

            strMessage += "<P align='justify'><Font face='Arial' size='2'>" +
                "This mail is in your mail box because you, or someone else " +
                " who had registered with " + Community.getLicenceProperties("ProductName") + ", " +
                " and had supplied this e-mail address, has requested a reminder " +
                " of his or her login details.</Font></p>";

            strMessage += "<P align='Left'><Font face='Arial' size='2'>" +
               "Please note these login details.<br>";
            if (!string.IsNullOrEmpty(sCommunityCode))
                strMessage += "&nbsp;&nbsp;Code&nbsp;&nbsp;&nbsp;&nbsp;: <Font color='red'>" + sCommunityCode + "</Font><br>";
            strMessage += "&nbsp;&nbsp;UserID&nbsp;&nbsp;&nbsp;: <Font color='red'>" + sLoginName + "</Font><br>" +
               "&nbsp;&nbsp;Password&nbsp;: <Font color='red'>" + sPassword + "</Font><br>" +
                "</P>";

            strMessage += "<P align='Left'><Font face='Arial' size='2'>" +
                 "After a successful log in, you may change the password, " +
                 "as well as the e-mail address associated with this user ID, " +
                 "by clicking on your Profile.</P>";


            strMessage += "<P align='left'>" +
                 "<Font face='Arial' size='2'>Thank you & Regards.</Font><br>" +
                 "<Font face='Arial' size='2'><strong>Customer Service</strong></Font><br>" +
                "<Font face='Arial' size='2'><strong>" + Community.getLicenceProperties("ProductName") + "</strong></Font><br>" +
                 "</P>";

            strMessage += "<a target='_blank' href='http://www.ComplianceMantra.com'><img id='imgCompliance1' src=CommonBL.ResolveUrl('Images/compliancelogo.gif') hspace='0' vspace='0' alt='Comliance Mantra' border='0'/></a><br>";

            strMessage += "</TD>";
            strMessage += "</TR>";
            strMessage += "</TABLE>";
            strMessage += "</body>";
            strMessage += "</html>";
            // -----------------------------------------------------------------
            #endregion

            sAttachment = ProjectDetails(FrontCommon.ProjectInfoTypeEnum.pathApplicationPath) + "/Images/compliancelogo.gif";

            if (!SendEmail(strSubject, strMessage, sEmail, sAttachment))
            {
               
                return false;
            }

            return true;
        }
        #endregion

        #region " Contact Us "
        public bool SendContactUsMail(string ContactName, string ContactMail, string ContactPhone, string ContactSubject, string ContactMessage, string Email)
        {
            string strSubject, strMessage, strAttachments;
            
            strSubject = ContactName + " has left a message with subject " + ContactSubject;

            strMessage = "<html>";
            strMessage += "<body>";
            strMessage += "<TABLE cellSpacing='0' cellPadding='1' width='100%' border='0'>";
            strMessage += "<TR>";
            strMessage += "<TD align='left' valign='top'>";
            strMessage += "<P align='left'>";
            strMessage += "<Font face='Arial' size='2'>";

            strMessage += "<P align='justify'><Font face='Arial' size='2'>" +
                ContactName + " has visited www.ComplianceMantra.com and has left the following message " +
                "<P align='center'><Font face='Arial' size='3'>" +
                Convert.ToChar(34) + ContactMessage + Convert.ToChar(34) +
               " </Font><Font face='Arial' size='2'> <BR> his or her Email ID is " + ContactMail +
               ((ContactPhone.Trim() != "") ? " and contact number is " + ContactPhone + "</Font></p>" : "");

            strMessage += "<a target='_blank' href='http://www.ComplianceMantra.com'><img  id='imgCompliance1' src=CommonBL.ResolveUrl('Images/compliancelogo.gif') hspace='0' vspace='0' alt='" + Community.getLicenceProperties("ProductName") + "' border='0'/></a><br>";
            strMessage += "</TD>";
            strMessage += "</TR>";
            strMessage += "</TABLE>";
            strMessage += "</body>";
            strMessage += "</html>";
            //  '--------------------------------------------
            strAttachments = ProjectDetails(FrontCommon.ProjectInfoTypeEnum.pathApplicationPath) + "/Images/compliancelogo.gif";

            if (!SendEmail(strSubject, strMessage, Email, strAttachments)) return false;

            _Message = "An Email is sent to the concerned person. We will get back to you shortly";
 
            return true;
        }

        #endregion 


        #region "Send Mail After User Registered Successful "
        /// <summary>
        /// Send the mail after the user is registered
        /// </summary>
        public void sendMailAfterRegistration(string strEmail, string strUserName, string strLoginName, string strPassword,string pUserID)
        {
            string strpUserIDEncrypted;
            bool blMailAfterRegistration;
            SendingMail Mail = new SendingMail();
            strpUserIDEncrypted = Community.Encrypt("FSL Software Technologies Ltd.", pUserID);
            blMailAfterRegistration = Mail.SendMailToRegisteredUser(strEmail, strUserName, strLoginName, strPassword, strpUserIDEncrypted);
        }
        #endregion
        
        #region " Date Time Code "
        public bool UpdateDateTimeConfig(string UserID, string TimeZone1ID, string TimeZone2ID, string TimeZone3ID)
        {
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@UserID ", ArrayLists.ToArrayList("@UserID ", UserID, SqlDbType.VarChar));
            spParam.Add("@TimeZone1ID ", ArrayLists.ToArrayList("@TimeZone1ID ", ((TimeZone1ID == "") ? null : TimeZone1ID), SqlDbType.VarChar));
            spParam.Add("@TimeZone2ID ", ArrayLists.ToArrayList("@TimeZone2ID ", ((TimeZone2ID == "") ? null : TimeZone2ID), SqlDbType.VarChar));
            spParam.Add("@TimeZone3ID ", ArrayLists.ToArrayList("@TimeZone3ID ", ((TimeZone3ID == "") ? null : TimeZone3ID), SqlDbType.VarChar));
            spParam.Add("@recUser", ArrayLists.ToArrayList("@recUser", UserID, SqlDbType.VarChar));


            DataSet dsDateTimeConfig = base.ExecuteData(spNames.Update_UserDateTimeConfig, spParam, false, false, true);
            if (base.Status != 0 && base.Message != "")
            {
                _Status = base.Status;
                _Message = base.Message;
                return false;

            }

            return true;

        }


        #endregion

        #region "User Master Functions-Insert, Delete, status change, Load, Save"

        public DataSet UserAttributes_Load(string primaryId)
        {
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@Usermaster_Id", ArrayLists.ToArrayList("@Usermaster_Id", primaryId, SqlDbType.VarChar));
            spParam.Add("@strColumns", ArrayLists.ToArrayList("@strColumns", null, SqlDbType.VarChar));

            DataSet ds = base.ExecuteData(spNames.Load_All_Attributes_of_A_User, spParam, false, false, true);
            if (base.Status != 0 && base.Message != "" || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds;
        }

        //public DataSet UserDetailsBasedonCommunity_Load(string CommunityID)
        //{
        //    Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
        //    spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.VarChar));
            

        //    DataSet ds = base.ExecuteData(spNames.Load_UserMasterNav, spParam, false, false, true);
        //    if (base.Status != 0 && base.Message != "" || ds == null)
        //    {
        //        _Status = base.Status;
        //        _Message = base.Message;
        //        return null;
        //    }
        //    return ds;
        //}


        public string UserDetail_Save(string pRowID, string sProcedureNm, int Admin, string type, string UserLease, int Active, 
                                        string _ParentID, string LoginName, string Password, string UserName, string Email,
                                        string Desg, string GroupId, string CompanyId, string CommunityId, string Mobile, string Phone)
        {
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@IsAdmin", ArrayLists.ToArrayList("@IsAdmin", Admin, SqlDbType.TinyInt));
            spParam.Add("@WebLogon", ArrayLists.ToArrayList("@WebLogon", 1, SqlDbType.TinyInt));
            spParam.Add("@Status", ArrayLists.ToArrayList("@Status", string.Empty, SqlDbType.Int));
            spParam.Add("@Type", ArrayLists.ToArrayList("@Type", type, SqlDbType.Int));
            if (UserLease != "")
                spParam.Add("@UserLease", ArrayLists.ToArrayList("@UserLease", UserLease, SqlDbType.DateTime));

            spParam.Add("@recDirty", ArrayLists.ToArrayList("@recDirty", 1, SqlDbType.Bit));
            spParam.Add("@recEnable", ArrayLists.ToArrayList("@recEnable", Active, SqlDbType.Bit));
            spParam.Add("@recDel", ArrayLists.ToArrayList("@recDel", 0, SqlDbType.Bit));
            spParam.Add("@Parentid", ArrayLists.ToArrayList("@Parentid", _ParentID, SqlDbType.VarChar));
            spParam.Add("@LoginName", ArrayLists.ToArrayList("@LoginName", Community.Encrypt("", LoginName), SqlDbType.VarChar));
            
            if(Password != "")
               spParam.Add("@Password", ArrayLists.ToArrayList("@Password", Community.Encrypt(LoginName, Password), SqlDbType.VarChar));

            spParam.Add("@UserName", ArrayLists.ToArrayList("@UserName", UserName, SqlDbType.VarChar));
            spParam.Add("@Email", ArrayLists.ToArrayList("@Email", Email, SqlDbType.VarChar));
            spParam.Add("@fContactId", ArrayLists.ToArrayList("@fContactId", string.Empty, SqlDbType.VarChar));
            spParam.Add("@Desg", ArrayLists.ToArrayList("@Desg", Desg, SqlDbType.VarChar));
            spParam.Add("@ID", ArrayLists.ToArrayList("@ID", pRowID, SqlDbType.Char));
            spParam.Add("@ContactType", ArrayLists.ToArrayList("@ContactType", string.Empty, SqlDbType.Char));
            spParam.Add("@LocId", ArrayLists.ToArrayList("@LocId", "DEL", SqlDbType.Char));
            spParam.Add("@RoleId", ArrayLists.ToArrayList("@RoleId", string.Empty, SqlDbType.Char));
            spParam.Add("@DeptID", ArrayLists.ToArrayList("@DeptID", string.Empty, SqlDbType.Char));
            spParam.Add("@recUser", ArrayLists.ToArrayList("@recUser", UserID, SqlDbType.Char));
            spParam.Add("@EmpID", ArrayLists.ToArrayList("@EmpID", string.Empty, SqlDbType.Char));
            spParam.Add("@CompGroup", ArrayLists.ToArrayList("@CompGroup", GroupId, SqlDbType.VarChar));
            spParam.Add("@CompanyApplicable", ArrayLists.ToArrayList("@CompanyApplicable", CompanyId, SqlDbType.VarChar));
            spParam.Add("@CommunityId", ArrayLists.ToArrayList("@CommunityId", CommunityId, SqlDbType.VarChar));

            spParam.Add("@Phone", ArrayLists.ToArrayList("@Phone", Phone, SqlDbType.VarChar));
            spParam.Add("@Mobile", ArrayLists.ToArrayList("@Mobile", Mobile, SqlDbType.VarChar));

            //SP_params.Add("@IsAdmin," + Admin);
            //SP_params.Add("@WebLogon," + WebLogon);
            //SP_params.Add("@Status,Null");
            //SP_params.Add("@Type," + _Type);
            //SP_params.Add("@EDIdt,Null");
            //SP_params.Add("@UserLease," + ((dtpLease.Checked) ? dtpLease.Value.ToString("dd-MMM-yyyy") : "Null"));
            //SP_params.Add("@recDirty,1");
            //SP_params.Add("@recEnable," + Active);
            //SP_params.Add("@recDel,0");
            //SP_params.Add("@Parentid," + _ParentID);
            //SP_params.Add("@LoginName," + Program.gblValues.Encrypt("", txtLogin.Text));
            //SP_params.Add("@Password," + Program.gblValues.Encrypt(txtLogin.Text, txtPassword.Text));
            //SP_params.Add("@UserName," + txtName.Text);
            //SP_params.Add("@Email," + txtEmail.Value);
            //SP_params.Add("@fContactId,Null");
            //SP_params.Add("@Desg," + txtDesignation.Text.Trim());
            //SP_params.Add("@ID," + ToSaveId);
            //SP_params.Add("@ContactType,Null");
            //SP_params.Add("@LocId,DEL");
            //SP_params.Add("@RoleId," + _roleID);
            //SP_params.Add("@DeptID,Null");
            //SP_params.Add("@recUser," + Program.gblValues.GetUserProperties("ID"));
            //SP_params.Add("@EmpID,Null");
            //SP_params.Add("@CompGroup," + hlpGroup.ID.Replace(',', ';'));
            //SP_params.Add("@CompanyApplicable," + hlpCompany.ID.Replace(',', ';'));
            //StoredH.ExecuteSP(SP_params, this.ActiveTransaction);


            DataSet ds = ExecuteData(sProcedureNm, spParam, false, false, true);

            // --- If Load has Error --------------------------------------------

            if (ds == null || _Status != 0 || _Message != "")
            {
                //_Message = Message;
                return _Message;
            }


            return string.Empty;

        }


        //public string UserMaster_StatusChanged(string PrimaryID, int NewStatus, string CommunityID)
        //{

            
        //    Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
        //    spParam.Add("@ID", ArrayLists.ToArrayList("@ID", PrimaryID, SqlDbType.Char));
        //    spParam.Add("@NewStatus", ArrayLists.ToArrayList("@NewStatus", NewStatus, SqlDbType.Bit));
        //    spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.VarChar));

        //    DataSet ds = ExecuteData(spNames.Change_Status_UserMaster, spParam, false, false, true);

        //    // --- If Load has Error --------------------------------------------

        //    if (ds == null || _Status != 0 || _Message != "")
        //    {
        //        _Message = "Unable to change User status";
        //        return _Message;
        //    }
        //    else
        //        _Message = "";

        //    return _Message;

        //}


        //public DataSet UserMasterRecords_StatusChanged(string PrimaryID, int NewStatus, string CommunityID)
        //{


        //    Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
        //    spParam.Add("@ID", ArrayLists.ToArrayList("@ID", PrimaryID, SqlDbType.Char));
        //    spParam.Add("@NewStatus", ArrayLists.ToArrayList("@NewStatus", NewStatus, SqlDbType.Bit));
        //    spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.VarChar));

        //    DataSet ds = ExecuteData(spNames.Change_Status_UserMaster, spParam, false, false, true);

        //    // --- If Load has Error --------------------------------------------

        //    if (ds == null || _Status != 0 || _Message != "")
        //    {
        //        _Message = "Unable to change User status";
        //        return null;
        //    }
        //    else
        //        _Message = "";

        //    return ds;

        //}


        public string companyMaster_Delete(string PrimaryID, string CommunityID)
        {

            
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@ID", ArrayLists.ToArrayList("@ID", PrimaryID, SqlDbType.Char));
            spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.VarChar));

            DataSet ds = ExecuteData(spNames.Delete_UserMaster, spParam, false, false, true);

            // --- If Load has Error --------------------------------------------

            if (ds == null || _Status != 0 || _Message != "")
            {
                _Message = "Unable to Delete user record!";
                return _Message;
            }
            else
                _Message = "";

            return _Message;

        }

        public DataSet UserMasterRecord_Delete(string PrimaryID, string CommunityID)
        {


            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@ID", ArrayLists.ToArrayList("@ID", PrimaryID, SqlDbType.Char));
            spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.VarChar));

            DataSet ds = ExecuteData(spNames.Delete_UserMaster, spParam, false, false, true);

            // --- If Load has Error --------------------------------------------

            if (ds == null || _Status != 0 || _Message != "" || ds.Tables.Count <1)
            {
                _Message = "Unable to Delete user record!";
                return null;
            }
            else
                _Message = "";

            return ds;

        }

        #endregion

     }
}
