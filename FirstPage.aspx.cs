using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telecom
{
    public partial class FirstPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnWebForm1_Click(object sender, EventArgs e)
        {
            // Redirect to WebForm1
            Response.Redirect("WebForm1.aspx");
        }

        protected void BtnLogin_Clic(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}