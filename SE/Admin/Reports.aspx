<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Reports.aspx.cs" Inherits="SE.Reports" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel ID="ReportUpdatePanel" runat="server">
        <ContentTemplate>
            <h1 class="page-header">Reports</h1>
            <asp:Panel ID="ReportOverviewPanel" runat="server">
                <h2>Overview</h2>
                <div class="table-responsive">
                    <asp:GridView ID="ReportOverview" CssClass="report table table-bordered" runat="server" 
                        OnRowDataBound = "OverviewRDB" OnSelectedIndexChanged = "OverviewSIC">
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Panel ID="ReportDetailsPanel" runat="server">
                <h2 runat="server" id="ReportDetailsHeading"></h2>
                <div class="table-responsive">
                    <asp:GridView ID="ReportDetails" CssClass="report-details table table-bordered" runat="server"></asp:GridView>
                    <asp:Label ID="ReportDetailsMessage" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            <asp:Panel ID="ReportButtons" runat="server">
                <div class="row">
                    <asp:LinkButton ID="GenerateReportButton" runat="server" OnClick="GenerateReportButton_Click">
                        <div class="col-xs-3 generateReport btn">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <i class="fa fa-bar-chart-o fa-5x"></i>
                                        </div>
                                        <div class="col-xs-9 text-right">
                                            <div class="huge"></div>
                                            <div>Generate Report</div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div class="panel-footer btn btn-default" style="width: 100%">
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="EmailReportButton" runat="server" OnClick="EmailReportButton_Click">
                        <div class="col-xs-3 emailReport btn">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <i class="fa fa-paper-plane fa-5x"></i>
                                        </div>
                                        <div class="col-xs-9 text-right">
                                            <div class="huge"></div>
                                            <div>Email Report</div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div class="panel-footer btn btn-default" style="width: 100%">
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="PrintReportButton" runat="server" OnClick="PrintReportButton_Click">
                        <div class="col-xs-3 printReport btn">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <i class="fa fa-print fa-5x"></i>
                                        </div>
                                        <div class="col-xs-9 text-right">
                                            <div class="huge"></div>
                                            <div>Print Report</div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div class="panel-footer btn btn-default" style="width: 100%">
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
