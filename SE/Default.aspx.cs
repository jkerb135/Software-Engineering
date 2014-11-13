using System;
using System.Web.Security;

namespace SE
{
    public class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
        }

    }
}