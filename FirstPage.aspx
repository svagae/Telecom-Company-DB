<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="Telecom.FirstPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /* Apply Flexbox to center content */
        body, html {
            height: 100%;
            margin: 0;
            display: flex;
            justify-content: center;  /* Center horizontally */
            align-items: center;      /* Center vertically */
            background-color: #f4f4f4; /* Optional: Add a light background color */
        }

        .container {
            text-align: center; /* Center the text inside the container */
        }

        .btn {
            width: 200px;  /* Adjust button width */
            padding: 12px;
            background-color: #007bff; /* Blue button */
            color: #fff;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            margin: 10px;  /* Add margin between buttons */
        }

        .btn:hover {
            background-color: #0056b3; /* Darker blue on hover */
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:Button ID="Button1" runat="server" Text="Sign As Customer" OnClick="BtnLogin_Clic" CssClass="btn" />
            <p>
                <asp:Button ID="Button2" runat="server" Text="Sign As Admin" OnClick="BtnWebForm1_Click" CssClass="btn"/>
            </p>
        </div>
    </form>
</body>
</html>

