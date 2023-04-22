using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace Samples
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public ITHeart.BL.GenMstBL CommonBL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                CommonBL = new ITHeart.BL.GenMstBL();
                LoadMenuList();
                lbluserName.InnerText = Session["UserName"].ToString();
            }
        }
        private void LoadMenuList()
        {
            string optionName = string.Empty;
            string cssClass = string.Empty;


            StringBuilder htmlTable = new StringBuilder();
            Session["MenuString"] = null;
            DataTable _mInfo = CommonBL.Load_LoginUser("DEL0000001", "", "");
            DataRow[] _parentItem = _mInfo.Select("ParentID='" + string.Empty + "'");
            for (int i = 0; i < _parentItem.Count(); i++)
            {
                cssClass = "fa fa-laptop";
                optionName = _parentItem[i]["OptionName"].ToString();
                string gmID = _parentItem[i]["ExtraParam"].ToString().Trim();
                string optionID = _parentItem[i]["OptionID"].ToString().Trim();
                var liParentActive = i == 0 ? "active treeview" : "treeview";

                //< a href = '../Controllerfile.aspx?OptionID=202076000n|" + GmID + "&GenID=" + GmID + "&ParamInfo=' > " + Name + " </ a >
               // htmlTable.Append("<li class= nav-item has-treeview " + liParentActive + "><a href='../Controllerfile.aspx?OptionID=" + optionID + "|" + gmID + "&GenID=" + gmID + "&ParamInfo='><i class='" + cssClass + "'></i><span>" + optionName + "</span> <i class='fa fa-angle-left pull-right'></i></a>");

                htmlTable.Append("<li class='nav-item has-treeview'><a class='nav-link' href='../Controllerfile.aspx?OptionID=" + optionID + "|" + gmID + "&GenID=" + gmID + "&ParamInfo='><i class='" + cssClass + "'></i> <p>"  + optionName +  "</p> <i class='right fas fa-angle-left'></i></a>");
                htmlTable.Append("<ul class='nav nav-treeview'>");
                string MenuID = Convert.ToString(_parentItem[i]["OptionID"]);

                DataRow[] dr = _mInfo.Select("ParentID='" + MenuID + "'");
                if (dr.Count() > 0)
                {
                    string childCssClass = string.Empty;
                    string childOptionName = string.Empty;
                    string childExtraParam = string.Empty;
                    string childParentID = string.Empty;
                    string childOptionID = string.Empty;
                    for (int j = 0; j < dr.Count(); j++)
                    {
                        childCssClass = "far fa-circle nav-icon";
                        childOptionName = dr[j]["OptionName"].ToString();
                        childExtraParam = dr[j]["ExtraParam"].ToString().Trim();
                        childParentID = dr[j]["ParentID"].ToString();
                        childOptionID = dr[j]["OptionID"].ToString().Trim();
                        var liActive = j == 0 && i == 0 ? "active" : "";
                        // htmlTable.Append("<li class=" + liActive + "><a href='../Controllerfile.aspx?OptionID=" + childOptionID + "|" + childExtraParam + "&GenID=" + childExtraParam + "&ParamInfo='><i class='" + childCssClass + "'></i>" + childOptionName + "</a></li>");
                        htmlTable.Append("<li class='nav-item liClick' ><a class='nav-link' href='../Controllerfile.aspx?OptionID=" + childOptionID + "|" + childExtraParam + "&GenID=" + childExtraParam + "&ParamInfo='><i class='" + childCssClass + "'></i><p>" + childOptionName + "</p></a></li>");
                    }
                    htmlTable.Append("</ul>");
                }
            }
            htmlTable.Append("</li>");
            Session["MenuString"] = Convert.ToString(htmlTable);
        }
    }
}