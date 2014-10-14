<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SE.Admin.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2>Profile</h2>
        </div>
    </div>
    <hr />
    <div class="col-lg-12">
        <div class="row">
            <div class="col-lg-4 left">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <i class="fa fa-users fa-fw"></i>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="panel-body">
                        <asp:DataGrid ID="categories" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False">
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 center" style="text-align:left">
                <div class="panel panel-primary ">
                    <div class="panel-heading">
                        <i class="fa fa-users fa-fw"></i>
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="panel-body">
                        <asp:DataGrid ID="tasks" CssClass="table table-bordered table-striped" runat="server" AllowPaging="False">
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
