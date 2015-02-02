<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupervisorRequests.aspx.cs" Inherits="SE.Admin.Requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <asp:UpdatePanel runat="server" ID="profile" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="HelpBtn" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-6">
                    <h1 class="page-header">
                        <asp:Label runat="server" ID="profileHeader">Requests</asp:Label></h1>
                </div>
                <div class="col-xs-6" style="margin-top: 20px;">
                    <h1>
                        <asp:LinkButton class="fa fa-question-circle pull-right" runat="server" ID="HelpBtn" OnClick="HelpBtn_OnClick" Style="text-decoration: none;"></asp:LinkButton></h1>
                </div>
            </div>
            <div class="row">
                <div class="panel panel-primary" id="categoryRequests">

                    <div class="panel-heading" style="cursor: pointer">
                        <i class="fa fa-users fa-fw"></i>
                        <asp:Label ID="Label1" runat="server" Text="Your Category Requests"></asp:Label>

                    </div>
                    <asp:UpdatePanel runat="server" ID="requestUpdatePanel" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="panel-body" id="catBody">
                                <div>
                                    <asp:GridView OnPageIndexChanging="requests_OnPageIndexChanging" AllowPaging="True" PageSize="6"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Pending Category Requests from other Supervisors"
                                        DataKeyNames="CategoryName" ID="requests" CssClass="table table-hover table-striped" GridLines="None" runat="server"
                                        AutoGenerateColumns="false" OnRowCommand="users_RowCommand">
                                        <PagerTemplate>
                                            <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrev" CssClass="btn btn-primary" />
                                            <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext" CssClass="btn btn-primary" />
                                        </PagerTemplate>
                                        <PagerSettings Position="Bottom"></PagerSettings>
                                        <Columns>
                                            <asp:BoundField DataField="CategoryID" HeaderText="ID" Visible="false" />
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                            <asp:BoundField DataField="RequestingUser" HeaderText="Requesting User" />
                                            <asp:BoundField DataField="Date" HeaderText="Date"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center">
                                                <HeaderTemplate></HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Button ID="AcceptRequest" CssClass="btn btn-success form-control accept" runat="server" CausesValidation="false" CommandName="AcceptRequest" Text="Accept" CommandArgument='<%# Eval("CategoryID") + ";" + Eval("RequestingUser") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center">
                                                <ItemTemplate>
                                                    <asp:Button ID="RejectRequest" CssClass="btn btn-danger form-control decline" runat="server" CausesValidation="false" CommandName="RejectRequest" Text="Reject" CommandArgument='<%# Eval("CategoryID") + ";" + Eval("RequestingUser") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="panel-footer">
                                <p class="text-warning"></p>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h4 class="modal-title" style="color: white;">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body" style="overflow-y: scroll; max-height: 350px">
                                    <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Ok</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
