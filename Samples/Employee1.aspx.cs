using System;
using System.Web.UI.WebControls;
using System.Data;
using ITHeart.BL;

namespace Samples
{
    public partial class Employee1 : System.Web.UI.Page
    {
        public ITHeart.BL.GenMstBL CommonBL;

        protected void Page_Load(object sender, EventArgs e)
        {
            CommonBL = new ITHeart.BL.GenMstBL();
            if (!IsPostBack)
            {
                bindCompanyDropDown(ddlcompany);
                //bindDropDownList("018", ddlcompany);
                bindDropDownList("006", ddlDepartment);
                bindDropDownList("007", ddlDesignation);
                bindDropDownList("010", ddlLocation);
                bindDropDownList("012", ddlShift);
                bindDropDownList("014", ddlEmployeeType);
                bindDropDownList("015", ddlDivision);
                bindDropDownList("016", ddlEmployeeCategory);
                bindDropDownList("025", ddlProject);
                bindDropDownList("026", ddlBank);
                //bindDropDown(ddlSalaryTemplate);
            }
        }

        private void bindDropDownList(string GenID, DropDownList ddlList)
        {
            ddlList.DataSource = CommonBL.Load_GmMst(GenID);
            ddlList.DataValueField = "ID";
            ddlList.DataTextField = "MAINDESCR";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }
        private void bindDropDown(DropDownList ddlList)
        {
            ddlList.DataSource = CommonBL.ExecuteData(spNames.salary_template_Load);
            ddlList.DataValueField = "SalaryTemplateId";
            ddlList.DataTextField = "Name";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }

        private void bindCompanyDropDown(DropDownList ddlList)
        {
            ddlList.DataSource = CommonBL.ExecuteData(spNames.Get_Company);
            ddlList.DataValueField = "pRowID";
            ddlList.DataTextField = "Name";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }


        private void LoadMasterData()
        {
            Table tblMasterData = new Table();
            TableRow tblRow;
            TableCell tblCell;
            DataTable dt;
            dt = CommonBL.Load_GmMstList(CommonBL.UserID);
            for (int i = 0; dt.Rows.Count > i; i++)
            {
                tblRow = new TableRow();
                tblCell = new TableCell();
                tblCell.Text = CreateLink(dt.Rows[i]["MasterName"].ToString(), dt.Rows[i]["GenID"].ToString());
                tblRow.Cells.Add(tblCell);
                tblMasterData.Rows.Add(tblRow);
            }
            // divLeftMenu.Controls.Add(tblMasterData);
        }
        private string CreateLink(string Name, string GmID)
        {
            string sReturn = "";
            sReturn = "<a href='../Controllerfile.aspx?OptionID=202076000n|" + GmID + "&GenID=" + GmID + "&ParamInfo='>" + Name + "</a>";
            return sReturn;
        }
    }
}