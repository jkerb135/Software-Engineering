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
            <asp:UpdatePanel ID="CategoryContainer" runat="server">
                <ContentTemplate>
                    <div class="error-messages form-group">
                        <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
                    </div>
                    <div class="success-messages form-group">
                        <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                    </div>
                    <asp:Panel ID="AddNewCategoryPanel" CssClass="form-group" runat="server">
                        <div class="form-inline">
                            <asp:TextBox ID="AddNewCategoryName" runat="server"
                                CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="AddNewCategoryButton" runat="server"
                                CssClass="btn btn-default" Text="Add New Category"
                                OnClick="AddNewCategory_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="EditCategoryPanel" runat="server">
                    <div class="form-group">
                        <asp:DropDownList ID="CategoryList" runat="server" CssClass="form-control"
                            DataValueField="CategoryID" DataTextField="CategoryName" AutoPostBack="true"
                            OnSelectedIndexChanged="CategoryList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    
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
                                    OnClick="MoveLeft_Click" />
                                <asp:Button ID="MoveRight" CssClass="btn btn-default" runat="server" Text=">"
                                    OnClick="MoveRight_Click" />
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
                                runat="server" Text="Cancel" OnClick="EditCategoryCancel_Click" />
                            <asp:Button ID="EditCategoryButton" CssClass="btn btn-default"
                                runat="server" Text="Update Category" OnClick="EditCategoryButton_Click" />
                        </div>
                        <asp:Button ID="DeleteCategoryButton" CssClass="btn btn-danger btn-lg clear block"
                            runat="server" Text="Delete" OnClick="DeleteCategoryButton_Click"
                            OnClientClick="return confirm('Are you sure you want to delete this category?');" />
                    </asp:Panel>
                    
            <asp:Panel ID="ListBoxPanel" CssClass="form-group" runat="server">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-3" style="padding:0px">
                            <asp:TextBox cssClass="form-control" ID="catFilter" runat="server" placeholder="Filter Categories" AutoPostBack="true" OnTextChanged="catFilter_TextChanged" ToolTip="Enter Category Search Here"></asp:TextBox>
                            <asp:ListBox cssClass="form-control" ID="catList" runat="server" Height="500px" OnSelectedIndexChanged="QueryTasks" AutoPostBack="True" ondblclick="ListBox1_DoubleClick(this)" ToolTip="Click To Navigate"></asp:ListBox>
                        </div>
                        <div class="col-lg-3" style="padding:0px">
                            <asp:TextBox cssClass="form-control" ID="taskFilter" runat="server" placeholder="Filter Tasks"></asp:TextBox>
                            <asp:ListBox  cssClass="col-lg-12 form-control" ID="taskList" runat="server" Height="500px" OnSelectedIndexChanged="QueryMainStep" AutoPostBack="True" ondblclick="ListBox1_DoubleClick(this)"></asp:ListBox>
                        </div>
                        <div class="col-lg-3" style="padding:0px">
                            <asp:TextBox cssClass="form-control" ID="mainFilter" runat="server" placeholder="Filter Main Steps"></asp:TextBox>
                            <asp:ListBox  cssClass="col-lg-12 form-control" ID="mainStep" runat="server" Height="500px" AutoPostBack="True" OnSelectedIndexChanged="QueryDetailedStep" ondblclick="ListBox1_DoubleClick(this)"></asp:ListBox>
                        </div>
                        <div class="col-lg-3" style="padding:0px">
                            <asp:TextBox cssClass="form-control" ID="detailFilter" runat="server" placeholder="Filter Detailed Steps"></asp:TextBox>
                            <asp:ListBox cssClass="col-lg-12 form-control" ID="detailedStep" runat="server" Height="500px"  ondblclick="ListBox1_DoubleClick(this)"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                        <asp:Button CssClass="btn btn-primary btn-md" ID="Button1" runat="server" Text="Create" />
                        <asp:Button CssClass="btn btn-primary btn-md" ID="Button2" runat="server" Text="Update"/>
                        <asp:Button CssClass="btn btn-danger btn-md" ID="Button3" runat="server" Text="Destroy"/>
                    </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
            </div>
    <asp:SqlDataSource ID="CategoryListSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        SelectCommand="SELECT * FROM [Categories] WHERE ([CreatedBy] = @CreatedBy)"
        ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:SessionParameter Name="CreatedBy" SessionField="UserName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
