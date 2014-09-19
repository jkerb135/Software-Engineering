<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="SE.Categories" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="SiteHead" runat="server">
</asp:Content>
<asp:Content ID="PageBody" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Categories</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-xs-12">
            <div class="error-messages">
                <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
            </div>
            <div class="success-messages">
                <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
            </div>
            <asp:Panel ID="AddNewCategoryPanel" CssClass="form-group" runat="server">
                <div class="form-inline">
                    <asp:TextBox ID="AddNewCategoryName" runat="server" 
                        CssClass="form-control"></asp:TextBox>
                    <asp:Button ID="AddNewCategoryButton" runat="server"
                        CssClass="btn btn-default" Text="Add New" 
                        onclick="AddNewCategory_Click" />
                </div>
            </asp:Panel>
            <div class="form-group">
                <asp:DropDownList ID="CategoryList" runat="server" CssClass="form-control"
                    DataValueField="CategoryID" DataTextField="Name" AutoPostBack="true"
                    onselectedindexchanged="CategoryList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <asp:Panel ID="EditCategoryPanel" runat="server">
                <div class="row form-group">
                    <div class="col-xs-6">
                        <asp:Label ID="AllUsersLabel" runat="server" 
                            Text="All Users"></asp:Label>
                        <asp:ListBox ID="AllUsers" CssClass="form-control" 
                            runat="server"></asp:ListBox>
                    </div>
                    <div class="col-xs-6">
                        <asp:Label ID="UsersInCategoryLabel" runat="server" 
                            Text="Users In Category"></asp:Label>
                        <asp:ListBox ID="UsersInCategory" CssClass="form-control col-xs-6" 
                            runat="server"></asp:ListBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 center">
                        <asp:Button ID="MoveLeft" CssClass="btn btn-default" runat="server" Text="<" 
                            onclick="MoveLeft_Click" />
                        <asp:Button ID="MoveRight" CssClass="btn btn-default" runat="server" Text=">" 
                            onclick="MoveRight_Click" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="EditCategoryNameLabel" runat="server" 
                        Text="Category Name"></asp:Label>
                    <asp:TextBox ID="EditCategoryName" CssClass="form-control" 
                        runat="server"></asp:TextBox>
                </div>
                <div class="inline-form right">
                    <asp:Button ID="EditCategoryCancel" CssClass="btn btn-default" 
                        runat="server" Text="Cancel" onclick="EditCategoryCancel_Click" />
                    <asp:Button ID="EditCategoryButton" CssClass="btn btn-default" 
                        runat="server" Text="Update Category" onclick="EditCategoryButton_Click" />
                </div>
                <asp:Button ID="DeleteCategoryButton" CssClass="btn btn-danger btn-lg clear block" 
                    runat="server" Text="Delete" onclick="DeleteCategoryButton_Click" 
                    OnClientClick="return confirm('Are you sure you want to delete this category?');" />
            </asp:Panel>
        </div>
    </div>
    <asp:SqlDataSource ID="CategoryListSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" 
        SelectCommand="SELECT * FROM [Categories]"></asp:SqlDataSource>
</asp:Content>
