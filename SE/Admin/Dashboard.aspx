<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SE.Admin.Dashboard" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
    <script>
        function blink() {
            $('#userManagement').fadeTo(100, 0.1).fadeTo(200, 1.0);
        }
        function newUserToast() {
            toastr.options.closeButton = true;
            toastr.success('New User Created');
        }
        function successToast() {
            toastr.options.closeButton = true;
            toastr.success('Users Information was successfully updated');
        }
        function errorToast(error) {
            toastr.options.closeButton = true;
            toastr.error(error);
        }
        function updateUsers() {
            $.ajax({
                type: "Get",
                url: "/api/users/getloggedon",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                },
                failure: function (response) {
                    console.log(response)
                },
                error: function (response) {
                    console.log(response);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="HelpBtn" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <!-- /.row -->
            <asp:Label ID="label" runat="server" Text=""></asp:Label>
            <asp:Label ID="label2" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="managerState" runat="server" Value="none" />
            <asp:MultiView ID="DashboardView" ActiveViewIndex="0" runat="server">
                <asp:View ID="Manager" runat="server">
                    <script src="../Scripts/Manager.js"></script>
                    <div class="row page-header"></div>
                    <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Account Manager</div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                    <ContentTemplate>
                                        <div class="form-inline" style="margin-bottom: 20px">
                                            <div class="form-group">
                                                Search By Username:
                                                <asp:TextBox runat="server" ID="userSearch" OnTextChanged="userSearch_OnTextChanged" CssClass="form-control" stlye="width:30%"></asp:TextBox><asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-primary" /><asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_OnClick" />
                                            </div>
                                        </div>
                                        <asp:GridView OnRowDeleting="GridView1_OnRowDeleting" OnRowDataBound="GridView1_OnRowDataBound" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowSorting="true" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="None" ShowFooter="True" CssClass="table table-striped" ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="6">
                                            <SortedAscendingHeaderStyle CssClass="fa fa-arrow-up"></SortedAscendingHeaderStyle>
                                            <SortedDescendingHeaderStyle CssClass="fa fa-arrow-down"></SortedDescendingHeaderStyle>
                                            <PagerTemplate>
                                                <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrev" CssClass="btn btn-primary" />
                                                <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext" CssClass="btn btn-primary" />
                                            </PagerTemplate>
                                            <PagerSettings Position="Bottom"></PagerSettings>
                                            <Columns>
                                                <asp:BoundField HeaderText="User ID" DataField="UserId" InsertVisible="False" SortExpression="UserId" Visible="false" />
                                                <asp:TemplateField HeaderText="Username" SortExpression="Username">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="UserLabel" Text='<%# Eval("UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="UserLabel" Text='<%# Eval("UserName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="UsernameTxt" CssClass="form-control" runat="server" Placeholder="Username"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="8.2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update Password">
                                                    <EditItemTemplate>
                                                        <asp:TextBox CssClass="form-control" runat="server" ID="PasswordTxt" Text='<%# Eval("Password") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="PasswordTxt" CssClass="form-control" runat="server" Placeholder="Password"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="8.2%" />
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
                                                <asp:BoundField HeaderText="Last Active Date" DataField="LastLoginDate" InsertVisible="False" SortExpression="LastLoginDate" ReadOnly="true" ItemStyle-Width="8.2%" />
                                                <asp:TemplateField HeaderText="Role Name" SortExpression="RoleName">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="RoleLbl" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="RoleLbl" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="RoleDrp" CssClass="form-control" OnSelectedIndexChanged="RoleDrp_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="Supervisor">Supervisor</asp:ListItem>
                                                            <asp:ListItem Value="User">User</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="10.2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assigned Supervisor" SortExpression="AssignedSupervisor">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="AssignLbl" Text='<%# Eval("AssignedSupervisor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" Visible="false" DataTextField="UserName" ID="AssignDrp" CssClass="form-control">
                                                            <asp:ListItem Value="Unassigned">Unassigned</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" Visible="false" DataTextField="UserName" ID="AssignDrp" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="8.2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active" ItemStyle-Width="5.2%" SortExpression="IsApproved">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LockOutLbl" Text='<%# Eval("IsApproved") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList runat="server" ID="LockOutDrp" CssClass="form-control" SelectedValue='<%# Eval("IsApproved") %>'>
                                                            <asp:ListItem Value="True">True</asp:ListItem>
                                                            <asp:ListItem Value="False">False</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="LockOutDrp" CssClass="form-control">
                                                            <asp:ListItem Value="True">True</asp:ListItem>
                                                            <asp:ListItem Value="False">False</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="10.2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="EditBtn" runat="server" CssClass="btn btn-warning" CommandName="Edit" Text="Edit" CausesValidation="False"></asp:LinkButton>
                                                        <!--<asp:LinkButton ID="DeleteBtn" runat="server" CssClass="btn btn-danger" CommandName="Delete" Text="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure to delete this record?');"></asp:LinkButton>-->
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="UpdateBtn" runat="server" CssClass="btn btn-primary" CommandName="Update" Text="Update" CausesValidation="False"></asp:LinkButton>
                                                        <asp:LinkButton ID="CancelBtn" runat="server" CssClass="btn btn-danger" CommandName="Cancel" Text="Cancel" CausesValidation="False"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="NewInsert" runat="server" Text="Add" CommandName="FooterInsert" CssClass="btn btn-success" CausesValidation="False" OnClick="NewInsert_Click"></asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle Width="12.2%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panel-footer text-danger">**All fields are required to create a new User**</div>
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ipawsTeamBConnectionString %>" SelectCommand="SELECT aspnet_Users.UserName, aspnet_Users.UserId FROM aspnet_Users INNER JOIN aspnet_UsersInRoles ON aspnet_Users.UserId = aspnet_UsersInRoles.UserId INNER JOIN aspnet_Roles ON aspnet_UsersInRoles.RoleId = aspnet_Roles.RoleId WHERE (aspnet_Roles.RoleName = 'Supervisor')"></asp:SqlDataSource>

                    </div>
                    <!-- /.col-xs-12 -->
                </asp:View>

                <asp:View ID="Supervisor" runat="server">

                    <div class="row">
                        <div class="col-xs-6">
                            <h1 class="page-header">Supervisor Dashboard</h1>
                        </div>
                        <div class="col-xs-6" style="margin-top: 20px;">
                            <h1>
                                <asp:LinkButton class="fa fa-question-circle pull-right" runat="server" ID="HelpBtn" OnClick="HelpBtn_OnClick" Style="text-decoration: none;"></asp:LinkButton></h1>
                        </div>
                    </div>
                    <div class="row">
                        <!-- /.col-xs-12 -->
                        <div class="col-xs-4">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <i class="fa fa-users fa-fw"></i>Active User List
                       
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel runat="server" ID="UpdaetPanel1">
                                        <ContentTemplate>
                                            <asp:GridView ID="activeUserList" EmptyDataText="No Users Logged In" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField="UserName" HeaderText="Supervisor" ItemStyle-Width="10%"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ipawsTeamBConnectionString %>" SelectCommand="SELECT [AssignedUser] FROM [MemberAssignments] WHERE (([AssignedSupervisor] = @AssignedSupervisor) AND ([IsUserLoggedIn] = @IsUserLoggedIn))">
        <SelectParameters>
            <asp:Parameter Name="AssignedSupervisor" Type="String" />
            <asp:Parameter DefaultValue="true" Name="IsUserLoggedIn" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
