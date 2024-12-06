<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="Telecom.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
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
    padding: 5px 5px;
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

       .textbox-style {
    width: 100px;               /* Set the width of the TextBox */
    padding: 5px;              /* Add some padding inside the TextBox */
    font-size: 16px;            /* Set the font size */
    border: 2px solid #888; /* Set border color */
    border-radius: 5px;        /* Add rounded corners */
    background-color: #888; /* Light blue background color */
}
   .styled-textbox {
        padding: 5px;
        font-size: 16px;
        border: 1px solid black;
        border-radius: 3px;
        background-color: #f0f8ff;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        transition: all 0.3s ease;
    }

    .styled-textbox:focus {
        border-color: #008CBA;
        background-color: #e6f7ff;
        box-shadow: 0 4px 8px rgba(0, 140, 186, 0.3);
        outline: none;
    }

    .styled-textbox::placeholder {
        color: #888;
        font-style: italic;
    }
            
        </style>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="mobileNum" runat="server" placeholder="Enter Mobile Number" CssClass="styled-textbox" Width="150px"></asp:TextBox><br />
<asp:TextBox ID="planID" runat="server" placeholder="Enter Plan ID" CssClass="styled-textbox" Width="150px"></asp:TextBox><br />
<asp:Button ID="removeBenefitsBtn" runat="server" Text="Remove Benefits" OnClick="removeBenefitsBtn_Click" CssClass="button" Width="200px"/><br />
<asp:Button ID="listSMSOffersBtn" runat="server" Text="List SMS Offers" OnClick="listSMSOffersBtn_Click" CssClass="button" Width="200px"/><br />

<asp:GridView ID="GridViewSMSOffers" runat="server" AutoGenerateColumns="True" AllowPaging="True" AllowSorting="True" PageSize="10" Width="100%" CssClass="styled-gridview"></asp:GridView>

        </div>
        <asp:Label ID="Label1" runat="server" Text="All Costumer Wallet" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="walletID" DataSourceID="SqlDataSource1" Width="100%" CssClass="styled-gridview">
            <Columns>
                <asp:BoundField DataField="walletID" HeaderText="walletID" ReadOnly="True" SortExpression="walletID" />
                <asp:BoundField DataField="current_balance" HeaderText="current_balance" SortExpression="current_balance" />
                <asp:BoundField DataField="currency" HeaderText="currency" SortExpression="currency" />
                <asp:BoundField DataField="last_modified_date" HeaderText="last_modified_date" SortExpression="last_modified_date" />
                <asp:BoundField DataField="nationalID" HeaderText="nationalID" SortExpression="nationalID" />
                <asp:BoundField DataField="mobileNo" HeaderText="mobileNo" SortExpression="mobileNo" />
                <asp:BoundField DataField="first_name" HeaderText="first_name" SortExpression="first_name" />
                <asp:BoundField DataField="last_name" HeaderText="last_name" SortExpression="last_name" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [CustomerWallet]"></asp:SqlDataSource>
        <asp:Label ID="Label2" runat="server" Text="All E-Shops-Vouchers" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="shopID,voucherID" DataSourceID="SqlDataSource2" Width="100%" CssClass="styled-gridview">
            <Columns>
                <asp:BoundField DataField="shopID" HeaderText="shopID" ReadOnly="True" SortExpression="shopID" />
                <asp:BoundField DataField="URL" HeaderText="URL" SortExpression="URL" />
                <asp:BoundField DataField="rating" HeaderText="rating" SortExpression="rating" />
                <asp:BoundField DataField="voucherID" HeaderText="voucherID" ReadOnly="True" SortExpression="voucherID" />
                <asp:BoundField DataField="value" HeaderText="value" SortExpression="value" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [E_shopVouchers]"></asp:SqlDataSource>
        <asp:Label ID="Label3" runat="server" Text="All Acount Payment" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="paymentID" DataSourceID="SqlDataSource3" Width="100%" CssClass="styled-gridview">
            <Columns>
                <asp:BoundField DataField="paymentID" HeaderText="paymentID" InsertVisible="False" ReadOnly="True" SortExpression="paymentID" />
                <asp:BoundField DataField="amount" HeaderText="amount" SortExpression="amount" />
                <asp:BoundField DataField="date_of_payment" HeaderText="date_of_payment" SortExpression="date_of_payment" />
                <asp:BoundField DataField="payment_method" HeaderText="payment_method" SortExpression="payment_method" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                <asp:BoundField DataField="mobileNo" HeaderText="mobileNo" SortExpression="mobileNo" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [AccountPayments]"></asp:SqlDataSource>
        <asp:Label ID="Label4" runat="server" Text="Number Of Cashback Transactions" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" Width="100%" CssClass="styled-gridview">
            <Columns>
                <asp:BoundField DataField="walletID" HeaderText="walletID" SortExpression="walletID" />
                <asp:BoundField DataField="count of transactions" HeaderText="count of transactions" SortExpression="count of transactions" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [Num_of_cashback]"></asp:SqlDataSource>
        <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Mobile Number" CssClass="styled-textbox"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Accepted Payment Transaction Last Year" CssClass="button" />
        <asp:Label ID="Label5" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <p>
            &nbsp;</p>
        <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Mobile Number" CssClass="styled-textbox" ></asp:TextBox>
        <asp:TextBox ID="TextBox3" runat="server"  placeholder="Enter PalnID" CssClass="styled-textbox"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Show The Amount of cashback Returned" OnClick="Button2_Click" CssClass="button" />
        <asp:Label ID="Label7" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <p>
            <asp:TextBox ID="TextBox4" runat="server" placeholder="Enter WalletID" CssClass="styled-textbox"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server" placeholder="Enter StartDate" CssClass="styled-textbox"></asp:TextBox>
            <asp:TextBox ID="TextBox6" runat="server" placeholder="Enter EndDate" CssClass="styled-textbox"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" Text="Average Sent Transactions" CssClass="button" OnClick="Button3_Click" />
            <asp:Label ID="Label8" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </p>
        <asp:TextBox ID="TextBox7" runat="server" placeholder="Enter Mobile Number" CssClass="styled-textbox"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" Text="Linked To Wallet?" CssClass="button" OnClick="Button4_Click" />
        <asp:Label ID="Label9" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <p>
            <asp:TextBox ID="TextBox8" runat="server" placeholder="Enter Mobile Number" CssClass="styled-textbox"></asp:TextBox>
            <asp:Button ID="Button5" runat="server" Text="Update Earned Points" CssClass="button" OnClick="Button5_Click" />
            <asp:Label ID="Label10" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </p>
    </form>
</body>
</html>
