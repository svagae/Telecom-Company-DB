<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Telecom.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <!-- Bootstrap CSS for Blue Theme -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        /* Custom CSS for Blue Theme */
        body {
            background-color: #e6f0ff; /* Light blue background */
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            font-family: Arial, sans-serif;
        }

        .login-container {
            background-color: #fff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
        }

        .login-header {
            text-align: center;
            margin-bottom: 20px;
            color: #007bff; /* Blue color for header */
        }

        .form-label {
            font-size: 16px;
            color: #333;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin-bottom: 20px;
            font-size: 14px;
        }

        .form-control:focus {
            border-color: #007bff; /* Blue focus border */
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
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
        .auto-style1 {
            direction: ltr;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2 class="login-header">Admin Login</h2>

            <div class="mb-3">
                <label for="txtAdminId" class="form-label">Admin ID</label>
                <asp:TextBox ID="AdmnId" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtAdminPassword" class="form-label">Password</label>
                <asp:TextBox ID="AdminPass" runat="server" TextMode="Password" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn" />
            </div>
        </div>
    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
    <p class="auto-style1">
        &nbsp;</p>
</body>
</html>

