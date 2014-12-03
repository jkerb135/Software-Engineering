using System;
using System.Web.Security;

namespace SE
{
    public class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(!User.Identity.IsAuthenticated ? FormsAuthentication.LoginUrl : "Admin/Dashboard.aspx");
        }
    }
}