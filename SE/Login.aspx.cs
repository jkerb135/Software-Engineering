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
            System.Web.UI.WebControls.Login controlLogin = (Login)sender;
            MembershipUser user = (MembershipUser)Membership.GetUser(controlLogin.UserName);

            if (Roles.IsUserInRole(user.UserName, "User"))
            {
                Response.Redirect("~/Default.aspx");
            }

            if (Roles.IsUserInRole(user.UserName, "Manager") || 
                Roles.IsUserInRole(user.UserName, "Supervisor"))
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
        }
    }
}