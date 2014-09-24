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
            // Selected user (used on edit user page)
            UserName = (Request.QueryString["username"] != null) ? Request.QueryString["username"] : "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show different content based on querystring
            
            // Create user page
            if (Request.QueryString["userpage"] == "createuser")
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.CreateUser;
                AssignedToContainer.Visible = false;
                if (!IsPostBack)
                {
                    BindSupervisors(AssignedTo);
                }
            }

            // Edit user page
            else if (Request.QueryString["userpage"] == "edituser" && 
                Membership.GetUser(UserName) != null && !Roles.IsUserInRole(UserName, "Manager"))
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.EditUser;

                // Populate list of supervisors if selected is a "user"
                if (Roles.IsUserInRole(UserName, "User"))
                {
                    if (!IsPostBack)
                    {
                        BindSupervisors(EditAssignedTo);
                    }
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
                UserList.DataSource = Member.CustomGetAllUsers();
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
            UserRoleContainer.Visible = false;

            // Add user to role
            Roles.AddUserToRole(CreateUserWizard.UserName, UserRole.SelectedValue);
           
            // Assign the user to supervisor
            if (UserRole.SelectedValue == "User")
            {
                Member.AssignToUser(CreateUserWizard.UserName, AssignedTo.SelectedValue);
            }
        }

        protected void EditUserButton_Click(object sender, EventArgs e)
        {
            var User = Membership.GetUser(UserName);
            string ErrorMessage = "";
            string SuccessMessage = "";

            EditErrorMessage.Text = String.Empty;
            EditSuccessMessage.Text = String.Empty;

            // Assigned to
            if (!String.IsNullOrEmpty(EditAssignedTo.SelectedValue))
            {
                if (!String.Equals(Member.UserAssignedTo(User.UserName).Trim(),
                    EditAssignedTo.SelectedValue.Trim()))
                {
                    Member.EditAssignToUser(User.UserName, EditAssignedTo.SelectedValue);
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

        protected void DeleteUserButton_Click(object sender, EventArgs e)
        {
            bool Error = false;

            if (Roles.IsUserInRole(UserName, "User"))
            {
                Member.RemoveAssignedUser(UserName);
            }
            else 
            {
                if (Member.SupervisorHasUsers(UserName))
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
            UserList.DataSource = Member.CustomGetAllUsers();
            UserList.DataBind();
        }

        private void BindSupervisors(DropDownList drp)
        {
            drp.DataSource = Roles.GetUsersInRole("Supervisor");
            drp.DataBind();

            Methods.AddBlankToDropDownList(drp);
        }
    }
}

