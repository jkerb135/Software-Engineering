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
            if (user == null) throw new ArgumentNullException("user");

            Session["UserName"] = user.UserName;

            if (Roles.IsUserInRole(user.UserName, "Manager") ||
                Roles.IsUserInRole(user.UserName, "Supervisor"))
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }

            if (Roles.IsUserInRole(user.UserName, "User"))
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}