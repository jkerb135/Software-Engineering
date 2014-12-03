using Newtonsoft.Json;
using SE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SE.Handlers
{
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

                if (user != null && !user.IsOnline)
                    loginSuccess = Membership.ValidateUser(username, password);

                if (loginSuccess)
                    FormsAuthentication.SetAuthCookie(username, true);

                // simulate Microsoft XSS protection
                var wrapper = new { d = loginSuccess ? "sign in success" : "sign in failed" };
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
                context.Response.Write(JsonConvert.SerializeObject(wrapper));
            }
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