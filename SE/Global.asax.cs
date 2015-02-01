/*
Author			: Josh Kerbaugh\Daniel Talley
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: Global.asax.cs
Purpose			: This file contains code for application level and session level events raised by our WebAPI and HTTP modules.
*/
using System;
using System.Web;
using System.Web.Security;
using System.Web.Http;
using System.Web.Mvc;

namespace SE
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated) return;
            //old authentication, kill it
            FormsAuthentication.SignOut();
            //or use Response.Redirect to go to a different page
            FormsAuthentication.RedirectToLoginPage("Session=Expired");
            HttpContext.Current.Response.End();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}