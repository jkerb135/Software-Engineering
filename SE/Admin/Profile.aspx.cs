using SE.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            Label1.Text = " Categories";
            Label2.Text = " Tasks";
            Label3.Text = " Users";
            QueryYourCategories(Request.QueryString["userName"]);
            QueryYourTasks(Request.QueryString["userName"]);
            QueryYourUsers(Request.QueryString["userName"]);
        }
        private void QueryYourCategories(string username)
        {
            CategorySource.SelectCommand = "Select CategoryName, CreatedTime From Categories Where CreatedBy = '" + Request.QueryString["userName"] + "'";
            categories.DataBind();
            
        }
        private void QueryYourTasks(string username)
        {
            tasks.DataSource = Task.GetSupervisorTasks(username);
            tasks.DataBind();
        }
        private void QueryYourUsers(string username)
        {
            users.DataSource = Member.CustomGetSupervisorsUsers(username);
            users.DataBind();
        }

        protected void categories_Sort(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = categories.DataSource as DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression + "DESC";
                categories.DataSource = dv;
                categories.DataBind();

            }
        }
    }
}