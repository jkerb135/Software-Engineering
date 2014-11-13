using System.Globalization;
using System.Linq;
using SE.Classes;
using System;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace SE.Admin
{
    public partial class Profile : System.Web.UI.Page
    {
        public static int categoryID, taskID;
        public static string assignedUsername;
        public static bool pendingRequest = false;
        readonly string user = Membership.GetUser().UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.Equals(Request.QueryString["userName"], user, StringComparison.CurrentCultureIgnoreCase))
            {
                YourInfo.Visible = false;
                OtherInfo.Visible = true;
                profileHeader.Text = "Request from " + Request.QueryString["userName"];
            }
            else
            {
                YourInfo.Visible = true;
                OtherInfo.Visible = false;
            }
            if (IsPostBack) return;
            //categories.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            Label1.Text = " Categories";
            Label2.Text = " Tasks";
            Label3.Text = " Users";
            QueryYourCategories();
            QueryYourTasks();
            QueryYourUsers();
            UsersInCategory.SelectCommand = addUserDataSource.SelectCommand = "select UserName,IsApproved,LastActivityDate from aspnet_Membership as p inner join aspnet_Users as r on p.UserId = r.UserId inner join aspnet_UsersInRoles as t on t.UserId = p.UserId inner join MemberAssignments as z on z.AssignedUser = r.UserName where t.RoleId = 'F0D05C09-B992-45A3-8F5E-4EA772C760FD' And AssignedSupervisor = '" + Membership.GetUser() + "'";
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
            const string queryString = "SELECT AssignedUser FROM CategoryAssignments WHERE CategoryID=@id";
            catUserLabel.Text = "Manage Users in Category: " + Category.GetCategoryName(categoryID);
            foreach (var box in AddUserGrid.Rows.Cast<GridViewRow>().Select(row => (CheckBox)row.FindControl("catUsersChk")).Where(box => box != null))
            {
                box.Checked = false;
            }
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", categoryID.ToString());
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    foreach (GridViewRow row in AddUserGrid.Rows)
                    {
                        var box = (CheckBox)row.FindControl("catUsersChk");
                        if (row.Cells[2].Text == "False")
                        {
                            box.Enabled = false;
                            row.Cells[2].Text = "User is Inactive";
                        }
                        else
                        {
                            box.Enabled = true;
                            row.Cells[2].Text = "User is Active";
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
            try
            {
                const string queryString = "Insert Into CategoryAssignments (AssignedUser, CategoryID) Values (@user, @id)";
                const string queryString2 = "Select Count(*) FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
                const string queryString3 = "Delete FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
                const string queryString5 = "Insert into TaskAssignments (TaskID,AssignedUser,CategoryID) Select TaskID, c.AssignedUser, c.CategoryID From Tasks t inner join CategoryAssignments c on c.CategoryID = t.CategoryID where c.AssignedUser = @user And c.CategoryID = @id";
                const string queryString6 = "Delete FROM TaskAssignments WHERE CategoryID=@id";
                using (var con = new SqlConnection(Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    cmd.Parameters.AddWithValue("@id", categoryID);
                    cmd.Parameters.AddWithValue("@user", DBNull.Value);

                    var cmd2 = new SqlCommand(queryString2, con);
                    cmd2.Parameters.AddWithValue("@id", categoryID);
                    cmd2.Parameters.AddWithValue("@user", DBNull.Value);

                    var cmd3 = new SqlCommand(queryString3, con);
                    cmd3.Parameters.AddWithValue("@id", categoryID);
                    cmd3.Parameters.AddWithValue("@user", DBNull.Value);

                    var cmd5 = new SqlCommand(queryString5, con);
                    cmd5.Parameters.AddWithValue("@id", categoryID);
                    cmd5.Parameters.AddWithValue("@user", DBNull.Value);

                    var cmd6 = new SqlCommand(queryString6, con);
                    cmd6.Parameters.AddWithValue("@id", categoryID);

                    con.Open();
                    lblModalTitle.Text = "Request is Successful";
                    lblModalBody.Text = String.Empty;
                    var flag = false;
                    foreach (GridViewRow row in AddUserGrid.Rows)
                    {
                        var box = (CheckBox)row.FindControl("catUsersChk");
                        cmd2.Parameters["@user"].Value = row.Cells[1].Text;
                        var count = (Int32)cmd2.ExecuteScalar();
                        if (box != null && box.Checked && count == 0)
                        {
                            cmd.Parameters["@user"].Value = row.Cells[1].Text;
                            cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                            cmd.ExecuteNonQuery();
                            cmd5.ExecuteNonQuery();
                            lblModalBody.Text += "Added: " + row.Cells[1].Text + " into " + Category.GetCategoryName(categoryID) + "<br/>";
                            flag = true;
                        }
                        else if (!box.Checked && count == 1)
                        {
                            lblModalBody.Text += "Removed: " + row.Cells[1].Text + " from " + Category.GetCategoryName(categoryID) + "<br/>";
                            cmd3.Parameters["@user"].Value = row.Cells[1].Text;
                            cmd3.ExecuteNonQuery();
                            cmd6.ExecuteNonQuery();
                            flag = true;
                        }
                    }
                    con.Close();
                    if (flag)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showCats();", true);
            }
            catch (Exception ex)
            {
                lblModalTitle.Text = "Error Processing Request";
                lblModalBody.Text = ex.ToString();
            }
        }

        protected void TaskGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UsersInTask.UseAccessibleHeader = true;
            UsersInTask.HeaderRow.TableSection = TableRowSection.TableHeader;
            taskID = Convert.ToInt32(e.CommandArgument);
            foreach (var box in from GridViewRow row in UsersInTask.Rows select (CheckBox)row.FindControl("UsersInTaskChk"))
            {
                box.Checked = false;
            }
            const string queryString = "SELECT AssignedUser FROM TaskAssignments WHERE TaskID=@id";
            Label4.Text = "Manage Users in Task: " + Task.GetTaskName(taskID);

            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", taskID);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    foreach (GridViewRow row in UsersInTask.Rows)
                    {
                        var box = (CheckBox)row.FindControl("UsersInTaskChk");
                        if (row.Cells[2].Text == "False")
                        {
                            box.Enabled = false;
                            row.Cells[2].Text = "User is Inactive";
                        }
                        else
                        {
                            row.Cells[2].Text = "User is Active";
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
            const string queryString4 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            const string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID,CategoryID) Values (@user, @id, @catID)";
            const string queryString6 = "Select Count(*) FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd4 = new SqlCommand(queryString4, con);
                cmd4.Parameters.AddWithValue("@id", taskID);
                cmd4.Parameters.AddWithValue("@user", DBNull.Value);

                var cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", taskID);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);
                cmd5.Parameters.AddWithValue("@catID", categoryID);

                var cmd6 = new SqlCommand(queryString6, con);
                cmd6.Parameters.AddWithValue("@id", taskID);
                cmd6.Parameters.AddWithValue("@user", DBNull.Value);

                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                var flag = false;
                foreach (GridViewRow row in UsersInTask.Rows)
                {
                    cmd4.Parameters["@user"].Value = row.Cells[1].Text;
                    cmd6.Parameters["@user"].Value = row.Cells[1].Text;
                    var count = (Int32)cmd6.ExecuteScalar();
                    var box = (CheckBox)row.FindControl("UsersInTaskChk");

                    if (box != null && box.Checked && count == 0)
                    {
                        lblModalBody.Text += "Added: " + row.Cells[1].Text + " into " + Task.GetTaskName(taskID) + "<br/>";
                        cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd5.ExecuteNonQuery();
                        flag = true;
                    }
                    else if (box != null && (!box.Checked && count == 1))
                    {
                        lblModalBody.Text += "Removed: " + row.Cells[1].Text + " from " + Task.GetTaskName(taskID) + "<br/>";
                        cmd4.ExecuteNonQuery();
                        flag = true;
                    }
                }
                con.Close();
                if (flag)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showTasks();", true);
            }
        }

        protected void users_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            assignedUsername = Convert.ToString(e.CommandArgument);
            if (e.CommandName == "AddCategories")
            {
                Label6.Text = "Manage " + e.CommandArgument.ToString() + " Categories";
                categoryData.Visible = true;
                userTasks.Visible = false;
                AllCategoriesSource.SelectCommand = "SELECT CategoryName FROM Categories WHERE CreatedBy= '" + user + "'";
                AllCategoriesGridView.DataBind();
                AllCategoriesGridView.UseAccessibleHeader = true;
                AllCategoriesGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                foreach (var box in from GridViewRow row in AllCategoriesGridView.Rows select (CheckBox)row.FindControl("AllCategoriesChk"))
                {
                    box.Checked = false;
                }
                const string queryString = "Select AssignedUser,CategoryName from CategoryAssignments inner join Categories on Categories.CategoryID = CategoryAssignments.CategoryID";
                using (var con = new SqlConnection(Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        foreach (var box in from GridViewRow row in AllCategoriesGridView.Rows let box = (CheckBox)row.FindControl("AllCategoriesChk") where row.Cells[1].Text == dr["CategoryName"].ToString() && dr["AssignedUser"].ToString() == assignedUsername select box)
                        {
                            box.Checked = true;
                        }
                    }

                    con.Close();
                }

            }
            else
            {
                Label5.Text = "Manage " + e.CommandArgument.ToString() + " Tasks";
                categoryData.Visible = false;
                userTasks.Visible = true;
                AllTasksDataSource.SelectCommand = "Select * From Tasks Inner Join Categories on Tasks.CategoryID = Categories.CategoryID Where Tasks.CreatedBy = '" + user + "'";
                AddTasksGridView.DataBind();
                AddTasksGridView.UseAccessibleHeader = true;
                AddTasksGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                foreach (var box in from GridViewRow row in AddTasksGridView.Rows select (CheckBox)row.FindControl("AddTaskChk"))
                {
                    box.Checked = false;
                }
                const string queryString = "Select TaskAssignments.AssignedUser,TaskName from TaskAssignments inner join Tasks on Tasks.TaskID = TaskAssignments.TaskID";
                using (var con = new SqlConnection(Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        foreach (var box in from GridViewRow row in AddTasksGridView.Rows let box = (CheckBox)row.FindControl("AddTaskChk") where row.Cells[2].Text == dr["TaskName"].ToString() && dr["AssignedUser"].ToString() == assignedUsername select box)
                        {
                            box.Checked = true;
                        }
                    }

                    con.Close();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "hideUsers();", true);
        }

        protected void AddCategoriesToUserBtn_Click(object sender, EventArgs e)
        {
            const string queryString = "Insert Into CategoryAssignments (AssignedUser, CategoryID) Values (@user, @id)";
            const string queryString2 = "Select Count(*) FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";

            const string queryString3 = "Insert into TaskAssignments (TaskID,AssignedUser,CategoryID) Select TaskID, c.AssignedUser, c.CategoryID From Tasks t inner join CategoryAssignments c on c.CategoryID = t.CategoryID where c.AssignedUser = @user And c.CategoryID = @id";
            const string queryString4 = "Delete FROM TaskAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            const string queryString5 = "Delete FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";

            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@user", assignedUsername);
                cmd.Parameters.AddWithValue("@id", DBNull.Value);

                var cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@user", assignedUsername);
                cmd2.Parameters.AddWithValue("@id", DBNull.Value);

                var cmd3 = new SqlCommand(queryString3, con);
                cmd3.Parameters.AddWithValue("@user", assignedUsername);
                cmd3.Parameters.AddWithValue("@id", DBNull.Value);

                var cmd4 = new SqlCommand(queryString4, con);
                cmd4.Parameters.AddWithValue("@user", assignedUsername);
                cmd4.Parameters.AddWithValue("@id", DBNull.Value);

                var cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@user", assignedUsername);
                cmd5.Parameters.AddWithValue("@id", DBNull.Value);


                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                var flag = false;
                foreach (GridViewRow row in AllCategoriesGridView.Rows)
                {
                    var box = (CheckBox)row.FindControl("AllCategoriesChk");
                    cmd.Parameters["@id"].Value = Category.GetCategoryId(row.Cells[1].Text);
                    cmd2.Parameters["@id"].Value = Category.GetCategoryId(row.Cells[1].Text);
                    cmd3.Parameters["@id"].Value = Category.GetCategoryId(row.Cells[1].Text);
                    cmd4.Parameters["@id"].Value = Category.GetCategoryId(row.Cells[1].Text);
                    cmd5.Parameters["@id"].Value = Category.GetCategoryId(row.Cells[1].Text);
                    if (box != null && box.Checked && (Int32)cmd2.ExecuteScalar() == 0)
                    {
                        lblModalBody.Text += "Added: " + assignedUsername.ToString() + " into " + row.Cells[1].Text + "<br/>";
                        cmd.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                        flag = true;
                    }
                    else if (box != null && (!box.Checked && (Int32)cmd2.ExecuteScalar() == 1))
                    {
                        lblModalBody.Text += "Removed: " + assignedUsername.ToString() + " from " + row.Cells[1].Text + "<br/>";
                        cmd4.ExecuteNonQuery();
                        cmd5.ExecuteNonQuery();
                        flag = true;
                    }
                }
                if (flag)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }

                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showUsers();", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            const string queryString4 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            const string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID,CategoryID) Values (@user, @id, @catID)";
            const string queryString6 = "Select Count(*) FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString4, con);
                cmd.Parameters.AddWithValue("@id", DBNull.Value);
                cmd.Parameters.AddWithValue("@user", assignedUsername);

                var cmd2 = new SqlCommand(queryString5, con);
                cmd2.Parameters.AddWithValue("@id", DBNull.Value);
                cmd2.Parameters.AddWithValue("@user", assignedUsername);
                cmd2.Parameters.AddWithValue("@catID", DBNull.Value);

                var cmd3 = new SqlCommand(queryString6, con);
                cmd3.Parameters.AddWithValue("@id", DBNull.Value);
                cmd3.Parameters.AddWithValue("@user", assignedUsername);

                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                var flag = false;
                foreach (GridViewRow row in AddTasksGridView.Rows)
                {
                    cmd.Parameters["@id"].Value = Task.GetTaskId(row.Cells[2].Text);
                    cmd2.Parameters["@id"].Value = Task.GetTaskId(row.Cells[2].Text);
                    cmd2.Parameters["@catID"].Value = Category.GetCategoryId(row.Cells[1].Text);
                    cmd3.Parameters["@id"].Value = Task.GetTaskId(row.Cells[2].Text);
                    var count = (Int32)cmd3.ExecuteScalar();
                    var box = (CheckBox)row.FindControl("AddTaskChk");
                    if (box != null && box.Checked && count == 0)
                    {
                        lblModalBody.Text += "Added: " + assignedUsername.ToString(CultureInfo.InvariantCulture) + " into " + row.Cells[2].Text + "<br/>";
                        cmd2.ExecuteNonQuery();
                        flag = true;
                    }
                    else if (!box.Checked && count == 1)
                    {
                        lblModalBody.Text += "Removed: " + assignedUsername + " from " + row.Cells[2].Text + "<br/>";
                        cmd.ExecuteNonQuery();
                        flag = true;
                    }
                }
                con.Close();
                if (flag)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showUsers();", true);
        }

        protected void RequestCatGrid_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "requestCat();", true);
            const string queryString = "Insert Into RequestedCategories (CategoryID, IsApproved, RequestingUser,CreatedBy,Date) Values (@id, @bool, @user,@owner,@date)";
            const string queryString2 = "Select count(*) from RequestedCategories Where CategoryID = @id and RequestingUser=@user";
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@bool", false);
                cmd.Parameters.AddWithValue("@owner", Request.QueryString["userName"].ToString());
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                var cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd2.Parameters.AddWithValue("@user", user);


                con.Open();
                pendingRequest = false;
                var count = (Int32)cmd2.ExecuteScalar();
                    if (count == 0)
                    {
                        cmd.ExecuteNonQuery();
                        pendingRequest = true;
                    }
                    else
                    {
                        error.CssClass = "text-danger";
                    }
                con.Close();
                queryCatRequestStatus();
            }
        }

        [WebMethod]
        public void queryCatRequestStatus()
        {
            const string queryString = "Select IsApproved,CategoryID From RequestedCategories Where RequestingUser=@user";
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@user", user);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    foreach (GridViewRow row in RequestCatGrid.Rows)
                    {
                        var approved = Convert.ToBoolean(dr["IsApproved"]);
                        var request = (Button)row.FindControl("RequestCat");
                        if (approved && Convert.ToInt32(dr["CategoryID"]) == Category.GetCategoryId(row.Cells[0].Text))
                        {
                            request.Text = "Approved";
                            request.CssClass = "btn btn-success form-control";
                            request.Enabled = false;
                        }
                        else if (!approved && Convert.ToInt32(dr["CategoryID"]) == Category.GetCategoryId(row.Cells[0].Text))
                        {
                            request.Text = "Pending";
                            request.CssClass = "btn btn-warning form-control";
                            request.Enabled = false;
                        }
                    }
                }

                con.Close();
            }
        }

        protected void RequestCatGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //RequestCatGrid.DataBind();
            queryCatRequestStatus();
        }

    }
}