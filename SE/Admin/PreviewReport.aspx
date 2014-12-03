<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewReport.aspx.cs" Inherits="SE.Admin.PreviewReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview Report</title>
    <link href="<%=ResolveUrl("~/StyleSheets/bootstrap.css") %>" rel="stylesheet">
</head>
<body>
    <form id="TheForm" runat="server">
        <asp:Panel ID="ReportOverviewPanel" runat="server">
            <h2>Overview</h2>
            <div class="table-responsive">
                <asp:GridView ID="ReportOverview" CssClass="report table table-bordered" runat="server">
                </asp:GridView>
            </div>
        </asp:Panel>
        <asp:Panel ID="ReportDetailsPanel" CssClass="table-responsive" runat="server"></asp:Panel>
        <asp:SqlDataSource ID="UserDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ipawsTeamBConnectionString %>" 
            SelectCommand="SELECT [AssignedUser] FROM [MemberAssignments]" 
            DataSourceMode="DataReader"></asp:SqlDataSource>
    </form>
</body>
</html>
