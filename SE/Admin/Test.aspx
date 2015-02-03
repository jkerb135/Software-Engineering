<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SE.Admin.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="/StyleSheets/JQueryUI/themes/base/jquery.ui.all.css">

    <!-- Signal R Client -->
    <script src="<%=ResolveUrl("~/Scripts/jquery.signalR-2.2.0.js") %>"></script>
    <script src="<%=ResolveUrl("~/signalr/hubs") %>"></script>
    <script>
        var contact = $.connection.userActivityHub;

        function start() {
            alert('here');
            $.connection.hub.qs = { 'username': "supervisor" };
            $.connection.hub.start({ jsonp: true }).done(console.log('done'));
        }
        function end() {
            alert('here');
            contact.server.disconnect("supervisor");
            $.connection.hub.stop();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Hello <%=Membership.GetUser().UserName%>
        <input id="Button1" type="button" value="start" onclick="start()"/>
        <input id="button2" type="button" value="end" onclick="end()"/>

    </div>
    </form>
</body>
</html>
