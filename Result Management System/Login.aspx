<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Result_Management_System.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login | Results Management System</title>

    <link href="LoginStyleSheet.css" rel="stylesheet" />
</head>
<body>
    <div class="box">
        <div class="content">
            <h1>Results Management System</h1><br />
            <h2>Login to start session</h2>
            
            <form id="form1" runat="server">
                <asp:TextBox class="field" ID="useremailTextBox" placeholder="User Email" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" runat="server"   
                ControlToValidate="useremailTextBox" ErrorMessage="Enter User Email" ForeColor="Red" Font-Size="11"></asp:RequiredFieldValidator><br />
                <asp:TextBox class="field" ID="passwordTextBox" placeholder="User Password" TextMode="Password" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" runat="server"   
                ControlToValidate="passwordTextBox" ErrorMessage="Enter Password" ForeColor="Red" Font-Size="11"></asp:RequiredFieldValidator><br />
                <asp:Button class="btn" ID="loginButton" Text="Login" runat="server" OnClick="loginEventMethod" /><br />
                <asp:Label ID="errorMessageLabel" runat="server"></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
