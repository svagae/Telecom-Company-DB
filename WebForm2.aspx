<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Telecom.WebForm2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Active Customer Accounts</title>
    <style>
/* lightblue-theme.css */
body {
    background-color: #E0F7FA; /* Light blue background */
    color: #00796B; /* Dark teal color for text */
    font-family: Arial, sans-serif; /* Set a clean font */
}

h1, h2, h3 {
    color: #0288D1; /* Light blue color for headings */
}

    .button {
    background-color: #0288D1; /* Light blue button */
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

    .button:hover {
    background-color: #0277BD; /* Slightly darker blue on hover */
}


        .styled-gridview {
        border-collapse: collapse;
         font-size: 18px;
        font-family: Arial, sans-serif;
            margin-top: 12px;
        }

    .styled-gridview th {
        background-color: #2196F3;
        color: white;
        text-align: left;
        padding: 12px;
        border: 1px solid #ddd;
    }

    .styled-gridview td {
        border: 1px solid #ddd;
        padding: 12px;
    }

    .styled-gridview tr:nth-child(even) {
        background-color: #f2f2f2;
    }

    .styled-gridview tr:hover {
        background-color: #ddd;
    }

    .styled-gridview th:hover {
        cursor: pointer; /* Makes headers interactive, e.g., for sorting */
    }
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            margin-left: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
        <div class="auto-style1">
        <div class="container mt-4">
           <asp:Label ID="Label2" runat="server" Text="All Service Plans" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            <!-- GridView to display customer profiles with active accounts -->
          
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="nationalID,mobileNo" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%" CssClass="styled-gridview" >
            <Columns>
                <asp:BoundField DataField="nationalID" HeaderText="nationalID" ReadOnly="True" SortExpression="nationalID" />
                <asp:BoundField DataField="first_name" HeaderText="first_name" SortExpression="first_name" />
                <asp:BoundField DataField="last_name" HeaderText="last_name" SortExpression="last_name" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="address" HeaderText="address" SortExpression="address" />
                <asp:BoundField DataField="date_of_birth" HeaderText="date_of_birth" SortExpression="date_of_birth" />
                <asp:BoundField DataField="mobileNo" HeaderText="mobileNo" ReadOnly="True" SortExpression="mobileNo" />
                <asp:BoundField DataField="account_type" HeaderText="account_type" SortExpression="account_type" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                <asp:BoundField DataField="start_date" HeaderText="start_date" SortExpression="start_date" />
                <asp:BoundField DataField="balance" HeaderText="balance" SortExpression="balance" />
                <asp:BoundField DataField="points" HeaderText="points" SortExpression="points" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [allCustomerAccounts]" ></asp:SqlDataSource>
                 <asp:Label ID="xyz" runat="server" Text="All physical stores Redeemed Vouchers" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="shopID,voucherID" DataSourceID="SqlDataSource2" Height="184px" Width="100%" CssClass="styled-gridview">
                <Columns>
                    <asp:BoundField DataField="shopID" HeaderText="shopID" ReadOnly="True" SortExpression="shopID" />
                    <asp:BoundField DataField="address" HeaderText="address" SortExpression="address" />
                    <asp:BoundField DataField="working_hours" HeaderText="working_hours" SortExpression="working_hours" />
                    <asp:BoundField DataField="voucherID" HeaderText="voucherID" ReadOnly="True" SortExpression="voucherID" />
                    <asp:BoundField DataField="value" HeaderText="value" SortExpression="value" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [PhysicalStoreVouchers]"></asp:SqlDataSource>
            <asp:Label ID="nmn" runat="server" Text="All Resolved Tickets" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            <br />
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="ticketID,mobileNo" DataSourceID="SqlDataSource3" Width="100%" CssClass="styled-gridview" >
                <Columns>
                    <asp:BoundField DataField="ticketID" HeaderText="ticketID" InsertVisible="False" ReadOnly="True" SortExpression="ticketID" />
                    <asp:BoundField DataField="mobileNo" HeaderText="mobileNo" ReadOnly="True" SortExpression="mobileNo" />
                    <asp:BoundField DataField="issue_description" HeaderText="issue_description" SortExpression="issue_description" />
                    <asp:BoundField DataField="priority_level" HeaderText="priority_level" SortExpression="priority_level" />
                    <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [allResolvedTickets]"></asp:SqlDataSource>
        </div>
            <asp:Button ID="Next" runat="server" OnClick="Next_Click" Text="Next" CssClass="button" />
            <asp:Button ID="prev" runat="server" OnClick="prev_Click" Text="prev" CssClass="button" />
        </div>
    </form>

   
</body>
</html>