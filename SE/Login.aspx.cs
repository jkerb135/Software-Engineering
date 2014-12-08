/*
Author			: Josh Kerbaugh\Daniel Talley
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: Login.aspx.cs
Purpose			: This file uses form auth to login each supervisor and manager into the system.
*/

using System;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SE
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void MainLogin_LoggedIn(object sender, EventArgs e)
        {
            var controlLogin = (Login)sender;
            var user = Membership.GetUser(controlLogin.UserName);
            if (user == null) throw new ArgumentNullException("sender");

            Session["UserName"] = user.UserName;

            if (Roles.IsUserInRole(user.UserName, "Manager") ||
                Roles.IsUserInRole(user.UserName, "Supervisor"))
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
        }
    }
}