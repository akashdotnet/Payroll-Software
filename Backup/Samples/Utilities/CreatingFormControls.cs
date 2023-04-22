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
using System.Collections.Generic;
using System.Text;
using ITHeart.BL;
using System.DataType;

namespace ITHeart
{
    public class CreatingFormControls : Common
    {

        System.Web.UI.WebControls.UnitConverter C = new UnitConverter();
        public int kCol = 0;
        public int kRow = 0;
        public bool bCanEdit = true;
        #region " Place Controls "
        public DataSet PlaceControls(string sReportID, string sUserID, Table tblContainer, ArrayList MandatoryCtrls, ArrayList KeyBasedCtrls, bool ShowUserSettings, string SkipFilds)
        {
            // ---- Load Configured Field List ---------------------------------
            Dictionary<string, ArrayList> sParam = new Dictionary<string, ArrayList>();

            sParam.Add("@ReportID", ArrayLists.ToArrayList("@ReportID", sReportID, SqlDbType.VarChar));
            sParam.Add("@UserID", ArrayLists.ToArrayList("@UserID", sUserID, SqlDbType.VarChar));
            sParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", GetDashboardGlobal("CommunityID"), SqlDbType.VarChar));
            DataSet ds = base.ExecuteData(spNames.Load_SelectionCriteriaDef_Objects, sParam); //StoredP.ExecuteSP(sParam);

            if (base.Status != 0 && base.Message != "")
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }

            DataRow[] drCtrls = ds.Tables[0].Select("tabControlObj is null", "kRow, kCol, TabIndex");
            // -----------------------------------------------------------------

            if (ShowUserSettings && ds.Tables[0].Select("UserID is not null").Length < 1) ShowUserSettings = false;

            // ---- Create Objects ---------------------------------------------
            for (int i = 0; i < drCtrls.Length; i++)
            {
                if (SkipFilds.Contains(Converts.ToString(drCtrls[i]["ObjID"]))) continue;
                AddControls(tblContainer, drCtrls[i], MandatoryCtrls, KeyBasedCtrls, i, true, ShowUserSettings, false);
            }
            Panel pnl = new Panel();
            pnl.ID = "pnlgap";
            pnl.Height = 0;



            //tblContainer.Controls.Add(pnl, 1, drCtrls.Length);
            //AddControlToWebTable(pnl, 1, drCtrls.Length+1, tblContainer); 

            // -----------------------------------------------------------------
            return ds;

        }
        #endregion

        #region " Create a Form Level Control "
        public void AddControls(Table tblContainer, DataRow dataRow, ArrayList MandatoryCtrls, ArrayList KeyBasedCtrls, int rowNumber, bool EnableReset, bool ShowUserSettings, bool bShowHControl)
        {
            #region " Initialize Variables  "
            //tblContainer.CellBorderStyle = TableCellBorderStyle.Single;
            int iWd = 0, KeyBased = 0, iRow, iCol;
            string sVal = "", sMsg = "";
            bool bVal;
            KeyBased = (int)Converts.ToDouble(dataRow["KeyBased"]);

            sMsg = "Please specify " + Converts.ToString(dataRow["CaptionP"]);

            Table tblSubContainer = tblContainer;
            Label lbl;
            TextBox txt;
            TextBox txtN;
            CheckBox chk;
            UserControl.DatePickerControl dtp, dtp2;
            DropDownList ddl;
            UserControl.HelpControl hlp;
           // UserControl.FileControl flp;
            Panel pnl;
            TableCell td;
            #endregion

            #region " Identify Containing Table "
            iRow = (int)Converts.ToDouble(dataRow["kRow"]);
            iCol = (int)Converts.ToDouble(dataRow["kCol"]);
            if (iRow > 0 && iCol > 0)
            {
                sVal = "BorderTop" + ((iCol > 1) ? " BorderLeft" : "");
                td = AddCellsToTable(iCol - 1, iRow - 1, tblContainer, 0, sVal);  // tblContainer.Rows[rowNumber].Cells[Col];

                try { tblSubContainer = (Table)td.Controls[0]; }
                catch
                {
                    tblSubContainer = new Table();
                    tblSubContainer.ID = "pnltblSub_" + (iRow - 1).ToString() + "_" + (iCol - 1).ToString();
                    tblSubContainer.CellPadding = 2; tblSubContainer.CellSpacing = 0; tblSubContainer.BorderWidth = 0;
                    td.Controls.Add(tblSubContainer);
                }
                rowNumber = tblSubContainer.Rows.Count;
            }

            #endregion

            #region " Label "
            lbl = CreateFormControl_Label("capP" + Converts.ToString(dataRow["ObjID"]),
                Converts.ToString(dataRow["CaptionP"]), 5, 0, 100, 20, true);
            bVal = (Converts.ToString(dataRow["ObjType"]).ToUpper() == "TITLE");
            if (bVal) lbl.CssClass = "DarkText PX11";
            AddControlToWebTable(lbl, 0, rowNumber, tblSubContainer, ((bVal) ? 2 : 0));

            if (bVal) return;
            #endregion

            switch (Converts.ToString(dataRow["ObjType"]).ToUpper())
            {
                #region " TXT, TXTM "
                case "TXT":
                case "TXTM":
                case "TXTN":
                case "TXTP":
                    txt = CreateFormControl_TextBox("txt" + Converts.ToString(dataRow["ObjID"]), "",
                        Converts.ToString(dataRow["MapField"]),
                        (int)Converts.ToDouble(dataRow["Maxlength"]),
                        0, 0, (int)Converts.ToDouble(dataRow["Width"]), 20);

                    if (Converts.ToString(dataRow["ObjType"]).ToUpper() == "TXTP")
                        txt.TextMode = TextBoxMode.Password;//    = '*';
                    else if (Converts.ToString(dataRow["ObjType"]).ToUpper() == "TXTM")
                    {
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.SkinID = "LogixText-10-Height-50";
                        // txt.Height = 60;
                    }
                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        txt.Text = Converts.ToString(dataRow["DefaultValue1"]);
                    else if (dataRow["DefaultValue"] != null)
                        txt.Text = Converts.ToString(dataRow["DefaultValue"]);
                    //tblSubContainer.Controls.Add(txt, 1, rowNumber);

                    //AddControlToWebTable(txt, 1, rowNumber, tblSubContainer,0);  

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(txt, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        if (kCol != 5)

                            AddControlToWebTable(txt, kCol++, kRow, tblSubContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(txt, kCol++, kRow, tblSubContainer, 0);
                        }
                    }

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(txt, sMsg, ""));

                    txt.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(txt);
                    break;
                #endregion

                #region " TXTN "
                //case "TXTN":
                //    txtN = CreateFormControl_TextBoxNumeric("tntn" + Converts.ToString(dataRow["ObjID"]), "",
                //        Converts.ToString(dataRow["MapField"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                //        (int)Converts.ToDouble(dataRow["ShowNotDefined"]),
                //        0, 0, (int)Converts.ToDouble(dataRow["Width"]), 20);

                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        txtN.Value = Converts.ToDouble(dataRow["DefaultValue1"]);
                //    else if (dataRow["DefaultValue"] != null)
                //        txtN.Value = Converts.ToDouble(dataRow["DefaultValue"]);
                //    //tblSubContainer.Controls.Add(txtN, 1, rowNumber);
                //    //AddControlToWebTable(txtN, 1, rowNumber, tblSubContainer,1);  
                //    if (!bShowHControl)//(kCol == -1)
                //        AddControlToWebTable(txtN, 1, rowNumber, tblSubContainer, 0);
                //    else
                //    {
                //        if (kCol != 5)
                //            AddControlToWebTable(txtN, kCol++, kRow, tblSubContainer, 0);

                //        else
                //        {
                //            kCol = 0;
                //            kRow = kRow + 1;

                //            AddControlToWebTable(txtN, kCol++, kRow, tblSubContainer, 0);
                //        }
                //    }


                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(txtN, sMsg, ""));

                //    txtN.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(txtN);
                //    break;
                #endregion

