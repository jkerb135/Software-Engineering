<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SE.Dashboard" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <!-- /.row -->
    <asp:Label ID="label" runat="server" Text=""></asp:Label>
    <asp:Label ID="label2" runat="server" Text=""></asp:Label>
    <asp:MultiView ID="DashboardView" ActiveViewIndex="0" runat="server">
        <asp:View ID="Manager" runat="server">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="page-header">Manager Dashboard</h1>
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
