<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="SE.Admin.Requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1 class="page-header">
                <asp:Label runat="server" ID="profileHeader">HURP</asp:Label></h1>
        </div>
    </div>
    <div class="panel panel-primary">

        <div class="panel-heading">
            <i class="fa fa-users fa-fw"></i>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
        <asp:UpdatePanel runat="server" ID="requestUpdatePanel" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body">
                    <div style="overflow: auto; max-height: 250px;">

                        <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Requests" DataSourceID="RequestSource" DataKeyNames="CategoryName" ID="requests" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" AutoGenerateColumns="True" OnRowCommand="users_RowCommand">
                            <Columns>

                                <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="AcceptRequest" CssClass="btn btn-success" runat="server" CausesValidation="false" CommandName="AcceptRequest" Text="Accept" CommandArgument='<%# Eval("CategoryID") + ";" + Eval("RequestingUser") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="RejectRequest" CssClass="btn btn-danger" runat="server" CausesValidation="false" CommandName="RejectRequest" Text="Reject" CommandArgument='<%# Eval("CategoryID") + ";" + Eval("RequestingUser") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="RequestSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="panel-footer">
                    <p class="text-warning"></p>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
