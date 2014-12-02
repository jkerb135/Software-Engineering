using System;
using System.Web.Security;
using System.Web.UI;

namespace SE
{
    public class Default1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(!User.Identity.IsAuthenticated ? FormsAuthentication.LoginUrl : "Admin/Dashboard.aspx");
        }
    }
}