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
using System.Web;
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
            var rememberMe = controlLogin.RememberMeSet;
            if (user == null) throw new ArgumentNullException("sender");

            Session["UserName"] = user.UserName;

            var timeout = rememberMe ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.
            var ticket = new FormsAuthenticationTicket(user.UserName, rememberMe, timeout);
            var encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
            {
                Expires = System.DateTime.Now.AddMinutes(timeout),
                HttpOnly = true
            };
            // Not my line
            // cookie not available in javascript.
            Response.Cookies.Add(cookie);


            if (Roles.IsUserInRole(user.UserName, "Manager") ||
                Roles.IsUserInRole(user.UserName, "Supervisor"))
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
        }
    }
}