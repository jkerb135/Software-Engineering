<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignalR.aspx.cs" Inherits="SE.Admin.SignalR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SiteHead" runat="server">
    <link type="text/css" rel="stylesheet" href="/StyleSheets/ChatStyle.css" />
    <link rel="stylesheet" href="/StyleSheets/JQueryUI/themes/base/jquery.ui.all.css">
    <script src="/Scripts/ui/jquery.ui.core.js"></script>
    <script src="/Scripts/ui/jquery.ui.widget.js"></script>
    <script src="/Scripts/ui/jquery.ui.mouse.js"></script>
    <script src="/Scripts/ui/jquery.ui.draggable.js"></script>
    <script src="/Scripts/ui/jquery.ui.resizable.js"></script>
    <script>


    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteBody" runat="server">
    <div id="divContainer">

        <div id="divChat" class="chatRoom">
            <div class="content">
                <div id="divusers" class="users"></div>
            </div>
            <div class="messageBar">
                <input class="textbox" type="text" id="txtMessage" />
                <input id="btnSendMsg" type="button" value="Send" class="submitButton" />
            </div>
        </div>

    </div>
</asp:Content>