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
using ITHeart.BL;
using System.DataType;

namespace ITHeart.UserControl
{
    [Themeable(true)]
    public partial class DatePickerControl : System.Web.UI.UserControl
    {
        private System.Web.UI.WebControls.UnitConverter f = new UnitConverter();
        public Common CommonBL = new Common();

        #region " Formating "
        public enum ShowMode { None = 0, OnMouseOver = 1, Always = 2 };

        public ShowMode border
        {
            get
            {
                if (ViewState[this.ID + "border"] != null && ViewState[this.ClientID + "border"]==null)
                    ViewState[this.ClientID + "border"] = (ShowMode)(ViewState[this.ID + "border"]);
                if (ViewState[this.ClientID + "border"] == null) return ShowMode.None; return (ShowMode)(ViewState[this.ClientID + "border"]);
            }
            set
            {
                if (value == border) return;
                ViewState[this.ClientID + "border"] = value;
                tdDatePickerControl.Style.Remove("border");
                if (value == ShowMode.Always)
                    tdDatePickerControl.Style.Add("border", "1pt solid silver");
                else
                    tdDatePickerControl.Style.Add("border", "0pt none transparent");
            }
        }
        public ShowMode icon
        {
            get
            {
                if (ViewState[this.ID + "icon"] != null && ViewState[this.ClientID + "icon"] == null)
                    ViewState[this.ClientID + "icon"] = (ShowMode)(ViewState[this.ID + "icon"]);
                if (ViewState[this.ClientID + "icon"] == null ) return ShowMode.None; 

                return (ShowMode)(ViewState[this.ClientID + "icon"]);
            }
            set
            {
                if (value == icon) return;
                ViewState[this.ClientID + "icon"] = value;
                imgDatePickerControl.Style.Remove("visibility");
                imgDatePickerControl.Style.Add("visibility", ((value == ShowMode.Always && !ReadOnly) ? "" : "hidden"));
            }
        }

        public string bgcolor
        {
            get
            {
                if (ViewState[this.ID + "bgcolor"] != null && ViewState[this.ClientID + "bgcolor"] == null)
                    ViewState[this.ClientID + "bgcolor"] = Converts.ToString(ViewState[this.ID + "bgcolor"]);
                return Converts.ToString(ViewState[this.ClientID + "bgcolor"]);
            }
            set
            {
                if (value == bgcolor) return;
                ViewState[this.ClientID + "bgcolor"] = value;
                txtDatePickerControl.Style.Remove("background-color");
                if (!Strings.IsNullOrEmpty(value)) txtDatePickerControl.Style.Add("background-color", value);
            }
        }

        public string cssClass
        {
            get
            {
                if (ViewState[this.ID + "cssClass"] != null && ViewState[this.ClientID + "cssClass"] == null)
                    ViewState[this.ClientID + "cssClass"] = Converts.ToString(ViewState[this.ID + "cssClass"]);
                return Converts.ToString(ViewState[this.ClientID + "cssClass"]);
            }
            set
            {
                if (value == cssClass) return;
                ViewState[this.ClientID + "cssClass"] = value;
               try { txtDatePickerControl.Attributes.Remove("class"); } catch {}
                if (!Strings.IsNullOrEmpty(value)) txtDatePickerControl.Attributes.Add("class", value);
            }
        }

        public int Width
        {
            get
            {
                if (ViewState[this.ID + "Width"] != null && ViewState[this.ClientID + "Width"] == null)
                    ViewState[this.ClientID + "Width"] = Converts.ToInt32(ViewState[this.ID + "Width"]);
                return Converts.ToInt32(ViewState[this.ClientID + "Width"]);
            }
            set
            {
                if (value == Width) return;
                ViewState[this.ClientID + "Width"] = value;
                try { tdDatePickerControl.Style.Remove("min-width"); } catch { }
                try { tdDatePickerControl.Style.Remove("max-width"); } catch { }
                try { divDatePickerControl.Style.Remove("max-width"); }catch { }
                try { divDatePickerControl.Style.Remove("max-width"); }catch { }
                try { txtDatePickerControl.Style.Remove("min-width"); }catch { }
                try { txtDatePickerControl.Style.Remove("max-width"); }catch { }
                try { txtDatePickerControl.Style.Remove("width"); }catch { }

                if (value > 0)
                {
                    tdDatePickerControl.Style.Add("min-width", value.ToString() + "px");
                    tdDatePickerControl.Style.Add("max-width", value.ToString() + "px");
                    divDatePickerControl.Style.Add("max-width", value.ToString() + "px");
                    divDatePickerControl.Style.Add("min-width", value.ToString() + "px");
                    txtDatePickerControl.Style.Add("min-width", (value-15).ToString() + "px");
                    txtDatePickerControl.Style.Add("max-width", (value - 15).ToString() + "px");
                    txtDatePickerControl.Style.Add("width", (value - 15).ToString() + "px");
                }
            }
        }

