<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SE.Admin.Profile" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <script>
        var urlParams;
        (window.onpopstate = function () {
            var match,
                pl = /\+/g,  // Regex for replacing addition symbol with a space
                search = /([^&=]+)=?([^&]*)/g,
                decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
                query = window.location.search.substring(1);

            urlParams = {};
            while ((match = search.exec(query)))
                urlParams[decode(match[1])] = decode(match[2]);
        })();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel runat="server" ID="profile" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="HelpBtn" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-6">
                    <h1 class="page-header">
                        <asp:Label runat="server" ID="profileHeader">User Assignment</asp:Label></h1>
                </div>
                <div class="col-xs-6" style="margin-top: 20px;">
                    <h1>
                        <asp:LinkButton class="fa fa-question-circle pull-right" runat="server" ID="HelpBtn" OnClick="HelpBtn_OnClick" Style="text-decoration: none;"></asp:LinkButton></h1>
                </div>
            </div>
            <div class="row">
                <asp:Panel ID="YourInfo" runat="server">
                    <div role="tabpanel">
                        <div class="col-lg-2">
                            <ul class="nav nav-pills nav-stacked" id="nav_tabs">
                                <li role="presentation" class="active"><a href="#cats">View Categories</a></li>
                                <li role="presentation"><a href="#taskpane">View Tasks</a></li>
                                <li role="presentation"><a href="#userpane">View Users</a></li>
                            </ul>
                        </div>
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane fade in active" id="cats">
                                <div id="catData">
                                    <div class="col-xs-10">

                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <i class="fa fa-users fa-fw"></i>
                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="panel-body">
                                                <div style="overflow: auto; max-height: 250px;">
                                                    <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" DataKeyNames="CategoryName" ID="categories" CssClass="table table-hover table-striped" GridLines="None" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%" />
                                                            <asp:BoundField DataField="CreatedTime" HeaderText="Created Date" ItemStyle-Width="36%" DataFormatString=" {0:d} " HtmlEncode="false" />
                                                            <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                                                <HeaderTemplate>Update Users In Category</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="AddUsers" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Update" CommandArgument='<%# Eval("CategoryID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="CategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                            <div class="panel-footer">
                                                <p class="text-warning">NOTE: Adding a User to a category assigns them all tasks under that category.</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="catUsers" style="display: none; min-height: 450px;">
                                    <div class="col-xs-10">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <i class="fa fa-users fa-fw"></i>
                                                <asp:Label ID="catUserLabel" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="panel-body">
                                                <div style="overflow: auto; max-height: 250px;">
                                                    <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Assigned Users" DataSourceID="UsersInCategory" ID="AddUserGrid" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="catUsersChk" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="UserName" HeaderText="Username" ItemStyle-Width="36%" />
                                                            <asp:BoundField DataField="IsApproved" HeaderText="User Status" ItemStyle-Width="36%" />
                                                            <asp:BoundField DataField="LastActivityDate" HeaderText="Latest Activity" ItemStyle-Width="36%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="UsersInCategory" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="AddUsersToCat" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="AddUsersToCat_Click" />
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="taskpane">
                                <div id="taskData">
                                    <div class="col-xs-10">
                                        <div class="panel panel-primary ">
                                            <div class="panel-heading">
                                                <i class="fa fa-users fa-fw"></i>
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="panel-body">
                                                <div style="overflow: auto; max-height: 250px;">
                                                    <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Tasks Are Created" DataSourceID="TaskSource" DataKeyNames="TaskName" ID="tasks" CssClass="table table-hover table-striped" GridLines="None" runat="server" OnRowCommand="TaskGrid_RowCommand" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%" />
                                                            <asp:BoundField DataField="TaskName" HeaderText="Task Name" ItemStyle-Width="36%" />
                                                            <asp:TemplateField ItemStyle-Width="28%">
                                                                <HeaderTemplate>Update Users In Tasks</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="AddUsersToTask" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Update" CommandArgument='<%# Eval("TaskID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="TaskSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="taskUsers" style="display: none; min-height: 450px;">
                                    <div class="col-xs-10">

                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <i class="fa fa-users fa-fw"></i>
                                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="panel-body">
                                                <div style="overflow: auto; max-height: 250px;">
                                                    <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Assigned Users" DataSourceID="addUserDataSource" ID="UsersInTask" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="UsersInTaskChk" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="UserName" HeaderText="Username" ItemStyle-Width="36%" />
                                                            <asp:BoundField DataField="IsApproved" HeaderText="User Status" ItemStyle-Width="36%" />
                                                            <asp:BoundField DataField="LastActivityDate" HeaderText="Latest Activity" ItemStyle-Width="36%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="addUserDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="AssignToTask" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="AssUsersToTask_Click" />
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="userpane">
                                <div id="userData">
                                    <div class="col-xs-10">
                                        <div class="panel panel-primary ">
                                            <div class="panel-heading">
                                                <i class="fa fa-users fa-fw"></i>
                                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="panel-body">
                                                <div style="overflow: auto; max-height: 250px;">
                                                    <asp:GridView ID="users" ShowHeaderWhenEmpty="true" EmptyDataText="No Assigned Users" DataSourceID="assignedUsersSource" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowSorting="true" AutoGenerateColumns="false" OnRowCommand="users_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="AssignedUser" HeaderText="User" />
                                                            <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="center">
                                                                <HeaderTemplate>Categories</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="AddUserToCategories" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddCategories" Text="Update" CommandArgument='<%# Eval("AssignedUser") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="center">
                                                                <HeaderTemplate>Tasks</HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="AddUsersToTasks" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddTasks" Text="Update" CommandArgument='<%# Eval("AssignedUser") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="assignedUsersSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                    <div id="categoryData" style="display:none">
                                        <div class="col-xs-10">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <i class="fa fa-users fa-fw"></i>
                                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <div style="overflow: auto; max-height: 250px;">
                                                        <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories Created" DataSourceID="AllCategoriesSource" ID="AllCategoriesGridView" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="AllCategoriesChk" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="AllCategoriesSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="AddUserToCategoriesBtn" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="AddCategoriesToUserBtn_Click" />
                                        </div>
                                    </div>

                                    <div id="tasksData" style="display:none">
                                        <div class="col-xs-10">

                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <i class="fa fa-users fa-fw"></i>
                                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="panel-body">
                                                    <div style="overflow: auto; max-height: 250px;">
                                                        <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Tasks Created" DataSourceID="AllTasksDataSource" ID="AddTasksGridView" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="AddTaskChk" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="42%" />
                                                                <asp:BoundField DataField="TaskName" HeaderText="Task Name" ItemStyle-Width="42%" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="AllTasksDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="Button1_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </asp:Panel>
                <asp:Panel ID="OtherInfo" runat="server">
                    <div id="requestCat" style="min-height: 450px;">
                        <div class="col-xs-12">

                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <i class="fa fa-users fa-fw"></i>
                                    <asp:Label ID="Label7" runat="server" Text="Request A Category"></asp:Label>
                                </div>
                                <div class="panel-body">
                                    <div style="overflow: auto; max-height: 250px;">
                                        <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories are assigned" DataKeyNames="CategoryName" ID="RequestCatGrid" CssClass="table table-hover table-striped" GridLines="None" runat="server" OnRowCommand="RequestCatGrid_RowCommand1" AutoGenerateColumns="false" OnRowDataBound="RequestCatGrid_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%" />
                                                <asp:BoundField DataField="CreatedTime" HeaderText="Created Time" ItemStyle-Width="36%" />
                                                <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                                    <HeaderTemplate>Request Category</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Button ID="RequestCat" CssClass="btn btn-primary form-control requestCatBtn" runat="server" CausesValidation="false" CommandName="Request" Text="Request" CommandArgument='<%# Eval("CategoryID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <asp:Label ID="error" runat="server"></asp:Label>
                                    <p class="text-warning">NOTE: Requesting a Category transfers all the tasks that coorespond with the Category.</p>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
            </div>


            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h4 class="modal-title" style="color: white;">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body" style="overflow-y: scroll; max-height: 350px">
                                    <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Ok</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).on("click", "#nav_tabs a", function (e) {
            e.preventDefault();
            var tab = document.getElementById(this.href.replace(document.URL + "#", ""));
            $(this).tab('show');
            if (tab.id !== "userpane") {
                var main_panel = tab.firstElementChild;
                var secondary_panel = tab.lastElementChild;
                console.log(secondary_panel);
                main_panel.style.display = "";
                secondary_panel.style.display = "none";
            } else {
                tab.childNodes[1].style.display = "";
                tab.childNodes[3].style.display = "none";
                tab.childNodes[5].style.display = "none";
            }

        });
        $(document).on("click", "input[value='Update']", function (e) {
            e.preventDefault();
            var parent = $(this).parents("div.active")[0];
            var main_panel = parent.firstElementChild;
            var secondary_panel = parent.lastElementChild;

            if (parent.id !== "userpane") {
                $(main_panel).fadeOut("slow", function() {
                    $(secondary_panel).fadeIn("slow");
                });
            } else {
                var panel = $(this).parents("div.active")[0];
                var secondary;
                console.log(this.id.indexOf("AddUserToCategories") >= 0);
                if (this.id.indexOf("AddUserToCategories") >= 0) {
                    secondary = panel.childNodes[3];
                } else {
                    secondary = panel.lastElementChild;
                }
                $(panel.firstElementChild).fadeOut("slow",function() {
                    $(secondary).fadeIn("slow");
                });
            }
        });
    </script>
</asp:Content>
