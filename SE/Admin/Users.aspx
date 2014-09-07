<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SE.Users" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Users</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <asp:MultiView ID="UsersMultiView" ActiveViewIndex="0" runat="server">
                
                <asp:View ID="CreateUser" runat="server">
                    <h2>Create User</h2>
                    <asp:CreateUserWizard ID="CreateUserWizard" CssClass="col-md-6 col-xs-12" 
                        runat="server" oncreateduser="CreateUserWizard_CreatedUser" LoginCreatedUser="false" ContinueDestinationPageUrl="~/Admin/Users.aspx">
                        <WizardSteps>
                            <asp:CreateUserWizardStep ID="CreateUserWizardStep" runat="server">
                                <ContentTemplate>
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
                                        <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password"></asp:Label>
                                        <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" 
                                            ErrorMessage="Confirm Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="ConfirmPasswordCompare" ControlToValidate="ConfirmPassword" 
                                            ControlToCompare="Password" runat="server" ErrorMessage="- Passwords must match."></asp:CompareValidator>
                                        <asp:TextBox ID="ConfirmPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="EmailLabel" runat="server" Text="Email"></asp:Label>
                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                            ErrorMessage="Email is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="UserRoleLabel" runat="server" Text="User Role"></asp:Label>
                                        <asp:RequiredFieldValidator ID="UserRoleRequired" runat="server" ControlToValidate="UserRole" 
                                            ErrorMessage="User Role is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="UserRole" CssClass="form-control" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Supervisor</asp:ListItem>
                                            <asp:ListItem>User</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="error-message">
                                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="CreateUserWizard" />
                                    </div>
                                </ContentTemplate>
                                <CustomNavigationTemplate>
                                    <asp:Button ID="CreateUserButton" CommandName="MoveNext" CssClass="btn btn-default" runat="server" 
                                        ValidationGroup="CreateUserWizard" CausesValidation="true" Text="Submit" />
                                </CustomNavigationTemplate>
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep runat="server">
                                <ContentTemplate>
                                    <p>Account has been successfully created.</p>
                                    <asp:Button ID="ContinueButton" CommandName="Continue" CssClass="btn btn-default" runat="server" Text="Continue" />
                                </ContentTemplate>
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </asp:View>

                <asp:View ID="EditUser" runat="server">
                    <h2>Edit User</h2>
                </asp:View>

                <asp:View ID="ManageUsers" runat="server">
                    <h2>Manage Users</h2>
                </asp:View>

            </asp:MultiView>
        </div>
    </div>
</asp:Content>
