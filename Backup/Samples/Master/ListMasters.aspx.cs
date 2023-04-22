using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Samples.Master
{
    public partial class ListMasters : System.Web.UI.Page
    {
        public ITHeart.BL.GenMstBL CommonBL;
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonBL = new ITHeart.BL.GenMstBL();
            LoadMasterData();
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
            divLeftMenu.Controls.Add(tblMasterData);
        }
        private string CreateLink(string Name, string GmID)
        {
            string sReturn = "";
            sReturn = "<a href='../Controller.aspx?OptionID=202076000n|" + GmID + "&GenID="+GmID+"&ParamInfo='>" + Name + "</a>";
            return sReturn;
        }
    }
}