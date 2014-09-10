using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using SE.Classes;
using System.Data.SqlClient;

namespace SE
{
    public partial class Users : System.Web.UI.Page
    {
        private Panel AssignedToContainer;
        private DropDownList UserRole;
        private DropDownList AssignedTo;
        private String UserName = String.Empty;
      
        private enum UserPage
        {
            NotSet = -1,
            CreateUser = 0,
            EditUser = 1,
            ManageUsers = 2
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Items from CreateUserWizard
            UserRole = (DropDownList)CreateUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("UserRole");
            AssignedToContainer = (Panel)CreateUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("AssignedToContainer");
            AssignedTo = (DropDownList)CreateUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("AssignedTo");

            // Selected user (used on edit user page)
            UserName = Request.QueryString["username"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show different content based on querystring
            
            // Create user page
            if (Request.QueryString["userpage"] == "createuser")
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.CreateUser;
                AssignedToContainer.Visible = false;
                BindSupervisors(AssignedTo);
            }

            // Edit user page
            else if (Request.QueryString["userpage"] == "edituser" && 
                Membership.GetUser(UserName) != null)
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.EditUser;

                // Populate list of supervisors if selected is a "user"
                if (Roles.IsUserInRole(UserName, "User"))
                {
                    BindSupervisors(EditAssignedTo);
                }
                else
                {
                    EditAssignedToContainer.Visible = false;
                }
            }

