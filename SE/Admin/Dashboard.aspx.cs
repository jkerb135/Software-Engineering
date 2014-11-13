using SE.Classes;
using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private String SelectedUserName = String.Empty;
        private enum DashView
        {
            Supervisor = 1, Manager = 0
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            AssignedToLabel.Visible = true;
            AssignedTo.Visible = true;
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && Roles.IsUserInRole(membershipUser.UserName, "Supervisor"))
            {
                DashboardView.ActiveViewIndex = (int) DashView.Supervisor;
                getActiveUsers();
                getRecentUsers();
                getActiveSupervisor();
            }
            else
            {
                var user = Membership.GetUser();
                if (user == null || !Roles.IsUserInRole(user.UserName, "Manager")) return;
                DashboardView.ActiveViewIndex = (int) DashView.Manager;
                getAllUsers();
                BindSupervisors(AssignedTo);
            }
        }
        protected void getAllUsers()
        {
            allUsersSource.SelectCommand = "Select a.AssignedSupervisor, b.UserName, b.LastActivityDate, c.Email, c.IsApproved, c.IsLockedOut, e.RoleName from MemberAssignments a right outer join aspnet_Users b on a.AssignedUser = b.UserName inner join aspnet_Membership c on b.UserId = c.UserId inner join aspnet_UsersInRoles d on c.UserId = d.UserId inner join aspnet_Roles e on d.RoleId = e.RoleId";
            allUsers.DataBind();
            allUsers.UseAccessibleHeader = true;
            allUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void getActiveUsers()
        {
            activeUserList.DataSource = Member.CustomGetActiveUsers();
            activeUserList.DataBind();
            activeUserList.UseAccessibleHeader = true;
            activeUserList.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        protected void getRecentUsers()
        {
            newMembers.DataSource = Member.CustomRecentlyAssigned();
            newMembers.DataBind();
            newMembers.UseAccessibleHeader = true;
            newMembers.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        protected void getActiveSupervisor()
        {
            signededIn.DataSource = Member.CustomGetActiveSupervisor();
            signededIn.DataBind();
            signededIn.UseAccessibleHeader = true;
            signededIn.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            var errorMessage = "";
            if (Member.ValidatePassword(Password.Text, ref errorMessage))
            {
                if (Membership.GetUser(UserName.Text) == null)
                {
                    // Add user to role
                    switch (managerState.Value)
                    {
                        case "User":
                            Roles.AddUserToRole(UserName.Text, "User");
                            Member.AssignToUser(UserName.Text, AssignedTo.SelectedValue);
                            AssignedToLabel.Visible = true;
                            AssignedTo.Visible = true;
                            break;
                        case "Supervisor":
                            AssignedTo.SelectedIndex = 0;
                            Roles.AddUserToRole(UserName.Text, "Supervisor");
                            AssignedToLabel.Visible = false;
                            AssignedTo.Visible = false;
                            break;
                    }

                    // Create User
                    Membership.CreateUser(UserName.Text, Password.Text);
                    var newMember = Membership.GetUser(UserName.Text);
                    newMember.Email = Email.Text;
                    Membership.UpdateUser(newMember);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "blink();", true);
                    //Success
                    UserName.Text = Password.Text = ConfirmPassword.Text = Email.Text = String.Empty;
                }
                else
                {
                    errorMessage = "Username already exists";
                }
            }

            CreateUserErrorMessage.Text = errorMessage;
        }

        private static void BindSupervisors(DropDownList drp)
        {
            drp.DataSource = Roles.GetUsersInRole("Supervisor");
            drp.DataBind();

            Methods.AddBlankToDropDownList(drp);
        }

        protected void allUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            allUsersSource.SelectCommand = "Select a.AssignedSupervisor, b.UserName, b.LastActivityDate, c.Email, c.IsApproved, c.IsLockedOut, e.RoleName from MemberAssignments a right outer join aspnet_Users b on a.AssignedUser = b.UserName inner join aspnet_Membership c on b.UserId = c.UserId inner join aspnet_UsersInRoles d on c.UserId = d.UserId inner join aspnet_Roles e on d.RoleId = e.RoleId";
            allUsers.PageIndex = e.NewPageIndex;
            allUsers.DataBind();
        }

    }
}