<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SE.Dashboard" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Dashboard</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
     <div class="row">
         <div class="col-lg-4">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <i class="fa fa-users fa-fw"></i> Active User List
            </div>
            <div class="panel-body">
                <asp:DataGrid ID="activeUserList" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False">
                </asp:DataGrid>
            </div>
            </div>
        </div>
        <div class="col-lg-4">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <i class="fa fa-users fa-fw"></i> Recently Assigned Users
            </div>
            <div class="panel-body">
                <asp:DataGrid ID="newMembers" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False">
                </asp:DataGrid>
            </div>
            </div>
        </div>
    </div>
</asp:Content>