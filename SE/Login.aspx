<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SE.Default" %>
<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>iPaws Login</title>

    <!-- Bootstrap Core CSS -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.0/css/bootstrap.min.css">
    <!-- SE CSS -->
    <link href="<%=ResolveUrl("~/StyleSheets/style.css") %>" rel="stylesheet" type="text/css" />

        <!-- Custom CSS -->
    <link href="<%=ResolveUrl("~/StyleSheets/sb-admin-2.css") %>" rel="stylesheet">


    

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body class="login">
    <form id="MainForm" runat="server">
        <div class="container centered">
            <div class="row">
                <div class="col-xs-4 col-xs-offset-4 ">
                                                                                        <div class="col-xs-12  col-centered">
                    <img src="Images/logo.png" width="300"/>
                </div>
                    <asp:Login ID="MainLogin" OnLoggedIn="MainLogin_LoggedIn" runat="server">
                        <LayoutTemplate>
                            <div class="login-panel panel panel-login ">
                                <div class="panel-heading" style="text-align:center">
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
                                            <asp:Button ID="Login" CommandName="Login" CssClass="btn btn-lg btn-primary btn-block" Text="Submit" runat="server"  />
                                        </fieldset>
                                    </div>
                                </div>   
                            </div>
                            <div>
                                <asp:Label ID="FailureText" CssClass="error-messages" runat="server" Text=""></asp:Label>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