                #region " DT "
                case "DT-":
                    dtp = CreateFormControl_DateTimePicker("dt_" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["MapField"]), 0, 0,
                        Converts.ToBoolean(dataRow["Mandatory"]),
                        (int)Converts.ToDouble(dataRow["Width"]) * 2, 20);

                    if (Converts.ToString(dataRow["MapField"]).ToUpper().Trim() == "STARTDATE" && gblFromDate.Trim() != "")
                        dtp.Date = Converts.ToString(Convert.ToDateTime(gblFromDate));
                    if (Converts.ToString(dataRow["MapField"]).ToUpper().Trim() == "ENDDATE" && gblToDate.Trim() != "")
                        dtp.Date = Converts.ToString(Convert.ToDateTime(gblToDate));
                    else
                    {
                        //if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        //    dtp.Date = Converts.ToString(Convert.ToDateTime(dataRow["DefaultValue1"]));
                        if (dataRow["DefaultValue"] != null && dataRow["DefaultValue"] != DBNull.Value)
                        {
                            try
                            {
                                dtp.Date = Converts.ToDateTime(Convert.ToDateTime(dataRow["DefaultValue"]), "dd-MMM-yyyy");
                            }
                            catch { }
                        }
                    }

                    //AddControlToWebTable(dtp, 1, rowNumber, tblSubContainer,0); 
                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(dtp, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        if (kCol != 5)

                            AddControlToWebTable(dtp, kCol++, kRow, tblSubContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(dtp, kCol++, kRow, tblSubContainer, 0);
                        }
                    }
                    //tblSubContainer.Controls.Add(dtp, 1, rowNumber);
                    // tblSubContainer.SetColumnSpan(dtp, 2);

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(dtp, sMsg, ""));

                    dtp.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(dtp);
                    break;
                #endregion

                #region " CHK "
                case "CHK":
                    chk = CreateFormControl_CheckBox("chk" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]),
                        0, 0, true);

                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue1"]) > 0);
                    else if (dataRow["DefaultValue"] != null)
                        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue"]) > 0);

                    //tblSubContainer.Controls.Add(chk, 1, rowNumber);
                    //richa
                    //if (Converts.ToString(dataRow["CaptionP"]) == "" || Converts.ToString(dataRow["CaptionP"]) == null)
                    //{
                    //    Label lblBlank = new Label();
                    //    lblBlank = CreateFormControl_Label("capP" + Converts.ToString(dataRow["ObjID"]),
                    //              Converts.ToString(dataRow["CaptionP"]), 5, 0, 100, 20, true);

                    //    //AddControlToWebTable(lblBlank, 0, rowNumber, tblSubContainer, 0);  
                    //    if (kCol != 4)

                    //        AddControlToWebTable(lblBlank, kCol++, kRow, tblSubContainer, 0);
                    //    else
                    //    {
                    //        kCol = 0;
                    //        kRow = kRow + 1;

                    //        AddControlToWebTable(lblBlank, kCol++, kRow, tblSubContainer, 0);
                    //    }

                    //}

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(chk, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        //AddControlToWebTable(chk, 1, rowNumber, tblSubContainer, 0);
                        if (kCol != 5)

                            AddControlToWebTable(chk, kCol++, kRow, tblSubContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(chk, kCol++, kRow, tblSubContainer, 0);
                        }
                    }
                    //tblSubContainer.SetColumnSpan(chk, 2);

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(chk, sMsg, ""));

                    chk.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(chk);
                    break;
                #endregion

                #region " CBO,DDL "
                case "CBO":
                    ddl = CreateFormControl_ComboBox("CBO" + Converts.ToString(dataRow["ObjID"]), Converts.ToString(dataRow["AddOnValue"]),
                        Converts.ToString(dataRow["MapField"]), 0, 0, (int)Converts.ToDouble(dataRow["Width"]), 22);
                    ddl.Height = 24;

                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue1"]).Trim();
                    else if (dataRow["DefaultValue"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue"]).Trim();
                    if (ddl.SelectedIndex < 0 && ddl.Items.Count > 0) ddl.SelectedIndex = 0;
                    // tblSubContainer.Controls.Add(ddl, 1, rowNumber);

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(ddl, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        //AddControlToWebTable(ddl, 1, rowNumber, tblSubContainer,0);  
                        if (kCol != 5)

                            AddControlToWebTable(ddl, kCol++, kRow, tblSubContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(ddl, kCol++, kRow, tblSubContainer, 0);
                        }
                    }
                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(ddl, sMsg, ""));

                    ddl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(ddl);
                    break;
                #endregion

                #region " CBONUM, CBOEQNUM, CBOTXT, CBOEQTXT, CBOD, CBOP, CBOEXDT "
                case "CBONUM":
                case "CBOEQNUM":
                case "CBOTXT":
                case "CBOEQTXT":
                case "CBOD":
                case "CBOP":
                case "CBOEQDT":
                    if (Converts.ToString(dataRow["ObjType"]) == "CBOD" || Converts.ToString(dataRow["ObjType"]) == "CBOEQDT")
                        iWd = 110;
                    if (Converts.ToString(dataRow["ObjType"]) == "CBOP" || Converts.ToString(dataRow["ObjType"]) == "CBOTXT" || Converts.ToString(dataRow["ObjType"]) == "CBOEQTXT")
                        iWd = 180;
                    else iWd = 80;

                    if (Converts.ToString(dataRow["ObjType"]) == "CBOEQNUM" || Converts.ToString(dataRow["ObjType"]) == "CBOEQDT")
                        sVal = "1~<=|2~=|3~>=";
                    else if (Converts.ToString(dataRow["ObjType"]) == "CBOEQTXT")
                        sVal = "1~Starts with|2~Contains|3~Does Not contain";
                    else sVal = Converts.ToString(dataRow["AddOnValue"]);

                    pnl = new Panel();
                    pnl.ID = "pnlcboNum" + Converts.ToString(dataRow["ObjID"]);
                    //pnl.Height = 24;
                    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]) + 400;  //(int)Converts.ToDouble(dataRow["Width"]) + 4;

                    ///Richa
                    Table tblCBOp = new Table();
                    tblCBOp.ID = "pnlcboNumtbl" + Converts.ToString(dataRow["ObjID"]);


                    ddl = CreateFormControl_ComboBox("cbotxtn" + Converts.ToString(dataRow["ObjID"]), sVal,
                       Converts.ToString(dataRow["MapField"]), 0, 0,
                       (int)Converts.ToDouble(dataRow["Width"]) - 100, 22);
                    ddl.Height = 24;//240

                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue1"]).Trim();
                    else if (dataRow["DefaultValue"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue"]).Trim();
                    if (ddl.SelectedIndex < 0 && ddl.Items.Count > 0) ddl.SelectedIndex = 0;

                    //pnl.Controls.Add(ddl);
                    ///Richa
                    AddControlToWebTable(ddl, 0, 0, tblCBOp, 0);

                    if (Converts.ToString(dataRow["ObjType"]) == "CBONUM" || Converts.ToString(dataRow["ObjType"]) == "CBOEQNUM")
                    {
                        //txtN = CreateFormControl_TextBoxNumeric("tntcbotxtn" + Converts.ToString(dataRow["ObjID"]), "",
                        //    Converts.ToString(dataRow["MapField1"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                        //    (int)Converts.ToDouble(dataRow["ShowNotDefined"]),
                        //    0, (int)pnl.Width.Value - 84, 80, 20);
                        //if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                        //    txtN.Value = Converts.ToDouble(dataRow["DefaultValue2"]);
                        ////pnl.Controls.Add(txtN);
                        //AddControlToWebTable(txtN, 1, 0, tblCBOp, 0);

                        //if (Converts.ToBoolean(dataRow["Mandatory"]))
                        //    MandatoryCtrls.Add(AddArrayList2ArrayList(txtN, sMsg, ""));
                        ////iWd = txtN.Left;
                    }
                    else if (Converts.ToString(dataRow["ObjType"]) == "CBOD" || Converts.ToString(dataRow["ObjType"]) == "CBOEQDT")
                    {
                        dtp = CreateFormControl_DateTimePicker("dtp1" + Converts.ToString(dataRow["ObjID"]),
                            Converts.ToString(dataRow["MapField1"]), 0, (int)pnl.Width.Value - 94,
                            Converts.ToBoolean(dataRow["Mandatory"]), 90, 20);

                        if (Converts.ToString(dataRow["MapField1"]).ToUpper().Trim() == "STARTDATE" && gblFromDate.Trim() != "")
                            dtp.Date = Converts.ToString(Convert.ToDateTime(gblFromDate));
                        if (Converts.ToString(dataRow["MapField1"]).ToUpper().Trim() == "ENDDATE" && gblToDate.Trim() != "")
                            dtp.Date = Converts.ToString(Convert.ToDateTime(gblToDate));
                        else
                        {
                            if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                                dtp.Date = Converts.ToString(Convert.ToDateTime(dataRow["DefaultValue2"]));
                        }
                        //pnl.Controls.Add(dtp);
                        AddControlToWebTable(dtp, 1, 0, tblCBOp, 0);

                        //iWd = dtp.Left;
                    }
                    else if (Converts.ToString(dataRow["ObjType"]) == "CBOP")
                    {
                        dtp = CreateFormControl_DateTimePicker("dtp1" + Converts.ToString(dataRow["ObjID"]),
                            Converts.ToString(dataRow["MapField1"]), 0, (int)pnl.Width.Value - 200,
                            Converts.ToBoolean(dataRow["Mandatory"]), 150, 20);
                        //pnl.Controls.Add(dtp);
                        AddControlToWebTable(dtp, 1, 0, tblCBOp, 0);

                        lbl = CreateFormControl_Label("lblSep" + Converts.ToString(dataRow["ObjID"]), "to",
                            0, 0, 0, 0, true);
                        //pnl.Controls.Add(lbl);
                        AddControlToWebTable(lbl, 2, 0, tblCBOp, 0);

                        dtp2 = CreateFormControl_DateTimePicker("dtp2" + Converts.ToString(dataRow["ObjID"]),
                           Converts.ToString(dataRow["MapField2"]), 0, (int)pnl.Width.Value - 94,
                           Converts.ToBoolean(dataRow["Mandatory"]), 150, 20);
                        //lbl.Left = dtp2.Left - lbl.Width - 2;
                        //dtp.Left = lbl.Left - dtp.Width - 2;
                        SetGlobalDateRange(dtp, dtp2);
                        //pnl.Controls.Add(dtp2);
                        AddControlToWebTable(dtp2, 3, 0, tblCBOp, 0);
                        //iWd = dtp.Left;
                    }
                    else // if (ConvertObject2String(dataRow["ObjType"]) == "ddlTXT" || ConvertObject2String(dataRow["ObjType"]) == "ddlEQTXT")
                    {
                        txt = CreateFormControl_TextBox("txtcbotxtn" + Converts.ToString(dataRow["ObjID"]), "",
                            Converts.ToString(dataRow["MapField1"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                            0, (int)pnl.Width.Value - 154, 150, 20);
                        if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                            ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue2"]).Trim();
                        //pnl.Controls.Add(txt);
                        AddControlToWebTable(txt, 1, 0, tblCBOp, 0);
                        if (Converts.ToBoolean(dataRow["Mandatory"]))
                            MandatoryCtrls.Add(AddArrayList2ArrayList(txt, sMsg, ""));
                        //iWd = txt.Left;
                    }
                    //ddl.Width = iWd - ddl.Left - 4;
                    //tblSubContainer.Controls.Add(pnl, 1, rowNumber);
                    //AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer,0);  

                    pnl.Controls.Add(tblCBOp);
                    if (Converts.ToString(dataRow["ObjType"]) == "CBOP")
                    {
                        //AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer, 0);
                        AddControlToWebTable(pnl, 1, kRow, tblSubContainer, 4);
                        kCol = 0;
                        kRow = kRow + 1;


                    }
                    else
                    {

                        if (!bShowHControl)//(kCol == -1)
                            AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer, 0);
                        else
                        {
                            if (kCol != 5)
                                AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 0);

                            else
                            {
                                kCol = 0;
                                kRow = kRow + 1;

                                AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 0);
                            }
                        }
                    }
                    // tblSubContainer.SetColumnSpan(pnl, 2);
                    if (dataRow["DefaultValue"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue"]).Trim();
                    if (ddl.SelectedIndex < 0 && ddl.Items.Count > 0) ddl.SelectedIndex = 0;

                    pnl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                    break;
                #endregion

                #region " DTP "
                case "DTP":
                    pnl = new Panel();
                    pnl.ID = "pnldtp" + Converts.ToString(dataRow["ObjID"]);
                    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]);

                    //pnl.Height = 24;
                    //pnl.BorderWidth = 1;
                    //pnl.BorderColor = System.Drawing.Color.Black;
                    ///Richa
                    Table tbldtp = new Table();
                    tbldtp.ID = "pnltbl" + Converts.ToString(dataRow["ObjID"]);
                    tbldtp.CellPadding = 0;
                    tbldtp.CellSpacing = 0;
                    //tbldtp.BorderStyle = BorderStyle.Solid;
                    //tbldtp.BorderWidth = 1;


                    dtp = CreateFormControl_DateTimePicker("dtp1" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["MapField"]), 0, 0,
                        Converts.ToBoolean(dataRow["Mandatory"]),
                        ((int)Converts.ToDouble(dataRow["Width"]) / 2), 20);
                    ///Richa
                    AddControlToWebTable(dtp, 0, 0, tbldtp, 0);

                    // pnl.Controls.Add(dtp);

                    sVal = Converts.ToString(dataRow["CaptionM"]);
                    if (sVal.Trim() == "") sVal = "to";
                    lbl = CreateFormControl_Label("lblSep" + Converts.ToString(dataRow["ObjID"]), sVal,
                        2, 0, 10, 20, true);

                    //pnl.Controls.Add(lbl);
                    AddControlToWebTable(lbl, 1, 0, tbldtp, 0);


                    dtp2 = CreateFormControl_DateTimePicker("dtp2" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["MapField1"]), 0, (int)pnl.Width.Value - 104,
                        Converts.ToBoolean(dataRow["Mandatory"]),
                        ((int)Converts.ToDouble(dataRow["Width"]) / 2), 20);
                    SetGlobalDateRange(dtp, dtp2);
                    //pnl.Controls.Add(dtp2);
                    AddControlToWebTable(dtp2, 2, 0, tbldtp, 0);


                    pnl.Controls.Add(tbldtp);


                    //lbl.Left = dtp2.Left - lbl.Width - 2;

                    ///tblSubContainer.Controls.Add(pnl, 1, rowNumber);

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        //AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer,0);  
                        if (kCol != 5)

                            AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 0);
                        }
                    }
                    // tblSubContainer.SetColumnSpan(pnl, 2);

                    pnl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                    break;
                #endregion

                #region " MS, MSM "
                case "MS":
                case "MSM":
                    hlp = CreateFormControl_HelpBox("hlp" + Converts.ToString(dataRow["ObjID"]), dataRow["CaptionM"].ToString(),
                        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]), dataRow["MapField3"].ToString(),
                        (Converts.ToString(dataRow["ObjType"]) == "MSM"), Converts.ToDouble(dataRow["ShowNotDefined"]) > 0,
                        3, 550, 450, (int)Converts.ToDouble(dataRow["Width"]),
                        Converts.ToString(dataRow["FASObjType"]), Converts.ToString(dataRow["LinkToObject"]));
                    UserControl.HelpControl hlpMulti = new ITHeart.UserControl.HelpControl();

                    //tblSubContainer.Controls.Add(hlp, 1, rowNumber);


                    if (Converts.ToString(dataRow["FASObjType"]) == "")
                    {
                        if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                            hlp.SelectID = Converts.ToString(dataRow["DefaultValue1"]);
                        else if (dataRow["DefaultValue"] != null)
                            hlp.SelectID = Converts.ToString(dataRow["DefaultValue"]);
                    }
                    else
                    {

                        

                    }

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(hlp, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        //AddControlToWebTable(hlp, 1, rowNumber, tblSubContainer,0);
                        if (kCol != 5)

                            AddControlToWebTable(hlp, kCol++, kRow, tblSubContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(hlp, kCol++, kRow, tblSubContainer, 0);
                        }
                    }
                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(hlp, sMsg, ""));
                    if (EnableReset) hlp.IsResetable = true;

                    hlp.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(hlp);
                    break;
                #endregion
                #region " FL-, FLT "
                //case "FL-":
                //case "FLT":
                //    flp = CreateFormControl_FileBox("flp" + Converts.ToString(dataRow["ObjID"]), dataRow["CaptionM"].ToString(),
                //        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]), dataRow["MapField3"].ToString(),
                //        (Converts.ToString(dataRow["ObjType"]) == "MSM"), Converts.ToDouble(dataRow["ShowNotDefined"]) > 0,
                //        3, 550, 450, (int)Converts.ToDouble(dataRow["Width"]),
                //        Converts.ToString(dataRow["FASObjType"]), Converts.ToString(dataRow["LinkToObject"]));

                //    flp.SetValue(Converts.ToString(dataRow["GenID"]), Converts.ToString(dataRow["MapField"]), bCanEdit);
                //    flp.ShowTextbox = (Converts.ToString(dataRow["ObjType"]).ToUpper() == "FLT");

                //    if (!bShowHControl)//(kCol == -1)
                //        AddControlToWebTable(flp, 1, rowNumber, tblSubContainer, 0);
                //    else
                //    {
                //        //AddControlToWebTable(hlp, 1, rowNumber, tblSubContainer,0);
                //        if (kCol != 5)

                //            AddControlToWebTable(flp, kCol++, kRow, tblSubContainer, 0);

                //        else
                //        {
                //            kCol = 0;
                //            kRow = kRow + 1;

                //            AddControlToWebTable(flp, kCol++, kRow, tblSubContainer, 0);
                //        }
                //    }
                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(flp, sMsg, ""));
                //    //if (EnableReset) hlp.setDefaults("", "null");

                //    flp.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(flp);
                //    break;
                #endregion
                #region RD
                case "RD":
                    pnl = new Panel();
                    int rdCol = 0;
                    int rdRow = 0;
                    //pnl.AutoSize = false;
                    //pnl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    pnl.Attributes.Add("AutoSizeMode", "GrowAndShrink");


                    int RDWidth = (int)Converts.ToDouble(dataRow["Width"]);

                    ///Add Properties of Panel
                    pnl.BorderStyle = BorderStyle.Solid;
                    pnl.BorderWidth = (Unit)C.ConvertFromString("1px");
                    pnl.BorderColor = System.Drawing.Color.LightGray;

                    //pnl.Width = RDWidth + 20;  //  tblSubContainer.Width - 30;
                    //pnl.Height = 5;
                    pnl.ID = "pnlrd" + Converts.ToString(dataRow["ObjID"]);
                    Table tblp = new Table();
                    tblp.ID = "pnltbl" + Converts.ToString(dataRow["ObjID"]);

                    //tblp.ColumnCount = 1;
                    //tblp.CellBorderStyle = tblSubContainer.CellBorderStyle;
                    //tblp.AutoSize = true;

                    pnl.Attributes.Add("Tag", Converts.ToString(dataRow["MapField"]));
                    tblp.Attributes.Add("Tag", Converts.ToString(dataRow["MapField"]));

                    sVal = "";
                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        sVal = Converts.ToString(dataRow["DefaultValue1"]);
                    else if (dataRow["DefaultValue"] != null)
                        sVal = Converts.ToString(dataRow["DefaultValue"]);

                    if (Converts.ToString(dataRow["AddOnValue"]) != "")
                    {
                        if (Converts.ToString(dataRow["AddOnValue"]).IndexOf("!$!") < 0)
                        {
                            string[] rdar = Converts.ToString(dataRow["AddOnValue"]).Split('|');
                            //tblp.RowCount = rdar.Length;
                            for (int j = 0; j < rdar.Length; j++)
                            {
                                RadioButton rdp = new RadioButton();
                                rdp.Width = pnl.Width;
                                rdp.CssClass = "SelectionControls";
                                ///Provide Group name 
                                rdp.GroupName = Converts.ToString(dataRow["ObjID"]);
                                rdp.ID = "rdo" + (j + 1).ToString() + Converts.ToString(dataRow["ObjID"]);
                                string[] _cb = rdar[j].ToString().Split('~');
                                if (_cb.Length < 1) continue;

                                rdp.Text = _cb[1].ToString();
                                rdp.Attributes.Add("Tag", _cb[0].ToString().Replace('!', '~'));
                                if ((sVal == "" && j == 0) || (sVal != "" && sVal.Replace('!', '~') == _cb[0].ToString().Trim().Replace('!', '~')))
                                    rdp.Checked = true;
                                //tblp.Controls.Add(rdp);

                                if (!bShowHControl)//(kCol == -1)
                                    AddControlToWebTable(rdp, 0, j, tblp, 0);
                                else
                                {
                                    //AddControlToWebTable(rdp, 0, j, tblp,0);  
                                    if (rdCol != 4)

                                        AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                    else
                                    {
                                        rdCol = 0;
                                        rdRow = rdRow + 1;

                                        AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string rdar = Converts.ToString(dataRow["AddOnValue"]).Substring(3);
                            if (rdar.Length > 0)
                            {
                                Dictionary<string, ArrayList> SP_paramsd = new Dictionary<string, ArrayList>();
                                SP_paramsd.Add("@Qry", ArrayLists.ToArrayList("@Qry", rdar.Replace(',', ';'), SqlDbType.VarChar));

                                DataSet dsrdp = base.ExecuteData(spNames.ExecuteQry, SP_paramsd);

                                if (!string.IsNullOrEmpty(base.Message))
                                {
                                    _Message = base.Message;
                                    return;
                                }

                                //tblp.RowCount = dsrdp.Tables[0].Rows.Count;
                                for (int j = 0; j < dsrdp.Tables[0].Rows.Count; j++)
                                {
                                    RadioButton rdp = new RadioButton();
                                    rdp.Width = (int)rdp.Width.Value + RDWidth;
                                    rdp.CssClass = "SelectionControls";

                                    ///provide Group Name
                                    rdp.GroupName = Converts.ToString(dataRow["ObjID"]);
                                    rdp.ID = "rdp" + (j + 1).ToString() + Converts.ToString(dataRow["ObjID"]);
                                    rdp.Attributes.Add("Tag", Converts.ToString(dsrdp.Tables[0].Rows[j]["MainID"]));
                                    rdp.Text = Converts.ToString(dsrdp.Tables[0].Rows[j]["MainDescr"]);
                                    pnl.Height = (int)pnl.Height.Value + (int)rdp.Height.Value;
                                    //pnl.Height += (int)rdp.Height.Value ;
                                    if ((sVal == "" && j == 0) || (sVal != "" && sVal.Replace(',', ';') == Converts.ToString(dsrdp.Tables[0].Rows[j]["MainID"]).Trim()))
                                        rdp.Checked = true;
                                    //tblp.Controls.Add(rdp);

                                    if (!bShowHControl)//(kCol == -1)
                                        AddControlToWebTable(rdp, 0, j, tblp, 0);
                                    else
                                    {
                                        //AddControlToWebTable(rdp, 0, j, tblp,0); 
                                        if (rdCol != 4)

                                            AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                        else
                                        {
                                            rdCol = 0;
                                            rdRow = rdRow + 1;

                                            AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    pnl.Controls.Add(tblp);
                    if (Converts.ToDouble(dataRow["MaxLength"]) > 0) pnl.Height = (int)Converts.ToDouble(dataRow["MaxLength"]);
                    // if (tblp.Controls.Count > 0) pnl.Height = tblp.Controls.Count * 30;

                    //tblSubContainer.Controls.Add(pnl, 1, rowNumber);
                    //AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer,0); 

                    // pnl.Width = RDWidth *  tblp.Controls.Count;

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer, 0);
                    else
                    {
                        if (kCol >= 1)
                        {
                            AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 4);
                            kCol = 0;
                            kRow = kRow + 1;

                        }
                    }

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(pnl, sMsg, ""));

                    pnl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);

                    break;
                #endregion

                #region CHKNUM
                //case "CHKNUM":
                //    pnl = new Panel();
                //    pnl.ID = "pnlchknum" + Converts.ToString(dataRow["ObjID"]);
                //    pnl.Height = 24;
                //    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]) + 4;

                //    chk = CreateFormControl_CheckBox("chknum" + Converts.ToString(dataRow["ObjID"]),
                //        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]),
                //        0, 0, true);

                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue1"]) > 0);
                //    else if (dataRow["DefaultValue"] != null)
                //        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue"]) > 0);

                //    pnl.Controls.Add(chk);

                //    txtN = CreateFormControl_TextBoxNumeric("tntchknum" + Converts.ToString(dataRow["ObjID"]), "",
                //        Converts.ToString(dataRow["MapField1"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                //        (int)Converts.ToDouble(dataRow["ShowNotDefined"]),
                //        0, (int)pnl.Width.Value - 64, 60, 20);
                //    if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                //        txtN.Text = Converts.ToDouble(dataRow["DefaultValue2"]);
                //    pnl.Controls.Add(txtN);

                //    //if (chk.Left + chk.Width < (int)pnl.Width.Value  - 40)
                //    //{
                //    //    txtN.Left = chk.Left + chk.Width + 4;
                //    //    txtN.Width = pnl.Width - txtN.Left - 4;
                //    //}
                //    //{
                //    //    txtN.Width = 40;
                //    //   txtN.Left = pnl.Width + txtN.Width - 4;
                //    //}

                //    //tblSubContainer.Controls.Add(pnl, 1, rowNumber);

                //    if (!bShowHControl)//(kCol == -1)
                //        AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer, 0);
                //    else
                //    {
                //        //AddControlToWebTable(pnl, 1, rowNumber, tblSubContainer,0);  
                //        if (kCol != 5)

                //            AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 0);

                //        else
                //        {
                //            kCol = 0;
                //            kRow = kRow + 1;

                //            AddControlToWebTable(pnl, kCol++, kRow, tblSubContainer, 0);
                //        }
                //    }
                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(txtN, sMsg, ""));

                //    pnl.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                //    break;
                #endregion

                #region "TAB"
                //case "TAB":
                //    // Label templbl=new Label();
                //    pnl = new Panel();
                //    pnl.ID = "pnlTab" + Converts.ToString(dataRow["ObjID"]);
                //    pnl.Height = 24;
                //    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]) + 104;
                //    pnl.Height = (int)Converts.ToDouble(dataRow["MaxLength"]) + 54;


                //    C1.Web.UI.Controls.C1TabControl.C1TabControl tb = new C1.Web.UI.Controls.C1TabControl.C1TabControl();
                //    //C1.Web.UI.Controls.C1TabControl.C1TabPageCollection tb = new C1.Web.UI.Controls.C1TabControl.C1TabPageCollection();

                //    tb.ID = "tbc" + Converts.ToString(dataRow["ObjID"]);
                //    tb.Attributes.Add("Tag", Converts.ToString(dataRow["MapField"]));

                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        tb.SelectedIndex = (int)Converts.ToDouble(dataRow["DefaultValue1"]);
                //    else if (dataRow["DefaultValue"] != null)
                //        tb.SelectedIndex = (int)Converts.ToDouble(dataRow["DefaultValue"]);


                //    tb.Width = (int)pnl.Width.Value - 4; tb.Height = (int)pnl.Height.Value - 4;
                //    string[] arrTabAddOn = Converts.ToString(dataRow["AddOnValue"]).Split('|');
                //    //tb.TabCount = arrTabAddOn.Length;
                //    for (int xloop = 0; xloop < arrTabAddOn.Length; xloop++)
                //    {
                //        string[] temp = arrTabAddOn[xloop].Split('~');
                //        DataRow[] dr = dataRow.Table.Select("tabControlObj='" + Converts.ToString(dataRow["ObjID"]) + "' and TabControlPage = " + temp[0]);
                //        if (dr.Length <= 0) continue;

                //        // tb.TabPages.Add("tabPage" + temp[0], temp[1]); 
                //        C1.Web.UI.Controls.C1TabControl.C1TabPage tbp = new C1.Web.UI.Controls.C1TabControl.C1TabPage();
                //        tbp.Text = temp[1].ToString();

                //        //  tb.TabPages[tb.TabPages.Count-1].ID  = temp[1].ToString();
                //        Table tbl = new Table();

                //        //tbp.RowCount = dr.Length;
                //        //tbp.ColumnCount = 2;
                //        //tbp.Dock = DockStyle.Fill;
                //        tbl.ID = "tabTableLayout" + Converts.ToString(dataRow["ObjID"]) + temp[0].ToString();
                //        //tb.TabPages.Add(C1.Web.UI.Controls.C1TabControl.C1TabPage);   

                //        // tb.TabPages[].Controls.Add(tbp);

                //        for (int yloop = 0; yloop < dr.Length; yloop++)
                //        {  ///confusion
                //            bShowHControl = false;
                //            //kCol = -1;
                //            AddControls(tbl, dr[yloop], MandatoryCtrls, KeyBasedCtrls, yloop, EnableReset, ShowUserSettings, bShowHControl);


                //        }

                //        //Panel pnl1 = new Panel();
                //        //pnl1.ID = "pnlgap";
                //        //pnl1.Height = 0;
                //        tbp.Controls.Add(tbl);
                //        tb.TabPages.AddAt((int)Converts.ToDouble(temp[0]), tbp);
                //        //tbp.Controls.Add(pnl1, 1, dr.Length + 1);
                //    }
                //    //tb.Dock = DockStyle.Fill;

                //    //AddControlToWebTable(tb, 1, rowNumber, tblSubContainer,4);  

                //    if (!bShowHControl) //(kCol == -1)
                //    {
                //        kRow = kRow + 1;
                //        Label lblBlank = new Label();
                //        lblBlank = CreateFormControl_Label("capPBnk" + Converts.ToString(dataRow["ObjID"]),
                //                  Converts.ToString(dataRow["CaptionP"]), 5, 0, 100, 20, true);

                //        AddControlToWebTable(lblBlank, 0, kRow, tblSubContainer, 0);
                //        AddControlToWebTable(tb, 1, kRow, tblSubContainer, 4);
                //    }


                //    //AddControlToWebTable(tb, 1, kRow, tblSubContainer, 4);  


                //    ///For gap
                //    Panel pnl1 = new Panel();
                //    pnl1.ID = "pnlgap";
                //    pnl1.Height = 24;

                //    // tblSubContainer.Controls.Add(pnl, 0, rowNumber);
                //    AddControlToWebTable(pnl1, 0, ++kRow, tblSubContainer, 5);


                //    //TableLayoutSettings tableSettings = tblSubContainer.LayoutSettings as TableLayoutSettings; ;
                //    //tableSettings.SetColumnSpan(pnl, 2);

                //    pnl.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                //    break;
                #endregion

                #region Spinner
                //case "SPN":
                //    pnl = new Panel();
                //    pnl.ID = "pnlnumupdn" + Converts.ToString(dataRow["ObjID"]);
                //    pnl.Height = 24;
                //    pnl.Width = Converts.ToInt32(dataRow["Width"].ToString()) + 4;

                //    num = CreateFormControl_NumericUpDown("num" + Converts.ToString(dataRow["ObjID"]),
                //        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]),
                //        0, 0, Converts.ToInt32(dataRow["Width"].ToString()), false);


                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        num.Value = (int)Converts.ToDouble(dataRow["DefaultValue1"]);
                //    else if (dataRow["DefaultValue"] != null)
                //        num.Value = (int)Converts.ToDouble(dataRow["DefaultValue"]);

                //    num.Increment = Math.Max((int)Converts.ToDouble(dataRow["MaxLength"]), 1);
                //    pnl.Controls.Add(num);

                //    tblSubContainer.Controls.Add(pnl, 1, rowNumber);

                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(num, sMsg, ""));

                //    pnl.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                //    break;
                #endregion
            }

        }
        #endregion

        #region " Create a Form Level Control "
        public void AddControlsV1(Table tblContainer, DataRow dataRow, ArrayList MandatoryCtrls, ArrayList KeyBasedCtrls, int rowNumber, bool EnableReset, bool ShowUserSettings, bool bShowHControl)
        {
            #region " Initialize Variables  "
            //tblContainer.CellBorderStyle = TableCellBorderStyle.Single;
            int iWd = 0, KeyBased = 0;
            string sVal = "", sMsg = "";

            KeyBased = (int)Converts.ToDouble(dataRow["KeyBased"]);
            sMsg = "Please specify " + Converts.ToString(dataRow["CaptionP"]);

            Label lbl;
            TextBox txt;
            TextBox txtN;
            CheckBox chk;
            UserControl.DatePickerControl dtp, dtp2;
            DropDownList ddl;
            UserControl.HelpControl hlp;
            //UserControl.FileControl flp;
            Panel pnl;
            #endregion

            #region " Label "
            if (kCol == 2)
            { AddControlToWebTable("&nbsp;", kCol++, kRow, tblContainer, 0); }

            lbl = CreateFormControl_Label("capP" + Converts.ToString(dataRow["ObjID"]),
                Converts.ToString(dataRow["CaptionP"]), 5, 0, 100, 20, true);
            if (!string.IsNullOrEmpty(Converts.ToString(dataRow["CaptionP"])))
            {
                if (KeyBased > 0)
                {
                    KeyBasedCtrls.Add(lbl);
                    lbl.ForeColor = System.Drawing.Color.Red;
                }
                lbl.Visible = (KeyBased < 1);
            }

            if (!bShowHControl)
                AddControlToWebTable(lbl, 0, rowNumber, tblContainer, 0);
            else
            {
                if (Converts.ToString(dataRow["ObjType"]) == "RD" && kCol > 1)
                {
                    kCol = 0;
                    kRow = kRow + 1;
                    AddControlToWebTable(lbl, kCol++, kRow, tblContainer, 0);

                }
                else if (kCol != 5)
                    AddControlToWebTable(lbl, kCol++, kRow, tblContainer, 0);
                else
                {
                    kCol = 0;
                    kRow = kRow + 1;
                    AddControlToWebTable(lbl, kCol++, kRow, tblContainer, 0);

                }
            }
            #endregion

            switch (Converts.ToString(dataRow["ObjType"]).ToUpper())
            {
                #region " TXT, TXTM "
                case "TXT":
                case "TXTM":
                case "TXTN":
                case "TXTP":
                    txt = CreateFormControl_TextBox("txt" + Converts.ToString(dataRow["ObjID"]), "",
                        Converts.ToString(dataRow["MapField"]),
                        (int)Converts.ToDouble(dataRow["Maxlength"]),
                        0, 0, (int)Converts.ToDouble(dataRow["Width"]), 20);

                    if (Converts.ToString(dataRow["ObjType"]).ToUpper() == "TXTP")
                        txt.TextMode = TextBoxMode.Password;//    = '*';
                    else if (Converts.ToString(dataRow["ObjType"]).ToUpper() == "TXTM")
                    {
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.SkinID = "LogixText-10-Height-50";
                        // txt.Height = 60;
                    }
                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        txt.Text = Converts.ToString(dataRow["DefaultValue1"]);
                    else if (dataRow["DefaultValue"] != null)
                        txt.Text = Converts.ToString(dataRow["DefaultValue"]);
                    //tblContainer.Controls.Add(txt, 1, rowNumber);

                    //AddControlToWebTable(txt, 1, rowNumber, tblContainer,0);  

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(txt, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        if (kCol != 5)

                            AddControlToWebTable(txt, kCol++, kRow, tblContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(txt, kCol++, kRow, tblContainer, 0);
                        }
                    }

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(txt, sMsg, ""));

                    txt.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(txt);
                    break;
                #endregion

                #region " TXTN "
                //case "TXTN":
                //    txtN = CreateFormControl_TextBoxNumeric("tntn" + Converts.ToString(dataRow["ObjID"]), "",
                //        Converts.ToString(dataRow["MapField"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                //        (int)Converts.ToDouble(dataRow["ShowNotDefined"]),
                //        0, 0, (int)Converts.ToDouble(dataRow["Width"]), 20);

                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        txtN.Value = Converts.ToDouble(dataRow["DefaultValue1"]);
                //    else if (dataRow["DefaultValue"] != null)
                //        txtN.Value = Converts.ToDouble(dataRow["DefaultValue"]);
                //    //tblContainer.Controls.Add(txtN, 1, rowNumber);
                //    //AddControlToWebTable(txtN, 1, rowNumber, tblContainer,1);  
                //    if (!bShowHControl)//(kCol == -1)
                //        AddControlToWebTable(txtN, 1, rowNumber, tblContainer, 0);
                //    else
                //    {
                //        if (kCol != 5)
                //            AddControlToWebTable(txtN, kCol++, kRow, tblContainer, 0);

                //        else
                //        {
                //            kCol = 0;
                //            kRow = kRow + 1;

                //            AddControlToWebTable(txtN, kCol++, kRow, tblContainer, 0);
                //        }
                //    }


                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(txtN, sMsg, ""));

                //    txtN.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(txtN);
                //    break;
                #endregion

                #region " DT "
                case "DT-":
                    dtp = CreateFormControl_DateTimePicker("dt_" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["MapField"]), 0, 0,
                        Converts.ToBoolean(dataRow["Mandatory"]),
                        (int)Converts.ToDouble(dataRow["Width"]) * 2, 20);

                    if (Converts.ToString(dataRow["MapField"]).ToUpper().Trim() == "STARTDATE" && gblFromDate.Trim() != "")
                        dtp.Date = Converts.ToString(Convert.ToDateTime(gblFromDate));
                    if (Converts.ToString(dataRow["MapField"]).ToUpper().Trim() == "ENDDATE" && gblToDate.Trim() != "")
                        dtp.Date = Converts.ToString(Convert.ToDateTime(gblToDate));
                    else
                    {
                        //if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        //    dtp.Date = Converts.ToString(Convert.ToDateTime(dataRow["DefaultValue1"]));
                        if (dataRow["DefaultValue"] != null && dataRow["DefaultValue"] != DBNull.Value)
                        {
                            try
                            {
                                dtp.Date = Converts.ToDateTime(Convert.ToDateTime(dataRow["DefaultValue"]), "dd-MMM-yyyy");
                            }
                            catch { }
                        }
                    }

                    //AddControlToWebTable(dtp, 1, rowNumber, tblContainer,0); 
                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(dtp, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        if (kCol != 5)

                            AddControlToWebTable(dtp, kCol++, kRow, tblContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(dtp, kCol++, kRow, tblContainer, 0);
                        }
                    }
                    //tblContainer.Controls.Add(dtp, 1, rowNumber);
                    // tblContainer.SetColumnSpan(dtp, 2);

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(dtp, sMsg, ""));

                    dtp.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(dtp);
                    break;
                #endregion

                #region " CHK "
                case "CHK":
                    chk = CreateFormControl_CheckBox("chk" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]),
                        0, 0, true);

                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue1"]) > 0);
                    else if (dataRow["DefaultValue"] != null)
                        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue"]) > 0);

                    //tblContainer.Controls.Add(chk, 1, rowNumber);
                    //richa
                    //if (Converts.ToString(dataRow["CaptionP"]) == "" || Converts.ToString(dataRow["CaptionP"]) == null)
                    //{
                    //    Label lblBlank = new Label();
                    //    lblBlank = CreateFormControl_Label("capP" + Converts.ToString(dataRow["ObjID"]),
                    //              Converts.ToString(dataRow["CaptionP"]), 5, 0, 100, 20, true);

                    //    //AddControlToWebTable(lblBlank, 0, rowNumber, tblContainer, 0);  
                    //    if (kCol != 4)

                    //        AddControlToWebTable(lblBlank, kCol++, kRow, tblContainer, 0);
                    //    else
                    //    {
                    //        kCol = 0;
                    //        kRow = kRow + 1;

                    //        AddControlToWebTable(lblBlank, kCol++, kRow, tblContainer, 0);
                    //    }

                    //}

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(chk, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        //AddControlToWebTable(chk, 1, rowNumber, tblContainer, 0);
                        if (kCol != 5)

                            AddControlToWebTable(chk, kCol++, kRow, tblContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(chk, kCol++, kRow, tblContainer, 0);
                        }
                    }
                    //tblContainer.SetColumnSpan(chk, 2);

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(chk, sMsg, ""));

                    chk.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(chk);
                    break;
                #endregion

                #region " CBO,DDL "
                case "CBO":
                    ddl = CreateFormControl_ComboBox("CBO" + Converts.ToString(dataRow["ObjID"]), Converts.ToString(dataRow["AddOnValue"]),
                        Converts.ToString(dataRow["MapField"]), 0, 0, (int)Converts.ToDouble(dataRow["Width"]), 22);
                    ddl.Height = 24;

                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue1"]).Trim();
                    else if (dataRow["DefaultValue"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue"]).Trim();
                    if (ddl.SelectedIndex < 0 && ddl.Items.Count > 0) ddl.SelectedIndex = 0;
                    // tblContainer.Controls.Add(ddl, 1, rowNumber);

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(ddl, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        //AddControlToWebTable(ddl, 1, rowNumber, tblContainer,0);  
                        if (kCol != 5)

                            AddControlToWebTable(ddl, kCol++, kRow, tblContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(ddl, kCol++, kRow, tblContainer, 0);
                        }
                    }
                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(ddl, sMsg, ""));

                    ddl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(ddl);
                    break;
                #endregion

                #region " CBONUM, CBOEQNUM, CBOTXT, CBOEQTXT, CBOD, CBOP, CBOEXDT "
                case "CBONUM":
                case "CBOEQNUM":
                case "CBOTXT":
                case "CBOEQTXT":
                case "CBOD":
                case "CBOP":
                case "CBOEQDT":
                    if (Converts.ToString(dataRow["ObjType"]) == "CBOD" || Converts.ToString(dataRow["ObjType"]) == "CBOEQDT")
                        iWd = 110;
                    if (Converts.ToString(dataRow["ObjType"]) == "CBOP" || Converts.ToString(dataRow["ObjType"]) == "CBOTXT" || Converts.ToString(dataRow["ObjType"]) == "CBOEQTXT")
                        iWd = 180;
                    else iWd = 80;

                    if (Converts.ToString(dataRow["ObjType"]) == "CBOEQNUM" || Converts.ToString(dataRow["ObjType"]) == "CBOEQDT")
                        sVal = "1~<=|2~=|3~>=";
                    else if (Converts.ToString(dataRow["ObjType"]) == "CBOEQTXT")
                        sVal = "1~Starts with|2~Contains|3~Does Not contain";
                    else sVal = Converts.ToString(dataRow["AddOnValue"]);

                    pnl = new Panel();
                    pnl.ID = "pnlcboNum" + Converts.ToString(dataRow["ObjID"]);
                    //pnl.Height = 24;
                    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]) + 400;  //(int)Converts.ToDouble(dataRow["Width"]) + 4;

                    ///Richa
                    Table tblCBOp = new Table();
                    tblCBOp.ID = "pnlcboNumtbl" + Converts.ToString(dataRow["ObjID"]);


                    ddl = CreateFormControl_ComboBox("cbotxtn" + Converts.ToString(dataRow["ObjID"]), sVal,
                       Converts.ToString(dataRow["MapField"]), 0, 0,
                       (int)Converts.ToDouble(dataRow["Width"]) - 100, 22);
                    ddl.Height = 24;//240

                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue1"]).Trim();
                    else if (dataRow["DefaultValue"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue"]).Trim();
                    if (ddl.SelectedIndex < 0 && ddl.Items.Count > 0) ddl.SelectedIndex = 0;

                    //pnl.Controls.Add(ddl);
                    ///Richa
                    AddControlToWebTable(ddl, 0, 0, tblCBOp, 0);

                    if (Converts.ToString(dataRow["ObjType"]) == "CBONUM" || Converts.ToString(dataRow["ObjType"]) == "CBOEQNUM")
                    {
                        //txtN = CreateFormControl_TextBoxNumeric("tntcbotxtn" + Converts.ToString(dataRow["ObjID"]), "",
                        //    Converts.ToString(dataRow["MapField1"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                        //    (int)Converts.ToDouble(dataRow["ShowNotDefined"]),
                        //    0, (int)pnl.Width.Value - 84, 80, 20);
                        //if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                        //    txtN.Value = Converts.ToDouble(dataRow["DefaultValue2"]);
                        ////pnl.Controls.Add(txtN);
                        //AddControlToWebTable(txtN, 1, 0, tblCBOp, 0);

                        //if (Converts.ToBoolean(dataRow["Mandatory"]))
                        //    MandatoryCtrls.Add(AddArrayList2ArrayList(txtN, sMsg, ""));
                        ////iWd = txtN.Left;
                    }
                    else if (Converts.ToString(dataRow["ObjType"]) == "CBOD" || Converts.ToString(dataRow["ObjType"]) == "CBOEQDT")
                    {
                        dtp = CreateFormControl_DateTimePicker("dtp1" + Converts.ToString(dataRow["ObjID"]),
                            Converts.ToString(dataRow["MapField1"]), 0, (int)pnl.Width.Value - 94,
                            Converts.ToBoolean(dataRow["Mandatory"]), 90, 20);

                        if (Converts.ToString(dataRow["MapField1"]).ToUpper().Trim() == "STARTDATE" && gblFromDate.Trim() != "")
                            dtp.Date = Converts.ToString(Convert.ToDateTime(gblFromDate));
                        if (Converts.ToString(dataRow["MapField1"]).ToUpper().Trim() == "ENDDATE" && gblToDate.Trim() != "")
                            dtp.Date = Converts.ToString(Convert.ToDateTime(gblToDate));
                        else
                        {
                            if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                                dtp.Date = Converts.ToString(Convert.ToDateTime(dataRow["DefaultValue2"]));
                        }
                        //pnl.Controls.Add(dtp);
                        AddControlToWebTable(dtp, 1, 0, tblCBOp, 0);

                        //iWd = dtp.Left;
                    }
                    else if (Converts.ToString(dataRow["ObjType"]) == "CBOP")
                    {
                        dtp = CreateFormControl_DateTimePicker("dtp1" + Converts.ToString(dataRow["ObjID"]),
                            Converts.ToString(dataRow["MapField1"]), 0, (int)pnl.Width.Value - 200,
                            Converts.ToBoolean(dataRow["Mandatory"]), 150, 20);
                        //pnl.Controls.Add(dtp);
                        AddControlToWebTable(dtp, 1, 0, tblCBOp, 0);

                        lbl = CreateFormControl_Label("lblSep" + Converts.ToString(dataRow["ObjID"]), "to",
                            0, 0, 0, 0, true);
                        //pnl.Controls.Add(lbl);
                        AddControlToWebTable(lbl, 2, 0, tblCBOp, 0);

                        dtp2 = CreateFormControl_DateTimePicker("dtp2" + Converts.ToString(dataRow["ObjID"]),
                           Converts.ToString(dataRow["MapField2"]), 0, (int)pnl.Width.Value - 94,
                           Converts.ToBoolean(dataRow["Mandatory"]), 150, 20);
                        //lbl.Left = dtp2.Left - lbl.Width - 2;
                        //dtp.Left = lbl.Left - dtp.Width - 2;
                        SetGlobalDateRange(dtp, dtp2);
                        //pnl.Controls.Add(dtp2);
                        AddControlToWebTable(dtp2, 3, 0, tblCBOp, 0);
                        //iWd = dtp.Left;
                    }
                    else // if (ConvertObject2String(dataRow["ObjType"]) == "ddlTXT" || ConvertObject2String(dataRow["ObjType"]) == "ddlEQTXT")
                    {
                        txt = CreateFormControl_TextBox("txtcbotxtn" + Converts.ToString(dataRow["ObjID"]), "",
                            Converts.ToString(dataRow["MapField1"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                            0, (int)pnl.Width.Value - 154, 150, 20);
                        if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                            ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue2"]).Trim();
                        //pnl.Controls.Add(txt);
                        AddControlToWebTable(txt, 1, 0, tblCBOp, 0);
                        if (Converts.ToBoolean(dataRow["Mandatory"]))
                            MandatoryCtrls.Add(AddArrayList2ArrayList(txt, sMsg, ""));
                        //iWd = txt.Left;
                    }
                    //ddl.Width = iWd - ddl.Left - 4;
                    //tblContainer.Controls.Add(pnl, 1, rowNumber);
                    //AddControlToWebTable(pnl, 1, rowNumber, tblContainer,0);  

                    pnl.Controls.Add(tblCBOp);
                    if (Converts.ToString(dataRow["ObjType"]) == "CBOP")
                    {
                        //AddControlToWebTable(pnl, 1, rowNumber, tblContainer, 0);
                        AddControlToWebTable(pnl, 1, kRow, tblContainer, 4);
                        kCol = 0;
                        kRow = kRow + 1;


                    }
                    else
                    {

                        if (!bShowHControl)//(kCol == -1)
                            AddControlToWebTable(pnl, 1, rowNumber, tblContainer, 0);
                        else
                        {
                            if (kCol != 5)
                                AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 0);

                            else
                            {
                                kCol = 0;
                                kRow = kRow + 1;

                                AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 0);
                            }
                        }
                    }
                    // tblContainer.SetColumnSpan(pnl, 2);
                    if (dataRow["DefaultValue"] != null)
                        ddl.SelectedValue = Converts.ToString(dataRow["DefaultValue"]).Trim();
                    if (ddl.SelectedIndex < 0 && ddl.Items.Count > 0) ddl.SelectedIndex = 0;

                    pnl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                    break;
                #endregion

                #region " DTP "
                case "DTP":
                    pnl = new Panel();
                    pnl.ID = "pnldtp" + Converts.ToString(dataRow["ObjID"]);
                    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]);

                    //pnl.Height = 24;
                    //pnl.BorderWidth = 1;
                    //pnl.BorderColor = System.Drawing.Color.Black;
                    ///Richa
                    Table tbldtp = new Table();
                    tbldtp.ID = "pnltbl" + Converts.ToString(dataRow["ObjID"]);
                    tbldtp.CellPadding = 0;
                    tbldtp.CellSpacing = 0;
                    //tbldtp.BorderStyle = BorderStyle.Solid;
                    //tbldtp.BorderWidth = 1;


                    dtp = CreateFormControl_DateTimePicker("dtp1" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["MapField"]), 0, 0,
                        Converts.ToBoolean(dataRow["Mandatory"]),
                        ((int)Converts.ToDouble(dataRow["Width"]) / 2), 20);
                    ///Richa
                    AddControlToWebTable(dtp, 0, 0, tbldtp, 0);

                    // pnl.Controls.Add(dtp);

                    sVal = Converts.ToString(dataRow["CaptionM"]);
                    if (sVal.Trim() == "") sVal = "&nbsp;to&nbsp;";
                    lbl = CreateFormControl_Label("lblSep" + Converts.ToString(dataRow["ObjID"]), sVal,
                        2, 0, 10, 20, true);

                    //pnl.Controls.Add(lbl);
                    AddControlToWebTable(lbl, 1, 0, tbldtp, 0);


                    dtp2 = CreateFormControl_DateTimePicker("dtp2" + Converts.ToString(dataRow["ObjID"]),
                        Converts.ToString(dataRow["MapField1"]), 0, (int)pnl.Width.Value - 104,
                        Converts.ToBoolean(dataRow["Mandatory"]),
                        ((int)Converts.ToDouble(dataRow["Width"]) / 2), 20);
                    SetGlobalDateRange(dtp, dtp2);
                    //pnl.Controls.Add(dtp2);
                    AddControlToWebTable(dtp2, 2, 0, tbldtp, 0);


                    pnl.Controls.Add(tbldtp);


                    //lbl.Left = dtp2.Left - lbl.Width - 2;

                    ///tblContainer.Controls.Add(pnl, 1, rowNumber);

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(pnl, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        //AddControlToWebTable(pnl, 1, rowNumber, tblContainer,0);  
                        if (kCol != 5)

                            AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 0);
                        }
                    }
                    // tblContainer.SetColumnSpan(pnl, 2);

                    pnl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                    break;
                #endregion

                #region " MS, MSM "
                case "MS":
                case "MSM":
                    hlp = CreateFormControl_HelpBox("hlp" + Converts.ToString(dataRow["ObjID"]), dataRow["CaptionM"].ToString(),
                        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]), dataRow["MapField3"].ToString(),
                        (Converts.ToString(dataRow["ObjType"]) == "MSM"), Converts.ToDouble(dataRow["ShowNotDefined"]) > 0,
                        3, 550, 450, (int)Converts.ToDouble(dataRow["Width"]),
                        Converts.ToString(dataRow["FASObjType"]), Converts.ToString(dataRow["LinkToObject"]));
                    UserControl.HelpControl hlpMulti = new ITHeart.UserControl.HelpControl();

                    //tblContainer.Controls.Add(hlp, 1, rowNumber);


                    if (Converts.ToString(dataRow["FASObjType"]) == "")
                    {
                        if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                            hlp.SelectID = Converts.ToString(dataRow["DefaultValue1"]);
                        else if (dataRow["DefaultValue"] != null)
                            hlp.SelectID = Converts.ToString(dataRow["DefaultValue"]);
                    }
                    

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(hlp, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        //AddControlToWebTable(hlp, 1, rowNumber, tblContainer,0);
                        if (kCol != 5)

                            AddControlToWebTable(hlp, kCol++, kRow, tblContainer, 0);

                        else
                        {
                            kCol = 0;
                            kRow = kRow + 1;

                            AddControlToWebTable(hlp, kCol++, kRow, tblContainer, 0);
                        }
                    }
                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(hlp, sMsg, ""));
                    //if (EnableReset) hlp.setDefaults("", "null");

                    hlp.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(hlp);
                    break;
                #endregion

                #region " FL-, FLT "
                //case "FL-":
                //case "FLT":
                //    flp = CreateFormControl_FileBox("flp" + Converts.ToString(dataRow["ObjID"]), dataRow["CaptionM"].ToString(),
                //        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]), dataRow["MapField3"].ToString(),
                //        (Converts.ToString(dataRow["ObjType"]) == "MSM"), Converts.ToDouble(dataRow["ShowNotDefined"]) > 0,
                //        3, 550, 450, (int)Converts.ToDouble(dataRow["Width"]),
                //        Converts.ToString(dataRow["FASObjType"]), Converts.ToString(dataRow["LinkToObject"]));

                //    flp.SetValue(Converts.ToString(dataRow["GenID"]), Converts.ToString(dataRow["MapField"]), bCanEdit);
                //    flp.ShowTextbox = (Converts.ToString(dataRow["ObjType"]).ToUpper() == "FLT");

                //    if (!bShowHControl)//(kCol == -1)
                //        AddControlToWebTable(flp, 1, rowNumber, tblContainer, 0);
                //    else
                //    {
                //        //AddControlToWebTable(hlp, 1, rowNumber, tblContainer,0);
                //        if (kCol != 5)

                //            AddControlToWebTable(flp, kCol++, kRow, tblContainer, 0);

                //        else
                //        {
                //            kCol = 0;
                //            kRow = kRow + 1;

                //            AddControlToWebTable(flp, kCol++, kRow, tblContainer, 0);
                //        }
                //    }
                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(flp, sMsg, ""));
                //    //if (EnableReset) hlp.setDefaults("", "null");

                //    flp.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(flp);
                //    break;
                #endregion

                #region RD
                case "RD":
                    pnl = new Panel();
                    int rdCol = 0;
                    int rdRow = 0;
                    //pnl.AutoSize = false;
                    //pnl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    pnl.Attributes.Add("AutoSizeMode", "GrowAndShrink");


                    int RDWidth = (int)Converts.ToDouble(dataRow["Width"]);

                    ///Add Properties of Panel
                    pnl.BorderStyle = BorderStyle.Solid;
                    pnl.BorderWidth = (Unit)C.ConvertFromString("1px");
                    pnl.BorderColor = System.Drawing.Color.LightGray;

                    //pnl.Width = RDWidth + 20;  //  tblContainer.Width - 30;
                    //pnl.Height = 5;
                    pnl.ID = "pnlrd" + Converts.ToString(dataRow["ObjID"]);
                    Table tblp = new Table();
                    tblp.ID = "pnltbl" + Converts.ToString(dataRow["ObjID"]);

                    //tblp.ColumnCount = 1;
                    //tblp.CellBorderStyle = tblContainer.CellBorderStyle;
                    //tblp.AutoSize = true;

                    pnl.Attributes.Add("Tag", Converts.ToString(dataRow["MapField"]));
                    tblp.Attributes.Add("Tag", Converts.ToString(dataRow["MapField"]));

                    sVal = "";
                    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                        sVal = Converts.ToString(dataRow["DefaultValue1"]);
                    else if (dataRow["DefaultValue"] != null)
                        sVal = Converts.ToString(dataRow["DefaultValue"]);

                    if (Converts.ToString(dataRow["AddOnValue"]) != "")
                    {
                        if (Converts.ToString(dataRow["AddOnValue"]).IndexOf("!$!") < 0)
                        {
                            string[] rdar = Converts.ToString(dataRow["AddOnValue"]).Split('|');
                            //tblp.RowCount = rdar.Length;
                            for (int j = 0; j < rdar.Length; j++)
                            {
                                RadioButton rdp = new RadioButton();
                                rdp.Width = pnl.Width;
                                rdp.CssClass = "SelectionControls";
                                ///Provide Group name 
                                rdp.GroupName = Converts.ToString(dataRow["ObjID"]);
                                rdp.ID = "rdo" + (j + 1).ToString() + Converts.ToString(dataRow["ObjID"]);
                                string[] _cb = rdar[j].ToString().Split('~');
                                if (_cb.Length < 1) continue;

                                rdp.Text = _cb[1].ToString();
                                rdp.Attributes.Add("Tag", _cb[0].ToString());
                                if ((sVal == "" && j == 0) || (sVal != "" && sVal == _cb[0].ToString().Trim()))
                                    rdp.Checked = true;
                                //tblp.Controls.Add(rdp);

                                if (!bShowHControl)//(kCol == -1)
                                    AddControlToWebTable(rdp, 0, j, tblp, 0);
                                else
                                {
                                    //AddControlToWebTable(rdp, 0, j, tblp,0);  
                                    if (rdCol != 4)

                                        AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                    else
                                    {
                                        rdCol = 0;
                                        rdRow = rdRow + 1;

                                        AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string rdar = Converts.ToString(dataRow["AddOnValue"]).Substring(3);
                            if (rdar.Length > 0)
                            {
                                Dictionary<string, ArrayList> SP_paramsd = new Dictionary<string, ArrayList>();
                                SP_paramsd.Add("@Qry", ArrayLists.ToArrayList("@Qry", rdar.Replace(',', ';'), SqlDbType.VarChar));

                                DataSet dsrdp = base.ExecuteData(spNames.ExecuteQry, SP_paramsd);

                                if (!string.IsNullOrEmpty(base.Message))
                                {
                                    _Message = base.Message;
                                    return;
                                }

                                //tblp.RowCount = dsrdp.Tables[0].Rows.Count;
                                for (int j = 0; j < dsrdp.Tables[0].Rows.Count; j++)
                                {
                                    RadioButton rdp = new RadioButton();
                                    rdp.Width = (int)rdp.Width.Value + RDWidth;
                                    rdp.CssClass = "SelectionControls";

                                    ///provide Group Name
                                    rdp.GroupName = Converts.ToString(dataRow["ObjID"]);
                                    rdp.ID = "rdp" + (j + 1).ToString() + Converts.ToString(dataRow["ObjID"]);
                                    rdp.Attributes.Add("Tag", Converts.ToString(dsrdp.Tables[0].Rows[j]["MainID"]));
                                    rdp.Text = Converts.ToString(dsrdp.Tables[0].Rows[j]["MainDescr"]);
                                    pnl.Height = (int)pnl.Height.Value + (int)rdp.Height.Value;
                                    //pnl.Height += (int)rdp.Height.Value ;
                                    if ((sVal == "" && j == 0) || (sVal != "" && sVal.Replace(',', ';') == Converts.ToString(dsrdp.Tables[0].Rows[j]["MainID"]).Trim()))
                                        rdp.Checked = true;
                                    //tblp.Controls.Add(rdp);

                                    if (!bShowHControl)//(kCol == -1)
                                        AddControlToWebTable(rdp, 0, j, tblp, 0);
                                    else
                                    {
                                        //AddControlToWebTable(rdp, 0, j, tblp,0); 
                                        if (rdCol != 4)

                                            AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                        else
                                        {
                                            rdCol = 0;
                                            rdRow = rdRow + 1;

                                            AddControlToWebTable(rdp, rdCol++, rdRow, tblp, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    pnl.Controls.Add(tblp);
                    if (tblp.Controls.Count > 0) pnl.Height = tblp.Controls.Count * 30;

                    //tblContainer.Controls.Add(pnl, 1, rowNumber);
                    //AddControlToWebTable(pnl, 1, rowNumber, tblContainer,0); 

                    // pnl.Width = RDWidth *  tblp.Controls.Count;

                    if (!bShowHControl)//(kCol == -1)
                        AddControlToWebTable(pnl, 1, rowNumber, tblContainer, 0);
                    else
                    {
                        if (kCol >= 1)
                        {
                            AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 4);
                            kCol = 0;
                            kRow = kRow + 1;

                        }
                    }

                    if (Converts.ToBoolean(dataRow["Mandatory"]))
                        MandatoryCtrls.Add(AddArrayList2ArrayList(pnl, sMsg, ""));

                    pnl.Visible = (KeyBased < 1);
                    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);

                    break;
                #endregion

                #region CHKNUM
                //case "CHKNUM":
                //    pnl = new Panel();
                //    pnl.ID = "pnlchknum" + Converts.ToString(dataRow["ObjID"]);
                //    pnl.Height = 24;
                //    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]) + 4;

                //    chk = CreateFormControl_CheckBox("chknum" + Converts.ToString(dataRow["ObjID"]),
                //        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]),
                //        0, 0, true);

                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue1"]) > 0);
                //    else if (dataRow["DefaultValue"] != null)
                //        chk.Checked = (Converts.ToDouble(dataRow["DefaultValue"]) > 0);

                //    pnl.Controls.Add(chk);

                //    txtN = CreateFormControl_TextBoxNumeric("tntchknum" + Converts.ToString(dataRow["ObjID"]), "",
                //        Converts.ToString(dataRow["MapField1"]), (int)Converts.ToDouble(dataRow["Maxlength"]),
                //        (int)Converts.ToDouble(dataRow["ShowNotDefined"]),
                //        0, (int)pnl.Width.Value - 64, 60, 20);
                //    if (ShowUserSettings && dataRow["DefaultValue2"] != null)
                //        txtN.Value = Converts.ToDouble(dataRow["DefaultValue2"]);
                //    pnl.Controls.Add(txtN);

                //    //if (chk.Left + chk.Width < (int)pnl.Width.Value  - 40)
                //    //{
                //    //    txtN.Left = chk.Left + chk.Width + 4;
                //    //    txtN.Width = pnl.Width - txtN.Left - 4;
                //    //}
                //    //{
                //    //    txtN.Width = 40;
                //    //   txtN.Left = pnl.Width + txtN.Width - 4;
                //    //}

                //    //tblContainer.Controls.Add(pnl, 1, rowNumber);

                //    if (!bShowHControl)//(kCol == -1)
                //        AddControlToWebTable(pnl, 1, rowNumber, tblContainer, 0);
                //    else
                //    {
                //        //AddControlToWebTable(pnl, 1, rowNumber, tblContainer,0);  
                //        if (kCol != 5)

                //            AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 0);

                //        else
                //        {
                //            kCol = 0;
                //            kRow = kRow + 1;

                //            AddControlToWebTable(pnl, kCol++, kRow, tblContainer, 0);
                //        }
                //    }
                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(txtN, sMsg, ""));

                //    pnl.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                //    break;
                #endregion

                #region "TAB"
                //case "TAB":
                //    // Label templbl=new Label();
                //    pnl = new Panel();
                //    pnl.ID = "pnlTab" + Converts.ToString(dataRow["ObjID"]);
                //    pnl.Height = 24;
                //    pnl.Width = (int)Converts.ToDouble(dataRow["Width"]) + 104;
                //    pnl.Height = (int)Converts.ToDouble(dataRow["MaxLength"]) + 54;


                //    C1.Web.UI.Controls.C1TabControl.C1TabControl tb = new C1.Web.UI.Controls.C1TabControl.C1TabControl();
                //    //C1.Web.UI.Controls.C1TabControl.C1TabPageCollection tb = new C1.Web.UI.Controls.C1TabControl.C1TabPageCollection();

                //    tb.ID = "tbc" + Converts.ToString(dataRow["ObjID"]);
                //    tb.Attributes.Add("Tag", Converts.ToString(dataRow["MapField"]));

                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        tb.SelectedIndex = (int)Converts.ToDouble(dataRow["DefaultValue1"]);
                //    else if (dataRow["DefaultValue"] != null)
                //        tb.SelectedIndex = (int)Converts.ToDouble(dataRow["DefaultValue"]);


                //    tb.Width = (int)pnl.Width.Value - 4; tb.Height = (int)pnl.Height.Value - 4;
                //    string[] arrTabAddOn = Converts.ToString(dataRow["AddOnValue"]).Split('|');
                //    //tb.TabCount = arrTabAddOn.Length;
                //    for (int xloop = 0; xloop < arrTabAddOn.Length; xloop++)
                //    {
                //        string[] temp = arrTabAddOn[xloop].Split('~');
                //        DataRow[] dr = dataRow.Table.Select("tabControlObj='" + Converts.ToString(dataRow["ObjID"]) + "' and TabControlPage = " + temp[0]);
                //        if (dr.Length <= 0) continue;

                //        // tb.TabPages.Add("tabPage" + temp[0], temp[1]); 
                //        C1.Web.UI.Controls.C1TabControl.C1TabPage tbp = new C1.Web.UI.Controls.C1TabControl.C1TabPage();
                //        tbp.Text = temp[1].ToString();

                //        //  tb.TabPages[tb.TabPages.Count-1].ID  = temp[1].ToString();
                //        Table tbl = new Table();

                //        //tbp.RowCount = dr.Length;
                //        //tbp.ColumnCount = 2;
                //        //tbp.Dock = DockStyle.Fill;
                //        tbl.ID = "tabTableLayout" + Converts.ToString(dataRow["ObjID"]) + temp[0].ToString();
                //        //tb.TabPages.Add(C1.Web.UI.Controls.C1TabControl.C1TabPage);   

                //        // tb.TabPages[].Controls.Add(tbp);

                //        for (int yloop = 0; yloop < dr.Length; yloop++)
                //        {  ///confusion
                //            bShowHControl = false;
                //            //kCol = -1;
                //            AddControls(tbl, dr[yloop], MandatoryCtrls, KeyBasedCtrls, yloop, EnableReset, ShowUserSettings, bShowHControl);


                //        }

                //        //Panel pnl1 = new Panel();
                //        //pnl1.ID = "pnlgap";
                //        //pnl1.Height = 0;
                //        tbp.Controls.Add(tbl);
                //        tb.TabPages.AddAt((int)Converts.ToDouble(temp[0]), tbp);
                //        //tbp.Controls.Add(pnl1, 1, dr.Length + 1);
                //    }
                //    //tb.Dock = DockStyle.Fill;

                //    //AddControlToWebTable(tb, 1, rowNumber, tblContainer,4);  

                //    if (!bShowHControl) //(kCol == -1)
                //    {
                //        kRow = kRow + 1;
                //        Label lblBlank = new Label();
                //        lblBlank = CreateFormControl_Label("capPBnk" + Converts.ToString(dataRow["ObjID"]),
                //                  Converts.ToString(dataRow["CaptionP"]), 5, 0, 100, 20, true);

                //        AddControlToWebTable(lblBlank, 0, kRow, tblContainer, 0);
                //        AddControlToWebTable(tb, 1, kRow, tblContainer, 4);
                //    }


                //    //AddControlToWebTable(tb, 1, kRow, tblContainer, 4);  


                //    ///For gap
                //    Panel pnl1 = new Panel();
                //    pnl1.ID = "pnlgap";
                //    pnl1.Height = 24;

                //    // tblContainer.Controls.Add(pnl, 0, rowNumber);
                //    AddControlToWebTable(pnl1, 0, ++kRow, tblContainer, 5);


                //    //TableLayoutSettings tableSettings = tblContainer.LayoutSettings as TableLayoutSettings; ;
                //    //tableSettings.SetColumnSpan(pnl, 2);

                //    pnl.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                //    break;
                #endregion

                #region Spinner
                //case "SPN":
                //    pnl = new Panel();
                //    pnl.ID = "pnlnumupdn" + Converts.ToString(dataRow["ObjID"]);
                //    pnl.Height = 24;
                //    pnl.Width = Converts.ToInt32(dataRow["Width"].ToString()) + 4;

                //    num = CreateFormControl_NumericUpDown("num" + Converts.ToString(dataRow["ObjID"]),
                //        Converts.ToString(dataRow["AddOnValue"]), Converts.ToString(dataRow["MapField"]),
                //        0, 0, Converts.ToInt32(dataRow["Width"].ToString()), false);


                //    if (ShowUserSettings && dataRow["DefaultValue1"] != null)
                //        num.Value = (int)Converts.ToDouble(dataRow["DefaultValue1"]);
                //    else if (dataRow["DefaultValue"] != null)
                //        num.Value = (int)Converts.ToDouble(dataRow["DefaultValue"]);

                //    num.Increment = Math.Max((int)Converts.ToDouble(dataRow["MaxLength"]), 1);
                //    pnl.Controls.Add(num);

                //    tblContainer.Controls.Add(pnl, 1, rowNumber);

                //    if (Converts.ToBoolean(dataRow["Mandatory"]))
                //        MandatoryCtrls.Add(AddArrayList2ArrayList(num, sMsg, ""));

                //    pnl.Visible = (KeyBased < 1);
                //    if (KeyBased > 0) KeyBasedCtrls.Add(pnl);
                //    break;
                #endregion
            }

        }


        private TableCell AddCellsToTable(int Col, int rowNumber, Table tblContainer, int colSpan)
        { return AddCellsToTable(Col, rowNumber, tblContainer, colSpan, ""); }

        private TableCell AddCellsToTable(int iColNo, int iRowNo, Table tblContainer, int colSpan, string sTDClass)
        {
            TableRow tr = (TableRow)null;
            TableCell td = (TableCell)null;

            #region " Find Row "
            try
            {
                tr = tblContainer.Rows[iRowNo];
            }
            catch (Exception eRow)
            {
                while (tblContainer.Rows.Count <= iRowNo)
                {
                    tr = new TableRow();
                    tblContainer.Rows.Add(tr);
                }
            }

            try { tr = tblContainer.Rows[iRowNo]; }
            catch (Exception eRow) { }
            if (tr == null) return null;
            #endregion

            #region " Find Cell "
            try
            {
                td = tr.Cells[iColNo];
            }
            catch (Exception eCol)
            {
                while (tr.Cells.Count <= iColNo)
                {
                    td = new TableCell();
                    if (colSpan > 0)
                        td.ColumnSpan = colSpan;
                    td.Attributes.Add("nowrap", "true");

                    td.Wrap = false;
                    td.Attributes.Add("Margin", "0");
                    td.VerticalAlign = VerticalAlign.Top;
                    td.CssClass = sTDClass;
                    td.Controls.Clear();
                    // td1.Text = "";
                    tr.Cells.Add(td);
                }
            }

            try
            { td = tr.Cells[iColNo]; }
            catch (Exception eCol) { td = (TableCell)null; }
            #endregion

            return td;


            //try
            //{
            //    td = tblContainer.Rows[rowNumber].Cells[Col];
            //}
            //catch
            //{
            //    while (tblContainer.Rows[rowNumber].Cells.Count <= Col + 1)
            //    {
            //        td = new TableCell();
            //        tblContainer.Rows[rowNumber].Cells.Add(td1);

            //    }
            //}

            //    //if (tblContainer.Rows.Count <= rowNumber + 1)
            //    //{

            //    //    for (int i = 0; i < (rowNumber + 1) - tblContainer.Rows.Count; i++)
            //    //    {
            //    //        TableRow tr = new TableRow();
            //    //        tblContainer.Rows.Add(tr);
            //    //    }
            //    //}


            //    if (tblContainer.Rows[rowNumber].Cells.Count <= Col + 1)
            //    {
            //        for (int i = 0; i < (Col + 1) - tblContainer.Rows[rowNumber].Cells.Count; i++)
            //        {
            //            TableCell td1 = new TableCell();
            //            if (colSpan > 0)
            //                td1.ColumnSpan = colSpan;
            //            td1.Attributes.Add("nowrap", "true");

            //            td1.Wrap = false;
            //            td1.Attributes.Add("Margin", "0");
            //            td1.CssClass = sTDClass;
            //            td1.Controls.Clear();
            //            // td1.Text = "";

            //            tblContainer.Rows[rowNumber].Cells.Add(td1);
            //        }
            //    }

            //    //td.Attributes.Add("nowrap", "true");
            //    //td = tblContainer.Rows[rowNumber].Cells[Col];
            //    ////td.BorderStyle = BorderStyle.Dotted;
            //    ////td.BorderWidth = 1;
            //    //td.Wrap = false;
            //    //td.Attributes.Add("Margin", "0");
            //    //td.Controls.Clear();
            //    //td.Text = "";
            //    //td.CssClass = sTDClass;
            //}
            //return td;
        }

        private TableCell AddControlToWebTable(ITHeart.UserControl.HelpControl hlp, int Col, int rowNumber, Table tblContainer, int colSpan)
        {
            TableCell td = AddCellsToTable(Col, rowNumber, tblContainer, colSpan);  // tblContainer.Rows[rowNumber].Cells[Col];
            td.Controls.Add(hlp);

            return td;
        }
        //private TableCell AddControlToWebTable(ITHeart.UserControl.FileControl hlp, int Col, int rowNumber, Table tblContainer, int colSpan)
        //{


        //    TableCell td = AddCellsToTable(Col, rowNumber, tblContainer, colSpan);  // tblContainer.Rows[rowNumber].Cells[Col];
        //    td.Controls.Add(hlp);
        //    return td;
        //}
        private TableCell AddControlToWebTable(ITHeart.UserControl.DatePickerControl dtp, int Col, int rowNumber, Table tblContainer, int colSpan)
        {


            TableCell td = AddCellsToTable(Col, rowNumber, tblContainer, colSpan); // tblContainer.Rows[rowNumber].Cells[Col];
            td.HorizontalAlign = HorizontalAlign.Left;
            //td.Width = dtp.Width+2;
            td.Controls.Add(dtp);
            return td;
        }


        private TableCell AddControlToWebTable(System.Web.UI.WebControls.WebControl Obj, int Col, int rowNumber, Table tblContainer, int colSpan)
        {


            TableCell td = AddCellsToTable(Col, rowNumber, tblContainer, colSpan);  // tblContainer.Rows[rowNumber].Cells[Col];
            td.HorizontalAlign = HorizontalAlign.Left;
            td.Controls.Add(Obj);
            return td;
        }

        private TableCell AddControlToWebTable(string blank, int Col, int rowNumber, Table tblContainer, int colSpan)
        {


            TableCell td = AddCellsToTable(Col, rowNumber, tblContainer, colSpan);  // tblContainer.Rows[rowNumber].Cells[Col];
            td.HorizontalAlign = HorizontalAlign.Left;
            td.Text = blank;
            td.Width = 5;

            return td;
        }

        #endregion

        #region " Individual Controls "
        public Label CreateFormControl_Label(string sName, string sText, int iTop, int iLeft, int iWidth, int iHeight, bool bAutoSize)
        {
            Label lSome = new Label();
            lSome.Text = sText;
            lSome.ID = sName;
            lSome.Style.Add("Left", iLeft.ToString());
            lSome.Style.Add("Top", iTop.ToString());
            //lSome.Width = iWidth;
            lSome.Height = iHeight;
            lSome.CssClass = "DarkText PX12";

            //lSome.SkinID = "LblPrompt";

            return lSome;
        }
        public TextBox CreateFormControl_TextBox(string sName, string sText, string sTag, int iMaxLength, int iTop, int iLeft, int iWidth, int iHeight)
        {
            TextBox tSome = new TextBox();
            tSome.Text = sText;
            tSome.ID = sName;
            tSome.Attributes.Add("Tag", sTag);
            //tSome.Tag = sTag;
            tSome.MaxLength = iMaxLength;
            tSome.Style.Add("Left", iLeft.ToString());
            tSome.Style.Add("Top", iTop.ToString());
            tSome.Width = iWidth + 20;
            tSome.Height = iHeight;

            tSome.SkinID = "LogixText-11-Height-20";
            return tSome;
        }
        //public C1.Web.UI.Controls.C1Input.C1NumericInput CreateFormControl_TextBoxNumeric(string sName, string sText, string sTag, int iMaxLength, int iIncrement, int iTop, int iLeft, int iWidth, int iHeight)
        //{
        //    C1.Web.UI.Controls.C1Input.C1NumericInput c1Some = new C1.Web.UI.Controls.C1Input.C1NumericInput();

        //    // c1Some.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //    c1Some.DecimalPlaces = ((iMaxLength > 0) ? iMaxLength : 0);
        //    c1Some.Attributes.Add("CustomFormat", GetFormatString(iMaxLength, true));
        //    c1Some.Style.Add("Left", iLeft.ToString());
        //    c1Some.Style.Add("Top", iTop.ToString());
        //    c1Some.ID = sName;
        //    c1Some.Width = iWidth + 20;
        //    c1Some.Height = iHeight;
        //    c1Some.TextAlign = HorizontalAlign.Right;
        //    c1Some.Attributes.Add("Tag", sTag);
        //    c1Some.Value = Converts.ToDouble(sText);
        //    c1Some.Increment = iIncrement;
        //    c1Some.CssClass = "QuestionMsg";



        //    return c1Some;
        //}
        public UserControl.DatePickerControl CreateFormControl_DateTimePicker(string sID, string sTag, int iTop, int iLeft, bool bMandatory, int iWidth, int iHeight)
        {
            return CreateFormControl_DateTimePicker(sID, "", "", sTag, "", "", "", "", "", iTop, iLeft, bMandatory, iWidth, iHeight, UserControl.DatePickerControl.ShowMode.Always, UserControl.DatePickerControl.ShowMode.OnMouseOver, true);
        }
        public UserControl.DatePickerControl CreateFormControl_DateTimePicker(string sID, string sValue, string sDefaultBlankText, string sTag,
            string sCssClass, string sLabelCssClass, string sLabelHoverCssClass, string sCssStyle, string sCallBackFunction,
            int iTop, int iLeft, bool bMandatory, int iWidth, int iHeight, 
            UserControl.DatePickerControl.ShowMode smBorder, UserControl.DatePickerControl.ShowMode smIcon, bool bEnable)
        {
            //UserControl.DatePickerControl dSome = new UserControl.DatePickerControl();
            UserControl.DatePickerControl dSome = LoadControl("~/UserControl/DatePickerControl.ascx") as UserControl.DatePickerControl;

            //if (Strings.IsNullOrEmpty(sCssClass)) sCssClass = "QuestionMsg";
            //if (Strings.IsNullOrEmpty(sValue) && !Strings.IsNullOrEmpty(sDefaultBlankText)) sValue = sDefaultBlankText;

            dSome.ID = sID;
            dSome.ReadOnly = !bEnable;
            dSome.Width = 94;
            dSome.Mandatory = bMandatory;
            dSome.border = smBorder;
            dSome.icon = smIcon;

            try { dSome.Date = sValue; }
            catch (Exception e2)
            { if (bMandatory) dSome.Date = Converts.ToString(DateTime.Now); }

            if (!Strings.IsNullOrEmpty(sTag)) dSome.Attributes.Add("Tag", sTag);

            if(iTop > 0) dSome.Attributes.Add("Top", iTop.ToString());
            if (iHeight > 0) dSome.Attributes.Add("Height", iHeight.ToString());

            dSome.Attributes.Add("Runat", "Server");
            if (!Strings.IsNullOrEmpty(sCssClass)) dSome.cssClass = sCssClass;

            if (bEnable && !Strings.IsNullOrEmpty(sCallBackFunction))
                dSome.CallbackFunction = sCallBackFunction;

            return dSome;
        }
        public CheckBox CreateFormControl_CheckBox(string sName, string sText, string sTag, int iTop, int iLeft, bool bAutoSize)
        {
            CheckBox cSome = new CheckBox();
            cSome.ID = sName;
            cSome.Text = sText.Trim();
            cSome.Attributes.Add("Tag", sTag);
            cSome.Style.Add("Left", iLeft.ToString());
            cSome.Style.Add("Top", iTop.ToString());
            cSome.Style.Add("AutoSize", bAutoSize.ToString());
            cSome.TextAlign = TextAlign.Right;
            cSome.CssClass = "QuestionMsg";

            return cSome;
        }
        //public static System.Windows.Forms.NumericUpDown CreateFormControl_NumericUpDown(string sName, string sText, string sTag, int iTop, int iLeft, int iWidth, bool bAutoSize)
        //{
        //    NumericUpDown nSome = new System.Windows.Forms.NumericUpDown();
        //    nSome.Name = sName;
        //    nSome.Text = sText;
        //    nSome.Tag = sTag;

        //    nSome.Left = iLeft;
        //    nSome.Top = iTop;
        //    nSome.Width = iWidth;
        //    nSome.AutoSize = bAutoSize;
        //    return nSome;
        //}
        public DropDownList CreateFormControl_ComboBox(string sName, string sFillString, string sTag, int iTop, int iLeft, int iWidth, int iHeight)
        {
            DropDownList ddlSome = new DropDownList();
            ddlSome.ID = sName;
            ddlSome.Attributes.Add("Tag", sTag);
            ddlSome.Style.Add("Left", iLeft.ToString());
            ddlSome.Style.Add("Top", iTop.ToString());
            if (iWidth > 100)
                ddlSome.Width = iWidth - 40;
            else
                ddlSome.Width = iWidth + 20;//iWidth - 40;
            ddlSome.Height = iHeight;

            if (sFillString.IndexOf("!$!") < 0)
            {
                FillDropDown(ddlSome, sFillString);
            }
            else if (sFillString.Trim() != "")
            {
                FillDropDownSQL(ddlSome, sFillString.Substring(3));
            }

            ddlSome.CssClass = "NinePX";
            return ddlSome;
        }
        //public UserControl.FileControl CreateFormControl_FileBox(string sName, string sBoundOptionID, string sHelpID, string sTag, string sAdditionalCriteria, bool bMultiSelect, bool bShowNotDefined, int iHelpPOS, int iHelpWidth, int iHelpHeight, int iWidth, string sFASObject, string sLinkedOn)
        //{
        //    UserControl.FileControl flpSome = LoadControl("~/UserControl/FileControl.ascx") as UserControl.FileControl;


        //    // UserControl.HelpControl hlpSome = new UserControl.HelpControl();
        //    flpSome.ID = sName;
        //    // flpSome.BoundOptionID = sBoundOptionID;
        //    flpSome.Attributes.Add("Tag", sTag);
        //    flpSome.HelpWidth = iWidth - 40;  //iWidth + 20;
        //    flpSome.Attributes.Add("Runat", "Server");
        //    flpSome.Attributes.Add("CssClass", "QuestionMsg");



        //    return flpSome;
        //}
        public UserControl.HelpControl CreateFormControl_HelpBox(string sName, string sBoundOptionID, string sHelpID, string sTag, string sAdditionalCriteria, bool bMultiSelect, bool bShowNotDefined, int iHelpPOS, int iHelpWidth, int iHelpHeight, int iWidth, string sFASObject, string sLinkedOn)
        {
            return CreateFormControl_HelpBox(sName, sBoundOptionID, sHelpID, sTag, "", "", "", sAdditionalCriteria, 
            bMultiSelect, bShowNotDefined, true, iHelpPOS, iHelpWidth, iHelpHeight, iWidth, sFASObject, sLinkedOn);
        }
        public UserControl.HelpControl CreateFormControl_HelpBox(string sName, string sBoundOptionID, string sHelpID, string sTag,
            string sSelectID, string sValueChangeJS, string sValueChangeJSParam, string sAdditionalCriteria,
            bool bMultiSelect, bool bShowNotDefined, bool bEnable, int iHelpPOS, int iHelpWidth, int iHelpHeight, int iWidth, 
            string sFASObject, string sLinkedOn)
        {
            UserControl.HelpControl hlpSome = LoadControl("~/UserControl/HelpControl.ascx") as UserControl.HelpControl;


            // UserControl.HelpControl hlpSome = new UserControl.HelpControl();
            hlpSome.ID = sName;
            hlpSome.BoundOptionID = sBoundOptionID;
            hlpSome.HelpID = sHelpID;
            hlpSome.Attributes.Add("Tag", sTag);
            hlpSome.HelpWidth = iWidth - 40;  //iWidth + 20;
            hlpSome.Attributes.Add("Runat", "Server");
            hlpSome.Attributes.Add("CssClass", "QuestionMsg");

            if (!bEnable) hlpSome.Enabled = false;
            if (!Strings.IsNullOrEmpty(sSelectID))
                hlpSome.SelectID = sSelectID;
            if (!Strings.IsNullOrEmpty(sValueChangeJS))
                hlpSome.ValueChangeJS = sValueChangeJS;
            if (!Strings.IsNullOrEmpty(sValueChangeJSParam))
                hlpSome.ValueChangeJSParam = sValueChangeJSParam;

            hlpSome.MultiSelect = ((bMultiSelect) ? 1 : 0);

            if (!Strings.IsNullOrEmpty(sAdditionalCriteria))
                hlpSome.AdditionalWhere = sAdditionalCriteria;


            //string[] lnk = sLinkedOn.Split(',');
            //for (int j = 0; j < lnk.Length; j++)
            //{
            //    if (lnk[j].Trim() != "")
            //        hlpSome.AdditionalWhere.Add(lnk[j] + ",");
            //}

            return hlpSome;
        }
        public ArrayList AddArrayList2ArrayList(object o, string m, string d)
        {
            ArrayList t = new ArrayList();
            t.Add(o);
            t.Add(m);
            t.Add(d);
            return t;
        }
        #endregion

        #region " Check Mendatory Fields "
        public bool CheckMandatoryControls(ArrayList MandatoryCtrls)
        {
            int i;
            for (i = 0; i < MandatoryCtrls.Count; i++)
            {
                ArrayList t = (ArrayList)MandatoryCtrls[i];

                switch (((Control)t[0]).ID.Substring(0, 3))
                {
                    case "txt":
                        if (((TextBox)t[0]).Text == "")
                        {
                            _Message = t[1].ToString();

                            return false;
                        }
                        break;
                    case "cbo":
                        if (Convert.ToString(((DropDownList)t[0]).SelectedValue) == "")
                        {
                            _Message = t[1].ToString();
                            //Program.ShowMessage(t[1].ToString());
                            return false;
                        }
                        break;
                    case "hlp":
                        if (((UserControl.HelpControl)t[0]).ID == "")
                        {
                            _Message = t[1].ToString();
                            //Program.ShowMessage(t[1].ToString());
                            return false;
                        }
                        break;
                    //case "tnt":
                    //    if (Converts.ToDouble(((C1.Web.UI.Controls.C1Input.C1NumericInput)t[0]).Value) == 0)
                    //    {
                    //        _Message = t[1].ToString();
                    //        //Program.ShowMessage(t[1].ToString());
                    //        return false;
                    //    }
                    //    break;
                    case "tmt":
                        if (((TextBox)t[0]).Text == "")
                        {
                            //Program.ShowMessage(t[1].ToString());
                            _Message = t[1].ToString();
                            return false;
                        }
                        break;
                    case "dtp":
                        if (((UserControl.DatePickerControl)t[0]).Date == "")
                        {
                            _Message = t[1].ToString();
                            //Program.ShowMessage(t[1].ToString());
                            return false;
                        }
                        break;
                    case "chk":
                        if (((CheckBox)t[0]).Checked == false)
                        {
                            _Message = t[1].ToString();
                            //Program.ShowMessage(t[1].ToString());
                            return false;
                        }
                        break;
                    case "rdp":
                        Panel pnl = (Panel)t[0];
                        if (pnl != null)
                        {
                            int flag = 0;
                            foreach (Control ctrlp in pnl.Controls[0].Controls)
                            {
                                if (((RadioButton)ctrlp).Checked)
                                {
                                    flag = 1;
                                }
                            }
                            if (flag == 0)
                            {
                                _Message = t[1].ToString();
                                //Program.ShowMessage(t[1].ToString());
                                return false;
                            }
                        }
                        break;
                }

            }
            return true;
        }
        #endregion

        #region " Toggle Key Based Controls "
        public void ToggleKeyBasedControls(ArrayList KeyBasedCtrls)
        {
            int i;
            for (i = 0; i < KeyBasedCtrls.Count; i++)
            {
                ((Control)KeyBasedCtrls[i]).Visible = (!((Control)KeyBasedCtrls[i]).Visible);
            }
        }
        #endregion

        #region " Object Linked Events "
        public void ControlEvent_Help_ValueChosenWithObj(UserControl.HelpControl sender, string ID)
        {
            SetMSHelpFilter(GetPlaceControlParentTable(sender), sender.ID.ToString(), ID);
        }
        public void ControlEvents_Bind(Control pCtrl)
        {
            //foreach (Control Ctrl in pCtrl.Controls)
            //{    
            //    if (Ctrl.ID == "" ) continue;
            //    if (Ctrl.HasControls())
            //    {

            //        foreach (Control subCtrl in Ctrl.Controls)
            //        {
            //            ControlEvents_Bind(subCtrl);

            //        }

            //    }
            //    switch (Ctrl.ID.Substring(0, 3))
            //    {
            //        case ("txt"):
            //            break;
            //        case ("tnt"):
            //            break;
            //        case ("ddl"):
            //            break;
            //        case ("dtp"):
            //            break;
            //        case ("rdo"):
            //            break;
            //        case ("chk"):
            //            break;
            //        case ("hlp"):
            //         ((UserControl.HelpControl)Ctrl).ValueChosenWithObj += new UserControl.HelpControl.ChosenClickedHandlerwithObject(ControlEvent_Help_ValueChosenWithObj);
            //            break;
            //        case ("pnl"):
            //        case ("tab"):
            //        case ("tbc"):
            //            ControlEvents_Bind(Ctrl);
            //            break;
            //    }
            //}
        }
        public void SetMSHelpFilter(Control pCtrl, string sSenderName, string sValue2Put)
        {
            string[] xvals = sSenderName.Split('@');
            Table tbl = null;


            if (pCtrl.GetType().Name != "Table" && pCtrl.Controls.Count > 0)
            {
                for (int i = 0; i < pCtrl.Controls.Count; i++)
                {
                    SetMSHelpFilter(pCtrl.Controls[i], sSenderName, sValue2Put);
                }

            }
            tbl = (Table)pCtrl;

            foreach (TableRow tr in tbl.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    foreach (Control ctrl in tc.Controls)//Control pCtrl.Controls
                    {

                        switch (ctrl.ID.Substring(0, 3).ToLower())
                        {
                            //foreach (Control Ctrl in pCtrl.Controls)
                            //{
                            //    switch (Ctrl.ID.Substring(0, 3))
                            //    {
                            case ("hlp"):
                                //((UserControl.HelpControl)Ctrl).PutAdditionalWhere(sSenderName, sValue2Put);
                                break;
                            case ("pnl"):
                                SetMSHelpFilter(ctrl, sSenderName, sValue2Put);
                                break;
                            case ("tbc"):
                            case ("tab"):
                                SetMSHelpFilter(ctrl, sSenderName, sValue2Put);
                                break;
                        }

                    }
                }
            }

        }
        public Control GetPlaceControlParentTable(Control sender)
        {

            if (sender.Parent.GetType().Name == "Table")
                return sender.Parent;
            else
                return GetPlaceControlParentTable(sender.Parent);
        }
        #endregion

        #region " Get & Set Control Values "
        public void SetControlValues(Control pCtrl, DataTable dtUser, string sIgnoreObjID, bool bIgnoreFASObjType)
        {
            bool bShowUserSettings = dtUser.Select("UserID is not null").Length > 0;
            GetNSetControlValues(pCtrl, dtUser, null, null, bShowUserSettings, false, sIgnoreObjID, bIgnoreFASObjType);
        }
        public void SetControlValues(Control pCtrl, DataRow dataRow)
        { GetNSetControlValues(pCtrl, null, dataRow, null, false, false, "", false); }
        public void SetControlValues(Control pCtrl, Dictionary<string, ArrayList> aList)
        { GetNSetControlValues(pCtrl, null, null, aList, false, false, "", false); }
        public void ResetControlValues(Control pCtrl)
        { GetNSetControlValues(pCtrl, null, null, null, false, false, "", false); }
        public string GetControlValues(Control pCtrl, DataRow dataRow)
        { return GetNSetControlValues(pCtrl, null, dataRow, null, false, true, "", false); }
        public string GetControlValues(Control pCtrl, Dictionary<string, ArrayList> aList)
        { return GetNSetControlValues(pCtrl, null, null, aList, false, true, "", false); }
        private string GetNSetControlValues(Control pCtrl, DataTable dtUser, DataRow dataRow, Dictionary<string, ArrayList> aList, bool bShowUserSettings, bool bSaveObj, string sIgnoreObjID, bool bIgnoreFASObjType)
        {
            string sFld, sVal, sUpdateQry = "";
            double dVal;
            Table tbl = null;
            bool IsIgnoreable;
            ArrayList ValueList;
            //if (pCtrl.HasControls())
            //{
            //   if(pCtrl.GetType == Table) 
            //      tbl = (Table)pCtrl;
            //   else
            //       GetNSetControlValues(pCtrl, dtUser, dataRow, aList, bShowUserSettings, bSaveObj, sIgnoreObjID, bIgnoreFASObjType);

            //}

            if (pCtrl.GetType().Name != "Table" && pCtrl.Controls.Count > 0)
            {
                for (int i = 0; i < pCtrl.Controls.Count; i++)
                {
                    sVal = GetNSetControlValues(pCtrl.Controls[i], dtUser, dataRow, aList, bShowUserSettings, bSaveObj, sIgnoreObjID, bIgnoreFASObjType);
                    sUpdateQry += ((string.IsNullOrEmpty(sUpdateQry) || string.IsNullOrEmpty(sVal) || sVal.Trim().StartsWith(",")) ? "" : ",") + sVal;
                    // sUpdateQry += GetNSetControlValues(pCtrl.Controls[i], dtUser, dataRow, aList, bShowUserSettings, bSaveObj, sIgnoreObjID, bIgnoreFASObjType);
                }
                return sUpdateQry;
            }
            tbl = (Table)pCtrl;

            foreach (TableRow tr in tbl.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    foreach (Control ctrl in tc.Controls)//Control pCtrl.Controls
                    {



                        if (ctrl.ID.Trim().Length < 3) continue;

                        // sFld = Converts.ToString(ctrl.ID);//tAG VALUE


                        IsIgnoreable = false;
                        try
                        {
                            switch (ctrl.ID.Substring(0, 3).ToLower())
                            {
                                #region " txt "
                                case ("txt"):
                                    sFld = Converts.ToString(((TextBox)ctrl).Attributes["Tag"]);
                                    if (bSaveObj)
                                    {
                                        sVal = Converts.ToString(((TextBox)ctrl).Text);
                                        //if (sVal != "" && ((TextBox)ctrl).PasswordChar.ToString() != "\0")
                                        //sVal = base.Community.Encrypt("", sVal); //Program.gblValues.Encrypt("", sVal);

                                        if (dataRow != null)
                                            dataRow[sFld] = sVal;
                                        else if (aList != null)
                                            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, sVal, SqlDbType.VarChar));

                                        //aList.Add("@" + sFld + "," + sVal.Replace(',', ';'));
                                        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "='" + sVal.Replace("'", "''").Replace(',', ',') + "'";
                                    }
                                    else
                                    {
                                        if (dtUser != null)
                                            sVal = GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
                                        else if (dataRow != null)
                                            sVal = Converts.ToString(dataRow[sFld]);
                                        else if (aList != null)
                                            sVal = GetDictionaryValue(aList, "@" + sFld);

                                        else sVal = "";
                                        if (IsIgnoreable) continue;
                                        //if (sVal != "" && ((TextBox)ctrl).PasswordChar.ToString() != "\0")
                                        //sVal = base.Community.Decrypt("", sVal);    //Program.gblValues.Decrypt("", sVal);
                                        ((TextBox)ctrl).Text = sVal;
                                        ((TextBox)ctrl).ForeColor = System.Drawing.Color.Black;
                                    }
                                    break;
                                #endregion

                                #region " tnt "
                                //case ("tnt"):
                                //    sFld = Converts.ToString(((C1.Web.UI.Controls.C1Input.C1NumericInput)ctrl).Attributes["Tag"]);
                                //    if (bSaveObj)
                                //    {
                                //        dVal = Converts.ToDouble(((C1.Web.UI.Controls.C1Input.C1NumericInput)ctrl).Value);
                                //        sVal = ((dVal == 0) ? "" : dVal.ToString());
                                //        if (dataRow != null)
                                //            dataRow[sFld] = dVal;
                                //        else if (aList != null)
                                //            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, sVal, SqlDbType.VarChar));
                                //        //aList.Add("@" + sFld + "," + dVal.ToString());
                                //        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "=" + sVal + "";
                                //    }
                                //    else
                                //    {
                                //        if (dtUser != null)
                                //            dVal = Converts.ToDouble(GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable));
                                //        else if (dataRow != null)
                                //            dVal = Converts.ToDouble(dataRow[sFld]);
                                //        else if (aList != null)
                                //            dVal = GetDictionaryNumValue(aList, "@" + sFld);
                                //        else dVal = 0;
                                //        if (IsIgnoreable) continue;
                                //        ((C1.Web.UI.Controls.C1Input.C1NumericInput)ctrl).Value = dVal;
                                //    }
                                //    break;
                                #endregion

                                #region " ddl "
                                case ("cbo"):
                                    sFld = Converts.ToString(((DropDownList)ctrl).Attributes["Tag"]);
                                    if (bSaveObj)
                                    {
                                        sVal = Converts.ToString(((DropDownList)ctrl).SelectedValue);
                                        if (dataRow != null)
                                            dataRow[sFld] = sVal;
                                        else if (aList != null)
                                            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, sVal, SqlDbType.VarChar));

                                        //aList.Add("@" + sFld + "," + sVal);
                                        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "='" + sVal.Replace("'", "''").Replace(',', ',') + "'";
                                    }
                                    else
                                    {
                                        if (dtUser != null)
                                            sVal = GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
                                        else if (dataRow != null)
                                            sVal = Converts.ToString(dataRow[sFld]);
                                        else if (aList != null)
                                            sVal = GetDictionaryValue(aList, "@" + sFld);
                                        else sVal = "";
                                        if (IsIgnoreable) continue;
                                        ((DropDownList)ctrl).SelectedValue = sVal;
                                        if (((DropDownList)ctrl).SelectedIndex < 0 && ((DropDownList)ctrl).Items.Count > 0) ((DropDownList)ctrl).SelectedIndex = 0;
                                    }
                                    break;
                                #endregion

                                #region " dt "
                                case ("dt_"):
                                case ("dtp"):
                                    sFld = Converts.ToString(((UserControl.DatePickerControl)ctrl).Attributes["Tag"]);

                                    if (bSaveObj)
                                    {
                                        sVal = GetDatePicker(((UserControl.DatePickerControl)ctrl), "");
                                        if (dataRow != null)
                                            dataRow[sFld] = ((sVal == "") ? null : sVal);
                                        else if (aList != null)
                                            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, ((sVal == "") ? null : sVal), SqlDbType.DateTime));

                                        // aList.Add("@" + sFld + "," + ((sVal == "") ? "Null" : sVal));
                                        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "=" + ((sVal == "") ? null : "'" + sVal + "'") + "";
                                    }
                                    else
                                    {

                                        if (dtUser != null)
                                            sVal = GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
                                        else if (dataRow != null)
                                            sVal = Converts.ToDateTime(dataRow[sFld], "dd-MMM-yyyy");
                                        else if (aList != null)
                                            sVal = Converts.ToDateTime(GetDictionaryValue(aList, "@" + sFld), "dd-MMM-yyyy");
                                        else sVal = "";
                                        if (IsIgnoreable) continue;
                                        if (sVal != "")
                                            SetDatePicker(((UserControl.DatePickerControl)ctrl), sVal, false);//((UserControl.DatePickerControl)ctrl).ShowCheckBox);
                                    }
                                    break;
                                #endregion

                                #region " rdo "
                                case ("rdo"):
                                    //sFld = Converts.ToString(pCtrl.Tag);
                                    //sVal = Converts.ToString(((RadioButton)ctrl).Tag);

                                    sFld = Converts.ToString(((Table)pCtrl).Attributes["Tag"]);
                                    sVal = Converts.ToString(((RadioButton)ctrl).Attributes["Tag"]);

                                    if (bSaveObj)
                                    {
                                        if (!((RadioButton)ctrl).Checked) continue;

                                        if (dataRow != null)
                                            dataRow[sFld] = sVal;
                                        else if (aList != null)
                                            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, sVal.Replace(";", ","), SqlDbType.VarChar));
                                        //aList.Add("@" + sFld + "," + sVal.Replace(',', ';'));
                                        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "='" + sVal.Replace("'", "''").Replace(',', ',') + "'";
                                    }
                                    else
                                    {
                                        if (dtUser != null)
                                            sVal = GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
                                        else if (dataRow != null)
                                            sVal = Converts.ToString(dataRow[sFld]);
                                        else if (aList != null)
                                            sVal = GetDictionaryValue(aList, "@" + sFld);
                                        else sVal = "";
                                        if (IsIgnoreable) continue;
                                        ((RadioButton)ctrl).Checked = (Converts.ToString(((RadioButton)ctrl).Attributes["Tag"]) == sVal);
                                    }
                                    break;
                                #endregion

                                #region " chk "
                                case ("chk"):
                                    sFld = Converts.ToString(((CheckBox)ctrl).Attributes["Tag"]);
                                    if (bSaveObj)
                                    {
                                        dVal = ((((CheckBox)ctrl).Checked) ? 1 : 0);
                                        if (dataRow != null)
                                            dataRow[sFld] = dVal;
                                        else if (aList != null)
                                            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, dVal.ToString(), SqlDbType.Bit));
                                        //aList.Add("@" + sFld + "," + dVal.ToString());
                                        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "=" + dVal.ToString() + "";
                                    }
                                    else
                                    {
                                        if (dtUser != null)
                                            dVal = Converts.ToDouble(GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable));
                                        else if (dataRow != null)
                                            dVal = Converts.ToDouble(dataRow[sFld]);
                                        else if (aList != null)
                                            dVal = GetDictionaryNumValue(aList, "@" + sFld);
                                        else if (sFld.ToLower().StartsWith("recenable"))
                                            dVal = 1;
                                        else
                                            dVal = 0;
                                        if (IsIgnoreable) continue;
                                        ((CheckBox)ctrl).Checked = dVal > 0;
                                    }
                                    break;
                                #endregion

                                #region " hlp "
                                case ("hlp"):
                                    sFld = Converts.ToString(((UserControl.HelpControl)ctrl).Attributes["Tag"]);
                                    if (bSaveObj)
                                    {
                                        sVal = Converts.ToString(((UserControl.HelpControl)ctrl).SelectID);
                                        //string sValForParam = Converts.ToString(((UserControl.HelpControl)ctrl).SelectID);
                                        if (dataRow != null)
                                            dataRow[sFld] = (string.IsNullOrEmpty(sVal) ? null : sVal);
                                        else if (aList != null)
                                            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, (string.IsNullOrEmpty(sVal) ? null : sVal), SqlDbType.VarChar));
                                        // aList.Add("@" + sFld + "," + (string.IsNullOrEmpty(sVal) ? null : sVal.Replace(',', ';')));
                                        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "=" + ((string.IsNullOrEmpty(sVal)) ? "Null" : "'" + sVal.Replace("'", "''") + "'") + "";
                                    }
                                    else
                                    {
                                        if (dtUser != null)
                                            sVal = GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
                                        else if (dataRow != null)
                                            sVal = Converts.ToString(dataRow[sFld]);
                                        else if (aList != null)
                                            sVal = GetDictionaryValue(aList, "@" + sFld);
                                        else sVal = "";
                                        if (IsIgnoreable) continue;
                                        ((UserControl.HelpControl)ctrl).SelectID = sVal;
                                    }
                                    break;
                                #endregion

                                #region " flp "
                                //case ("flp"):
                                //    sFld = Converts.ToString(((UserControl.FileControl)ctrl).Attributes["Tag"]);
                                //    if (bSaveObj)
                                //    {
                                //        ValueList = ((UserControl.FileControl)ctrl).GetValue();
                                //        aList.Add("@" + sFld, ValueList);
                                //        //ValueList.Add(sFld);
                                //        //ValueList.Add(Converts.ToString(((UserControl.FileControl)ctrl).getExtension));
                                //        //ValueList.Add(Converts.ToString(((UserControl.FileControl)ctrl).FileName));
                                //        //ValueList.Add(((UserControl.FileControl)ctrl).FileContent);
                                //        //ValueList.Add(Converts.ToString(((UserControl.FileControl)ctrl).HelpText));
                                //        //aList.Add("@" + sFld, ValueList);

                                //        // sVal = Converts.ToString(((UserControl.FileControl)ctrl).SelectID);
                                //        ////string sValForParam = Converts.ToString(((UserControl.HelpControl)ctrl).SelectID);
                                //        //if (dataRow != null)
                                //        //    dataRow[sFld] = (string.IsNullOrEmpty(sVal) ? null : sVal);
                                //        //else if (aList != null)
                                //        //{

                                //        //}
                                //        //// aList.Add("@" + sFld + "," + (string.IsNullOrEmpty(sVal) ? null : sVal.Replace(',', ';')));
                                //        //sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "=" + ((string.IsNullOrEmpty(sVal)) ? "Null" : "'" + sVal.Replace("'", "''") + "'") + "";
                                //    }
                                //    else
                                //    {
                                //        //if (dtUser != null)
                                //        //    sVal = GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
                                //        //else if (dataRow != null)
                                //        //    sVal = Converts.ToString(dataRow[sFld]);
                                //        //else if (aList != null)
                                //        //    sVal = GetDictionaryValue(aList, "@" + sFld);
                                //        //else sVal = "";
                                //        //if (IsIgnoreable) continue;
                                //        //((UserControl.FileControl)ctrl).SetValue("MoreDetails", sVal, bCanEdit);
                                //    }
                                //    break;
                                #endregion

                                #region " num - Spinner "
                                //case ("num"):
                                //    if (bSaveObj)
                                //    {
                                //        dVal = ConvertObject2Numeric(((NumericUpDown)ctrl).Value);
                                //        if (dataRow != null)
                                //            dataRow[sFld] = dVal;
                                //        else if (aList != null)
                                //            aList.Add("@" + sFld + "," + dVal.ToString());
                                //        sUpdateQry += ((sUpdateQry == "") ? "" : "; ") + " " + sFld + " = " + dVal.ToString() + " ";
                                //    }
                                //    else
                                //    {
                                //        if (dtUser != null)
                                //            dVal = ConvertObject2Numeric(GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable));
                                //        else if (dataRow != null)
                                //            dVal = Program.ConvertObject2Numeric(dataRow[sFld]);
                                //        else if (aList != null)
                                //            dVal = GetArrayListNumValue(aList, "@" + sFld);
                                //        else dVal = 0;
                                //        if (IsIgnoreable) continue;
                                //        ((NumericUpDown)ctrl).Value = (int)dVal;
                                //    }
                                //    break;
                                #endregion

                                #region " tab, pnl "
                                case ("tab"):
                                case ("pnl"):
                                case ("tbl"):
                                    sVal = GetNSetControlValues(ctrl, dtUser, dataRow, aList, bShowUserSettings, bSaveObj, sIgnoreObjID, bIgnoreFASObjType);
                                    sUpdateQry += ((string.IsNullOrEmpty(sUpdateQry) || string.IsNullOrEmpty(sVal) || sVal.Trim().StartsWith(",")) ? "" : ",") + sVal;
                                    break;
                                #endregion

                                #region " tbc - Tab Page "
                                //case ("tbc"):
                                //    sFld = Converts.ToString(((C1.Web.UI.Controls.C1TabControl.C1TabControl)ctrl).Attributes["Tag"]);
                                //    if (bSaveObj)
                                //    {
                                //        //C1.Web.UI.Controls.C1TabControl.C1TabControl tb = new C1.Web.UI.Controls.C1TabControl.C1TabControl();
                                //        dVal = Converts.ToDouble(((C1.Web.UI.Controls.C1TabControl.C1TabControl)ctrl).SelectedIndex);
                                //        if (dataRow != null)
                                //            dataRow[sFld] = dVal;
                                //        else if (aList != null)
                                //            aList.Add("@" + sFld, ArrayLists.ToArrayList("@" + sFld, dVal.ToString(), SqlDbType.VarChar));
                                //        // aList.Add("@" + sFld + "," + dVal.ToString());
                                //        sUpdateQry += ((sUpdateQry == "") ? "" : ",") + " " + sFld + "=" + dVal.ToString() + "";
                                //    }
                                //    else
                                //    {
                                //        if (dtUser != null)
                                //            dVal = Converts.ToDouble(GetDefaultOrUserValue(dtUser, sFld, bShowUserSettings, sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable));
                                //        else if (dataRow != null)
                                //            dVal = Converts.ToDouble(dataRow[sFld]);
                                //        else if (aList != null)
                                //            dVal = GetDictionaryNumValue(aList, "@" + sFld);
                                //        else dVal = 0;
                                //        if (IsIgnoreable) continue;
                                //        ((C1.Web.UI.Controls.C1TabControl.C1TabControl)ctrl).SelectedIndex = (int)dVal;
                                //    }

                                //    sUpdateQry += GetNSetControlValues(ctrl, dtUser, dataRow, aList, bShowUserSettings, bSaveObj, sIgnoreObjID, bIgnoreFASObjType);
                                //    break;
                                #endregion
                            }
                        }
                        catch (Exception e1) { }
                    }

                }

            }
            return sUpdateQry;

        }
        private string GetDefaultOrUserValue(DataTable dtUser, string sFld, bool bUserValue, string sIgnoreObjID, bool bIgnoreFASObjType, ref bool IsIgnoreable)
        {
            IsIgnoreable = true;
            if (sFld == "") return "";

            IsIgnoreable = false;
            DataRow[] dr;
            if (!bUserValue)
            {
                dr = dtUser.Select("MapField='" + sFld + "'");
                if (dr.Length < 1) return "";
                return GetFASObjValue(dr[0], "DefaultValue", sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable);
            }

            dr = dtUser.Select("MapField='" + sFld + "'");
            if (dr.Length > 0)
            { return GetFASObjValue(dr[0], "DefaultValue1", sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable); }

            dr = dtUser.Select("MapField1='" + sFld + "'");
            if (dr.Length > 0)
            { return GetFASObjValue(dr[0], "DefaultValue2", sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable); }

            dr = dtUser.Select("MapField2='" + sFld + "'");
            if (dr.Length > 0)
            { return GetFASObjValue(dr[0], "DefaultValue3", sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable); }

            dr = dtUser.Select("MapField3='" + sFld + "'");
            if (dr.Length > 0)
            { return GetFASObjValue(dr[0], "DefaultValue4", sIgnoreObjID, bIgnoreFASObjType, ref IsIgnoreable); }

            return "";

        }
        private string GetFASObjValue(DataRow dr0, string sDataFieldName, string sIgnoreObjID, bool bIgnoreFASObjType, ref bool IsIgnoreable)
        {
            IsIgnoreable = false;

            switch (Converts.ToString(dr0["FASObjType"]).Trim().ToUpper())
            {
                case "FI":
                    return base.FiscalYearID;
                    break;
                case "GR":
                    //return base.InvestorGroupID;
                    break;
                case "CL":
                    //return base.CompanyID;
                    break;
            }
            if (bIgnoreFASObjType && Converts.ToString(dr0["FASObjType"]).Trim() != "") IsIgnoreable = true;
            if (sIgnoreObjID.Trim() != "" && sIgnoreObjID.IndexOf("_" + Converts.ToString(dr0["ObjId"]) + "_") > 0) IsIgnoreable = true;
            return Converts.ToString(dr0[sDataFieldName]);

        }
        #endregion

        #region " Save Control Settings into tbl_UserReportSettings "
        internal bool SaveControlValues(string ReportID, DataTable dtSys, Dictionary<string, ArrayList> aList, string UserID, string sIgnoreObjID, bool bIgnoreFASObjType)
        {
            return SaveControlValues(ReportID, dtSys, aList, UserID, sIgnoreObjID, bIgnoreFASObjType, "");

        }
        internal bool SaveControlValues(string ReportID, DataTable dtSys, Dictionary<string, ArrayList> aList, string UserID, string sIgnoreObjID, bool bIgnoreFASObjType, string CommunityID)
        {
            if (dtSys == null || dtSys.Rows.Count == 0) return false;
            int iLoop;
            string sObjID, sFld, sVal;

            DataSet ds;
            Dictionary<string, ArrayList> SP_params;


            for (iLoop = 0; iLoop < dtSys.Rows.Count; iLoop++)
            {
                SP_params = new Dictionary<string, ArrayList>();
                if (bIgnoreFASObjType && Converts.ToString(dtSys.Rows[iLoop]["FASObjType"]).Trim() != "") continue;
                sObjID = Converts.ToString(dtSys.Rows[iLoop]["ObjId"]);
                if (sIgnoreObjID.Trim() != "" && sIgnoreObjID.IndexOf("_" + sObjID + "_") > 0) continue;

                #region " Build Parameter List "

                SP_params.Add("@UserID", ArrayLists.ToArrayList("@UserID", UserID, SqlDbType.VarChar));
                SP_params.Add("@ReportID", ArrayLists.ToArrayList("@ReportID", ReportID, SqlDbType.VarChar));
                SP_params.Add("@ObjID", ArrayLists.ToArrayList("@ObjID", sObjID, SqlDbType.VarChar));

                if (CommunityID != "")
                    SP_params.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.Char));


                sFld = Converts.ToString(dtSys.Rows[iLoop]["MapField"]);
                if (sFld != "")
                {

                    sVal = GetDictionaryValue(aList, "@" + sFld);
                    if (sVal != "")
                        SP_params.Add("@DefaultValue1", ArrayLists.ToArrayList("@DefaultValue1", sVal, SqlDbType.VarChar));
                }

                sFld = Converts.ToString(dtSys.Rows[iLoop]["MapField1"]);
                if (sFld != "")
                {
                    sVal = GetDictionaryValue(aList, "@" + sFld);
                    if (sVal != "")
                        SP_params.Add("@DefaultValue2", ArrayLists.ToArrayList("@DefaultValue2", sVal, SqlDbType.VarChar));

                }

                sFld = Converts.ToString(dtSys.Rows[iLoop]["MapField2"]);
                if (sFld != "")
                {
                    sVal = GetDictionaryValue(aList, "@" + sFld);
                    if (sVal != "")
                        SP_params.Add("@DefaultValue3", ArrayLists.ToArrayList("@DefaultValue3", sVal, SqlDbType.VarChar));
                }
                #endregion
                ds = base.ExecuteData(spNames.Save_UserReportSettings, SP_params);
                //StoredP.ExecuteSP(SP_params);
                if (!string.IsNullOrEmpty(Message))
                { _Message = base.Message; return false; }
            }
            return true;
        }
        public bool DeleteControlValues(string ReportID, string UserID)
        {

            Dictionary<string, ArrayList> SP_params = new Dictionary<string, ArrayList>();
            SP_params.Add("@UserID", ArrayLists.ToArrayList("@UserID", UserID, SqlDbType.VarChar));
            SP_params.Add("@ReportID", ArrayLists.ToArrayList("@ReportID", ReportID, SqlDbType.VarChar));
            DataSet ds = base.ExecuteData(spNames.Delete_UserReportSettings, SP_params);

            if (!string.IsNullOrEmpty(base.Message))
            {
                _Status = base.Status;
                _Message = base.Message;
                return false;
            }
            return true;


        }
        #endregion


        public string GetDatePicker(UserControl.DatePickerControl dtObj, string sIfNull)
        {

            string sVal = "";

            // if (!dtObj.ShowCheckBox || (dtObj.ShowCheckBox && dtObj.Checked))

            sVal = dtObj.Date; //sVal = dtObj.Value.ToString("dd-MMM-yyyy");

            if (sVal == "") sVal = sIfNull;

            return sVal;

        }
        public void SetDatePicker(UserControl.DatePickerControl dtObj, string sLongDate, bool IsOptional)
        {
            try
            {
                dtObj.Date = sLongDate;
                // if (IsOptional) dtObj.Checked = true;
            }
            catch (Exception e2)
            {
                //if (IsOptional) dtObj.Checked = false;
                // else dtObj.Value = DateTime.Now;
                dtObj.Date = Converts.ToString(DateTime.Now);
            }
        }


        public void SetGlobalDateRange(UserControl.DatePickerControl dtp1, UserControl.DatePickerControl dtp2)
        {

            dtp1.Date = "01-Apr-2011";
            dtp2.Date = DateTime.Now.ToString("dd-MMM-yyyy");
            // dtp1.Value = DateTime.Now; dtp2.Value = DateTime.Now;

            //if (base.gblFromDate.Trim() != "")
            //    dtp1.Date = Converts.ToString(Convert.ToDateTime(base.gblFromDate));
            //if (base.gblToDate.Trim() != "")
            //    dtp2.Date = Converts.ToString(Convert.ToDateTime(base.gblToDate));

            //if (Convert.ToDateTime(dtp2.Date.ToString()) < Convert.ToDateTime(dtp2.Date.ToString()))
            //    dtp2.Date = dtp1.Date;


        }


    }
}