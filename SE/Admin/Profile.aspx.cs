﻿using SE.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace SE.Admin
{
    public partial class Profile : System.Web.UI.Page
    {
        public static int categoryID, taskID;
        public static string assignedUsername;
        public static bool pendingRequest = false;
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
                //categories.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
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
            string queryString = "SELECT AssignedUser FROM CategoryAssignments WHERE CategoryID=@id";
            catUserLabel.Text = "Manage Users in Category: " + Category.getCategoryName(categoryID);
            foreach (GridViewRow row in AddUserGrid.Rows)
            {
                CheckBox box = (CheckBox)row.FindControl("catUsersChk");
                if (box != null)
                {
                    box.Checked = false;
                }

            }
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
                        CheckBox box = (CheckBox)row.FindControl("catUsersChk");
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
            try {
            string queryString = "Insert Into CategoryAssignments (AssignedUser, CategoryID) Values (@user, @id)";
            string queryString2 = "Select Count(*) FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            string queryString3 = "Delete FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
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
                cmd3.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", categoryID);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd6 = new SqlCommand(queryString6, con);
                cmd6.Parameters.AddWithValue("@id", categoryID);

                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                bool flag = false;
                foreach (GridViewRow row in AddUserGrid.Rows)
                {
                    CheckBox box = (CheckBox)row.FindControl("catUsersChk");
                    cmd2.Parameters["@user"].Value = row.Cells[1].Text;
                    Int32 count = (Int32)cmd2.ExecuteScalar();
                    if (box != null && box.Checked && count == 0)
                    {
                        cmd.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd.ExecuteNonQuery();
                        cmd5.ExecuteNonQuery();
                        lblModalBody.Text += "Added: " + row.Cells[1].Text + " into " + Category.getCategoryName(categoryID) + "<br/>";
                        flag = true;
                    }
                    else if (!box.Checked && count == 1)
                    {
                        lblModalBody.Text += "Removed: " + row.Cells[1].Text + " from " + Category.getCategoryName(categoryID) + "<br/>";
                        cmd3.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd3.ExecuteNonQuery();
                        cmd6.ExecuteNonQuery();
                        flag = true;
                    }
                }
                con.Close();
                if (flag){
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
            foreach (GridViewRow row in UsersInTask.Rows)
            {
                CheckBox box = (CheckBox)row.FindControl("UsersInTaskChk");
                box.Checked = false;
            }
            string queryString = "SELECT AssignedUser FROM TaskAssignments WHERE TaskID=@id";
            Label4.Text = "Manage Users in Task: " + Task.GetTaskName(taskID);

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
                        CheckBox box = (CheckBox)row.FindControl("UsersInTaskChk");
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
            string queryString4 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID,CategoryID) Values (@user, @id, @catID)";
            string queryString6 = "Select Count(*) FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {  
                SqlCommand cmd4 = new SqlCommand(queryString4, con);
                cmd4.Parameters.AddWithValue("@id", taskID);
                cmd4.Parameters.AddWithValue("@user", DBNull.Value);

                SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@id", taskID);
                cmd5.Parameters.AddWithValue("@user", DBNull.Value);
                cmd5.Parameters.AddWithValue("@catID", categoryID);

                SqlCommand cmd6 = new SqlCommand(queryString6, con);
                cmd6.Parameters.AddWithValue("@id", taskID);
                cmd6.Parameters.AddWithValue("@user", DBNull.Value);

                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                bool flag = false;
                foreach (GridViewRow row in UsersInTask.Rows)
                {
                    cmd4.Parameters["@user"].Value = row.Cells[1].Text;
                    cmd6.Parameters["@user"].Value = row.Cells[1].Text;
                    Int32 count = (Int32)cmd6.ExecuteScalar();
                    CheckBox box = (CheckBox)row.FindControl("UsersInTaskChk");

                    if (box != null && box.Checked && count == 0)
                    {
                        lblModalBody.Text += "Added: " + row.Cells[1].Text + " into " + Task.GetTaskName(taskID) + "<br/>";
                        cmd5.Parameters["@user"].Value = row.Cells[1].Text;
                        cmd5.ExecuteNonQuery();
                        flag = true;
                    }
                    else if (!box.Checked && count == 1)
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
                foreach (GridViewRow row in AllCategoriesGridView.Rows)
                {
                    CheckBox box = (CheckBox)row.FindControl("AllCategoriesChk");
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
                            CheckBox box = (CheckBox)row.FindControl("AllCategoriesChk");
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
                Label5.Text = "Manage " + e.CommandArgument.ToString() + " Tasks";
                categoryData.Visible = false;
                userTasks.Visible = true;
                AllTasksDataSource.SelectCommand = "Select * From Tasks Inner Join Categories on Tasks.CategoryID = Categories.CategoryID Where Tasks.CreatedBy = '" + user + "'";
                AddTasksGridView.DataBind();
                AddTasksGridView.UseAccessibleHeader = true;
                AddTasksGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                foreach (GridViewRow row in AddTasksGridView.Rows)
                {
                    CheckBox box = (CheckBox)row.FindControl("AddTaskChk");
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
                            CheckBox box = (CheckBox)row.FindControl("AddTaskChk");
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
            string queryString2 = "Select Count(*) FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            
            string queryString3 = "Insert into TaskAssignments (TaskID,AssignedUser,CategoryID) Select TaskID, c.AssignedUser, c.CategoryID From Tasks t inner join CategoryAssignments c on c.CategoryID = t.CategoryID where c.AssignedUser = @user And c.CategoryID = @id";
            string queryString4 = "Delete FROM TaskAssignments WHERE CategoryID=@id AND AssignedUser=@user";
            string queryString5 = "Delete FROM CategoryAssignments WHERE CategoryID=@id AND AssignedUser=@user";

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

                SqlCommand cmd5 = new SqlCommand(queryString5, con);
                cmd5.Parameters.AddWithValue("@user", assignedUsername);
                cmd5.Parameters.AddWithValue("@id", DBNull.Value);

                
                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                bool flag = false;
                foreach (GridViewRow row in AllCategoriesGridView.Rows)
                {
                    CheckBox box = (CheckBox)row.FindControl("AllCategoriesChk");
                    cmd.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd2.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd3.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd4.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd5.Parameters["@id"].Value = Category.getCategoryID(row.Cells[1].Text);
                    if (box != null && box.Checked && (Int32)cmd2.ExecuteScalar() == 0)
                    {
                        lblModalBody.Text += "Added: " + assignedUsername.ToString() + " into " + row.Cells[1].Text + "<br/>";
                        cmd.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                        flag = true;
                    }
                    else if (!box.Checked && (Int32)cmd2.ExecuteScalar() == 1)
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
            string queryString4 = "Delete FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            string queryString5 = "Insert Into TaskAssignments (AssignedUser, TaskID,CategoryID) Values (@user, @id, @catID)";
            string queryString6 = "Select Count(*) FROM TaskAssignments WHERE TaskID=@id AND AssignedUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString4, con);
                cmd.Parameters.AddWithValue("@id", DBNull.Value);
                cmd.Parameters.AddWithValue("@user", assignedUsername);

                SqlCommand cmd2 = new SqlCommand(queryString5, con);
                cmd2.Parameters.AddWithValue("@id", DBNull.Value);
                cmd2.Parameters.AddWithValue("@user", assignedUsername);
                cmd2.Parameters.AddWithValue("@catID", DBNull.Value);

                SqlCommand cmd3 = new SqlCommand(queryString6, con);
                cmd3.Parameters.AddWithValue("@id", DBNull.Value);
                cmd3.Parameters.AddWithValue("@user", assignedUsername);

                con.Open();
                lblModalTitle.Text = "Request is Successful";
                lblModalBody.Text = String.Empty;
                bool flag = false;
                foreach (GridViewRow row in AddTasksGridView.Rows)
                {
                    cmd.Parameters["@id"].Value = Task.getTaskID(row.Cells[2].Text);
                    cmd2.Parameters["@id"].Value = Task.getTaskID(row.Cells[2].Text);
                    cmd2.Parameters["@catID"].Value = Category.getCategoryID(row.Cells[1].Text);
                    cmd3.Parameters["@id"].Value = Task.getTaskID(row.Cells[2].Text);
                    Int32 count = (Int32)cmd3.ExecuteScalar();
                    CheckBox box = (CheckBox)row.FindControl("AddTaskChk");
                    if (box != null && box.Checked && count == 0)
                    {
                        lblModalBody.Text += "Added: " + assignedUsername.ToString() + " into " + row.Cells[2].Text + "<br/>";
                        cmd2.ExecuteNonQuery();
                        flag = true;
                    }
                    else if (!box.Checked && count == 1)
                    {
                        lblModalBody.Text += "Removed: " + assignedUsername.ToString() + " from " + row.Cells[2].Text + "<br/>";
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
            string queryString = "Insert Into RequestedCategories (CategoryID, IsApproved, RequestingUser) Values (@id, @bool, @user)";
            string queryString2 = "Select count(*) from RequestedCategories Where CategoryID = @id and RequestingUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@bool", false);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd2.Parameters.AddWithValue("@user", user);

                
                con.Open();
                lblModalTitle.Text = "Request Pending";
                lblModalBody.Text = String.Empty;
                pendingRequest = false;
                Int32 count = (Int32)cmd2.ExecuteScalar();
                try
                {
                    if (count == 0) { 
                    cmd.ExecuteNonQuery();
                    lblModalTitle.Text = "Request is Successful";
                    lblModalBody.Text += "Your request has been submited. The supervisors whos category you have request will have to accept the request in order for you to be able to access it. A notification will be visible on the supervisors next login.";
                    pendingRequest = true;
                    }
                    else
                    {
                        error.CssClass = "text-danger";
                    }
                }
                catch (Exception e1)
                {
                    lblModalTitle.Text = "Request is Unsuccessful";
                    lblModalBody.Text += "There was an error processing you category request please try again later";
                    pendingRequest = false;
                }
                con.Close();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                queryCatRequestStatus();
                upModal.Update();
            }
        }

        [WebMethod]
        public void queryCatRequestStatus()
        {
            string queryString = "Select IsApproved,CategoryID From RequestedCategories Where RequestingUser=@user";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@user", user);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    foreach (GridViewRow row in RequestCatGrid.Rows)
                    {
                        bool approved = Convert.ToBoolean(dr["IsApproved"]);
                        Button request = (Button)row.FindControl("RequestCat");
                        if (approved && Convert.ToInt32(dr["CategoryID"]) == Category.getCategoryID(row.Cells[0].Text))
                        {
                            request.Text = "Approved";
                            request.CssClass = "btn btn-success form-control";
                            request.Enabled = false;
                        }
                        else if (!approved && Convert.ToInt32(dr["CategoryID"]) == Category.getCategoryID(row.Cells[0].Text))
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
            queryCatRequestStatus();
        }

    }
}