        public string cssStyle
        {
            get
            {
                if (ViewState[this.ID + "cssStyle"] != null && ViewState[this.ClientID + "cssStyle"] == null)
                    ViewState[this.ClientID + "cssStyle"] = Converts.ToString(ViewState[this.ID + "cssStyle"]);
                return Converts.ToString(ViewState[this.ClientID + "cssStyle"]);
            }
            set
            {
                if (value == cssStyle) return;
                ViewState[this.ClientID + "cssStyle"] = value;

                if (Strings.IsNullOrEmpty(value)) return;

                #region " Set Textbox Style "
                int iLoop;
                string[] sRow, sCol;
                sRow = cssStyle.Split(';');
                for (iLoop = 0; iLoop < sRow.Length; iLoop++)
                {
                    sCol = sRow[iLoop].Split(':');
                    if (Strings.IsNullOrEmpty(sCol[0])) continue;
                    try
                    {
                        txtDatePickerControl.Style.Remove(sCol[0]);
                        if (sCol.Length > 1 && !Strings.IsNullOrEmpty(sCol[1])) txtDatePickerControl.Style.Add(sCol[0], sCol[1]);
                    }
                    catch { }
                }
                #endregion
            }
        }

        #endregion

        #region " Properties "
        public bool ReadOnly
        {
            get
            {
                if (ViewState[this.ID + "ReadOnly"] != null && ViewState[this.ClientID + "ReadOnly"] == null)
                    ViewState[this.ClientID + "ReadOnly"] = Converts.ToBoolean(ViewState[this.ID + "ReadOnly"]);
                return Converts.ToBoolean(ViewState[this.ClientID + "ReadOnly"]);
            }
            set
            {
                if (value == ReadOnly) return;
                ViewState[this.ClientID + "ReadOnly"] = value;
                imgDatePickerControl.Style.Remove("visibility");
                imgDatePickerControl.Style.Add("visibility", ((icon == ShowMode.Always && !value) ? "" : "hidden"));
                try {txtDatePickerControl.Attributes.Remove("readonly"); } catch {}
                if (value) txtDatePickerControl.Attributes.Add("readonly", "readonly");
            }
        }
        public bool Mandatory
        {
            get
            {
                if (ViewState[this.ID + "Mandatory"] != null && ViewState[this.ClientID + "Mandatory"] == null)
                    ViewState[this.ClientID + "Mandatory"] = Converts.ToBoolean(ViewState[this.ID + "Mandatory"]);
                return Converts.ToBoolean(ViewState[this.ClientID + "Mandatory"]);
            }
            set { ViewState[this.ClientID + "Mandatory"] = value; }
        }

        public string CallbackFunction
        {
            get
            {
                if (ViewState[this.ID + "CallbackFunction"] != null && ViewState[this.ClientID + "CallbackFunction"] == null)
                    ViewState[this.ClientID + "CallbackFunction"] = Converts.ToString(ViewState[this.ID + "CallbackFunction"]);
                return Converts.ToString(ViewState[this.ClientID + "CallbackFunction"]);
            }
            set { ViewState[this.ClientID + "CallbackFunction"] = value; }
        }

        public string Date
        {
            get { return txtDatePickerControl.Value; }
            set
            {
                if (value == Date) return;
                txtDatePickerControl.Value = value;
            }
        }
        public string DateFormat { get { return "dd-MMM-yyyy"; /* Converts.ToString(ViewState[this.ClientID + "DateFormat"]); */ } set { ViewState[this.ClientID + "DateFormat"] = value; } }
        #endregion

        public void SetAtForm(int iWidth, bool bMandatory)
        {
            border = ShowMode.Always; icon = ShowMode.OnMouseOver;
            Width = iWidth; Mandatory = bMandatory;
        }
        #region " Page Methods "
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string sVal;

           
            #region " Attach Events "
             sVal = "ShowDatePickerObject(\"" + border.ToString() + "\", \"" + icon.ToString() + "\", "
                + " " + (ReadOnly ? "1" : "0") + ", \"" + tdDatePickerControl.ClientID + "\", \"" + imgDatePickerControl.ClientID + "\"";
            tdDatePickerControl.Attributes.Add("onmouseover", sVal + ",true)");
            tdDatePickerControl.Attributes.Add("onmouseout", sVal + ",false)");

            txtDatePickerControl.Attributes.Add("onchange", this.ClientID + "_Date_Validating(this)");

            imgDatePickerControl.Src = CommonBL.ResolveUrl("~/Images/CalendarIcon.gif");
            imgDatePickerControl.Attributes.Add("onclick", "ShowC1Calendar(\"" + this.ClientID + "\", \"" + txtDatePickerControl.ClientID + "\")");
            #endregion
 
            BuildClientSideScript();
        }
        #endregion

