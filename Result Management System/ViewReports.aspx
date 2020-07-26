<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReports.aspx.cs" Inherits="Result_Management_System.ViewReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Reports | Results Management System</title>
</head>
<body>
    <div>
        <h1>Results Management System</h1>
        <hr />
        <h2>View Reports</h2>
        <a href="#">View Reports</a> | <a href="ManageResults.aspx">Manage Results</a>
        <form id="form1" runat="server">
            <div>
                <p>
                    <asp:Button ID="logoutButton" Text="Logout" runat="server" OnClick="logoutEventMethod" />
                </p>
                <p>
                    Hello <asp:Label ID="userLabel" runat="server" />
                </p>
            </div>
            <div>
                <p>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="studentIDLabel" runat="server" Text="Student ID"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="unitCodeLabel" runat="server" Text="Unit Code"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="yearLabel" runat="server" Text="Year"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="semesterLabel" runat="server" Text="Semester"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:DropDownList ID="studentIDList" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="<Select Student>" Value="0" />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="unitCodeList" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="<Select Unit>" Value="0" />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="yearList" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="<Select Year>" Value="0" />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="semesterList" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                    <asp:ListItem Text="<Select Semester>" Value="0" />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                                <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchEventMethod" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </p>
            </div>
            <div>
                <p>
                    <asp:Label ID="resultLabel" runat="server"></asp:Label>
                </p>
            </div>
            <div>
                <asp:GridView ID="reportGridView" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Student ID">
                            <ItemTemplate>
                                <asp:Label ID="studentIDLabel" runat="server" Text='<%# Eval("studentID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Code">
                            <ItemTemplate>
                                <asp:Label ID="unitCodeLabel" runat="server" Text='<%# Eval("unitCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                                <asp:Label ID="yearLabel" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Semester">
                            <ItemTemplate>
                                <asp:Label ID="semesterLabel" runat="server" Text='<%# Eval("semester") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assessment 1 Score">
                            <ItemTemplate>
                                <asp:Label ID="assessment1ScoreLabel" runat="server" Text='<%# Eval("assessment1Score") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assessment 2 Score">
                            <ItemTemplate>
                                <asp:Label ID="assessment2ScoreLabel" runat="server" Text='<%# Eval("assessment2Score") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam Score">
                            <ItemTemplate>
                                <asp:Label ID="examScoreLabel" runat="server" Text='<%# Eval("examScore") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Score">
                            <ItemTemplate>
                                <asp:Label ID="unitScoreLabel" runat="server" Text='<%# Eval("unitScore") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grade">
                            <ItemTemplate>
                                <asp:Label ID="gradeLabel" runat="server" Text='<%# Eval("grade") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Photo">
                            <ItemTemplate>
                                <asp:Image ID="studentPhotoImage" runat="server" ImageUrl='<%# Eval("studentPhoto") %>' Height="100px" Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="unitOutlineButton" runat="server" OnClick="downloadUnitOutline" Text='<%# System.IO.Path.GetFileName(Eval("unitOutline").ToString()) %>'></asp:LinkButton>
                                <asp:Label ID="currentUnitOutlineLabel" runat="server" Text='<%# Eval("unitOutline") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </div>
</body>
</html>
