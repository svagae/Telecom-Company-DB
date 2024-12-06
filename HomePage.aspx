<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Telecom.HomePage" %>

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
        width: 100px;
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
            &nbsp;<asp:Label ID="Label2" runat="server" Text="All Service Palns" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="planID" DataSourceID="SqlDataSource1" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-top: 9px" CssClass="styled-gridview" Height="241px">
            <Columns>
                <asp:BoundField DataField="planID" HeaderText="planID" InsertVisible="False" ReadOnly="True" SortExpression="planID" />
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                <asp:BoundField DataField="SMS_offered" HeaderText="SMS_offered" SortExpression="SMS_offered" />
                <asp:BoundField DataField="minutes_offered" HeaderText="minutes_offered" SortExpression="minutes_offered" />
                <asp:BoundField DataField="data_offered" HeaderText="data_offered" SortExpression="data_offered" />
                <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [allServicePlans]"></asp:SqlDataSource>
        <asp:Label ID="Label6" runat="server" Text="Consumtion" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <br />
        <asp:GridView ID="GridView6" runat="server" Width="100%" AutoGenerateColumns="True" CssClass="styled-gridview">
        </asp:GridView>
        <asp:Label ID="Label3" runat="server" Text="Unsubscribed Plans" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="styled-gridview" Height="206px"> 
        </asp:GridView>
        <asp:Label ID="Label4" runat="server" Text="All Benefits" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="benefitID" DataSourceID="SqlDataSource2" Width="100%" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" CssClass="styled-gridview" Height="223px" >
            <Columns>
                <asp:BoundField DataField="benefitID" HeaderText="benefitID" InsertVisible="False" ReadOnly="True" SortExpression="benefitID" />
                <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
                <asp:BoundField DataField="validity_date" HeaderText="validity_date" SortExpression="validity_date" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                <asp:BoundField DataField="mobileNo" HeaderText="mobileNo" SortExpression="mobileNo" />
            </Columns>
        </asp:GridView>
            <asp:Label ID="Label5" runat="server" Text="Current Month Consumption" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            <asp:GridView ID="GridView4" runat="server" Width="100%" style="margin-top: 11px" AutoGenerateColumns="True" CssClass="styled-gridview">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [allBenefits]"></asp:SqlDataSource>
        <asp:Label ID="Label7" runat="server" Text="Cashback Transactions" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>

        <asp:GridView ID="GridView5" runat="server" Width="100%" AutoGenerateColumns="True" CssClass="styled-gridview">
        </asp:GridView>
        <asp:Label ID="Label12" runat="server" Text="Top 10 Successful Payments With Highest Value" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView9" runat="server" Width="100%" CssClass="styled-gridview">
        </asp:GridView>
        <asp:Label ID="Label13" runat="server" Text="All Shops" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" DataKeyNames="shopID" DataSourceID="SqlDataSource5" Width="100%" CssClass="styled-gridview">
            <Columns>
                <asp:BoundField DataField="shopID" HeaderText="shopID" InsertVisible="False" ReadOnly="True" SortExpression="shopID" />
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="SELECT * FROM [allShops]"></asp:SqlDataSource>
        <asp:Label ID="Label14" runat="server" Text="All Service Plans Past 5 Months" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="True" Width="100%" CssClass="styled-gridview">
        </asp:GridView>
        <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Amount" CssClass ="styled-textbox"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" placeholder="PlanID" CssClass ="styled-textbox"></asp:TextBox>
        <asp:TextBox ID="TextBox3" runat="server" placeholder="Enter Payment Method" CssClass ="styled-textbox"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Renew" OnClick="RenewButton_Click" CssClass="button" />
        <p>
            <asp:Label ID="Label16" runat="server" Text="Label" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </p>
        <asp:TextBox ID="TextBox4" runat="server" placeholder="Enter Mobile Number" CssClass ="styled-textbox"></asp:TextBox>
        <asp:TextBox ID="TextBox5" runat="server" placeholder="Enter Amount" CssClass ="styled-textbox"></asp:TextBox>
        <asp:TextBox ID="TextBox6" runat="server" placeholder="Enter Payment Method" CssClass ="styled-textbox"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Recharge" CssClass="button"/>
        <p>
            <asp:TextBox ID="TextBox7" runat="server" placeholder="Enter VoucherID" CssClass ="styled-textbox"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" Text="Redeem" CssClass="button" OnClick="Button3_Click"/>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="Button4" runat="server" Text="Highest Value Voucher" CssClass="button" OnClick="Button4_Click" />
            <asp:Label ID="Label18" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </p>
        <p>
        <asp:Button ID="Button5" runat="server" Text="Number of NOT Resolved Technical Support Tickets" CssClass="button" OnClick="Button5_Click" />
        <asp:Label ID="Label19" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            </p>
        <p>
            <asp:Button ID="Button6" runat="server" Text="Get Extra Amount" CssClass="button" OnClick="Button6_Click" />
            <asp:Label ID="Label11" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </p>
        <p>
            <asp:Button ID="Button7" runat="server" Text="Remaining Amount" CssClass="button" OnClick="Button7_Click" />
        <asp:Label ID="Label10" runat="server" Text="" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
        </p>
    </form>
</body>
</html>
