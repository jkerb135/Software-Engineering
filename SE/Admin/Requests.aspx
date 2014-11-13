<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="SE.Admin.Requests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">

     <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <i class="fa fa-users fa-fw"></i>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div style="overflow: auto; max-height: 250px;">
                                            <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" DataKeyNames="CategoryName" ID="categories" CssClass="table table-hover table-striped" GridLines="None" runat="server" AllowPaging="True" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-Width="36%" />
                                                    <asp:BoundField DataField="CreatedTime" HeaderText="Created Date" ItemStyle-Width="36%" DataFormatString=" {0:d} " HtmlEncode="false" />
                                                    <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="center">
                                                        <HeaderTemplate>Update Users In Category</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="AddUsers" CssClass="btn btn-primary form-control" runat="server" CausesValidation="false" CommandName="AddUsers" Text="Update" CommandArgument='<%# Eval("CategoryID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="CategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <p class="text-warning">NOTE: Adding a User to a category assigns them all tasks under that category.</p>
                                    </div>
                                </div>
</asp:Content>
