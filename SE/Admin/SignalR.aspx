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

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            Click on the
            ListBox and type the word to search<br />
            <br />
            <br />
            <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
            <br />          
            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ListBox1"> 
            <br />
            &nbsp;<br />
            Click on the
            DropDownList and type the word to search<br />
            <br />
                <asp:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="DropDownList1">
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>           
            <br />
        </div>
</asp:Content>
