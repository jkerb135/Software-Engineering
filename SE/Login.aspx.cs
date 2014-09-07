using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            var User = Membership.GetUser(controlLogin.UserName);

            if (Roles.IsUserInRole(User.UserName, "Manager") ||
                Roles.IsUserInRole(User.UserName, "Supervisor"))
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }

            if (Roles.IsUserInRole(User.UserName, "User"))
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}