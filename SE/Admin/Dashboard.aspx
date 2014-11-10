<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SE.Dashboard" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <!-- /.row -->
    <asp:Label ID="label" runat="server" Text=""></asp:Label>
    <asp:Label ID="label2" runat="server" Text=""></asp:Label>
    <asp:MultiView ID="DashboardView" ActiveViewIndex="0" runat="server">
        <asp:View ID="Manager" runat="server">
            <script src="../Scripts/Manager.js"></script>
            <div class="row page-header">
                <div class="col-xs-12">
                    <div class="row">
                        <asp:UpdatePanel ID="ManageUserGrid" runat="server">
                            <ContentTemplate>
                                <div class="col-xs-11 userManager" style="padding: 5px;">
                                    <div class="panel panel-primary" style="position: fixed; top: 70px">
                                        <div class="panel-heading">
                                            <i class="fa fa-users fa-fw"></i>User Management
                                        </div>
                                        <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                            <asp:GridView ID="allUsers" DataSourceID="allUsersSource" CssClass="table table-bordered table-striped" runat="server" AllowPaging="false" AutoGenerateColumns="false" ShowHeader="true" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField="UserName" HeaderText="Users" ItemStyle-Width="10%"></asp:BoundField>
                                                    <asp:BoundField DataField="RoleName" HeaderText="Role" ItemStyle-Width="10%"></asp:BoundField>
                                                    <asp:BoundField DataField="AssignedSupervisor" HeaderText="Assigned Supervisor" ItemStyle-Width="10%" NullDisplayText="Not Assigned"></asp:BoundField>
                                                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="10%"></asp:BoundField>
                                                    <asp:BoundField DataField="LastActivityDate" HeaderText="Last Active Date" ItemStyle-Width="10%" DataFormatString="{0:d}"></asp:BoundField>
                                                    <asp:BoundField DataField="IsApproved" HeaderText="Active" ItemStyle-Width="10%"></asp:BoundField>
                                                    <asp:BoundField DataField="IsLockedOut" HeaderText="Account Locked" ItemStyle-Width="10%"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="allUsersSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                                <div id="contentWrapper" class="col-xs-12">
                                    <div id="usermanager" class="col-xs-12">
                                        <div class="col-xs-3">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <i class="fa fa-users fa-5x"></i>
                                                        </div>
                                                        <div class="col-xs-9 text-right">
                                                            <div class="huge"></div>
                                                            <div>Add New Users</div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="addUser">
                                                    <div class="panel-footer btn btn-default" style="width: 100%">
                                                        <span class="pull-left">Add</span>
                                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <i class="fa fa-user fa-5x"></i>
                                                        </div>
                                                        <div class="col-xs-9 text-right">
                                                            <div class="huge"></div>
                                                            <div>Add New Supervisor</div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="addSupervisor">
                                                    <div class="panel-footer btn btn-default" style="width: 100%">
                                                        <span class="pull-left">Add</span>
                                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="addUser">
                                        <div class="col-md-6 col-xs-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="error-messages form-group">
                                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CreateUserWizard" />
                                                    </div>
                                                    <asp:Panel ID="AssignedToContainer" CssClass="form-group" runat="server">
                                                        <asp:Label ID="AssignedToLabel" runat="server" Text="Assigned To"></asp:Label>
                                                        <asp:DropDownList ID="AssignedTo" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="Username is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="Password" Display="None"
                                                    ControlToCompare="ConfirmPassword" runat="server" ErrorMessage="Passwords must match."
                                                    ValidationGroup="CreateUserWizard"></asp:CompareValidator>
                                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label4" runat="server" Text="Confirm Password"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmPassword"
                                                    ErrorMessage="Confirm Password is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Email"
                                                    ErrorMessage="Email is required." ValidationGroup="CreateUserWizard">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="Email" ErrorMessage="E-mail address must be in a valid format" Display="None"
                                                    ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" ValidationGroup="CreateUserWizard">
                                                </asp:RegularExpressionValidator>
                                                <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="Button1" CssClass="btn btn-default right" runat="server"
                                                ValidationGroup="CreateUserWizard" CausesValidation="true" Text="Submit"
                                                OnClick="CreateUserButton_Click" />
                                        </div>

                                    </div>
                                    <div id="addSupervisor">
                                        <div class="col-md-6 col-xs-12">
                                            <asp:UpdatePanel ID="UserRoleContainer" runat="server">
                                                <ContentTemplate>
                                                    <div class="error-messages form-group">
                                                        <asp:Literal ID="CreateUserErrorMessage" runat="server"></asp:Literal>
                                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="CreateUserWizard" />
                                                    </div>
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
                                                OnClick="CreateUserButton_Click" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <!-- /.col-xs-12 -->
        </asp:View>

        <asp:View ID="Supervisor" runat="server">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="page-header">Supervisor Dashboard</h1>
                </div>
                <!-- /.col-xs-12 -->
                <div class="col-xs-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>Active User List
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="activeUserList" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False" AutoGenerateColumns="false" ShowHeader="false" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="UserName" HeaderText="Supervisor" ItemStyle-Width="10%"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>Recently Assigned Users
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="newMembers" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False" AutoGenerateColumns="false" ShowHeader="false" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="UserName" HeaderText="Supervisor" ItemStyle-Width="10%"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>Supervisors
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="signededIn" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False" AutoGenerateColumns="false" ShowHeader="false" GridLines="None">
                                <Columns>
                                    <asp:HyperLinkField DataTextField="UserName" HeaderText="Supervisor" ItemStyle-Width="10%"></asp:HyperLinkField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="panel-footer">
                            <p class="text-warning">NOTE: Click on name to request categorys/tasks</p>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
