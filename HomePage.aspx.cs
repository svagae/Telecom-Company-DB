using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace Telecom
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["UserMobile"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    string mobileNumber = Session["UserMobile"].ToString();

                    LoadConsumption(mobileNumber);
                    LoadUnsubscribedPlans(mobileNumber);
                    loadusageplancurrentmonth(mobileNumber);
                    loadcashbacktransactions(mobileNumber);
                    BindTopPaymentsToGrid(mobileNumber);
                    BindSubscribedPlansToGrid(mobileNumber);
                    GetCashbackAmount(mobileNumber);
                }
            }
        }
        private void LoadUnsubscribedPlans(string mobileNumber)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Unsubscribed_Plans", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@mobile_num", mobileNumber));

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                }
                catch (Exception ex)
                {
                    Label3.Text = "Error loading unsubscribed plans: " + ex.Message;
                }
            }
        }

        private void loadusageplancurrentmonth(string mobileNumber)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = "SELECT * FROM [Usage_Plan_CurrentMonth](@mobile_num)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add(new SqlParameter("@mobile_num", mobileNumber));

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Check if DataTable is empty
                    if (dt.Rows.Count == 0)
                    {
                        // Create empty row with same structure as the DataTable
                        DataRow dr = dt.NewRow();
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (column.DataType == typeof(string))
                            {
                                dr[column.ColumnName] = ""; // Assign empty string for string columns
                            }
                            else
                            {
                                dr[column.ColumnName] = 0; // Assign 0 for other data types
                            }
                        }
                        dt.Rows.Add(dr);
                    }

                    // Bind DataTable to GridView4
                    GridView4.DataSource = dt;
                    GridView4.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle errors
                    Label5.Text = "Error loading plan usage: " + ex.Message;
                }
            }
        }



        private void loadcashbacktransactions(string mobileNumber)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // First, retrieve NationalID from the database using mobileNumber
                    string nationalIDQuery = "SELECT NationalID FROM Wallet WHERE mobileNo = @mobileNumber";  // Adjust the table and column names as needed

                    SqlCommand nationalIDCmd = new SqlCommand(nationalIDQuery, conn);
                    nationalIDCmd.Parameters.Add(new SqlParameter("@mobileNumber", mobileNumber));

                    conn.Open();
                    var nationalID = nationalIDCmd.ExecuteScalar();  // Get the NationalID from the database

                    if (nationalID != null)
                    {
                        // If NationalID is found, proceed to call the Cashback_Wallet_Customer function
                        string query = "SELECT * FROM [Cashback_Wallet_Customer](@NID)"; // Calling the function

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.Add(new SqlParameter("@NID", nationalID));  // Pass the NationalID parameter

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the result to GridView5
                        GridView5.DataSource = dt;
                        GridView5.DataBind();
                    }
                    else
                    {
                        Label7.Text = "National ID not found for this mobile number.";
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors
                    Label7.Text = "Error loading cashback transactions: " + ex.Message;
                }
            }
        }

        private void LoadConsumption(string mobileNumber)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // Query to get the planName, startDate, and endDate using the mobile number
                    string query = @"
                SELECT TOP 1 s.name AS PlanName, p.start_date AS StartDate, p.end_date AS EndDate
                FROM Plan_Usage p
                INNER JOIN Service_plan s ON s.planID = p.planID
                WHERE p.mobileNo = @MobileNumber
                ORDER BY p.start_date DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    string planName = null;
                    DateTime startDate = DateTime.MinValue;
                    DateTime endDate = DateTime.MinValue;

                    if (reader.Read())
                    {
                        planName = reader["PlanName"].ToString();
                        startDate = Convert.ToDateTime(reader["StartDate"]);
                        endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    reader.Close();

                    if (!string.IsNullOrEmpty(planName) && startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                    {
                        // Call the Consumption function and bind the result to GridView6
                        string consumptionQuery = "SELECT * FROM [Consumption](@PlanName, @StartDate, @EndDate)";

                        SqlCommand consumptionCmd = new SqlCommand(consumptionQuery, conn);
                        consumptionCmd.Parameters.AddWithValue("@PlanName", planName);
                        consumptionCmd.Parameters.AddWithValue("@StartDate", startDate);
                        consumptionCmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter da = new SqlDataAdapter(consumptionCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            GridView6.DataSource = dt;
                            GridView6.DataBind();
                           
                        }
                        else
                        {
                            Label6.Text = "No consumption data found for this mobile number.";
                            GridView6.DataSource = null;
                            GridView6.DataBind();
                        }
                    }
                    else
                    {
                        Label6.Text = "No plan found for this mobile number.";
                    }
                }
                catch (Exception ex)
                {
                    Label6.Text = "Error loading data: " + ex.Message;
                }
            }
        }

        




        private void BindTopPaymentsToGrid(string mobileNumber)
        {
            // Replace with your actual connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Top_Successful_Payments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView9.DataSource = dt;
                    GridView9.DataBind();
                }
            }
        }


        private void BindSubscribedPlansToGrid(string mobileNumber)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Subscribed_plans_5_Months(@MobileNo)", con))
                {
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNumber);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Check if DataTable is empty
                    if (dt.Rows.Count == 0)
                    {
                        // Create empty row with same structure as the DataTable
                        DataRow dr = dt.NewRow();
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (column.DataType == typeof(string))
                            {
                                dr[column.ColumnName] = ""; // Assign empty string for string columns
                            }
                            else
                            {
                                dr[column.ColumnName] = 0; // Assign 0 for other data types
                            }
                        }
                        dt.Rows.Add(dr);

                        // Optional: Customize a message in the GridView to indicate no data
                        GridView11.EmptyDataText = "No subscribed plans in the last 5 months.";
                    }

                    // Bind DataTable to GridView
                    GridView11.DataSource = dt;
                    GridView11.DataBind();
                }
            }
        }





        protected void RenewButton_Click(object sender, EventArgs e)
        {
            if (Session["UserMobile"] == null)
            {
                Response.Write("<script>alert('Session expired. Please log in again.');</script>");
                Response.Redirect("Login.aspx");
                return;
            }
            // Assuming the user's mobile number is already known (e.g., retrieved from session or user profile)
            string mobileNumber = Session["UserMobile"].ToString(); // Replace with actual mobile number logic
            string paymentMethod = TextBox3.Text;
            decimal amount;
            int planId;

            // Validate and parse user input
            if (!decimal.TryParse(TextBox1.Text, out amount))
            {
                Response.Write("<script>alert('Invalid amount! Please enter a valid number.');</script>");
                return;
            }

            if (!int.TryParse(TextBox2.Text, out planId))
            {
                Response.Write("<script>alert('Invalid Plan ID! Please enter a valid number.');</script>");
                return;
            }

            try
            {
                // Call stored procedure
                RenewPlan(mobileNumber, amount, paymentMethod, planId);
                Response.Write("<script>alert('Payment processed successfully!');</script>");
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }

        // Method to execute the stored procedure
        private void RenewPlan(string mobileNumber, decimal amount, string paymentMethod, int planId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString(); // Update with your DB connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Initiate_plan_payment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@payment_method", paymentMethod);
                    cmd.Parameters.AddWithValue("@plan_id", planId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



        private void GetCashbackAmount(string mobileNumber)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();  // Open the connection

                    // Fetch paymentId and benefitId based on the mobile number
                    int paymentId = GetPaymentId(mobileNumber, conn);
                    int benefitId = GetBenefitId(mobileNumber, conn);

                    // If either paymentId or benefitId is not found, show an error message
                    if (paymentId == 0 || benefitId == 0)
                    {
                        Label16.Text = "Unable to retrieve payment or benefit information.";
                        Label16.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // Call the stored procedure to calculate cashback
                    SqlCommand cmd = new SqlCommand("Payment_wallet_cashback", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Pass only 3 parameters to the procedure as required by the definition
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);
                    cmd.Parameters.AddWithValue("@payment_id", paymentId);
                    cmd.Parameters.AddWithValue("@benefit_id", benefitId);

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();

                    // Now let's fetch the cashback amount from the Wallet table if needed (as the proc updates it)
                    string cashbackQuery = "SELECT current_balance FROM Wallet WHERE mobileNo = @mobile_num";
                    SqlCommand balanceCmd = new SqlCommand(cashbackQuery, conn);
                    balanceCmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

                    var cashbackAmount = balanceCmd.ExecuteScalar();
                    if (cashbackAmount != DBNull.Value)
                    {
                        Label16.Text = $"The cashback amount is: {cashbackAmount} units.";
                        Label16.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        Label16.Text = "No cashback information found.";
                        Label16.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors
                    Label16.Text = "Error processing cashback: " + ex.Message;
                    Label16.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        // Function to get the payment ID based on the mobile number
        private int GetPaymentId(string mobileNumber, SqlConnection conn)
        {
            string query = "SELECT TOP 1 paymentID FROM Payment WHERE mobileNo = @mobile_num AND status = 'successful' ORDER BY date_of_payment DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

            var result = cmd.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToInt32(result) : 0; // Return 0 if no payment is found
        }

        // Function to get the benefit ID based on the mobile number
        private int GetBenefitId(string mobileNumber, SqlConnection conn)
        {
            string query = "SELECT TOP 1 benefitID FROM Cashback c Join Wallet w On c.walletid = w.walletid WHERE w.mobileNo = @mobile_num ORDER BY credit_date";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

            var result = cmd.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToInt32(result) : 0; // Return 0 if no benefit is found
        }


        protected void RechargeButton_Click(object sender, EventArgs e)
        {
            string mobileNumber = TextBox4.Text.Trim();
            decimal amount;
            string paymentMethod = TextBox6.Text.Trim();

            // Validate and parse user input
            if (string.IsNullOrEmpty(mobileNumber) || mobileNumber.Length != 11)
            {
                Response.Write("<script>alert('Please enter a valid 11-digit mobile number.');</script>");
                return;
            }

            if (!decimal.TryParse(TextBox5.Text.Trim(), out amount) || amount <= 0)
            {
                Response.Write("<script>alert('Please enter a valid amount greater than 0.');</script>");
                return;
            }

            if (string.IsNullOrEmpty(paymentMethod))
            {
                Response.Write("<script>alert('Please enter a payment method.');</script>");
                return;
            }

            try
            {
                // Call the stored procedure
                InitiateBalancePayment(mobileNumber, amount, paymentMethod);
                Response.Write("<script>alert('Recharge successful!');</script>");
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
            }
        }

        // Method to execute the stored procedure
        private void InitiateBalancePayment(string mobileNumber, decimal amount, string paymentMethod)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString(); // Update with your DB connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Initiate_balance_payment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@payment_method", paymentMethod);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Retrieve the mobile number from session
            if (Session["UserMobile"] == null)
            {
                Response.Write("<script>alert('Session expired. Please log in again.');</script>");
                Response.Redirect("Login.aspx");
                return;
            }

            string mobileNumber = Session["UserMobile"].ToString();
            int voucherId;

            // Validate and parse user input
            if (!int.TryParse(TextBox7.Text.Trim(), out voucherId) || voucherId <= 0)
            {
                Response.Write("<script>alert('Please enter a valid Voucher ID.');</script>");
                return;
            }

            try
            {
                // Execute the stored procedure
                RedeemVoucherPoints(mobileNumber, voucherId);
                Response.Write("<script>alert('Voucher redeemed successfully!');</script>");
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("no enough points to redeem voucher"))
                {
                    Response.Write("<script>alert('Not enough points to redeem the voucher.');</script>");
                }
                else
                {
                    Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('An unexpected error occurred: {ex.Message}');</script>");
            }
        }

        // Method to execute the stored procedure
        private void RedeemVoucherPoints(string mobileNumber, int voucherId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Redeem_voucher_points", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);
                    cmd.Parameters.AddWithValue("@voucher_id", voucherId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            // Retrieve the mobile number from the session
            string inputMobileNo = Session["UserMobile"]?.ToString();  // Assuming session key is "UserMobile"

            // Validate the mobile number from session
            if (string.IsNullOrEmpty(inputMobileNo))
            {
                Label18.Text = "Mobile number not found in session. Please log in.";
                Label18.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("dbo.Account_Highest_Voucher", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the mobile number from session as a parameter
                        command.Parameters.AddWithValue("@mobile_num", inputMobileNo);

                        // Execute the command and get the result
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                // Retrieve the voucher ID
                                int voucherID = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);

                                // Display the result in Label18
                                Label18.Text = $"Highest Voucher ID: {voucherID}";
                                Label18.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                // If no voucher is found
                                Label18.Text = "No voucher found for the given Mobile Number.";
                                Label18.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exception
                Label18.Text = "A database error occurred: " + sqlEx.Message;
                Label18.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                // Handle general exception
                Label18.Text = "An unexpected error occurred: " + ex.Message;
                Label18.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            // Retrieve the mobile number from the session
            string inputMobileNo = Session["UserMobile"]?.ToString();  // Assuming session key is "UserMobile"

            // Validate the mobile number from session
            if (string.IsNullOrEmpty(inputMobileNo))
            {
                Label19.Text = "Mobile number not found in session. Please log in.";
                Label19.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Retrieve the National ID from the database based on the mobile number
            string nationalID = GetNationalIDByMobile(inputMobileNo);
            if (string.IsNullOrEmpty(nationalID))
            {
                Label19.Text = "National ID not found for the given mobile number.";
                Label19.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Retrieve National ID from TextBox (if applicable)
            // string nationalID = TextBoxNID.Text.Trim();

            // Validate the National ID
            if (string.IsNullOrEmpty(nationalID))
            {
                Label19.Text = "Please enter a valid National ID.";
                Label19.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("dbo.Ticket_Account_Customer", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add National ID as a parameter to the command
                        command.Parameters.AddWithValue("@NID", nationalID);

                        // Execute the command and get the result
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                // Retrieve the count of unresolved tickets
                                int unresolvedTickets = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);

                                // Display the result in Label19
                                Label19.Text = $"Unresolved Tickets: {unresolvedTickets}";
                                Label19.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                // If no unresolved tickets are found
                                Label19.Text = "No unresolved tickets found for the given National ID.";
                                Label19.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exception
                Label19.Text = "A database error occurred: " + sqlEx.Message;
                Label19.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                // Handle general exception
                Label19.Text = "An unexpected error occurred: " + ex.Message;
                Label19.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method to fetch National ID by mobile number
        private string GetNationalIDByMobile(string mobileNo)
        {
            string nationalID = string.Empty;
            string connStr = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Query to get the National ID based on mobile number
                    string query = "SELECT nationalID FROM customer_account WHERE mobileNo = @MobileNo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                        nationalID = cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle error for fetching National ID if necessary
                Label19.Text = "Error fetching National ID: " + ex.Message;
                Label19.ForeColor = System.Drawing.Color.Red;
            }

            return nationalID;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {

            // Retrieve the mobile number from the session
            string mobileNumber = Session["UserMobile"]?.ToString();  // Assuming session key is "UserMobile"

            // Validate the mobile number from session
            if (string.IsNullOrEmpty(mobileNumber))
            {
                Label11.Text = "Mobile number not found in session. Please log in.";
                Label11.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                // Get extra amount using the mobile number
                int extraAmount = GetExtraAmountByMobile(mobileNumber);

                // Display the result in Label11
                if (extraAmount > 0)
                {
                    Label11.Text = $"The extra amount is: {extraAmount}";
                    Label11.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Label11.Text = "No extra amount found for the given mobile number.";
                    Label11.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Label11.Text = $"An error occurred: {ex.Message}";
                Label11.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int GetExtraAmountByMobile(string mobileNumber)
        {
            int extraAmount = 0;

            // Replace with your connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Fetch paymentId and planId using the mobile number
                int paymentId = GetPaymentIdByMobile(mobileNumber, con);
                int planId = GetPlanIdByMobile(mobileNumber, con);

                using (SqlCommand cmd = new SqlCommand("SELECT dbo.function_extra_amount(@paymentId, @planId)", con))
                {
                    cmd.Parameters.AddWithValue("@paymentId", paymentId);
                    cmd.Parameters.AddWithValue("@planId", planId);

                    object result = cmd.ExecuteScalar();
                    extraAmount = result != null ? Convert.ToInt32(result) : 0;
                }
            }

            return extraAmount;
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            // Retrieve the mobile number from the session
            string mobileNumber = Session["UserMobile"]?.ToString();  // Assuming session key is "UserMobile"

            // Validate the mobile number from session
            if (string.IsNullOrEmpty(mobileNumber))
            {
                Label10.Text = "Mobile number not found in session. Please log in.";
                Label10.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Get the paymentId and planId based on mobile number (modify this as per your requirement)
            int paymentId = GetPaymentIdByMobile(mobileNumber);
            int planId = GetPlanIdByMobile(mobileNumber);

            // Call the function to get the remaining amount
            int remainingAmount = GetRemainingAmount(paymentId, planId);

            // Display the result in Label10
            if (remainingAmount > 0)
            {
                Label10.Text = $"Remaining Amount: {remainingAmount}";
                Label10.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label10.Text = "No remaining amount.";
                Label10.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method to get the remaining amount by calling the function
        private int GetRemainingAmount(int paymentId, int planId)
        {
            int remainingAmount = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT dbo.function_remaining_amount(@paymentId, @planId)", con))
                {
                    cmd.Parameters.AddWithValue("@paymentId", paymentId);
                    cmd.Parameters.AddWithValue("@planId", planId);

                    object result = cmd.ExecuteScalar();
                    remainingAmount = result != null ? Convert.ToInt32(result) : 0;
                }
            }

            return remainingAmount;
        }



        // Helper methods to get IDs based on mobile number
        private int GetPaymentIdByMobile(string mobileNumber, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT paymentid FROM Payment WHERE mobileNo = @MobileNumber", con))
            {
                cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                return (int)cmd.ExecuteScalar();
            }
        }

        private int GetPlanIdByMobile(string mobileNumber, SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT sp.planID FROM Service_plan sp INNER JOIN Subscription s ON sp.planID = s.planId WHERE s.mobileNo = @MobileNumber", con))
            {
                cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                return (int)cmd.ExecuteScalar();
            }
        }





        // Method to get the payment ID based on the mobile number
        private int GetPaymentIdByMobile(string mobileNumber)
        {
            int paymentId = 0; // Default value if not found

            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // SQL query to retrieve the paymentId associated with the mobile number
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 paymentId FROM Payment WHERE mobileNo = @MobileNumber", con))
                {
                    cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);

                    // Execute query and get the paymentId
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        paymentId = Convert.ToInt32(result);
                    }
                }
            }

            return paymentId;
        }

        // Method to get the plan ID based on the mobile number
        private int GetPlanIdByMobile(string mobileNumber)
        {
            int planId = 0; // Default value if not found

            string connectionString = WebConfigurationManager.ConnectionStrings["Telecom_Team_Team_66"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // SQL query to retrieve the planId associated with the mobile number
                using (SqlCommand cmd = new SqlCommand("SELECT sp.planID FROM Service_plan sp INNER JOIN Subscription s ON sp.planID = s.planId WHERE s.mobileNo = @MobileNumber", con))
                {
                    cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);

                    // Execute query and get the planId
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        planId = Convert.ToInt32(result);
                    }
                }
            }

            return planId;
        }


    }
}



 
    