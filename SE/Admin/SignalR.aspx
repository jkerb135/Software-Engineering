<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignalR.aspx.cs" Inherits="SE.Admin.SignalR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <script src="<%=ResolveUrl("//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js") %>"></script>
    <link href="<%=ResolveUrl("~/Content/toastr.css") %>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/toastr.js") %>"></script>
    <script src="<%=ResolveUrl("~/Scripts/jquery.signalR-2.1.2.js") %>"></script>
    <script src="<%=ResolveUrl("~/signalr/hubs") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
        <fieldset>
            <!-- Form Name -->
            <legend>Contact</legend>

            <!-- Text input-->
            <div class="control-group">
                <label class="control-label" for="txtName">Name</label>
                <div class="controls">
                    <input id="txtName" name="txtName" type="text" class="input-medium">

                </div>
            </div>

            <!-- Textarea -->
            <div class="control-group">
                <label class="control-label" for="txtComments">Comments</label>
                <div class="controls">
                    <textarea id="txtComments" name="txtComments"></textarea>
                </div>
            </div>

            <!-- Button -->
            <div class="control-group">
                <label class="control-label" for="btnSubmit"></label>
                <div class="controls">
                    <button id="btnSubmit" name="btnSubmit" class="btn btn-primary">Submit</button>
                </div>
            </div>

        </fieldset>
</asp:Content>
