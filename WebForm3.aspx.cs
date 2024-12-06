using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telecom
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void get_Click1(object sender, EventArgs e)
        {
            string conns = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66ConnectionString"].ToString();

            if (string.IsNullOrEmpty(planID.Text) || string.IsNullOrEmpty(Date.Text))
            {
                Response.Write("<script>alert('Both Plan ID and Date must be provided.');</script>");
                return;
            }

            if (!int.TryParse(planID.Text, out int planId))
            {
                Response.Write("<script>alert('Please enter a valid numeric Plan ID.');</script>");
                return;
            }

            if (!DateTime.TryParse(Date.Text, out DateTime parsedDate))
            {
                Response.Write("<script>alert('Please enter a valid date in the correct format (YYYY-MM-DD).');</script>");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conns))
                {
                    string query = "SELECT * FROM dbo.Account_Plan_date(@sub_date, @plan_id)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@sub_date", parsedDate);
                        cmd.Parameters.AddWithValue("@plan_id", planId);

                        con.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                GridView3.DataSource = dt;
                                GridView3.DataBind();
                            }
                            else
                            {
                                Response.Write("<script>alert('No data found for the given inputs.');</script>");
                                GridView3.DataSource = null;
                                GridView3.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }

        protected void getUsageBtn_Click(object sender, EventArgs e)
        {
            // Retrieve the connection string from Web.config
            string conns = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66ConnectionString"].ToString();

            // Validate inputs
            if (string.IsNullOrEmpty(mobileNum.Text) || string.IsNullOrEmpty(startDate.Text))
            {
                Response.Write("<script>alert('Both Mobile Number and Start Date must be provided.');</script>");
                return;
            }

            // Validate that the Mobile Number is a valid 11-character number
            if (mobileNum.Text.Length != 11 || !mobileNum.Text.All(char.IsDigit))
            {
                Response.Write("<script>alert('Please enter a valid 11-digit Mobile Number.');</script>");
                return;
            }

            // Validate the Start Date format (YYYY-MM-DD)
            if (!DateTime.TryParse(startDate.Text, out DateTime parsedStartDate))
            {
                Response.Write("<script>alert('Please enter a valid Start Date in the format YYYY-MM-DD.');</script>");
                return;
            }

            try
            {
                // Open connection to the database
                using (SqlConnection con = new SqlConnection(conns))
                {
                    string query = "SELECT * FROM dbo.Account_Usage_Plan(@mobile_num, @start_date)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SQL query
                        cmd.Parameters.AddWithValue("@mobile_num", mobileNum.Text);
                        cmd.Parameters.AddWithValue("@start_date", parsedStartDate);

                        // Open connection and fetch the data using SqlDataAdapter
                        con.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Bind the results to the GridView
                            if (dt.Rows.Count > 0)
                            {
                                GridViewUsage.DataSource = dt;
                                GridViewUsage.DataBind();
                            }
                            else
                            {
                                Response.Write("<script>alert('No usage data found for the provided inputs.');</script>");
                                GridViewUsage.DataSource = null;
                                GridViewUsage.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }
        protected void next_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm4.aspx");
        }

        protected void prev_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }

        protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}
    

