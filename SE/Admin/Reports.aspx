<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Reports.aspx.cs" Inherits="SE.Reports" %>
<%@ Register TagPrefix="se" TagName="ReportPreview" Src="~/UserControls/ReportPreview.ascx" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel ID="ReportUpdatePanel" runat="server">
        <ContentTemplate>
            <h1 ID="PageHeader" class="page-header" runat="server">Reports</h1>
            <asp:Panel ID="ReportButtons" runat="server">
                <div class="success-messages">
                    <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                </div>
                <div class="error-messages">
                    <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
                    <asp:ValidationSummary ID="EmailSummary" ValidationGroup="EmailReportGroup" runat="server" />
                </div>
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
                    <asp:LinkButton ID="PrintReportButton" CssClass="print-report" runat="server">
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
            <asp:Panel ID="EmailReportForm" runat="server">
                <div class="row">
                    <div class="col-xs-5">
                        <div class="form-group">
                            <asp:Label ID="FromEmailLabel" runat="server" Text="Sender Email Address:"></asp:Label>
                            <asp:RequiredFieldValidator ID="FromEmailRequired" runat="server" ControlToValidate="FromEmail" 
                                ErrorMessage="Sender email is required." ValidationGroup="EmailReportGroup">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="FromEmailValid" runat="server" 
                                ControlToValidate="FromEmail" ErrorMessage="Sender email address must be in a valid format" Display="None" 
                                ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" ValidationGroup="EmailReportGroup">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="FromEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="ToEmailLabel" runat="server" Text="Receiver Email Address:"></asp:Label>
                            <asp:RequiredFieldValidator ID="ToEmailRequired" runat="server" ControlToValidate="ToEmail" 
                                ErrorMessage="Receiver email is required." ValidationGroup="EmailReportGroup">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="ToEmail" ErrorMessage="Receiver email address must be in a valid format" Display="None" 
                                ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" ValidationGroup="EmailReportGroup">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="ToEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="EmailFormButton" CssClass="btn btn-default right block" ValidationGroup="EmailReportGroup" CausesValidation="true"
                                runat="server" Text="Submit" OnClick="EmailFormButton_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="ReportControl" CssClass="hidden" runat="server">
                <se:ReportPreview runat="server" ID="ReportPreviewControl"></se:ReportPreview>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
