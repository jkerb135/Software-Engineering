<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SE.Default1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Page</title>
</head>
<body>
    <form id="MainForm" runat="server">
    <div>
        <h1>Welcome <asp:LoginName ID="UserLoginName" runat="server" />!</h1>
        <asp:LoginStatus ID="LoginStatus" runat="server" />
    </div>
    </form>
</body>
</html>
