<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SE.Default" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SB Admin 2 - Bootstrap Admin Theme</title>

    <!-- Bootstrap Core CSS -->
    <link href="<%=ResolveUrl("~/StyleSheets/bootstrap.min.css") %>" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="<%=ResolveUrl("~/StyleSheets/plugins/metisMenu/metisMenu.min.css") %>" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="<%=ResolveUrl("~/StyleSheets/sb-admin-2.css") %>" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="<%=ResolveUrl("~/FontAwesome/css/font-awesome.min.css") %>" rel="stylesheet" type="text/css">

    <!-- SE CSS -->
    <link href="<%=ResolveUrl("~/StyleSheets/style.css") %>" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body>
    <form id="MainForm" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <asp:Login ID="MainLogin" OnLoggedIn="MainLogin_LoggedIn" runat="server">
                        <LayoutTemplate>
                            <div class="login-panel panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Please Sign In</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="login-form">
                                        <fieldset>
                                            <div class="form-group">
                                                <asp:TextBox ID="UserName" CssClass="form-control" PlaceHolder="Username" 
                                                    runat="server" TextMode="SingleLine"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="PassWord" CssClass="form-control" PlaceHolder="Password" 
                                                    runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="RememberMe" runat="server" />
                                                    Remember Me
                                                </label>
                                            </div>
                                            <asp:Button ID="Login" CommandName="Login" CssClass="btn btn-lg btn-success btn-block" Text="Submit" runat="server"  />
                                        </fieldset>
                                    </div>
                                </div>   
                            </div>
                            <div>
                                <asp:Label ID="FailureText" CssClass="error-message" runat="server" Text=""></asp:Label>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </div>
        </div>

        <!-- jQuery Version 1.11.0 -->
        <script src="<%=ResolveUrl("~/Scripts/jquery-1.11.0.js") %>"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="<%=ResolveUrl("~/Scripts/bootstrap.min.js") %>"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="<%=ResolveUrl("~/Scripts/plugins/metisMenu/metisMenu.min.js") %>"></script>

        <!-- Custom Theme JavaScript -->
        <script src="<%=ResolveUrl("~/Scripts/sb-admin-2.js") %>"></script>

        <!-- SE JavaScript -->
        <script src="<%=ResolveUrl("~/Scripts/scripts.js") %>"></script>
    </form>
</body>
</html>
