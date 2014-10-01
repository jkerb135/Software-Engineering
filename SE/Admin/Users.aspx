<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SE.Users" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Users</h1>
        </div>
    </div>
    <asp:MultiView ID="UsersMultiView" ActiveViewIndex="0" runat="server">
                
        <asp:View ID="CreateUser" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <h2>Create User</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <asp:UpdatePanel ID="UserRoleContainer" runat="server">
                        <ContentTemplate>
                            <div class="error-messages form-group">
                                <asp:Literal ID="CreateUserErrorMessage" runat="server"></asp:Literal>
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="CreateUserWizard" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="UserRoleLabel" runat="server" Text="User Role"></asp:Label>
                                <asp:RequiredFieldValidator ID="UserRoleRequired" runat="server" ControlToValidate="UserRole" 
                                    ErrorMessage="User Role is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                <asp:DropDownList ID="UserRole" CssClass="form-control" runat="server" 
                                    AutoPostBack="true" onselectedindexchanged="UserRole_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Supervisor</asp:ListItem>
                                    <asp:ListItem>User</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Panel ID="AssignedToContainer" CssClass="form-group" runat="server">
                                <asp:Label ID="AssignedToLabel" runat="server" Text="Assigned To"></asp:Label>
                                <asp:RequiredFieldValidator ID="AssignedToRequired" runat="server" ControlToValidate="AssignedTo"
                                    ErrorMessage="Assigned To is required" ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                <asp:DropDownList ID="AssignedTo" CssClass="form-control" runat="server"></asp:DropDownList>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>   
                    <div class="form-group">
                        <asp:Label ID="UserNameLabel" runat="server" Text="Username"></asp:Label>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                            ErrorMessage="Username is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="PasswordLabel" runat="server" Text="Password"></asp:Label>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                            ErrorMessage="Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="PasswordCompare" ControlToValidate="Password" Display="None"
                            ControlToCompare="ConfirmPassword" runat="server" ErrorMessage="Passwords must match." 
                            ValidationGroup="CreateUserWizard"></asp:CompareValidator>
                        <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password"></asp:Label>
                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" 
                            ErrorMessage="Confirm Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="ConfirmPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="EmailLabel" runat="server" Text="Email"></asp:Label>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                            ErrorMessage="Email is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="EmailValid" runat="server" 
                            ControlToValidate="Email" ErrorMessage="E-mail address must be in a valid format" Display="None" 
                            ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" ValidationGroup="CreateUserWizard">
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:Button ID="CreateUserButton" CssClass="btn btn-default right" runat="server" 
                        ValidationGroup="CreateUserWizard" CausesValidation="true" Text="Submit" 
                        onclick="CreateUserButton_Click" />
                </div>
            </div>
        </asp:View>

        <asp:View ID="EditUser" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <h2>Edit User</h2>
                    <p>Note: all fields are optional</p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <div class="error-messages form-group">
                        <asp:Label ID="EditErrorMessage" runat="server"></asp:Label>
                        <asp:ValidationSummary ID="EditValidationSummary" ValidationGroup="EditUser" runat="server" />
                    </div>
                    <div class="success-messages form-group">
                        <asp:Label ID="EditSuccessMessage" runat="server"></asp:Label>
                    </div>
                    <asp:Panel ID="EditAssignedToContainer" CssClass="form-group" runat="server">
                        <asp:Label ID="EditAssignedToLabel" runat="server" Text="Assign User To"></asp:Label>
                        <asp:DropDownList ID="EditAssignedTo" CssClass="form-control" runat="server"></asp:DropDownList>
                    </asp:Panel>
                    <div class="form-group">
                        <asp:Label ID="EditEmailLabel" runat="server" Text="Email"></asp:Label>
                        <asp:RegularExpressionValidator ID="EditEmailValid" runat="server" 
                            ControlToValidate="EditEmail" ErrorMessage="E-mail address must be in a valid format" Display="None" 
                            ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" ValidationGroup="EditUser">
                        </asp:RegularExpressionValidator>
                        <asp:TextBox ID="EditEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="EditPasswordLabel" runat="server" Text="Password"></asp:Label>
                        <asp:CompareValidator ID="EditPasswordCompare" ControlToValidate="EditPassword" Display="None"
                            ControlToCompare="EditConfirmPassword" runat="server" ErrorMessage="Passwords must match." 
                            ValidationGroup="EditUser"></asp:CompareValidator>
                        <asp:TextBox ID="EditPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="EditConfirmPasswordLabel" runat="server" Text="Confirm Password"></asp:Label>
                        <asp:TextBox ID="EditConfirmPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:Button ID="EditUserButton" CssClass="btn btn-default right" runat="server" 
                        ValidationGroup="EditUser" CausesValidation="true" Text="Submit" onclick="EditUserButton_Click" />                      
                    <asp:Button ID="DeleteUserButton" CssClass="btn btn-danger btn-lg clear block" runat="server" Text="Delete User"
                        onclick="DeleteUserButton_Click" OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                </div>
            </div>
        </asp:View>

        <asp:View ID="ManageUsers" runat="server">
            <div class="row">
                <div class="col-lg-12">  
                    <h2>Manage Users</h2>
                    <asp:UpdatePanel ID="ManageUserGrid" runat="server">
                        <ContentTemplate>
                            <div class="success-messages form-group">
                                <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                            </div>
                            <div class="table-responsive">
                                <asp:DataGrid ID="UserList" CssClass="table table-bordered table-striped" runat="server" AllowPaging="true" OnPageIndexChanged="UserList_Change" AllowSorting="true">
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:View>

    </asp:MultiView>
</asp:Content>