        #region " Build Client-side Script "
        private void BuildClientSideScript()
        {

            // ---- Include Script Files --------------------------------------------
            // ----------------------------------------------------------------------

            System.Text.StringBuilder sbScript = new System.Text.StringBuilder("");

            sbScript.Append("<script language='javascript' type='text/javascript'>" + "\n");

            #region " Declare Variables "
            // ---- Declare Variables -----------------------------------------------
            sbScript.Append("   var " + this.ClientID + "_Date = '" + (!Strings.IsNullOrEmpty(Date) ? Date : (Mandatory ? DateTime.Now.ToString(DateFormat) : "")) + "';" + "\n");
            // ----------------------------------------------------------------------
            #endregion

            #region " Get & Set Value "
            // ---- Get Value ------------------------------------------------------
            sbScript.Append("function " + this.ClientID + "_GetValue() {" + "\n");
            sbScript.Append("   var sVal = '', oText; " + "\n");
            sbScript.Append("   oText = document.getElementById('" + txtDatePickerControl.ClientID + "');" + "\n");
            sbScript.Append("   if(oText != null) sVal = oText.value;" + "\n");
            sbScript.Append("   return sVal;" + "\n");
           // sbScript.Append("   return " + this.ClientID + "_Date;" + "\n");
            sbScript.Append("}" + "\n");
            // ----------------------------------------------------------------------
            // ---- Set Value ------------------------------------------------------
            sbScript.Append("function " + this.ClientID + "_SetValue(value) {" + "\n");
            sbScript.Append(" var oText; " + "\n");
            sbScript.Append(" oText = document.getElementById('" + txtDatePickerControl.ClientID + "');" + "\n");
            sbScript.Append(" if(oText == null) return;" + "\n");
            sbScript.Append(" oText.value = value;" + "\n");
            sbScript.Append(" " + this.ClientID + "_Date = value;" + "\n");
            sbScript.Append("}" + "\n");
            // ----------------------------------------------------------------------

            // ---- Date Set Property -----------------------------------------------
            sbScript.Append("function " + this.ClientID + "_Date_Enable(bEnable) {" + "\n");
            sbScript.Append("   var obj;" + "\n");
            sbScript.Append("   obj = document.getElementById('" + txtDatePickerControl.ClientID + "');" + "\n");
            sbScript.Append("   if(obj != null) obj.readOnly = !bEnable;" + "\n");
            sbScript.Append("   obj = document.getElementById('" + imgDatePickerControl.ClientID + "');" + "\n");
            sbScript.Append("   if(obj != null) obj.style.display = (bEnable ? '' : 'none');" + "\n");
            sbScript.Append("}" + "\n");
            // ----------------------------------------------------------------------
            #endregion

            #region " Date Selected "
            // ---- Date Fed --------------------------------------------------------
            sbScript.Append("function " + this.ClientID + "_Date_Validating(oText) {" + "\n");
            if (ReadOnly) sbScript.Append(" return true;" + "\n");
            sbScript.Append("   if(oText == null) return true;" + "\n");
            sbScript.Append("   var bRollback = false;" + "\n");
            sbScript.Append("   if(!IsBlank(oText.value)) {" + "\n");
            sbScript.Append("       oText.value = getDate(oText.value);" + "\n");
            sbScript.Append("       bRollback = IsBlank(oText.value);" + "\n");
            sbScript.Append("   }" + "\n");
            if (Mandatory) sbScript.Append("   else bRollback = true;" + "\n");

            sbScript.Append("   if(bRollback) { " + "\n");
            sbScript.Append("       oText.value = " + this.ClientID + "_Date;" + "\n");
            sbScript.Append("       oText.focus();" + "\n");
            sbScript.Append("       return false;" + "\n");
            sbScript.Append("   }" + "\n");
            
            sbScript.Append("   " + this.ClientID + "_Date_AfterEdit();" + "\n");
            sbScript.Append("   return true;" + "\n");
            sbScript.Append("}" + "\n");
            // ----------------------------------------------------------------------

            // ---- Date Selected ---------------------------------------------------
            sbScript.Append("function " + this.ClientID + "_Date_AfterEdit() {" + "\n");
            if (ReadOnly) sbScript.Append(" return true;" + "\n");
            sbScript.Append("   var oText;" + "\n");
            sbScript.Append("   oText = document.getElementById('" + txtDatePickerControl.ClientID + "');" + "\n");
            sbScript.Append("   if(oText == null) return false;" + "\n");
            if (Mandatory)
            {
                sbScript.Append("   if(oText.value == '') {" + "\n");
                sbScript.Append("       oText.value = " + this.ClientID + "_Date;" + "\n");
                sbScript.Append("       oText.focus();" + "\n");
                sbScript.Append("       return false;" + "\n");
                sbScript.Append("   }" + "\n");
            }

            sbScript.Append(" var bChanged = oText.value.toLowerCase() != " + this.ClientID + "_Date.toLowerCase();" + "\n");
            sbScript.Append("   " + this.ClientID + "_Date = oText.value;" + "\n");

            if (!Strings.IsNullOrEmpty(CallbackFunction))
                sbScript.Append("   if(bChanged) " + CallbackFunction.Replace("##DATVALUE##", this.ClientID + "_Date") + ";" + "\n");

            sbScript.Append("   return true;" + "\n");
            sbScript.Append("}" + "\n");
            // ----------------------------------------------------------------------
            #endregion

            sbScript.Append("</script>" + "\n");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "@JSScript" + this.ClientID, sbScript.ToString(), false);
        }
        #endregion
    }
}
       

