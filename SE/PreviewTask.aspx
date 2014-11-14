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
		  <script src="http://code.jquery.com/mobile/1.4.3/jquery.mobile-1.4.3.min.js"></script><script src="Scripts/Preview/preview.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="page" id="dotask" data-theme="d">
			<div data-role="header" data-position="fixed">
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
                            <link href="StyleSheets/Preview/css.css" rel="stylesheet" />
						</li>
						<div data-role="collapsible">
						  <h1>Click here for more detailed instructions.</h1>

						  <div class="ui-grid-a">
							  <div class="ui-block-a">
								<div id="detailstep">
								</div>
							  </div>

							  <div class="ui-block-b">
								<div id="image">
								</div>
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
			<div data-role="footer" data-position="fixed">
			
			<div data-role="navbar">	
				<ul data-type="horizontal">
					<li><a href="#home" id="clearrun" class="ui-btn ui-icon-home ui-btn-icon-top">Home</a></li>
				</ul>		
			</div>
			</div>
		</div>

    </form>
</body>
</html>
