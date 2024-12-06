using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telecom
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void mohamed_Click(object sender, EventArgs e)
        {
            // Retrieve input values
            string mobileNumber = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();

            // Connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL command to call the function
                    string query = "SELECT dbo.AccountLoginValidation(@mobile_num, @pass)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);
                        cmd.Parameters.AddWithValue("@pass", password);

                        // Execute command and retrieve result
                        object resultObj = cmd.ExecuteScalar();
                        if (resultObj != null && int.TryParse(resultObj.ToString(), out int result) && result == 1)
                        {
                            // Login successful
                            Session["UserMobile"] = mobileNumber; // Store mobile number in session

                            Response.Redirect("HomePage.aspx");
                        }
                        else
                        {
                            // Login failed
                            Response.Write("<script>alert('Invalid credentials. Please try again.');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error (if applicable) and show message
                    Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
                }
            }
        }

    }
}

