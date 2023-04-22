using System;
using System.Web.UI.WebControls;
using System.Data;
using ITHeart.BL;

namespace Samples.Master
{
    public partial class Company : System.Web.UI.Page
    {
        public ITHeart.BL.GenMstBL CommonBL;
        protected void Page_Load(object sender, EventArgs e)
        {
           // bindDropDown(ddlLegalCountry);
           // bindDropDown(ddlMasterCountry);
            //bindDropDown(ddlLegalState);
            //bindDropDown(ddlMasterState);
            //DataSet ds = CommonBL.ExecuteData(spNames.getCountry);
            //DataTable dt = new DataTable();
            //ddlLegalCountry.DataSource = CommonBL.ExecuteData(spNames.getCountry);
            //ddlLegalCountry.DataValueField = "Country_Id";
            //ddlLegalCountry.DataTextField = "Country_Name";
            //ddlLegalCountry.DataBind();
            //ddlLegalCountry.Items.Insert(0, new ListItem("--Please Select--", "0"));

            //ddlMasterCountry.DataSource = CommonBL.ExecuteData(spNames.getCountry);
            //ddlMasterCountry.DataValueField = "Country_Id";
            //ddlMasterCountry.DataTextField = "Country_Name";
            //ddlMasterCountry.DataBind();
            //ddlMasterCountry.Items.Insert(0, new ListItem("--Please Select--", "0"));


            //ddlLegalState.DataSource = CommonBL.ExecuteData(spNames.getState);
            //ddlLegalState.DataValueField = "StateID";
            //ddlLegalState.DataTextField = "StateName";
            //ddlLegalState.DataBind();
            //ddlLegalState.Items.Insert(0, new ListItem("--Please Select--", "0"));

            //ddlMasterState.DataSource = CommonBL.ExecuteData(spNames.getState);
            //ddlMasterState.DataValueField = "StateID";
            //ddlMasterState.DataTextField = "StateName";
            //ddlMasterState.DataBind();
            //ddlMasterState.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }
        private void bindDropDown(DropDownList ddlList)
        {
            ddlList.DataSource = CommonBL.ExecuteData(spNames.getCountry);
            ddlList.DataValueField = "Country_Id";
            ddlList.DataTextField = "Country_Name";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("--Please Select--", "0"));
        }
    }
}