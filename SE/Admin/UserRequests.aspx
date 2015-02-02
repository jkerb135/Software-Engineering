<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRequests.aspx.cs" Inherits="SE.Admin.UserRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="panel panel-primary" id="categoryRequests">

            <div class="panel-heading" style="cursor: pointer">
                <i class="fa fa-users fa-fw"></i>
                <asp:Label ID="Label1" runat="server" Text="User Task Requests"></asp:Label>
            </div>
            <asp:UpdatePanel runat="server" ID="requestUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="panel-body">
                        <div>
                            <asp:GridView OnPageIndexChanging="requests_OnPageIndexChanging" AllowPaging="True" PageSize="6"
                                ShowHeaderWhenEmpty="true" EmptyDataText="No Pending Tasks Requests from your Users"
                                DataKeyNames="TaskName" ID="requests" CssClass="table table-hover table-striped" GridLines="None" runat="server"
                                AutoGenerateColumns="false" OnRowCommand="requests_OnRowCommand_RowCommand" OnRowDeleting="requests_OnRowDeleting">
                                <PagerTemplate>
                                    <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrev" CssClass="btn btn-primary" />
                                    <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext" CssClass="btn btn-primary" />
                                </PagerTemplate>
                                <PagerSettings Position="Bottom"></PagerSettings>
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Id" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name" />
                                    <asp:BoundField DataField="TaskDescription" HeaderText="Description" />
                                    <asp:BoundField DataField="UserName" HeaderText="Requesting User" />
                                    <asp:BoundField DataField="DateCompleted" HeaderText="Date"></asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="AcceptRequest" CssClass="btn btn-success form-control accept" runat="server" CausesValidation="false" CommandName="AcceptRequest" Text="Accept" CommandArgument='<%# Eval("Id") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="DeleteBtn" runat="server" CssClass="btn btn-danger form-control" CommandName="Delete" Text="Reject" CausesValidation="False" OnClientClick="return confirm('Are you sure to delete this request?');" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
    <div class="modal fade" id="taskRequestModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="taskrequestid"/>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title" style="color: white;">
                                <asp:Label ID="lblModalTitle" runat="server" Text="Creating a Task"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-inline">
                                <div class="input-group">
                                    <asp:Label ID="Usernamelbl" runat="server" Text="Username"></asp:Label>
                                    <asp:TextBox ID="UsernameTxt" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="input-group">
                                    <asp:Label ID="TaskNameLbl" runat="server" Text="Task Name"></asp:Label>
                                    <asp:TextBox ID="TaskNameTxt" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="input-group">
                                    <asp:Label ID="CategoryLbl" runat="server" Text="Create a Category"></asp:Label>
                                    <asp:TextBox ID="CategoryText" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="input-group">
                                    <asp:Label ID="Label2" runat="server" Text="Select a Category"></asp:Label>
                                    <asp:DropDownList ID="CategoryDrp" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="null">--Select A Category--</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Back</button>
                            <asp:Button runat="server" Text="Create Task" CssClass="btn btn-success" ID="AddTask" OnClick="AddTask_OnClick"/>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
