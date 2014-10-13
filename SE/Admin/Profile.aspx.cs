using SE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE.Admin
{
    public partial class Profile : System.Web.UI.Page
    {
        string user = Membership.GetUser().UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = user  + " Categories";
            Label2.Text = user + " Tasks";
            QueryYourCategories(user);
            QueryYourTasks(user);
            if (Request.QueryString["userName"].ToUpper() != user.ToUpper() && Request.Path != "Profile.aspx") 
            {
                Label1.Text = Request.QueryString["userName"] + " Categories";
                Label2.Text = Request.QueryString["userName"] + " Tasks";
                QueryYourCategories(Request.QueryString["userName"]);
                QueryYourTasks(Request.QueryString["userName"]);
            }
        }
        private void QueryYourCategories(string username)
        {
            categories.DataSource = Category.GetSupervisorCategories(username);
            categories.DataBind();
        }
        private void QueryYourTasks(string username)
        {
            tasks.DataSource = Task.GetSupervisorTasks(username);
            tasks.DataBind();
        }
    }
}