<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewTask.aspx.cs" Inherits="SE.PreviewTask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheets/Preview/css.css" rel="stylesheet" />
    <link href="StyleSheets/Preview/themed.css" rel="stylesheet" />
    <link href="StyleSheets/Preview/jquery.mobile.icons.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.3/jquery.mobile.structure-1.4.3.min.css" /> 
		  <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script> 
		  <script src="http://code.jquery.com/mobile/1.4.3/jquery.mobile-1.4.3.min.js"></script>
<script src="Scripts/Preview/preview.js"></script>
    <style>
table td
{
    text-align: center;
    vertical-align: middle;
    padding: 5px;
    position: relative;
}

table td img
{
    vertical-align: middle;
    display: inline-block;
}

table td p
{
    display: inline-block;
    width: 430px;
    background: #ccc;
    vertical-align: middle
}
    </style>
    <script type='text/javascript'>
<!--
$(document).bind('mobileinit',function(){
    $.extend(  $.mobile , {
      defaultPageTransition: "none"
    });
});
//-->
</script>
    <style type='text/css'>
<!--
    @media only screen and (min-width: 1025px){
        #dotask {
            width: 960px !important;
            margin: 0 auto !important;
            position: relative !important;
        }
    }
-->
</style>
</head>
<body>
<!-- TASK PAGE -->
		<div data-role="page" id="dotask" data-theme="d" style="width:50%">
			<div data-role="header" style="width:960px">
			<a href="#" class="ui-btn ui-icon-back ui-btn-icon-left" data-rel="back">Go Back</a>
			<!--<a href="#" id="reset" class="ui-btn ui-icon-refresh ui-btn-icon-right">Restart</a>-->
				
				<span><iframe src="http://free.timeanddate.com/clock/i4d2cptu/n781/fcfff/tct/pct/ts1" frameborder="0" width="95" height="19" allowTransparency="true" align="right"></iframe>
				
				<iframe src="http://free.timeanddate.com/clock/i4d2cv3b/n781/fcfff/tct/pct/tt1/tw0" frameborder="0" width="131" height="19" allowTransparency="true" align="right"></iframe><br /><h3 style="text-align:center">.:Task Manager:.</h3>
				</span>
			</div>
			<div data-role="content">	
					<ul data-role="listview" data-count-theme="c" data-inset="true">
						<li data-role="list-divider">Follow the steps below to complete a task.</li>
						
						<li id="mainstep">
						<h2 id="steptitle"></h2>
						<div id="av"></div>
						</li>
						<div data-role="collapsible" id="detailSteps">
						  <h1>Click here for more detailed instructions.</h1>

						  <div class="ui-grid-a">
							  <div id="detailstep">
								</div>
							</div>
						  
						 
						</div>
						<li><marquee behavior="alternate" id="pump" style="color:red;">Great Job!</marquee></li>
						<li id="done"><button>Done</button></li>
						<li id="next"><button>Next</button></li>
						<li id="finish"><button>Finish</button></li>
						<li id="start"><button>Start</button></li>
						
						
						<li id="bot" data-role="list-divider">You still need to complete the following steps.</li>
						
						
						
					</ul>

			</div>
			<div data-role="footer" style="width:960px">
			
			<div data-role="navbar">	
				<ul data-type="horizontal">
					<li><a href="#home" id="clearrun" class="ui-btn ui-icon-home ui-btn-icon-top">Home</a></li>
				</ul>		
			</div>
			</div>
		</div>

	


</body>
</html>
