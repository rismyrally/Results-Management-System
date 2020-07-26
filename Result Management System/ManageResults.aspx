<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageResults.aspx.cs" Inherits="Result_Management_System.ManageResults" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Results | Results Management System</title>
</head>
<body>
    <div>
        <h1>Results Management System</h1>
        <hr />
        <h2>Manage Results</h2>
        <a href="#">Manage Results</a> | <a href="ViewReports.aspx">View Reports</a>
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
                                <asp:Label ID="unitcodelabel" runat="server" Text="Unit Code"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="unitcodedropdownlist" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="unitSelectedEventMethod">
                                    <asp:ListItem Text="<Select Unit>" Value="0" />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="unitcodemessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="studentidlabel" runat="server" Text="Student ID"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="studentiddropdownlist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studentSelectedEventMethod" Enabled="false">
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="studentidmessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="yearlabel" runat="server" Text="Year"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="yeardropdownlist" runat="server" Enabled="false"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="yearmessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="semesterlabel" runat="server" Text="Semester"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="semesterdropdownlist" runat="server" Enabled="false"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="semestermessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="assessment1scorelabel" runat="server" Text="Assessment 1 Score"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="assessment1scoretextbox" runat="server" Enabled="false" MaxLength="5"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="assessment1scoremessage" runat="server" AutoPostBack="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="assessment2scorelabel" runat="server" Text="Assessment 2 Score"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="assessment2scoretextbox" runat="server" Enabled="false" MaxLength="5"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="assessment2scoremessage" runat="server" AutoPostBack="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="examscorelabel" runat="server" Text="Exam Score"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="examscoretextbox" runat="server" Enabled="false" MaxLength="5"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="examscoremessage" runat="server" AutoPostBack="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableFooterRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Button ID="addresultbutton" runat="server" Text="Add Result" Enabled="false" OnClick="addResultEventMethod" />
                            </asp:TableCell>
                        </asp:TableFooterRow>
                    </asp:Table>
                </p>
            </div>
            <div>
                <p>
                    <asp:Label ID="resultLabel" runat="server"></asp:Label>
                </p>
            </div>
            <div>
                <asp:GridView ID="resultsGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="unitCode, studentID" 
                    OnPageIndexChanging="uGV_PageIndexChangingEvent" 
                    OnRowCancelingEdit="uGV_RowCancelingEditEvent" 
                    OnRowDeleting="uGV_RowDeletingEvent" 
                    OnRowEditing="uGV_RowEditingEvent" 
                    OnRowUpdating="uGV_RowUpdatingEvent">
                    <Columns>
                        <asp:TemplateField HeaderText="Unit Code">
                            <ItemTemplate>
                                <asp:Label ID="unitCodeLabel" runat="server" Text='<%# Eval("unitCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID">
                            <ItemTemplate>
                                <asp:Label ID="studentIDLabel" runat="server" Text='<%# Eval("studentID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Photo">
                            <ItemTemplate>
                                <asp:Image ID="studentPhotoImage" runat="server" ImageUrl='<%# Eval("studentPhoto") %>' Height="100px" Width="100px" />
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
                            <EditItemTemplate>
                                <asp:TextBox ID="assessment1ScoreTextBox" runat="server" Text='<%# Eval("assessment1Score") %>' MaxLength="5"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="assessment1ScoreRequiredValidator" runat="server" ControlToValidate="assessment1ScoreTextBox" ValidationGroup="Update" ErrorMessage="Required Feild" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RangeValidator ID="assessment1ScoreRangeValidator" runat="server" ControlToValidate="assessment1ScoreTextBox" ValidationGroup="Update" Type="Double" MinimumValue="0" MaximumValue="20" Display="Dynamic" ErrorMessage="Score should be between 0 to 20" ForeColor="Red"></asp:RangeValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assessment 2 Score">
                            <ItemTemplate>
                                <asp:Label ID="assessment2ScoreLabel" runat="server" Text='<%# Eval("assessment2Score") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="assessment2ScoreTextBox" runat="server" Text='<%# Eval("assessment2Score") %>' MaxLength="5"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="assessment2ScoreRequiredValidator" runat="server" ControlToValidate="assessment2ScoreTextBox" ValidationGroup="Update" ErrorMessage="Required Feild" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RangeValidator ID="assessment2ScoreRangeValidator" runat="server" ControlToValidate="assessment2ScoreTextBox" ValidationGroup="Update" Type="Double" MinimumValue="0" MaximumValue="20" Display="Dynamic" ErrorMessage="Score should be between 0 to 20" ForeColor="Red"></asp:RangeValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam Score">
                            <ItemTemplate>
                                <asp:Label ID="examScoreLabel" runat="server" Text='<%# Eval("examScore") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="examScoreTextBox" runat="server" Text='<%# Eval("examScore") %>' MaxLength="5"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="examScoreRequiredValidator" runat="server" ControlToValidate="examScoreTextBox" ValidationGroup="Update" ErrorMessage="Required Feild" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RangeValidator ID="examScoreRangeValidator" runat="server" ControlToValidate="examScoreTextBox" ValidationGroup="Update" Type="Double" MinimumValue="0" MaximumValue="60" Display="Dynamic" ErrorMessage="Score should be between 0 to 60" ForeColor="Red"></asp:RangeValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Score">
                            <ItemTemplate>
                                <asp:Label ID="unitScoreLabel" runat="server" Text='<%# Eval("unitScore") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="editButton" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want delete this record?');"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="updateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="Update"></asp:LinkButton>
                                <asp:LinkButton ID="cancelButton" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </div>
</body>
</html>
