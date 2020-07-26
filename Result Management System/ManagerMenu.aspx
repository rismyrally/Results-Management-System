<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerMenu.aspx.cs" Inherits="Result_Management_System.ManagerMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager Menu | Results Management System</title>
</head>
<body>
    <div>
        <h1>Results Management System</h1>
        <hr />
        <h2>Main Menu</h2>
        <%--<a href="#">Main Menu</a>--%>
        <form id="form1" runat="server">
            <div>
                <asp:Button ID="logoutButton" Text="Logout" runat="server" OnClick="logoutEventMethod" />
                <p>
                    Hello <asp:Label ID="userLabel" runat="server" />
                </p>
            </div>
            <div>
                <p>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="manageResultsButton" runat="server" Text="Manage Results" OnClick="manageResultsEventMethod" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="viewReportsButton" runat="server" Text="View Reports" OnClick="viewReportsEventMethod" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </p>
            </div>
        </form>
    </div>
</body>
</html>
