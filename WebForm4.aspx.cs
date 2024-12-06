using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telecom
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void removeBenefitsBtn_Click(object sender, EventArgs e)
        {
            string conns = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66ConnectionString"].ToString();

            // Validate inputs
            if (string.IsNullOrEmpty(mobileNum.Text) || string.IsNullOrEmpty(planID.Text))
            {
                Response.Write("<script>alert('Both Mobile Number and Plan ID must be provided.');</script>");
                return;
            }

            if (!int.TryParse(planID.Text, out int planId))
            {
                Response.Write("<script>alert('Please enter a valid numeric Plan ID.');</script>");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conns))
                {
                    string query = "EXEC Benefits_Account @mobile_num, @plan_id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@mobile_num", mobileNum.Text);
                        cmd.Parameters.AddWithValue("@plan_id", planId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Benefits removed successfully.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }

        protected void listSMSOffersBtn_Click(object sender, EventArgs e)
        {
            string conns = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66ConnectionString"].ToString();

            // Validate Mobile Number
            if (string.IsNullOrEmpty(mobileNum.Text))
            {
                Response.Write("<script>alert('Please provide a valid Mobile Number.');</script>");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conns))
                {
                    string query = "SELECT * FROM dbo.Account_SMS_Offers(@mobile_num)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@mobile_num", mobileNum.Text);

                        con.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Bind the result to the GridView
                            if (dt.Rows.Count > 0)
                            {
                                GridViewSMSOffers.DataSource = dt;
                                GridViewSMSOffers.DataBind();
                            }
                            else
                            {
                                Response.Write("<script>alert('No SMS offers found for the given mobile number.');</script>");
                                GridViewSMSOffers.DataSource = null;
                                GridViewSMSOffers.DataBind();
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


        protected void Button1_Click(object sender, EventArgs e)
        {

            // Get the mobile number from TextBox1
            string inputMobileNo = TextBox1.Text.Trim();

            // Validate the input to ensure it's not empty
            if (string.IsNullOrEmpty(inputMobileNo))
            {
                Label5.Text = "Please enter a valid Mobile Number.";
                Label5.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("dbo.Account_Payment_Points", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the mobile number as a parameter
                        command.Parameters.AddWithValue("@mobile_num", inputMobileNo);

                        // Execute the command and get the results
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                // If data is found, retrieve the count and sum of points
                                int paymentCount = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                                decimal totalPoints = rdr.IsDBNull(1) ? 0 : rdr.GetDecimal(1);

                                // Display the result in Label5
                                Label5.Text = $"Accepted Transactions: {paymentCount}<br />Total Earned Points: {totalPoints}";
                                Label5.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                // If no data is found, display a message
                                Label5.Text = "No data found for the given Mobile Number.";
                                Label5.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exception
                Label5.Text = "A database error occurred: " + sqlEx.Message;
                Label5.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                // Handle general exception
                Label5.Text = "An unexpected error occurred: " + ex.Message;
                Label5.ForeColor = System.Drawing.Color.Red;
            }
        }

     

        protected void Button2_Click(object sender, EventArgs e)
        {
            string inputMobileNo = TextBox2.Text.Trim();
            string inputWalletID = TextBox3.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(inputMobileNo) || string.IsNullOrEmpty(inputWalletID))
            {
                Label7.Text = "Please enter both Mobile Number and Wallet ID.";
                Label7.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!int.TryParse(inputWalletID, out int walletID))
            {
                Label7.Text = "Wallet ID must be a valid number.";
                Label7.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("SELECT dbo.Wallet_Cashback_Amount(@walletID, @planID)", conn))
                    {
                        // Replace with an appropriate planID; consider retrieving it dynamically based on your requirements
                        int planID = 1; // Replace with actual logic if necessary

                        // Add parameters
                        command.Parameters.AddWithValue("@walletID", walletID);
                        command.Parameters.AddWithValue("@planID", planID);

                        object result = command.ExecuteScalar(); // Get the result of the scalar function

                        if (result != null && result != DBNull.Value)
                        {
                            int cashbackAmount = Convert.ToInt32(result);
                            Label7.Text = $"Cashback Amount: {cashbackAmount}";
                            Label7.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Label7.Text = "No cashback amount found for the provided Wallet ID and Plan ID.";
                            Label7.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Label7.Text = "A database error occurred: " + sqlEx.Message;
                Label7.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                Label7.Text = "An unexpected error occurred: " + ex.Message;
                Label7.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void Button3_Click(object sender, EventArgs e)
        {
            // Get input from textboxes
            string inputWalletID = TextBox4.Text.Trim();
            string inputStartDate = TextBox5.Text.Trim();
            string inputEndDate = TextBox6.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(inputWalletID) || string.IsNullOrEmpty(inputStartDate) || string.IsNullOrEmpty(inputEndDate))
            {
                Label8.Text = "Please enter Wallet ID, Start Date, and End Date.";
                Label8.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!int.TryParse(inputWalletID, out int walletID))
            {
                Label8.Text = "Wallet ID must be a valid number.";
                Label8.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!DateTime.TryParse(inputStartDate, out DateTime startDate) || !DateTime.TryParse(inputEndDate, out DateTime endDate))
            {
                Label8.Text = "Start Date and End Date must be valid dates.";
                Label8.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("SELECT dbo.Wallet_Transfer_Amount(@walletID, @start_date, @end_date)", conn))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@walletID", walletID);
                        command.Parameters.AddWithValue("@start_date", startDate);
                        command.Parameters.AddWithValue("@end_date", endDate);

                        object result = command.ExecuteScalar(); // Execute the function and get the result

                        if (result != null && result != DBNull.Value)
                        {
                            int avgTransferAmount = Convert.ToInt32(result);
                            Label8.Text = $"Average Transfer Amount: {avgTransferAmount}";
                            Label8.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Label8.Text = "No transfer data found for the given inputs.";
                            Label8.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Label8.Text = "A database error occurred: " + sqlEx.Message;
                Label8.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                Label8.Text = "An unexpected error occurred: " + ex.Message;
                Label8.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            // Get input from the textbox
            string inputMobileNo = TextBox7.Text.Trim();

            // Validate the input
            if (string.IsNullOrEmpty(inputMobileNo))
            {
                Label9.Text = "Please enter a valid Mobile Number.";
                Label9.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("SELECT dbo.Wallet_MobileNo(@mobile_num)", conn))
                    {
                        // Add parameter
                        command.Parameters.AddWithValue("@mobile_num", inputMobileNo);

                        object result = command.ExecuteScalar(); // Execute the function and get the result

                        if (result != null && result != DBNull.Value)
                        {
                            bool hasWallet = Convert.ToBoolean(result);

                            if (hasWallet)
                            {
                                Label9.Text = "The mobile number is associated with a wallet.";
                                Label9.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                Label9.Text = "The mobile number is not associated with a wallet.";
                                Label9.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            Label9.Text = "No data found.";
                            Label9.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Label9.Text = "A database error occurred: " + sqlEx.Message;
                Label9.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                Label9.Text = "An unexpected error occurred: " + ex.Message;
                Label9.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            // Get the mobile number from TextBox8
            string inputMobileNo = TextBox8.Text.Trim();

            // Validate the input
            if (string.IsNullOrEmpty(inputMobileNo))
            {
                Label10.Text = "Please enter a valid Mobile Number.";
                Label10.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Execute the stored procedure
                    using (SqlCommand command = new SqlCommand("dbo.Total_Points_Account", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter
                        command.Parameters.AddWithValue("@mobile_num", inputMobileNo);

                        // Execute the procedure
                        command.ExecuteNonQuery();

                        // Optionally, retrieve updated points (if needed) after execution.
                        using (SqlCommand selectCommand = new SqlCommand("SELECT points FROM customer_account WHERE mobileNo = @mobile_num", conn))
                        {
                            selectCommand.Parameters.AddWithValue("@mobile_num", inputMobileNo);
                            object result = selectCommand.ExecuteScalar();
                            if (result != null)
                            {
                                int totalPoints = Convert.ToInt32(result);
                                Label10.Text = $"Total Points for Mobile Number {inputMobileNo}: {totalPoints}";
                                Label10.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                Label10.Text = "No data found for the given Mobile Number.";
                                Label10.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Label10.Text = "A database error occurred: " + sqlEx.Message;
                Label10.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                Label10.Text = "An unexpected error occurred: " + ex.Message;
                Label10.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}



