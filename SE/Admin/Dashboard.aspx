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
            </div>
        </asp:View>

        <asp:View ID="Supervisor" runat="server">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="page-header">Supervisor Dashboard</h1>
                </div>
                <!-- /.col-xs-12 -->
            </div>
            <div class="row">
                <div class="col-xs-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>Active User List
                        </div>
                        <div class="panel-body">
                            <asp:DataGrid ID="activeUserList" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False">
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>Recently Assigned Users
                        </div>
                        <div class="panel-body">
                            <asp:DataGrid ID="newMembers" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False">
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>Active Supervisors
                        </div>
                        <div class="panel-body">
                            <asp:DataGrid ID="signededIn" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False" AutoGenerateColumns="false" ShowHeader="false" GridLines="None">
                                <Columns>
                                   <asp:BoundColumn DataField="UserName" HeaderText="Supervisor" ItemStyle-Width="10%"></asp:BoundColumn>
                                   <asp:BoundColumn DataField="Online" HeaderText="Online" ItemStyle-Width="3%" ItemStyle-CssClass="center"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
