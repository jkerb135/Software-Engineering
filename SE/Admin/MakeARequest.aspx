<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MakeARequest.aspx.cs" Inherits="SE.Admin.MakeARequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <asp:UpdatePanel runat="server" ID="requestUpdatePanel" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-8">
                            <asp:TextBox runat="server" ID="SearchTxt" class="form-control" placeholder="Search for Category Name or Supervisor"/>
                            </div>
                                    <asp:Button runat="server" ID="Search" Text="Search" CssClass="btn btn-primary" OnClick="Search_OnClick" />
                        </div>
                    </div>
                        </div>
                </div>
                <div class="row">
                    <div class="col-md-11" style="float: none;margin:0 auto">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label7" runat="server" Text="Request A Category"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div>
                                <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="Search Above to find Categories" ID="QueryGridView" CssClass="table table-hover table-striped" 
                                    GridLines="None" runat="server" AutoGenerateColumns="False" OnRowCommand="QueryGridView_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="20%" />
                                        <asp:HyperLinkField DataTextField="CreatedBy" HeaderText="Category Owner" ItemStyle-Width="20%"></asp:HyperLinkField>
                                        <asp:BoundField DataField="CreatedTime" HeaderText="Created Time" ItemStyle-Width="40%" />
                                        <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                            <HeaderTemplate>Request Category</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID="RequestCat" CssClass="btn btn-primary form-control requestCatBtn" runat="server" CausesValidation="false" CommandName="Request" Text="Request" CommandArgument='<%# Eval("CategoryID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <p class="text-warning">NOTE: Requesting a Category transfers all the tasks that coorespond with the Category.</p>
                        </div>
                    </div>
                        </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
