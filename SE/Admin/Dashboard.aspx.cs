using SE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private String SelectedUserName = String.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            // Selected user (used on edit user page)
            SelectedUserName = (Request.QueryString["username"] != null) ? Request.QueryString["username"] : "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Roles.IsUserInRole(SelectedUserName, "Supervisor"))
            {
                getActiveUsers();
                getRecentUsers();
            }
            
        }
        protected void getActiveUsers()
        {
            activeUserList.DataSource = Member.CustomGetActiveUsers();
            activeUserList.DataBind();
        }
        protected void getRecentUsers()
        {
            newMembers.DataSource = Member.CustomRecentlyAssigned();
            newMembers.DataBind();
        }
    }
}
