<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Telecom.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <style>
    .center {
      text-align: center;           /* Centers text horizontally */
    display: block;               /* Makes the element block-level */
    margin-left: auto;            /* Centers horizontally */
    margin-right: auto;           /* Centers horizontally */
    height: 34px;                 /* Sets the height of the element */
    position: relative;           /* Required for absolute positioning inside */
    top: 50%;                     /* Moves the element 50% from the top of its parent */
    transform: translateY(-50%);
        }  /* Stack elements vertically */
   .textbox-style {
    width: 300px;               /* Set the width of the TextBox */
    padding: 10px;              /* Add some padding inside the TextBox */
    font-size: 16px;            /* Set the font size */
    border: 2px solid #888; /* Set border color */
    border-radius: 5px;        /* Add rounded corners */
    background-color: #888; /* Light blue background color */
}
   .styled-textbox {
        width: 300px;
        padding: 10px;
        font-size: 16px;
        border: 2px solid black;
        border-radius: 5px;
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
            .btn {
            width: 100%;
            padding: 12px;
            background-color: #007bff; /* Blue button */
            color: #fff;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #0056b3; /* Darker blue on hover */
        }
    
</style>
<body>
    <form id="form1" runat="server">
        <div class="center">
            <asp:Label ID="Username" runat="server" Text="Username" style="font-size: 30px; font-family: Arial, sans-serif; color: black;"></asp:Label>
        </div>
        <p class="center">
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" CssClass ="styled-textbox"></asp:TextBox>
        </p>
        <p class="center">
            <asp:Label ID="Password" runat="server" Text="Password" style="font-size: 30px; font-family: Arial, sans-serif; color: black;"></asp:Label>
        </p>
        <p class="center">
        <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged" CssClass ="styled-textbox"></asp:TextBox>
        </p>
        <p class="center">
            <asp:Button ID="mohamed" runat="server" Text="Log In" OnClick="mohamed_Click" CssClass="btn" Height="46px" Width="109px" />
        </p>
    </form>
</body>
</html>
