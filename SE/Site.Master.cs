using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SE
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            username.Text = " " + Membership.GetUser().UserName.ToUpper() + " ";
            if (!Roles.IsUserInRole(UserName, "Manager"))
            {
                CreateUserMenu.Visible = false;
                ReportsMenu.Visible = false;
            }
            else
            {
                CategoriesMenu.Visible = false;
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}