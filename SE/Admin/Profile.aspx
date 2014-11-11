<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SE.Admin.Profile" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <script>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel runat="server" ID="profile" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="page-header"><asp:Label runat="server" ID="profileHeader">User Assignment</asp:Label></h1>
                </div>
            <asp:Panel ID="YourInfo" runat="server">
                    <div class="profileNav">
                        <input type="button" class="catButton btn btn-primary" value="View Assigned Categories" style="width: 200px;" />
                        <input type="button" class="taskButton btn btn-primary" value="View Assigned Tasks" style="width: 200px;" />
                        <input type="button" class="assignedUsers btn btn-primary" value="View Assigned Users" style="width: 200px;" />
                    </div>

                    <div class="dataTables">
                        <div id="catData" style="display:none; min-height:250px;"">
                            <div class="col-xs-10">

                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" DataKeyNames="CategoryName" ID="categories" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false">
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
                        <div id="catUsers" style="display: none;  min-height:250px;""">
                            <div class="col-xs-10">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="catUserLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Assigned Users" DataSourceID="UsersInCategory" ID="AddUserGrid" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" AutoGenerateColumns="false">
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
                        <div id="taskData" style="display: none;  min-height:250px;""">
                            <div class="col-xs-10">
                                <div class="panel panel-primary ">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Tasks Are Created" DataSourceID="TaskSource" DataKeyNames="TaskName" ID="tasks" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="true" OnRowCommand="TaskGrid_RowCommand" AutoGenerateColumns="false">
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
                        <div id="taskUsers" style="display: none;  min-height:250px;""">
                            <div class="col-xs-10">

                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Assigned Users" DataSourceID="addUserDataSource" ID="UsersInTask" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" AutoGenerateColumns="false">
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
                        <div id="userData" style="display: none;  min-height:250px;"">
                            <div class="col-xs-10">
                                <div class="panel panel-primary ">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ID="users" ShowHeaderWhenEmpty="true" EmptyDataText="No Assigned Users" DataSourceID="assignedUsersSource" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" OnRowCommand="users_RowCommand">
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
                        <div id="panel" style="display: none;  min-height:250px;""">
                            <asp:Panel runat="server" ID="categoryData" Visible="false">
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
                            </asp:Panel>

                            <asp:Panel runat="server" ID="userTasks" Visible="false">
                                <div class="col-xs-10">

                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <i class="fa fa-users fa-fw"></i>
                                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="panel-body">
                                            <div style="overflow: auto; max-height: 250px;">
                                                <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories Created" DataSourceID="AllTasksDataSource" ID="AddTasksGridView" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false">
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
                            </asp:Panel>
                        </div>
                    </div>
            </asp:Panel>
            <asp:Panel ID="OtherInfo" runat="server">
                    <div class="profileNav">
                        <input type="button" class="requestCat btn btn-primary" value="Request Assigned Categories" style="width: 225px;"/>
                        <input type="button" class="requestTask btn btn-primary" value="Request Assigned Tasks" style="width: 225px;"/>
                    </div>

                    <div class="dataTables" style="padding-left: 20px;">
                        <div id="requestCat" style="display: none; min-height:250px;"">
                            <div class="col-xs-10">

                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label7" runat="server" Text="Request A Category"></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" DataKeyNames="CategoryName" ID="RequestCatGrid" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" OnRowCommand="RequestCatGrid_RowCommand1" AutoGenerateColumns="false"  OnRowDataBound="RequestCatGrid_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%" />
                                                    <asp:BoundField DataField="CreatedTime" HeaderText="Created Date" ItemStyle-Width="36%" DataFormatString=" {0:d} " HtmlEncode="false" />
                                                    <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                                        <HeaderTemplate>Request Category</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="RequestCat" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="Request" Text="Request"  CommandArgument='<%# Eval("CategoryID") %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <asp:Label ID="error" runat="server"></asp:Label>
                                        <p class="text-warning">NOTE: Requesting a Category transfers all the tasks that coorespond with the Category.</p>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div id="requestTask" style="display: none;min-height:250px;">
                            <div class="col-xs-10">
                                <div class="panel panel-primary ">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label9" runat="server" Text="Request A Task"></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Tasks Are Created" DataSourceID="TaskSource" DataKeyNames="TaskName" ID="GridView3" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="true" OnRowCommand="TaskGrid_RowCommand" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%" />
                                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name" ItemStyle-Width="36%" />
                                                    <asp:TemplateField ItemStyle-Width="28%">
                                                        <HeaderTemplate>Request Tasks</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="RequestTask" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Request" CommandArgument='<%# Eval("TaskID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <p class="text-warning">NOTE: Requesting a task has to be approved by the assigned supervisor</p>
                                    </div>
                                </div>
                            </div>
            </asp:Panel>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content" >
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title" style="color:white;"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body"style="overflow-y:scroll; max-height:350px">
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
    <script type="text/javascript">
            $(document).on("click", ".requestButton", function () {
                console.log(localStorage.getItem("username").toString());
                contact.server.contactFormSubmitted(localStorage.getItem("username").toString());
            });
            var openDiv;
            $(document).on("click", ".catButton", function () {
                $(openDiv).hide('slide', { direction: 'left' }, 1000);
                $(openDiv).promise().done(function () {
                    $('#catData').show('slide', { direction: 'left' }, 1000);
                    openDiv = $('#catData');
                });
            });
            $(document).on("click", ".taskButton", function () {
                $(openDiv).hide('slide', { direction: 'left' }, 1000);
                $(openDiv).promise().done(function () {
                    $('#taskData').show('slide', { direction: 'left' }, 1000);
                    openDiv = $('#taskData');
                });
            });
            $(document).on("click", ".assignedUsers", function () {
                $(openDiv).hide('slide', { direction: 'left' }, 1000);
                $(openDiv).promise().done(function () {
                    $('#userData').show('slide', { direction: 'left' }, 1000);
                    openDiv = $('#userData');
                });
            });
            $(document).on("click", ".requestCat", function () {
                $(openDiv).hide('slide', { direction: 'left' }, 1000);
                $(openDiv).promise().done(function () {
                    $('#requestCat').show('slide', { direction: 'left' }, 1000);
                    openDiv = $('#requestCat');
                });
            });
            $(document).on("click", ".requestTask", function () {
                $(openDiv).hide('slide', { direction: 'left' }, 1000);
                $(openDiv).promise().done(function () {
                    $('#requestTask').show('slide', { direction: 'left' }, 1000);
                    openDiv = $('#requestTask');
                });
            });
        function hideCats() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#catUsers').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#catUsers');
            });
        };
        function showCats() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#catData').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#catData');
            });
        };
        function hideTasks() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#taskUsers').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#taskUsers');
            });
        };
        function showTasks() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#taskData').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#taskData');
            });
        };
        function showUsers() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#userData').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#userData');
            });
        };
        function hideUsers() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#panel').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#panel');
            });
        };
        function requestCat() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#requestCat').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#requestCat');
            });
        };
        function requestTask() {
            $(openDiv).hide('slide', { direction: 'left' }, 1000);
            $(openDiv).promise().done(function () {
                $('#requestTask').show('slide', { direction: 'left' }, 1000);
                openDiv = $('#requestTask');
            });
        };
    </script>
</asp:Content>
