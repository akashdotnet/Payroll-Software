using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Collections.Generic;
using ITHeart.BL;
using System.DataType;

namespace ITHeart.UserControl
{
    public partial class HelpControl : System.Web.UI.UserControl
    {
        public virtual event ChosenClickedHandler ValueChosen;
        public delegate void ChosenClickedHandler(string ID);
        
        public virtual event ChosenClickedHandlerwithObject ValueChosenWithObj;
        public delegate void ChosenClickedHandlerwithObject(UserControl.HelpControl sender, string ID);
        private string _defaultId = "";
        private string _defaultText = "";

        //Theme Properties
        protected string _VisualStylePath, _VisualStyle;
        public string VisualStylePath { get { return _VisualStylePath; } set { _VisualStylePath = value; } }
        public string VisualStyle { get { return _VisualStyle; } set { _VisualStyle = value; } }

        protected string _boundOptionId;
        public string BoundOptionID
        { get { return _boundOptionId; } set { _boundOptionId = value; /* SetButtonsVisibility(); */ } }

        private bool _Enabled=true;
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                txtHelp.Enabled = value;
                hdEnabled.Value = (txtHelp.Enabled ? "1" : "0");
            }
        }

        public int HelpWidth
        {
            get { return (int)ViewState["HelpWidth"]; }
            set
            {
                //_helpWidth = value;
                
                ViewState["HelpWidth"] = value;
                if (ViewState["HelpWidth"] == null)
                    ViewState["HelpWidth"] = 200;
               
                    pnlHelpControl.Width = (int)ViewState["HelpWidth"];
                    txtHelp.Width = (int)ViewState["HelpWidth"] - ((_IsNullable == 1) ? 45 : 30) - ((_IsResetable) ? 45 : 30); 
               
            }
        }

        public int HelpHeight
        {
            get { return (int)ViewState["HelpHeight"]; }
            set
            {
               
                ViewState["HelpHeight"] = value;
                if (ViewState["HelpHeight"] == null)
                    ViewState["HelpHeight"] = 20;
                   pnlHelpControl.Height = (int)ViewState["HelpHeight"];
            }
        }

        protected string _SelectID, _HelpText;       
        public string SelectID {
            get {

                //return ViewState[this.ID + "_selected"].ToString();    
                ViewState[this.ID + "_selected"] = hdHelpSelectID.Value.Replace("!~", ""); 
                return hdHelpSelectID.Value.Replace("!~",""); 
            }
            set
            {
                if (value == "" || value.ToUpper() == "NULL")
                {
                    hdHelpSelectID.Value = "";
                    hdDefaultID.Value = "";
                    txtHelp.Text = "";
                    return;
                }
                hdHelpSelectID.Value=value;
                hdDefaultID.Value = value;
                ViewState[this.ID + "_selected"] = value;
                BuildDisplayText();
                hdDefaultText.Value = txtHelp.Text;


            } 
           }
       
        public string LinkObj
        {
            get {
                return hdLinkedObj.Value;
            }
            set
            {
                if (value == "" || value.ToUpper() == "NULL")
                {
                    hdLinkedObj.Value = "";
                    return;
                }
                hdLinkedObj.Value = value;
            }
        }


        public int MultiSelect
        {
            get
            {

                return ((hdMulti.Value == "") ? 0 : Convert.ToInt32(hdMulti.Value));
            }
            set
            {

                hdMulti.Value = value.ToString();
            }
        }


        public string Link
        {
            get
            {

                return hdLink.Value;
            }
            set
            {
                if (value == "" || value.ToUpper() == "NULL")
                {
                    hdLink.Value = "";
                    return;
                }
                hdLink.Value = value;
            }
        }
        public string HelpText 
        { 
            get 
            { 
                return txtHelp.Text ; 
            }

            set
            {
               txtHelp.Text = value;
            }
        }
        private int _IsNullable = 0;
        public int IsNullable { get { return _IsNullable; } set { _IsNullable = value; } }
        private bool _IsResetable = false;
        public bool IsResetable { get { return _IsResetable; } set { _IsResetable = value; } }
        // HelpId and AdditionalParameters Properties
        
        protected Dictionary<string, string> _AdditionalParameters = new Dictionary<string, string>();
        /// <summary>
        /// Help ID
        /// </summary>
        protected string _HelpId;
        public string HelpID
        { get { return _HelpId; } set { _HelpId = value; } }
        /// <summary>
        /// AdditionalParameters 
        /// </summary>
        public Dictionary<string, string> AdditionalParameters 
        { 
            get
            {
                return _AdditionalParameters;
            }
            set
            { _AdditionalParameters = value;

            }
        }

        public string AdditionalWhere { get { return hdAdditonalFiler.Value.ToString(); } set { hdAdditonalFiler.Value = value; } }

        public string ValueChangeJS { get { return hdValueChangeJS.Value.ToString(); } set { hdValueChangeJS.Value = value; } }

        public string ValueChangeJSParam { get { return hdValueChangeJSParam.Value.ToString(); } set { hdValueChangeJSParam.Value = value; } }

        public ITHeart.BL.Common CommonBL = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
   
            Page.ClientScript.RegisterClientScriptInclude("HelpCtrlScript", CommonBL.ResolveUrl("~/UserControl/HelpControl.ascx.js"));
            BuildClientSideJSScript();

            //<link href="<% Response.Write(this.ResolveClientUrl("App_Themes") + "/" + _VisualStyle); %>/Default.css" rel="stylesheet" type="text/css" />
           // HLPCSSID.Href = "App_Themes/" + CommonBL.ThemeName + "/Default.css";
            CommonBL = new ITHeart.BL.Common();
            if (ViewState[this.ID + "_selected"] == null)
                ViewState[this.ID + "_selected"] = "";

            //if (HelpControlDecorator == null ) return;
            //if(_VisualStyle != null)
              //  HelpControlDecorator.VisualStyle = _VisualStyle;

            chkMakeNullHelp.Attributes.Add("onclick", "javascript:MakeEnable('" + this.ClientID + "')");
            imgReset.Visible = IsResetable;
            tdReset.Visible = IsResetable;

            imghelp.Src = CommonBL.ResolveUrl("~/Images/help1.png");
          
            imghelp.Attributes.Add("onclick", this.ClientID + "OpenGenHelp('" + CommonBL.ProjectDetails(Common.ProjectInfoTypeEnum.pathWebFormController)
                + "?OptionID=help" + ((!Strings.IsNullOrEmpty(ValueChangeJS) ) ? "&OnFocusFX=" + this.ClientID + "ValueChanged" : "") + "','" + HelpID + "','" 
                + txtHelp.ClientID + "','" + hdHelpSelectID.ClientID + "','" + BoundOptionID + "','" + MultiSelect + "') ");

            imgReset.Src = CommonBL.ResolveUrl("~/Images/reset.jpg");
            imgReset.Attributes.Add("onclick", this.ClientID + "ResetHelp()");
            chkMakeNullHelp.Visible = (_IsNullable == 1);
            tdMakeNullHelp.Visible = (_IsNullable == 1);

            //imghelp.Visible = (((chkMakeNullHelp.Visible && chkMakeNullHelp.Checked) || (_IsNullable == 0)) && Enabled);
            
            if (IsPostBack)
            {
                if (hdHelpSelectID.Value == "" && hdHelpSelectID.Value != "!~" && hdHelpSelectID.Value != ViewState[this.ID + "_selected"].ToString())
                {
                    hdHelpSelectID.Value = ViewState[this.ID + "_selected"].ToString();
                    hdDefaultID.Value = ViewState[this.ID + "_selected"].ToString();
                }
                else if (hdHelpSelectID.Value == "!~")
                {
                    hdHelpSelectID.Value = "";
                    txtHelp.Text = "";
                    hdDefaultText.Value = "";
                    hdDefaultID.Value = "";
                    ViewState[this.ID + "_selected"] = "";
                }
                if (hdHelpSelectID.Value.Trim() != "" )
                    BuildDisplayText();


            }

            if (chkMakeNullHelp.Visible)
                chkMakeNullHelp.Checked = (SelectID == "");
            if (ViewState["HelpWidth"] == null)
                ViewState["HelpWidth"] = 200;


            txtHelp.Width = (int)ViewState["HelpWidth"] - ((_IsNullable == 1) ? 45 : 30) - ((_IsResetable) ? 45 : 30);
          //  txtHelp.Attributes.Add("onchange", "javascript:" +  this.ClientID + "ValueChanged()");

        }

        private void BuildDisplayText()
        {
            int jCount = SelectID.Split(',').Length;
            if (jCount > 4)
            {

                this.txtHelp.Text = jCount.ToString("##,##0") + " Selected";
                this.txtHelp.Text = jCount.ToString("##,##0") + " Selected";
                return;
            }
         
            DataSet ds;
            Dictionary<string, ArrayList> aListParam ;
            switch (this.BoundOptionID)
            {
                default:
                    aListParam = new Dictionary<string, ArrayList>();
                    aListParam.Add("@OptionID", ArrayLists.ToArrayList("@OptionID", (BoundOptionID + Strings.Space(9)).Substring(0, 9), SqlDbType.VarChar));
                    aListParam.Add("@pID", ArrayLists.ToArrayList("@pID", SelectID, SqlDbType.VarChar));
                    aListParam.Add("@HelpID", ArrayLists.ToArrayList("@HelpID", HelpID, SqlDbType.VarChar));

                    //for (int i = 0; i < _AdditionalWhere.Count; i++)
                    //    SP_params.Add(this._AdditionalWhere[i].ToString());
                    ds = CommonBL.ExecuteData(spNames.Load_Text_For_Help_User_Control, aListParam);
                    if (ds == null)
                    {
                        //Program.ShowMessage(StoredP.ErrorMessage);
                        return;
                    }

                    string xtext = "";
                    if (SelectID.ToLower().IndexOf("##null") >= 0) xtext = "(Not Defined)";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        xtext = xtext + ((xtext.Trim() != "" && ds.Tables[0].Rows[i][0].ToString() != "") ? ", " : "") + ds.Tables[0].Rows[i][0].ToString();
                    this.txtHelp.Text = xtext;
                    this.hdDefaultText.Value = xtext;
                    this.hdDefaultID.Value = SelectID;

                    //if (ds.Tables[0].Columns.Count > 1)
                    //{
                    //    xtext = "";
                    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //    {
                    //        for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                    //        {
                    //            xtext = xtext + ((xtext.Trim() != "" && ds.Tables[0].Rows[i][j].ToString() != "") ? ", " : "") + ds.Tables[0].Rows[i][j].ToString();
                    //        }
                    //        xtext = xtext + "~";
                    //    }
                    //    this.ExtraText = xtext;
                    //}

                    break;
            }

        }

        #region " Build Client-side Javascript "
        private void BuildClientSideJSScript()
        {
            System.Text.StringBuilder sbScript = new System.Text.StringBuilder("");

            sbScript.Append("<script language='javascript'>" + "\n");
            sbScript.Append("var MEHelp;");
            sbScript.Append(ID + "= new HelpControl('" + ClientID + "');" + "\n");
            sbScript.Append("MEHelp=" + ID + ";" + "\n");
            sbScript.Append("MEHelp.Enable('" + ClientID + "');" + "\n");
            sbScript.Append("function Choosen() { }" + "\n");

            sbScript.Append("function MakeEnable(ClientID) {" + "\n");
            sbScript.Append("MEHelp.Enable(ClientID);" + "\n");
            sbScript.Append("}" + "\n");
            sbScript.Append("function " + ClientID + "ResetHelp() {" + "\n");
            sbScript.Append("MEHelp.Reset('" + ClientID + "');" + "\n");
            sbScript.Append("}" + "\n");

            sbScript.Append("function  " + this.ClientID + "ValueChanged() {" + "\n");
            sbScript.Append("var obj1 = document.getElementById('" + this.ClientID + "_hdValueChangeJS');" + "\n");
            sbScript.Append("var obj2 = document.getElementById('" + this.ClientID + "_hdValueChangeJSParam');" + "\n");
            sbScript.Append("var obj3 = document.getElementById('" + this.ClientID + "_hdHelpSelectID');" + "\n");
            sbScript.Append("var obj4 = document.getElementById('" + txtHelp.ClientID + "');" + "\n");
            sbScript.Append("var pList, fxnString='';" + "\n");
            sbScript.Append("if (obj1.value !=''){" + "\n");
            sbScript.Append("  pList =obj2.value.replace(/" + Convert.ToChar(253).ToString() + "/g,'\",\"') " + "\n");
            sbScript.Append("  pList = pList.replace(/##ID##/g,obj3.value).replace(/##TEXT##/g,obj4.value) " + "\n");
            sbScript.Append("   eval(obj1.value+'(\"'+pList+'\");');" + "\n");
            sbScript.Append("   }" + "\n");
            sbScript.Append("}" + "\n");

            // Added Multiple company selected while adding department For TVS Moters By Amit Tiwari on 24-Oct-2015 Start
            if (_HelpId == "545")
            {
                sbScript.Append("function  " + this.ClientID + "OpenGenHelp(jPath,_HelpId,txtBoxID,ReturnValueBoxID,OptionID,Multiple,MULTI_CID) {" + "\n");
                sbScript.Append("var MULTI_CID = GetValueByID('ctl00_MainContent_hlpCompany_hdHelpSelectID');" + "\n");
                //sbScript.Append("var CompanyID = GetValueByID('" + hdHelpSelectID.ClientID + "');" + "\n");
            }
            else
            {
                sbScript.Append("function  " + this.ClientID + "OpenGenHelp(jPath,_HelpId,txtBoxID,ReturnValueBoxID,OptionID,Multiple) {" + "\n");
            }
            // Added Multiple company selected while adding department For TVS Moters By Amit Tiwari on 24-Oct-2015 End

            //sbScript.Append("function  " + this.ClientID + "OpenGenHelp(jPath,_HelpId,txtBoxID,ReturnValueBoxID,OptionID,Multiple) {" + "\n");
            sbScript.Append("var LinkedSelected = '';" + "\n");
            sbScript.Append("var obj1 = document.getElementById('" + this.ClientID + "_hdEnabled');" + "\n");
            sbScript.Append("if(obj1 ==undefined) return;" + "\n");
            sbScript.Append("if(obj1.value =='' || obj1.value =='0') return;" + "\n");
            sbScript.Append("var Selectedid = document.getElementById(ReturnValueBoxID);" + "\n");
            sbScript.Append("var SelectedKeys = Selectedid.value;" + "\n");
            sbScript.Append("var AdditionalFilterBox=document.getElementById('" + hdAdditonalFiler.ClientID + "');" + "\n");
            if (LinkObj != "")
            {
                sbScript.Append("var LinkedObjMulti='" + LinkObj + "';" + "\n");
                sbScript.Append("var LinkedObjArr=LinkedObjMulti.split('|');" + "\n");
                sbScript.Append("for(i=0;i<LinkedObjArr.length;i++){ " + "\n");
               // sbScript.Append("       alert(LinkedObjArr[i]);");
                sbScript.Append("       var objArr=LinkedObjArr[i].split('@') ;" + "\n");
                sbScript.Append("       var LObj=document.getElementById(objArr[0]+'_hdHelpSelectID')  ;" + "\n");
                sbScript.Append("       var LObjchk=document.getElementById(objArr[0]+'_chkMakeNullHelp')  ;" + "\n");
                sbScript.Append("       if(LObj != null && LObj.value!='' && LObj.value!='!~'){" + "\n");
                if (_IsNullable == 1)
                {
                    sbScript.Append("       if(LObjchk!=null && !LObjchk.checked){" + "\n");
                    sbScript.Append("       LinkedSelected +='&' + objArr[1]+'='+LObj.value ;}" + "\n");
                }
                else
                {
                    sbScript.Append("       LinkedSelected +='&' + objArr[1]+'='+LObj.value ;" + "\n");
                }
                ///sbScript.Append("       alert(LinkedSelected);");
                sbScript.Append("       }" + "\n");
                sbScript.Append("   }" + "\n");
            }
            if (CommonBL.DisplayMaster == 2)
            {
                //sbScript.Append("window.open(jPath + '&ID=' + _HelpId + '&Helptext=' + txtBoxID + '&BoundOptionID=' + OptionID + '&ReturnValueBoxID='" + "\n");
                //sbScript.Append("+ ReturnValueBoxID + '&SelectedKeys='+SelectedKeys+'&MultiSelect=' + Multiple + AdditionalFilterBox.value + LinkedSelected + '', '', 'scrollbars=1; status=no;location=no;');}" + "\n");

                // Added Multiple company selected while adding department For TVS Moters By Amit Tiwari on 24-Oct-2015 Start
                if (_HelpId == "545")
                {
                    sbScript.Append("window.open(jPath + '&ID=' + _HelpId + '&Helptext=' + txtBoxID + '&BoundOptionID=' + OptionID + '&ReturnValueBoxID='" + "\n");
                    sbScript.Append("+ ReturnValueBoxID + '&SelectedKeys='+SelectedKeys+'&MultiSelect=' + Multiple + AdditionalFilterBox.value + LinkedSelected +'&MULTI_CID='+MULTI_CID+ '', '', 'scrollbars=1; status=no;location=no;');}" + "\n");
                }
                else
                {
                    sbScript.Append("window.open(jPath + '&ID=' + _HelpId + '&Helptext=' + txtBoxID + '&BoundOptionID=' + OptionID + '&ReturnValueBoxID='" + "\n");
                    sbScript.Append("+ ReturnValueBoxID + '&SelectedKeys='+SelectedKeys+'&MultiSelect=' + Multiple + AdditionalFilterBox.value + LinkedSelected + '', '', 'scrollbars=1; status=no;location=no;');}" + "\n");
                }
                // Added Multiple company selected while adding department For TVS Moters By Amit Tiwari on 24-Oct-2015 End
            }
            else
            {
                //sbScript.Append("javascript:ShowC1Window(jPath + '&ID=' + _HelpId + '&Helptext=' + txtBoxID + '&BoundOptionID=' + OptionID + '&ReturnValueBoxID='" + "\n");
                //sbScript.Append("+ ReturnValueBoxID + '&SelectedKeys='+SelectedKeys+'&MultiSelect=' + Multiple + AdditionalFilterBox.value + LinkedSelected + '&DisplayMaster=2', 'Help',180,20,500,600,1,1 );}" + "\n");

                // Added Multiple company selected while adding department For TVS Moters By Amit Tiwari on 24-Oct-2015 Start
                if (_HelpId == "545")
                {
                    sbScript.Append("javascript:ShowC1Window(jPath + '&ID=' + _HelpId + '&Helptext=' + txtBoxID + '&BoundOptionID=' + OptionID + '&ReturnValueBoxID='" + "\n");
                    sbScript.Append("+ ReturnValueBoxID + '&SelectedKeys='+SelectedKeys+'&MultiSelect=' + Multiple + AdditionalFilterBox.value + LinkedSelected +'&MULTI_CID='+MULTI_CID+ '&DisplayMaster=2', 'Help',180,20,500,600,1,1 );}" + "\n");
                }
                else
                {
                    sbScript.Append("javascript:ShowC1Window(jPath + '&ID=' + _HelpId + '&Helptext=' + txtBoxID + '&BoundOptionID=' + OptionID + '&ReturnValueBoxID='" + "\n");
                    sbScript.Append("+ ReturnValueBoxID + '&SelectedKeys='+SelectedKeys+'&MultiSelect=' + Multiple + AdditionalFilterBox.value + LinkedSelected + '&DisplayMaster=2', 'Help',180,20,500,600,1,1 );}" + "\n");
                }
                // Added Multiple company selected while adding department For TVS Moters By Amit Tiwari on 24-Oct-2015 End
            }
            
            sbScript.Append("</script>" + "\n");


            ScriptManager.RegisterStartupScript(this, this.GetType(), "@@@@MyPopUpScript" + ClientID, sbScript.ToString(), false);
        }
        #endregion
    }
}

