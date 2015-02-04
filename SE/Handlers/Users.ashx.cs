using Newtonsoft.Json;
using SE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SE.Handlers
{
    [EnableCors("*","*","*")]
    public class Users : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (context.Request.Form["action"] == "login")
            {
                string username = context.Request.Form["username"];
                string password = context.Request.Form["password"];
                bool loginSuccess = false;

                MembershipUser user = Membership.GetUser(username);

                if (user != null && Roles.IsUserInRole(user.UserName,"User"))
                    loginSuccess = Membership.ValidateUser(username, password);

                if (loginSuccess)
                    FormsAuthentication.SetAuthCookie(username, true);

                // simulate Microsoft XSS protection
                var wrapper = new { d = loginSuccess ? "sign in success" : "sign in failed" };
                context.Response.ContentType = "application/json";
                context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                context.Response.AppendHeader("Access-Control-Allow-Headers", "x-requested-with");
                context.Response.Write(JsonConvert.SerializeObject(wrapper));
            }

            if (context.Request.Form["action"] == "logout")
            {
                string username = context.Request.Form["username"];
                bool logoutSuccess = false;

                MembershipUser user = Membership.GetUser(username);

                if (user != null && user.IsOnline)
                {
                    FormsAuthentication.SignOut();
                    user.LastActivityDate = DateTime.UtcNow.AddMinutes(-(Membership.UserIsOnlineTimeWindow + 1));
                    Membership.UpdateUser(user);
                    logoutSuccess = true;
                }

                // simulate Microsoft XSS protection
                var wrapper = new { d = logoutSuccess ? "logged out" : "not logged in" };
                context.Response.ContentType = "application/json";
                context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                context.Response.AppendHeader("Access-Control-Allow-Headers", "x-requested-with");
                context.Response.Write(JsonConvert.SerializeObject(wrapper));
            }
        }
        private void SetAllowCrossSiteRequestOrigin(HttpContext context)
        {
            string origin = context.Request.Headers["Origin"];
            if (!String.IsNullOrEmpty(origin))
                //You can make some sophisticated checks here
                context.Response.AppendHeader("Access-Control-Allow-Origin", origin);
            else
                //This is necessary for Chrome/Safari actual request
                context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}