<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SE.Dashboard" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
    <script>
        function blink() {
            $('#userManagement').fadeTo(100, 0.1).fadeTo(200, 1.0);
        }
    </script>
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <!-- /.row -->
    <asp:Label ID="label" runat="server" Text=""></asp:Label>
    <asp:Label ID="label2" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="managerState" runat="server" Value="none" />
    <asp:MultiView ID="DashboardView" ActiveViewIndex="0" runat="server">
        <asp:View ID="Manager" runat="server">
            <script src="../Scripts/Manager.js"></script>
            <div class="row page-header"></div>
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>
                        <div class="form-inline">
                        <div class="form-group">
                                    Search By Username: <asp:TextBox runat="server" ID="userSearch" CssClass="form-control" stlye="width:30%"></asp:TextBox><asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-primary"/><asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_OnClick"/>
                        </div>
                        </div>        
                        <asp:GridView OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowSorting="true" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" GridLines="None" ShowFooter="True" CssClass="table table-striped" ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="6">
                            <SortedAscendingHeaderStyle CssClass="fa fa-arrow-up"></SortedAscendingHeaderStyle>
                        <SortedDescendingHeaderStyle CssClass="fa fa-arrow-down"></SortedDescendingHeaderStyle>
                            <PagerTemplate>
                            <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrev" CssClass="btn btn-primary" />
                            <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext" CssClass="btn btn-primary" />
                        </PagerTemplate>
                        <PagerSettings Position="Top"></PagerSettings>
                            <Columns>
                                <asp:BoundField HeaderText="User ID" DataField="UserId" InsertVisible="False" SortExpression="UserId" Visible="false" />
                                <asp:TemplateField HeaderText="Username" SortExpression="Username">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="UserLabel" Text='<%# Eval("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="form-control" runat="server" ID="UsernameTxt" Text='<%# Eval("UserName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="UsernameTxt" CssClass="form-control" runat="server" Placeholder="Username"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemStyle Width="8.2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password" SortExpression="Password">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="PasswordLbl" Text='<%# Eval("Password") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="form-control" runat="server" ID="PasswordTxt" Text='<%# Eval("Password") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="PasswordTxt" CssClass="form-control" runat="server" Placeholder="Password"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemStyle Width="5.2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" ItemStyle-Width="5.2%" SortExpression="Email">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="EmailLbl" Text='<%# Eval("Email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="form-control" runat="server" ID="EmailTxt" Text='<%# Eval("Email") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="EmailTxt" CssClass="form-control" runat="server" Placeholder="Email"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Last Active Date" DataField="LastLoginDate" InsertVisible="False" SortExpression="LastLoginDate" ReadOnly="true" ItemStyle-Width="8.2%"/>
                                <asp:TemplateField HeaderText="Role Name" SortExpression="RoleName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="RoleLbl" Text='<%# Eval("RoleName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="RoleDrp" CssClass="form-control" SelectedValue='<%# Eval("RoleName") %>' AutoPostBack="true">
                                            <asp:ListItem Value="Supervisor">Supervisor</asp:ListItem>
                                            <asp:ListItem Value="User">User</asp:ListItem>
                                        </asp:DropDownList> 

                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList runat="server" ID="RoleDrp" CssClass="form-control" OnSelectedIndexChanged="RoleDrp_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Supervisor">Supervisor</asp:ListItem>
                                            <asp:ListItem Value="User">User</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle Width="10.2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned User" SortExpression="AssignedSupervisor">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="AssignLbl" Text='<%# Eval("AssignedSupervisor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" Enabled="false" DataTextField="UserName" ID="AssignDrp" CssClass="form-control" DataSourceID="SqlDataSource2" SelectedValue='<%# Eval("AssignedSupervisor") %>'>
                                        </asp:DropDownList> 

                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList runat="server" Enabled="false" DataTextField="UserName" ID="AssignDrp" CssClass="form-control" DataSourceID="SqlDataSource2">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle Width="10.2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disabled" ItemStyle-Width="5.2%" SortExpression="IsLockedOut">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LockOutLbl" Text='<%# Eval("IsLockedOut") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="LockOutDrp" CssClass="form-control" SelectedValue='<%# Eval("IsLockedOut") %>'>
                                            <asp:ListItem Value="False">Enabled</asp:ListItem>
                                            <asp:ListItem Value="True">Disabled</asp:ListItem>
                                        </asp:DropDownList> 

                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList runat="server" ID="LockOutDrp" CssClass="form-control">
                                            <asp:ListItem Value="False">Enabled</asp:ListItem>
                                            <asp:ListItem Value="True">Disabled</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemStyle Width="10.2%" />
                                </asp:TemplateField>
                                 <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:LinkButton ID="EditBtn" runat="server" CssClass="btn btn-warning" CommandName="Edit" Text="Edit" CausesValidation="False"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="UpdateBtn" runat="server" CssClass="btn btn-primary" CommandName="Update" Text="Update" CausesValidation="False"></asp:LinkButton>
                                    <asp:LinkButton ID="CancelBtn" runat="server" CssClass="btn btn-danger" CommandName="Cancel" Text="Cancel" CausesValidation="False"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="NewInsert" runat="server" Text="Add" CommandName="FooterInsert" CssClass="btn btn-success" CausesValidation="False" OnClick="NewInsert_Click" ></asp:LinkButton>
                                </FooterTemplate>
                                <ItemStyle Width="15.2%" />
                            </asp:TemplateField>
                                 </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ipawsTeamBConnectionString %>" SelectCommand="SELECT aspnet_Users.UserName, aspnet_Users.UserId FROM aspnet_Users INNER JOIN aspnet_UsersInRoles ON aspnet_Users.UserId = aspnet_UsersInRoles.UserId INNER JOIN aspnet_Roles ON aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId WHERE (aspnet_Roles.RoleName = 'Supervisor')"></asp:SqlDataSource>
                <div class="row">
                    <div id="contentWrapper" class="col-xs-12">
                        <div id="usermanager" class="col-xs-12">
                            <div class="col-xs-3 addUser btn">
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
                                    <div>
                                        <div class="panel-footer btn btn-default" style="width: 100%">
                                            <span class="pull-left">Add</span>
                                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-3 btn addSupervisor">
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
                                    <div class="panel-footer btn-default" style="width: 100%">
                                        <span class="pull-left">Add</span>
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-3 btn generateReport">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <i class="fa fa-bar-chart-o fa-5x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <div class="huge"></div>
                                                <div>Report Generation</div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="panel-footer btn btn-default" style="width: 100%">
                                            <span class="pull-left">Add</span>
                                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="addSupervisor">
                        <asp:HiddenField ID="supervisorState" runat="server" />
                        <div class="col-md-6 col-xs-12">
                            <asp:UpdatePanel ID="UserRoleContainer" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="CreateUserButton" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="error-messages form-group">
                                        <asp:Literal ID="CreateUserErrorMessage" runat="server"></asp:Literal>
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="CreateUserWizard" />
                                    </div>
                                    <div id="usersContainer">
                                        <asp:Panel ID="AssignedToContainer" CssClass="form-group" runat="server">
                                            <asp:Label ID="AssignedToLabel" runat="server" Text="Assigned To"></asp:Label>
                                            <asp:DropDownList ID="AssignedTo" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </asp:Panel>
                                    </div>
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
                                    <input type="button" value="Back" class="btn btn-danger" id="goback" />
                                    <asp:Button ID="CreateUserButton" CssClass="btn btn-success right" runat="server"
                                        ValidationGroup="CreateUserWizard" CausesValidation="true" Text="Submit"
                                        OnClick="CreateUserButton_Click" />

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div id="reportGeneration">

                        <div class="col-xs-12">
                            <div class="col-xs-11 reportManager" style="padding: 5px;">
                                <div class="panel panel-primary generation">
                                    <div class="panel-heading" id="reportHeader" style="cursor: pointer">
                                        <i class="fa fa-users fa-fw"></i>Report Management
                                    <i class="fa fa-arrow-down right" id="rptHead"></i>

                                    </div>
                                    <div class="panel-body">
                                        <asp:GridView ID="reports" DataSourceID="reportGenerationSource" CssClass="table table-bordered table-striped" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" ShowHeader="true" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="UserName" HeaderText="Users" ItemStyle-Width="10%"></asp:BoundField>
                                                <asp:BoundField DataField="Comment" HeaderText="IP Address" ItemStyle-Width="10%"></asp:BoundField>
                                            </Columns>
                                            <PagerSettings Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous" />
                                            <PagerTemplate>
                                                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrev" CssClass="btn btn-primary" />
                                                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext" CssClass="btn btn-primary" />
                                            </PagerTemplate>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="reportGenerationSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
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
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h3>
                        </div>
                        <div class="modal-body">
                            <h4>
                                <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label></h4>
                            <br />
                            <h4 style="text-align: left">
                                <asp:Label ID="lblSincere" runat="server" Text=""></asp:Label></h4>
                            <h4 style="text-align: left">
                                <asp:Label ID="From" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-footer">
                            <asp:CheckBox ID="ShowAgain" runat="server" /><asp:Label ID="Label1" runat="server" Text="Don't Show On Login."></asp:Label>
                            <asp:Button runat="server" class="btn btn-primary" data-dismiss="modal" aria-hidden="true" Text="Close" UseSubmitBehavior="false" ID="SaveState" OnClick="SaveState_Click"></asp:Button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
