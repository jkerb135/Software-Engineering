<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SE.Admin.Profile" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="../Scripts/Custom/profile.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-xs-12">
            <h2>Profile</h2>
        </div>
    </div>
    <hr />
 
    <div class="row">
        <div class="profileNav">
            <input type="button" id="catButton" class="btn btn-primary" value="View Assigned Categories" style="width: 200px;" />
            <input type="button" id="taskButton" class="btn btn-primary" value="View Assigned Tasks" style="width: 200px;" />
            <input type="button" id="assignedUsers" class="btn btn-primary" value="View Assigned Users" style="width: 200px;" />
        </div>
        <asp:Panel ID="YourInfo" runat="server">
        <div class="dataTables">
            <div id="catData" style="display: none">
                <div class="col-xs-10">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div style=" overflow:auto; max-height:250px;">
                            <asp:GridView EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" DataKeyNames="CategoryName" ID="categories" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%"/>
                                    <asp:BoundField DataField="CreatedTime" HeaderText="Created Date" ItemStyle-Width="36%" DataFormatString=" {0:d} " HtmlEncode="false" />
                                    <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                        <HeaderTemplate>Update Users In Category</HeaderTemplate>
                                        <ItemTemplate><asp:Button ID="AddUsers" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Update" CommandArgument='<%# Eval("CategoryID") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="CategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="catUsers" style="display: none;">
                <div class="col-xs-10">
                    
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="catUserLabel" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div style=" overflow:auto; max-height:250px;">
                            <asp:GridView DataSourceID="UsersInCategory" ID="AddUserGrid" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate><asp:CheckBox ID="CheckBox1" runat="server" /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="UsersInCategory" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                                </div>
                        </div>
                    </div>
                    <asp:Button ID="AddUsersToCat" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="AddUsersToCat_Click"/>
                </div>
            </div>
            <div id="taskData" style="display: none;">
                <div class="col-xs-10">
                    <div class="panel panel-primary ">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div style=" overflow:auto; max-height:250px;">
                            <asp:GridView EmptyDataText="No Tasks Are Created" DataSourceID="TaskSource" DataKeyNames="TaskName" ID="tasks" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="true" OnRowCommand="TaskGrid_RowCommand"  AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%"/>
                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name" ItemStyle-Width="36%"/>
                                    <asp:TemplateField ItemStyle-Width="28%">
                                        <HeaderTemplate>Update Users In Tasks</HeaderTemplate>
                                        <ItemTemplate><asp:Button ID="AddUsersToTask" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Update" CommandArgument='<%# Eval("TaskID") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="TaskSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="taskUsers" style="display: none;">
                <div class="col-xs-10">
                    
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div style=" overflow:auto; max-height:250px;">
                            <asp:GridView DataSourceID="addUserDataSource" ID="UsersInTask" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate><asp:CheckBox ID="usersTaskChk" runat="server" /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="addUserDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                                </div>
                        </div>
                    </div>
                    <asp:Button ID="AssignToTask" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="AssUsersToTask_Click"/>
                </div>
            </div>
            <div id="userData" style="display: none;">
                <div class="col-xs-10">
                    <div class="panel panel-primary ">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div style=" overflow:auto; max-height:250px;">
                            <asp:GridView ID="users" DataSourceID="assignedUsersSource" CssClass="table table-hover table-striped" GridLines="None"  runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" OnRowCommand="users_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="AssignedUser" HeaderText="User"/>
                                <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="center">
                                    <HeaderTemplate>Categories</HeaderTemplate>
                                        <ItemTemplate><asp:Button ID="AddUserToCategories" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddCategories" Text="Update" CommandArgument='<%# Eval("AssignedUser") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100px" ItemStyle-CssClass="center">
                                    <HeaderTemplate>Tasks</HeaderTemplate>
                                        <ItemTemplate><asp:Button ID="AddUsersToTasks" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddTasks" Text="Update" CommandArgument='<%# Eval("AssignedUser") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="assignedUsersSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="panel" style="display: none;" >
            <asp:Panel runat="server" ID="categoryData" Visible="false">
                <div class="col-xs-10">
                    
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div style=" overflow:auto; max-height:250px;">
                            <asp:GridView DataSourceID="AllCategoriesSource" ID="AllCategoriesGridView" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false" >
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate><asp:CheckBox ID="usersTaskChk" runat="server" /></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name"/>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="AllCategoriesSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                                </div>
                        </div>
                    </div>
                    <asp:Button ID="AddUserToCategoriesBtn" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="AddCategoriesToUserBtn_Click"/>
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
                            <div style=" overflow:auto; height:250px;">
                            <asp:GridView DataSourceID="AllTasksDataSource" ID="AddTasksGridView" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="2%">
                                        <ItemTemplate><asp:CheckBox ID="usersTaskChk" runat="server" /></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="42%"/>
                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name"  ItemStyle-Width="42%"/>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="AllTasksDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                                </div>
                        </div>
                    </div>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Add Selected Users" OnClick="Button1_Click"/>
                </div>
            </asp:Panel>
                    </div>
            </div>
    </asp:Panel>
    </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    <script type="text/javascript">
        $(function () {
            $(document).on("click", "#catButton", function () {
                $("#panel").hide('slow');
                $("#catUsers").hide("slow");
                $("#catData").show("slow");
                $("#userData").hide("slow");
                $("#taskData").hide("slow");
                $("#taskUsers").hide('slow');
                
            });
            $(document).on("click", "#taskButton", function () {
                $("#panel").hide('slow');
                $("#taskUsers").hide('slow');
                $("#catUsers").hide("slow");
                $("#catData").hide("slow");
                $("#userData").hide("slow");
                $("#taskData").show("slow");
            });
            $(document).on("click", "#assignedUsers", function () {
                $("#panel").hide('slow');
                $("#taskUsers").hide('slow');
                $("#catUsers").hide("slow");
                $("#catData").hide("slow");
                $("#taskData").hide("slow");
                $("#userData").show("slow");
            });
        });
        function hideCats() {
            $("#catData").hide("slow");
            $("#catUsers").show('slow');

        };
        function showCats() {
            $("#catData").show("slow");
            $("#catUsers").hide('slow');

        };
        function hideTasks() {
            $("#taskData").hide("slow");
            $("#taskUsers").show('slow');

        };
        function showTasks() {
            $("#taskData").show("slow");
            $("#taskUsers").hide('slow');

        };
        function hideUsers() {
            $("#panel").show("slow");
            $("#userData").hide('slow');
        };
        function showUsers() {
            $("#userData").show("slow");
            $("#panel").hide('slow');

        };
    </script>
</asp:Content>
