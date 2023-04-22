using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples
{
    public partial class Login : System.Web.UI.Page
    {
        public ITHeart.BL.GenMstBL CommonBL;
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonBL = new ITHeart.BL.GenMstBL();
            
        }
        private void LoadMenuList()
        {
            DataTable dt;
            dt = CommonBL.Load_LoginUser(CommonBL.UserID,TextBox1.Text, TextBox2.Text);
            if (dt!=null && dt.Rows.Count > 0)
            {
                Session["UserName"] = TextBox1.Text;
                // Response.Redirect("controllerfile.aspx?optionid=202076000&paraminfo=");
                 Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Label1.Text = "Your username and word is incorrect";
                Label1.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadMenuList();
        }
    }
}