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
        public static int categoryID, taskID;
        public static string assignedUsername;
        string user = Membership.GetUser().UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["userName"].ToUpper() != user.ToUpper())
            {
                YourInfo.Visible = false;
                OtherInfo.Visible = true;
            }
            else
            {
                YourInfo.Visible = true;
                OtherInfo.Visible = false;
            }
            if (!IsPostBack)
            {
                categories.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
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
            CategorySource.SelectCommand = "Select * From Categories Where CreatedBy = '" + Request.QueryString["userName"] + "'";
            categories.DataBind();
            categories.UseAccessibleHeader = true;
            categories.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        private void QueryYourTasks()
        {
            TaskSource.SelectCommand = "Select * From Tasks Inner Join Categories on Tasks.CategoryID = Categories.CategoryID Where Tasks.CreatedBy = '" + Request.QueryString["userName"] + "'";
            tasks.DataBind();
            tasks.HeaderRow.TableSection = TableRowSection.TableHeader;
            tasks.UseAccessibleHeader = true;
        }
        private void QueryYourUsers()
        {
            assignedUsersSource.SelectCommand = "Select AssignedUser From MemberAssignments Where AssignedSupervisor = '" + Request.QueryString["userName"] + "'";
            users.DataBind();
            users.UseAccessibleHeader = true;
            users.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            AddUserGrid.UseAccessibleHeader = true;
            AddUserGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            categoryID = Convert.ToInt32(e.CommandArgument);
            foreach (GridViewRow row in AddUserGrid.Rows)
            {
                CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                box.Checked = false;
            }
            string queryString = "SELECT AssignedUser FROM CategoryAssignments WHERE CategoryID=@id";
            catUserLabel.Text = "Manage Users";

            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", categoryID.ToString());
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
            string queryString5 = "Insert into TaskAssignments (TaskID,AssignedUser,CategoryID) Select TaskID, c.AssignedUser, c.CategoryID From Tasks t inner join CategoryAssignments c on c.CategoryID = t.CategoryID where c.AssignedUser = @user And c.CategoryID = @id";
            string queryString6 = "Delete FROM TaskAssignments WHERE CategoryID=@id";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id",categoryID);
                cmd.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@id", categoryID);
                cmd2.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                cmd3.Parameters.AddWithValue("@id", categoryID);

                SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", categoryID);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd6 = new SqlCommand(queryString6, con);
                cmd6.Parameters.AddWithValue("@id", categoryID);

                con.Open();
                cmd3.ExecuteNonQuery();
                cmd6.ExecuteNonQuery();
                foreach (GridViewRow row in AddUserGrid.Rows)
                {
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;

                    if (box != null && box.Checked)
                    {
                        cmd.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd.ExecuteNonQuery();
                        cmd5.ExecuteNonQuery();
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
            UsersInTask.UseAccessibleHeader = true;
            UsersInTask.HeaderRow.TableSection = TableRowSection.TableHeader;
            taskID = Convert.ToInt32(e.CommandArgument);
            foreach (GridViewRow row in UsersInTask.Rows)
            {
                CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                box.Checked = false;
            }
            string queryString = "SELECT AssignedUser FROM TaskAssignments WHERE TaskID=@id";
            Label4.Text = "Manage Users in Task";

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
            string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID,CategoryID) Values (@user, @id, @catID)";
            string queryString6 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {  
                SqlCommand cmd4 = new SqlCommand(queryString4, con);
                cmd4.Parameters.AddWithValue("@id", taskID);

                SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", taskID);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);
                cmd5.Parameters.AddWithValue("@catID", categoryID);

                SqlCommand cmd6 = new SqlCommand(queryString6, con);
                cmd6.Parameters.AddWithValue("@id", taskID);
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

        protected void users_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            assignedUsername = Convert.ToString(e.CommandArgument);
            if (e.CommandName == "AddCategories")
            {
                categoryData.Visible = true;
                userTasks.Visible = false;
                AllCategoriesSource.SelectCommand = "SELECT CategoryName FROM Categories WHERE CreatedBy= '" + user + "'";
                AllCategoriesGridView.DataBind();
                AllCategoriesGridView.UseAccessibleHeader = true;
                AllCategoriesGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                foreach (GridViewRow row in UsersInTask.Rows)
                {
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                    box.Checked = false;
                }
                string queryString = "Select AssignedUser,CategoryName from CategoryAssignments inner join Categories on Categories.CategoryID = CategoryAssignments.CategoryID";
                using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        foreach (GridViewRow row in AllCategoriesGridView.Rows)
                        {
                            CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                            if (row.Cells[1].Text == dr["CategoryName"].ToString() && dr["AssignedUser"].ToString() == assignedUsername)
                            {
                                box.Checked = true;
                            }
                        }
                    }

                    con.Close();
                }

            }
            else
            {
                categoryData.Visible = false;
                userTasks.Visible = true;
                AllTasksDataSource.SelectCommand = "Select * From Tasks Inner Join Categories on Tasks.CategoryID = Categories.CategoryID Where Tasks.CreatedBy = '" + user + "'";
                AddTasksGridView.DataBind();
                AddTasksGridView.UseAccessibleHeader = true;
                AddTasksGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                foreach (GridViewRow row in AddTasksGridView.Rows)
                {
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                    box.Checked = false;
                }
                string queryString = "Select TaskAssignments.AssignedUser,TaskName from TaskAssignments inner join Tasks on Tasks.TaskID = TaskAssignments.TaskID";
                using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        foreach (GridViewRow row in AddTasksGridView.Rows)
                        {
                            CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                            if (row.Cells[2].Text == dr["TaskName"].ToString() && dr["AssignedUser"].ToString() == assignedUsername)
                            {
                                box.Checked = true;
                            }
                        }
                    }

                    con.Close();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "hideUsers();", true);
        }

        protected void AddCategoriesToUserBtn_Click(object sender, EventArgs e)
        {
            string queryString = "Insert Into CategoryAssignments (AssignedUser, CategoryID) Values (@user, @id)";
            string queryString2 = "DELETE FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            string queryString3 = "Insert into TaskAssignments (TaskID,AssignedUser,CategoryID) Select TaskID, c.AssignedUser, c.CategoryID From Tasks t inner join CategoryAssignments c on c.CategoryID = t.CategoryID where c.AssignedUser = @user And c.CategoryID = @id";
            string queryString4 = "Delete FROM TaskAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@user", assignedUsername);
                cmd.Parameters.AddWithValue("@id", DBNull.Value);

                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@user", assignedUsername);
                cmd2.Parameters.AddWithValue("@id", DBNull.Value);

                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                cmd3.Parameters.AddWithValue("@user", assignedUsername);
                cmd3.Parameters.AddWithValue("@id", DBNull.Value);

                SqlCommand cmd4 = new SqlCommand(queryString4, con);
                cmd4.Parameters.AddWithValue("@user", assignedUsername);
                cmd4.Parameters.AddWithValue("@id", DBNull.Value);

                con.Open();
                foreach (GridViewRow row in AllCategoriesGridView.Rows)
                {
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                    cmd.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd2.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd3.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd4.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd4.ExecuteNonQuery();
                    if (box != null && box.Checked)
                    {
                        cmd2.ExecuteNonQuery();

                        cmd.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                    }
                    else if (!box.Checked)
                    {
                        cmd2.ExecuteNonQuery();
                        cmd4.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showUsers();", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string queryString4 = "Delete FROM TaskAssignments WHERE TaskID=@id";
            string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID,CategoryID) Values (@user, @id, @catID)";
            string queryString6 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString4, con);
                cmd.Parameters.AddWithValue("@id", DBNull.Value);

                SqlCommand cmd2 = new SqlCommand(queryString5, con);
                cmd2.Parameters.AddWithValue("@id", DBNull.Value);
                cmd2.Parameters.AddWithValue("@user", assignedUsername);
                cmd2.Parameters.AddWithValue("@catID", DBNull.Value);

                SqlCommand cmd3 = new SqlCommand(queryString6, con);
                cmd3.Parameters.AddWithValue("@id", DBNull.Value);
                cmd3.Parameters.AddWithValue("@user", assignedUsername);

                con.Open();
                foreach (GridViewRow row in AddTasksGridView.Rows)
                {
                    cmd.Parameters["@id"].Value = Task.getTaskID(row.Cells[2].Text);
                    cmd2.Parameters["@id"].Value = Task.getTaskID(row.Cells[2].Text);
                    cmd2.Parameters["@catID"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd3.Parameters["@id"].Value = Task.getTaskID(row.Cells[2].Text);
                    CheckBox box = row.Cells[0].Controls[0] as CheckBox;
                    if (box != null && box.Checked)
                    {
                        cmd3.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                    }
                    else if (!box.Checked)
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showUsers();", true);
        }

        protected void RequestCatGrid_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            string queryString = "Insert Into RequestedCategories (CategoryID, IsApproved, RequestingUser) Values (@id, @bool, @user)";
            string queryString2 = "Select count(*) from RequestedCategories Where CategoryID = @id and RequestingUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd.Parameters.AddWithValue("@user", Request.QueryString["userName"].ToString());
                cmd.Parameters.AddWithValue("@bool", false);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd2.Parameters.AddWithValue("@user", Request.QueryString["userName"].ToString());

                
                con.Open();
                Int32 count = (Int32)cmd2.ExecuteScalar();
                try
                {
                    if (count == 0) { 
                    cmd.ExecuteNonQuery();
                    error.CssClass = "text-success";
                    error.Text = "Request Processed";
                    }
                    else
                    {
                        error.CssClass = "text-danger";
                        error.Text = "Request Exists";
                    }
                }
                catch (Exception e1)
                {
                    error.CssClass = "text-danger";
                    error.Text = "Request Not Processed";
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showCats();", true);
        }
      

    }
}