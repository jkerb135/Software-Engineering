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
            if (Request.QueryString["userName"].ToUpper() != user.ToUpper())
            {
                YourInfo.Visible = false;
                //OtherInfo.Visible = true;
            }
            if (!IsPostBack)
            {
                Label1.Text = " Categories";
                Label2.Text = " Tasks";
                Label3.Text = " Users";
                QueryYourCategories();
                QueryYourTasks();
                QueryYourUsers();
                UsersInCategory.SelectCommand = addUserDataSource.SelectCommand = "select UserName,IsApproved,LastActivityDate from aspnet_Membership as p inner join aspnet_Users as r on p.UserId = r.UserId inner join aspnet_UsersInRoles as t on t.UserId = p.UserId inner join MemberAssignments as z on z.AssignedUser = r.UserName where t.RoleId = 'F0D05C09-B992-45A3-8F5E-4EA772C760FD' And AssignedSupervisor = '" + Membership.GetUser() + "'";
            }
        }
        private void QueryYourCategories()
        {
            CategorySource.SelectCommand = "Select CategoryName, CreatedTime From Categories Where CreatedBy = '" + Request.QueryString["userName"] + "'";
            categories.DataBind();

        }
        private void QueryYourTasks()
        {
            TaskSource.SelectCommand = "Select CategoryName, TaskName, Tasks.CreatedTime From Tasks Inner Join Categories on Tasks.CategoryID = Categories.CategoryID Where Tasks.CreatedBy = '" + Request.QueryString["userName"] + "'";
            tasks.DataBind();
        }
        private void QueryYourUsers()
        {
            //users.DataSource = Member.CustomGetSupervisorsUsers(username);
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            foreach (GridViewRow row in AddUserGrid.Rows)
            {
                CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                box.Checked = false;
            }
            string queryString = "SELECT AssignedUser FROM CategoryAssignments WHERE CategoryID=@id";
            string id = e.CommandArgument.ToString();
            catUserLabel.Text = "Manage Users in " + id;
            var catID = Category.getCategoryID(id);
            Label5.Text = catID.ToString();

            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", catID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    foreach (GridViewRow row in AddUserGrid.Rows)
                    {
                        CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                        CheckBox isApproved = row.Cells[2].Controls[0] as CheckBox;
                        if (!isApproved.Checked)
                        {
                            box.Enabled = false;
                        }
                        else
                        {
                            box.Enabled = true;
                        }
                        if (row.Cells[1].Text == dr["AssignedUser"].ToString())
                        {
                            box.Checked = true;
                        }
                    }
                }

                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "hideCats();", true);
        }

        protected void AddUsersToCat_Click(object sender, EventArgs e)
        {
            string queryString = "Insert Into CategoryAssignments (AssignedUser, CategoryID) Values (@user, @id)";
            string queryString2 = "DELETE FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            string queryString3 = "Delete FROM CategoryAssignments WHERE CategoryID=@id";
            //string queryString5 = "Insert into TaskAssignments (TaskID,AssignedUser) Select TaskID, c.AssignedUser From Tasks t inner join CategoryAssignments c on c.CategoryID = t.CategoryID where c.AssignedUser = @user And c.CategoryID = @id";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", Label5.Text);
                cmd.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@id", Label5.Text);
                cmd2.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                cmd3.Parameters.AddWithValue("@id", Label5.Text);


                /*SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", Label5.Text);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);*/


                con.Open();
                cmd3.ExecuteNonQuery();
                foreach (GridViewRow row in AddUserGrid.Rows)
                {
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;

                    if (box != null && box.Checked)
                    {
                        cmd.Parameters["@user"].Value = row.Cells[1].Text;
                        //cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd.ExecuteNonQuery();
                        //cmd5.ExecuteNonQuery();
                    }
                    else if (!box.Checked)
                    {
                        cmd2.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd2.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showCats();", true);
        }

        protected void TaskGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            foreach (GridViewRow row in UsersInTask.Rows)
            {
                CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                box.Checked = false;
            }
            string queryString = "SELECT AssignedUser FROM TaskAssignments WHERE TaskID=@id";
            string id = e.CommandArgument.ToString();
            Label4.Text = "Manage Users in " + id;
            var taskID = Task.getTaskID(id);
            Label5.Text = taskID.ToString();

            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", taskID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    foreach (GridViewRow row in UsersInTask.Rows)
                    {
                        CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                        CheckBox isApproved = row.Cells[2].Controls[0] as CheckBox;
                        if (!isApproved.Checked)
                        {
                            box.Enabled = false;
                        }
                        if (row.Cells[1].Text == dr["AssignedUser"].ToString())
                        {
                            box.Checked = true;
                        }
                    }
                }

                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "hideTasks();", true);
        }

        protected void AssUsersToTask_Click(object sender, EventArgs e)
        {
            string queryString4 = "Delete FROM TaskAssignments WHERE TaskID=@id";
            string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID) Values (@user, @id)";
            string queryString6 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {  
                SqlCommand cmd4 = new SqlCommand(queryString4, con);
                cmd4.Parameters.AddWithValue("@id", Label5.Text);

                SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", Label5.Text);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd6 = new SqlCommand(queryString6, con);
                cmd6.Parameters.AddWithValue("@id", Label5.Text);
                cmd6.Parameters.AddWithValue("@user", DBNull.Value);

                con.Open();
                cmd4.ExecuteNonQuery();
                foreach (GridViewRow row in UsersInTask.Rows)
                {
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;

                    if (box != null && box.Checked)
                    {
                        cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd5.ExecuteNonQuery();
                    }
                    else if (!box.Checked)
                    {
                        cmd6.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd6.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showTasks();", true);
        }

    }
}