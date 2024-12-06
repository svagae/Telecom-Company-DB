using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telecom
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private const string AdminId = "admin123";
        private const string AdminPassword = "adminpassword";

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
                // Get values from the form
                string enteredAdminId = AdmnId.Text.Trim();
                string enteredAdminPassword = AdminPass.Text.Trim();

                // Check if the entered credentials match the hardcoded ones
                if (enteredAdminId == AdminId && enteredAdminPassword == AdminPassword)
                {
                    // Redirect to the dashboard on successful login
                    Response.Redirect("WebForm2.aspx"); // Replace with your dashboard page
                }
                else
                {
                    // Display error message if credentials are incorrect
                  
                }
            }
        }
    }
   