            // Manage user page
            else
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.ManageUsers;
                UserList.DataSource = CustomGetAllUsers();
                UserList.DataBind();
            }
        }

        protected void UserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserRole.SelectedValue == "User")
            {
                AssignedToContainer.Visible = true;
            }
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            // Add user to role
            Roles.AddUserToRole(CreateUserWizard.UserName, UserRole.SelectedValue);
           
            // Assign the user to supervisor
            if (UserRole.SelectedValue == "User")
            {
                AssignToUser(CreateUserWizard.UserName, AssignedTo.SelectedValue);
            }
        }

        protected void EditUserButton_Click(object sender, EventArgs e)
        {
            EditUserNow();
        }

        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            bool Error = false;

            if (Roles.IsUserInRole(UserName, "User"))
            {
                RemoveAssignedUser(UserName);
            }
            else 
            {
                if (DBMethods.SupervisorHasUsers(UserName))
                {
                    Error = true;
                    EditErrorMessage.Text = "This account has users assigned to it. " +
                        "Please reassign them to another supervisor before deleting";
                }
            }

            if (!Error)
            {
                Membership.DeleteUser(UserName, true);
                Response.Redirect("~/Admin/Users.aspx");
            }
        }

        protected void UserList_Change(Object sender, DataGridPageChangedEventArgs e)
        {
            // Set CurrentPageIndex to the page the user clicked.
            UserList.CurrentPageIndex = e.NewPageIndex;

            // Rebind the data. 
            UserList.DataSource = CustomGetAllUsers();
            UserList.DataBind();
        }

        /// <summary>Populates a dataset of all users by username, email and user role
        /// </summary> 
        private DataSet CustomGetAllUsers()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables.Add("Users");

            MembershipUserCollection muc;
            muc = Membership.GetAllUsers();

            bool UserIsSupervisor;

            dt.Columns.Add("Username", Type.GetType("System.String"));
            dt.Columns.Add("Email", Type.GetType("System.String"));
            dt.Columns.Add("User Role", Type.GetType("System.String"));
            dt.Columns.Add("Assigned To", Type.GetType("System.String"));

            /* Here is the list of columns returned of the Membership.GetAllUsers() method
             * UserName, Email, PasswordQuestion, Comment, IsApproved
             * IsLockedOut, LastLockoutDate, CreationDate, LastLoginDate
             * LastActivityDate, LastPasswordChangedDate, IsOnline, ProviderName
             */

            foreach (MembershipUser mu in muc)
            {
                if (!Roles.IsUserInRole(mu.UserName, "Manager"))
                {
                    DataRow dr;
                    UserIsSupervisor = Roles.IsUserInRole(mu.UserName, "Supervisor");

                    dr = dt.NewRow();
                    dr["Username"] = "<a href='?userpage=edituser&username=" + mu.UserName + "'>" + mu.UserName + "</a>";
                    dr["Email"] = mu.Email;
                    dr["User Role"] = UserIsSupervisor ? "Supervisor" : "User";
                    dr["Assigned To"] = !UserIsSupervisor ? DBMethods.UserAssignedTo(mu.UserName) : "";

                    dt.Rows.Add(dr);
                }
            }
            return ds;
        }

        /// <summary>Updates a specific users information
        /// </summary> 
        private void EditUserNow() {
            var User = Membership.GetUser(UserName);
            string ErrorMessage = "";
            string SuccessMessage = "";

            EditErrorMessage.Text = String.Empty;
            EditSuccessMessage.Text = String.Empty;

            // Assigned to
            if (!String.IsNullOrEmpty(EditAssignedTo.SelectedValue))
            {
                if (!String.Equals(DBMethods.UserAssignedTo(User.UserName).Trim(),
                    EditAssignedTo.SelectedValue.Trim()))
                {
                    EditAssignToUser(User.UserName, EditAssignedTo.SelectedValue);
                    SuccessMessage += "User successfully reassigned.<br/>";
                }
                else
                {
                    ErrorMessage += "User is already assigned to this supervisor.<br/>";
                }
            }

            // User email
            if (!String.IsNullOrEmpty(EditEmail.Text))
            {
                User.Email = EditEmail.Text;
                Membership.UpdateUser(User);
                SuccessMessage += "Email successfully updated.<br/>";
            }

            // User password
            if (!String.IsNullOrEmpty(EditPassword.Text))
            {
                bool Error = false;

                // Password is less then required length
                if (EditPassword.Text.Length < Membership.MinRequiredPasswordLength)
                {
                    ErrorMessage += "Password must be at least " +
                        Membership.MinRequiredPasswordLength + " characters.<br/>";
                    Error = true;
                }

                // Password does not contain minimum special characters
                if (EditPassword.Text.Count(c => !char.IsLetterOrDigit(c)) <
                    Membership.MinRequiredNonAlphanumericCharacters)
                {
                    ErrorMessage += "Password must contain at least " + 
                        Membership.MinRequiredNonAlphanumericCharacters + 
                        " non-alphanumeric characters.<br/>";
                    Error = true;
                }

                // Success
                if (!Error)
                {
                    User.ChangePassword(User.ResetPassword(), EditPassword.Text);
                    SuccessMessage += "Password successfully updated.<br/>";
                }
            }

            // Display messages
            EditErrorMessage.Text = ErrorMessage;
            EditSuccessMessage.Text = SuccessMessage;

            // Clear fields and focus email field
            EditEmail.Text = String.Empty;
            EditPassword.Text = String.Empty;
            EditConfirmPassword.Text = String.Empty;
            EditAssignedTo.ClearSelection();
            EditEmail.Focus();
        }

        private void BindSupervisors(DropDownList drp)
        {
            if (!IsPostBack)
            {
                drp.DataSource = Roles.GetUsersInRole("Supervisor");
                drp.DataBind();

                // Add default blank list item
                drp.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                drp.SelectedIndex = 0;
            }
        }

        /// <summary>Assign user to supervisor
        /// </summary> 
        private void AssignToUser(string User, string Supervisor)
        {
            string queryString =
                "INSERT INTO MemberAssignments (AssignedUser, AssignedSupervisor) " +
                "VALUES (@user,@supervisor)";

            using (SqlConnection con = new SqlConnection(
                DBMethods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);
                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Edit assign user to supervisor
        /// </summary> 
        private void EditAssignToUser(string User, string Supervisor)
        {
            string queryString =
                "UPDATE MemberAssignments " +
                "SET AssignedSupervisor=@supervisor " +
                "WHERE AssignedUser=@user";

            using (SqlConnection con = new SqlConnection(
                DBMethods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);
                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Remove user assignment row
        /// </summary> 
        private void RemoveAssignedUser(string User)
        {
            string queryString =
                "DELETE FROM MemberAssignments " +
                "WHERE AssignedUser=@user";

            using (SqlConnection con = new SqlConnection(
                DBMethods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}

