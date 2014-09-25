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
using System.Diagnostics;

namespace SE
{
    public partial class Users : System.Web.UI.Page
    {
        private String SelectedUserName = String.Empty;
      
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
            SelectedUserName = (Request.QueryString["username"] != null) ? Request.QueryString["username"] : "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show different content based on querystring
            
            // Create user page
            if (!IsPostBack)
            {
                if (Request.QueryString["userpage"] == "createuser")
                {
                    UsersMultiView.ActiveViewIndex = (int)UserPage.CreateUser;
                    AssignedToContainer.Visible = false;
                    BindSupervisors(AssignedTo);
                }

                // Edit user page
                else if (Request.QueryString["userpage"] == "edituser" &&
                    Membership.GetUser(SelectedUserName) != null && !Roles.IsUserInRole(SelectedUserName, "Manager"))
                {
                    UsersMultiView.ActiveViewIndex = (int)UserPage.EditUser;

                    // Populate list of supervisors if selected is a "user"
                    if (Roles.IsUserInRole(SelectedUserName, "User"))
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
                    ShowManageUserPage();
                }
            }
        }

        protected void UserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserRole.SelectedValue == "User")
            {
                AssignedToContainer.Visible = true;
            }
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            string ErrorMessage = "";

            if (Member.ValidatePassword(Password.Text, ref ErrorMessage))
            {
                if (Membership.GetUser(UserName.Text) == null)
                {
                    // Add user to role
                    Roles.AddUserToRole(UserName.Text, UserRole.SelectedValue);

                    // Assign the user to supervisor
                    if (UserRole.SelectedValue == "User")
                    {
                        Member.AssignToUser(UserName.Text, AssignedTo.SelectedValue);
                    }

                    // Create User
                    Membership.CreateUser(UserName.Text, Password.Text);
                    MembershipUser NewMember = Membership.GetUser(UserName.Text);
                    NewMember.Email = Email.Text;
                    Membership.UpdateUser(NewMember);

                    //Success
                    ShowManageUserPage();
                    SuccessMessage.Text = "User has been successfully created";
                }
                else
                {
                    ErrorMessage = "Username already exists";
                }
            }

            CreateUserErrorMessage.Text = ErrorMessage;
        }

        protected void EditUserButton_Click(object sender, EventArgs e)
        {
            var User = Membership.GetUser(SelectedUserName);
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

            // User password
            if (!String.IsNullOrEmpty(EditPassword.Text) &&
                Member.ValidatePassword(EditPassword.Text, ref ErrorMessage))
            {
                User.ChangePassword(User.ResetPassword(), EditPassword.Text);
                SuccessMessage += "Password successfully updated.<br/>";
            }

            // User email
            if (!String.IsNullOrEmpty(EditEmail.Text))
            {
                User.Email = EditEmail.Text;
                Membership.UpdateUser(User);
                SuccessMessage += "Email successfully updated.<br/>";
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

            if (Roles.IsUserInRole(SelectedUserName, "User"))
            {
                Member.RemoveAssignedUser(SelectedUserName);
            }
            else 
            {
                if (Member.SupervisorHasUsers(SelectedUserName))
                {
                    Error = true;
                    EditErrorMessage.Text = "This account has users assigned to it. " +
                        "Please reassign them to another supervisor before deleting";
                }
            }

            if (!Error)
            {
                Membership.DeleteUser(SelectedUserName, true);
                ShowManageUserPage();
                SuccessMessage.Text = "User has been successfully deleted";
            }
        }

        protected void UserList_Change(Object sender, DataGridPageChangedEventArgs e)
        {
            SuccessMessage.Text = "";

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

        private void ShowManageUserPage()
        {
            UsersMultiView.ActiveViewIndex = (int)UserPage.ManageUsers;
            UserList.DataSource = Member.CustomGetAllUsers();
            UserList.DataBind();
        }
    }
}

