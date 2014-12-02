<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="SE.Admin.Requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <script>
        $(document).on('click', '#categoryRequests', function() {
            $('#catBody').slideToggle();
            $('#catDir').toggleClass("fa-arrow-up fa-arrow-down");
        });
        $(document).on('click', '#taskRequests', function() {
            $('#taskBody').slideToggle();
            $('#taskDir').toggleClass("fa-arrow-up fa-arrow-down");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h1 class="page-header">
                <asp:Label runat="server" ID="profileHeader">Requests</asp:Label></h1>
        </div>
    </div>
    <div class="panel panel-primary" id="categoryRequests">

        <div class="panel-heading" style="cursor: pointer">
            <i class="fa fa-users fa-fw"></i>
            <asp:Label ID="Label1" runat="server" Text="Your Category Requests"></asp:Label>
            <i class="fa fa-arrow-down right" id="catDir"></i>

        </div>
        <asp:UpdatePanel runat="server" ID="requestUpdatePanel" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel-body" id="catBody" >
                    <div style="max-height: 250px; overflow: auto;">

                        <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Pending Category Requests from other Supervisors" DataSourceID="RequestSource" DataKeyNames="CategoryName" ID="requests" CssClass="table table-hover table-striped" GridLines="None" runat="server" AutoGenerateColumns="false" OnRowCommand="users_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="CategoryID" HeaderText="ID" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name"/>
                                <asp:BoundField DataField="RequestingUser" HeaderText="Requesting User"/>
                                <asp:BoundField DataField="Date" HeaderText="Date"></asp:BoundField>
                                <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="AcceptRequest" CssClass="btn btn-success form-control" runat="server" CausesValidation="false" CommandName="AcceptRequest" Text="Accept" CommandArgument='<%# Eval("CategoryID") + ";" + Eval("RequestingUser") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center">
                                    <ItemTemplate>
                                        <asp:Button ID="RejectRequest" CssClass="btn btn-danger form-control" runat="server" CausesValidation="false" CommandName="RejectRequest" Text="Reject" CommandArgument='<%# Eval("CategoryID") + ";" + Eval("RequestingUser") %>' />
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