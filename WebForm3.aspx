<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="Telecom.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        .auto-style3 {
            text-align: center;
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style3">
        <div class="auto-style3">
        <div>
        </div><asp:Label ID="Label2" runat="server" Text="Customer profile with active accounts" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%" CssClass="styled-gridview">
                <Columns>
                    <asp:BoundField DataField="mobileNo" HeaderText="mobileNo" SortExpression="mobileNo" />
                    <asp:BoundField DataField="pass" HeaderText="pass" SortExpression="pass" />
                    <asp:BoundField DataField="balance" HeaderText="balance" SortExpression="balance" />
                    <asp:BoundField DataField="account_type" HeaderText="account_type" SortExpression="account_type" />
                    <asp:BoundField DataField="start_date" HeaderText="start_date" SortExpression="start_date" />
                    <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                    <asp:BoundField DataField="points" HeaderText="points" SortExpression="points" />
                    <asp:BoundField DataField="nationalID" HeaderText="nationalID" SortExpression="nationalID" />
                    <asp:BoundField DataField="planID" HeaderText="planID" InsertVisible="False" ReadOnly="True" SortExpression="planID" />
                    <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                    <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                    <asp:BoundField DataField="SMS_offered" HeaderText="SMS_offered" SortExpression="SMS_offered" />
                    <asp:BoundField DataField="minutes_offered" HeaderText="minutes_offered" SortExpression="minutes_offered" />
                    <asp:BoundField DataField="data_offered" HeaderText="data_offered" SortExpression="data_offered" />
                    <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Telecom_Team_Team_66ConnectionString %>" SelectCommand="Account_Plan" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" OnSelecting="SqlDataSource2_Selecting"></asp:SqlDataSource>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="100%" CssClass="styled-gridview">
            </asp:GridView>
            <asp:Label ID="Label3" runat="server" Text="Date" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
            <asp:TextBox ID="Date" runat="server" CssClass="styled-textbox"  /><br />
&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="PalnID" style="font-size: 30px; font-family: Arial, sans-serif; color: dodgerblue;"></asp:Label>
&nbsp; <asp:TextBox ID="planID" runat="server" CssClass="styled-textbox"  /><br />
<asp:Button ID="get" runat="server" Text="get customer accounts" OnClick="get_Click1" CssClass="button"/>
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="True" AllowPaging="True" AllowSorting="True" PageSize="10" CssClass="styled-gridview" Width="100%"/>
            <asp:TextBox ID="mobileNum" runat="server" placeholder="Enter Mobile Number" CssClass="styled-textbox" ></asp:TextBox><br />
<asp:TextBox ID="startDate" runat="server" placeholder="Enter Start Date (YYYY-MM-DD)" CssClass="styled-textbox" ></asp:TextBox><br />
<asp:Button ID="getUsageBtn" runat="server" Text="Get Usage" OnClick="getUsageBtn_Click" CssClass="button" /><br />

<asp:GridView ID="GridViewUsage" runat="server" AutoGenerateColumns="True" AllowPaging="True" AllowSorting="True" PageSize="10" Width="100%" CssClass="styled-gridview">
</asp:GridView>
        </div>
        <asp:Button ID="next" runat="server" Text="next" OnClick="next_Click" CssClass="button" />
        <asp:Button ID="prev" runat="server" OnClick="prev_Click" Text="prev" CssClass="button" />
        </div>
    </form>
</body>
</html>
