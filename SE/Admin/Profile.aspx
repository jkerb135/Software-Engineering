﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="SE.Admin.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="../Scripts/Custom/profile.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h2>Profile</h2>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="profileNav">
            <input type="button" id="catButton" class="btn btn-primary" value="View Assigned Categories" style="width: 200px;" />
            <input type="button" id="taskButton" class="btn btn-primary" value="View Assigned Tasks" style="width: 200px;" />
            <input type="button" id="assignedUsers" class="btn btn-primary" value="View Assigned Users" style="width: 200px;" />
        </div>
        <div class="dataTables">
            <div id="catData" style="display: none">
                <div class="col-xs-10">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:GridView EmptyDataText="No Categories are assigned" DataSourceID="CategorySource" ID="categories" CssClass="table table-bordered table-striped" runat="server" AllowPaging="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate><asp:Button ID="AddUserToCat" runat="server" Text="Update Users" CssClass="btn btn-primary" /></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="CategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>">

                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
            <div id="taskData" style="display: none;">
                <div class="col-xs-10">
                    <div class="panel panel-primary ">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="tasks" CssClass="table table-bordered table-striped" runat="server" AllowPaging="true" AllowSorting="true">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div id="userData" style="display: none;">
                <div class="col-xs-10">
                    <div class="panel panel-primary ">
                        <div class="panel-heading">
                            <i class="fa fa-users fa-fw"></i>
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="users" CssClass="table table-bordered table-striped" runat="server" AllowPaging="true" AllowSorting="true">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
