<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUnits.aspx.cs" Inherits="Result_Management_System.Manage_Units" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Units | Results Management System</title>
</head>
<body>
    <div>
        <h1>Results Management System</h1>
        <hr />
        <h2>Manage Units</h2>
        <%--<a href="#">Manage Units</a>--%>
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
                                <asp:Label ID="unitcodelabel" runat="server" Text="Unit Code"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="unitcodetextbox" runat="server" MaxLength="7"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="unitcodemessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="unittitlelabel" runat="server" Text="Unit Title"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="unittitletextbox" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="unittitlemessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="unitcoordinatorlabel" runat="server" Text="Unit Coordinator"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="unitcoordinatortexttox" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="unitcoordinatormessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="unitoutlinelabel" runat="server" Text="Unit Outline"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:FileUpload ID="unitoutlinefileupload" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="unitoutlinemessage" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableFooterRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Button ID="addunitbutton" runat="server" Text="Add Unit" OnClick="addUnitEventMethod" />
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
                <asp:GridView ID="unitsGridView" runat="server" AutoGenerateColumns="false" DataKeyNames="unitCode" 
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
                        <asp:TemplateField HeaderText="Unit Title">
                            <ItemTemplate>
                                <asp:Label ID="unitTitleLabel" runat="server" Text='<%# Eval("unitTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="unitTitleTextBox" runat="server" Text='<%# Eval("unitTitle") %>'></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="unitTitleRequiredValidator" runat="server" ControlToValidate="unitTitleTextBox" ValidationGroup="Update" ErrorMessage="Required Feild" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Coordinator">
                            <ItemTemplate>
                                <asp:Label ID="unitCoordinatorLabel" runat="server" Text='<%# Eval("unitCoordinator") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="unitCoordinatorTextBox" runat="server" Text='<%# Eval("unitCoordinator") %>'></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="unitCoordinatorRequiredValidator" runat="server" ControlToValidate="unitCoordinatorTextBox" ValidationGroup="Update" ErrorMessage="Required Feild" ForeColor="Red"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Outline">
                            <ItemTemplate>
                                <asp:LinkButton ID="unitOutlineButton" runat="server" OnClick="downloadUnitOutline" Text='<%# System.IO.Path.GetFileName(Eval("unitOutline").ToString()) %>'></asp:LinkButton>
                                <asp:Label ID="currentUnitOutlineLabel" runat="server" Text='<%# Eval("unitOutline") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="unitOutlineButton" runat="server" CommandName="Download" Text='<%# System.IO.Path.GetFileName(Eval("unitOutline").ToString()) %>'></asp:LinkButton><br />
                                <asp:FileUpload ID="unitOutlineFileUpload" runat="server" />
                                <asp:Label ID="currentUnitOutlineLabel" runat="server" Text='<%# Eval("unitOutline") %>' Visible="false"></asp:Label>
                            </EditItemTemplate>
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
