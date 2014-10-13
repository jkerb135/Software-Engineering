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
        private enum DashView
        {
            Supervisor = 1, Manager = 0
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                if (Roles.IsUserInRole(Membership.GetUser().UserName, "Supervisor"))
                {
                    DashboardView.ActiveViewIndex = (int)DashView.Supervisor;
                    getActiveUsers();
                    getRecentUsers();
                }
                else if (Roles.IsUserInRole(Membership.GetUser().UserName, "Manager"))
                {
                    DashboardView.ActiveViewIndex = (int)DashView.Manager;
                }
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
