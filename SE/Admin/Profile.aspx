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
                                <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <asp:GridView EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" DataKeyNames="CategoryName" ID="categories" CssClass="table table-bordered table-striped" runat="server" AllowPaging="True" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate><asp:Button ID="AddUsers" CssClass="btn btn-primary" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Add Users" CommandArgument='<%# Eval("CategoryName") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="CategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
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
                            <asp:GridView DataSourceID="UsersInCategory" ID="AddUserGrid" CssClass="table table-bordered table-striped" runat="server" AllowPaging="True">
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
                            <asp:GridView EmptyDataText="No Tasks Are Created" DataSourceID="TaskSource" DataKeyNames="TaskName" ID="tasks" CssClass="table table-bordered table-striped" runat="server" AllowPaging="true" OnRowCommand="TaskGrid_RowCommand">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate><asp:Button ID="AddUsersToTask" CssClass="btn btn-primary" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Add Users" CommandArgument='<%# Eval("TaskName") %>' /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="TaskSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
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
                            <asp:GridView DataSourceID="addUserDataSource" ID="UsersInTask" CssClass="table table-bordered table-striped" runat="server" AllowPaging="True">
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
                            <asp:GridView ID="users" CssClass="table table-bordered table-striped" runat="server" AllowPaging="true" AllowSorting="true">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    <script type="text/javascript">
        $(function () {
            $(document).on("click", "#catButton", function () {
                $("#catUsers").hide("slow");
                $("#catData").show("slow");
                $("#userData").hide("slow");
                $("#taskData").hide("slow");
                $("#taskUsers").hide('slow');
                
            });
            $(document).on("click", "#taskButton", function () {
                $("#taskUsers").hide('slow');
                $("#catUsers").hide("slow");
                $("#catData").hide("slow");
                $("#userData").hide("slow");
                $("#taskData").show("slow");
            });
            $(document).on("click", "#assignedUsers", function () {
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
    </script>
</asp:Content>
