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
    <script src="<%=ResolveUrl("~/Scripts/jquery.signalR-2.2..js") %>"></script>
    <script src="<%=ResolveUrl("~/signalr/hubs") %>"></script>
    <script>
        $(function () {
            var contact = $.connection.userActivityHub;
            $.connection.hub.start().done();
 
            contact.client.recieve = function (message) {
                alert(message);
            }
        });
        function sendMessage() {
            alert('here');
            contact.server.sendMessage('Dave Mackey', "hello", "Hello");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Hello <%=Membership.GetUser().UserName%>
        <input id="Button1" type="button" value="button" onclick="sendMessage()"/>

    </div>
    </form>
</body>
</html>
