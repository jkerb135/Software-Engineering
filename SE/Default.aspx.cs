/*
Author			: Josh Kerbaugh\Daniel Talley
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: Default.aspx.cs
Purpose			: This file uses redirects users to Dashboard.aspx if they are already logged in. If no it takes them to the login page.
*/
using System;
using System.Web.Security;

namespace SE
{
    public class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(!User.Identity.IsAuthenticated ? FormsAuthentication.LoginUrl : "Admin/Dashboard.aspx");
        }
    }
}