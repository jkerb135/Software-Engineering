using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace SE
{
    public partial class Users : System.Web.UI.Page
    {
        private DropDownList UserRole;
        private enum UserPage
        {
            NotSet = -1,
            CreateUser = 0,
            EditUser = 1,
            ManageUsers = 2
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UserRole = (DropDownList)CreateUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("UserRole");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["userpage"] == "createuser")
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.CreateUser;
            }
            else if (Request.QueryString["userpage"] == "edituser" && 
                Membership.GetUser(Request.QueryString["username"]) != null)
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.EditUser;
            }
            else
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.ManageUsers;
                UserList.DataSource = CustomGetAllUsers();
                UserList.DataBind();
            }
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            Roles.AddUserToRole(CreateUserWizard.UserName, UserRole.SelectedValue);
        }

        protected void EditUserButton_Click(object sender, EventArgs e)
        {
            EditUserNow();
        }

        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            Membership.DeleteUser(Request.QueryString["username"], true);
            Response.Redirect("~/Admin/Users.aspx");
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

            dt.Columns.Add("Username", Type.GetType("System.String"));
            dt.Columns.Add("Email", Type.GetType("System.String"));
            dt.Columns.Add("User Role", Type.GetType("System.String"));

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
                    dr = dt.NewRow();
                    dr["Username"] = "<a href='?userpage=edituser&username=" + mu.UserName + "'>" + mu.UserName + "</a>";
                    dr["Email"] = mu.Email;
                    dr["User Role"] = Roles.IsUserInRole(mu.UserName, "Supervisor") ? "Supervisor" : "User";

                    dt.Rows.Add(dr);
                }
            }
            return ds;
        }

        /// <summary>Updates a specific users information
        /// </summary> 
        private void EditUserNow() {
            var User = Membership.GetUser(Request.QueryString["username"]);
            String Message = "";

            EditMessage.Text = String.Empty;

            // User email
            if (!String.IsNullOrEmpty(EditEmail.Text))
            {
                User.Email = EditEmail.Text;
                Membership.UpdateUser(User);
                Message += "Email successfully updated.<br/>";
            }

            // User password
            if (!String.IsNullOrEmpty(EditPassword.Text))
            {
                Boolean Error = false;

                // Passwords do not match
                if (EditPassword.Text.Trim() != EditConfirmPassword.Text.Trim())
                {
                    Message += "Passwords must match.<br/>";
                    Error = true;
                }

                // Password is less then required length
                if (EditPassword.Text.Length < Membership.MinRequiredPasswordLength)
                {
                    Message += "Password must be at least " +
                        Membership.MinRequiredPasswordLength + " characters.<br/>";
                    Error = true;
                }

                // Password does not contain minimum special characters
                if (EditPassword.Text.Count(c => !char.IsLetterOrDigit(c)) <
                    Membership.MinRequiredNonAlphanumericCharacters)
                {
                    Message += "Password must contain at least " + 
                        Membership.MinRequiredNonAlphanumericCharacters + 
                        " non-alphanumeric characters.<br/>";
                    Error = true;
                }

                // Success
                if (!Error)
                {
                    User.ChangePassword(User.ResetPassword(), EditPassword.Text);
                    Message += "Password successfully updated.<br/>";
                }
            }

            // User role
            if (!String.IsNullOrEmpty(EditUserRole.Text))
            {
                if (!Roles.IsUserInRole(User.UserName, EditUserRole.SelectedValue))
                {
                    String RemoveRole = (EditUserRole.SelectedValue == "Supervisor") ? "User" : "Supervisor";
                    Roles.AddUserToRole(User.UserName, EditUserRole.SelectedValue);

                    // Remove users previous role
                    Roles.RemoveUserFromRole(User.UserName, RemoveRole);

                    Message += "User role successfully updated.";
                }
                else
                {
                    Message += "User is already a " + EditUserRole.SelectedValue + ".";
                }
            }

            // Display Message
            EditMessage.Text = Message;

            // Clear fields
            EditEmail.Text = String.Empty;
            EditPassword.Text = String.Empty;
            EditConfirmPassword.Text = String.Empty;
            EditUserRole.ClearSelection();
            EditEmail.Focus();
        }
    }
}