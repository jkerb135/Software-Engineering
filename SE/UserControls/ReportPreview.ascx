<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportPreview.ascx.cs" Inherits="SE.UserControls.ReportPreview" %>

<asp:Panel ID="EmailContents" runat="server">
    <h2>Overview</h2>
    <div class="table-responsive">
        <asp:GridView ID="EmailGrid" CssClass="report table table-bordered" runat="server">
        </asp:GridView>
    </div>
</asp:Panel>
<asp:SqlDataSource ID="UserDataSource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ipawsTeamBConnectionString %>" 
    SelectCommand="SELECT [AssignedUser] FROM [MemberAssignments]" 
    DataSourceMode="DataReader"></asp:SqlDataSource>