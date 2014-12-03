<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewReport.aspx.cs" Inherits="SE.Admin.PreviewReport" %>
<%@ Register TagPrefix="se" TagName="ReportPreview" Src="~/UserControls/ReportPreview.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview Report</title>
    <link href="<%=ResolveUrl("~/StyleSheets/bootstrap.css") %>" rel="stylesheet">
</head>
<body>
    <form id="TheForm" runat="server">
        <se:ReportPreview runat="server" ID="ReportPreviewControl"></se:ReportPreview>
    </form>
</body>
</html>
