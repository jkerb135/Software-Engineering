using System.Globalization;
using SE.Classes;
using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly MembershipUser _membershipUser = Membership.GetUser();
        private String _selectedUserName = String.Empty;
        private enum DashView
        {
            Supervisor = 1, Manager = 0
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            AssignedToLabel.Visible = true;
            AssignedTo.Visible = true;
            var textInfo = new CultureInfo("en-US",false).TextInfo;

            var formatUsername = textInfo.ToTitleCase(_membershipUser.UserName);
            if (_membershipUser != null && Roles.IsUserInRole(_membershipUser.UserName, "Supervisor"))
            {
                if (_membershipUser.Comment == null && _membershipUser.Comment == null)
                {
                    _membershipUser.Comment = "justLoggedOn";
                    Membership.UpdateUser(_membershipUser);
                    lblModalTitle.Text = "Welcome " + formatUsername + "!";
                    lblModalBody.Text =
                        "This is your portal to the interactive personal assistant web-app system (or just iPAWS for short). This first page is your Dashboard where you can see if you have any active users online, if you have any newly assigned users or what other supervisors are using the system. The Dashboard is just a staging ground to keep you up to date on what is going on. If you would like to create, update or delete a Category or task go to the Task Manager on the left hand menu. If you would like to view what users are assigned to what task or category or if you would like to update who is assigned to what go to User Assignment. If you would like to review requests from other supervisors go to the Supervisor Requests. Lastly if you would like to request or view reports go to the Reports. We hope you enjoy the web-app.";
                    lblSincere.Text = "Cheers,";
                    From.Text = "iPAWS Team B";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();",true);
                    upModal.Update();
                }
                DashboardView.ActiveViewIndex = (int) DashView.Supervisor;
                GetActiveUsers();
                GetRecentUsers();
                GetActiveSupervisor();
            }
            else
            {
                var user = Membership.GetUser();
                if (user == null || !Roles.IsUserInRole(user.UserName, "Manager")) return;
                DashboardView.ActiveViewIndex = (int) DashView.Manager;
                GetAllUsers();
                BindSupervisors(AssignedTo);
            }
        }

        protected void SaveState_Click(object sender, EventArgs e)
        {
            if (!ShowAgain.Checked) return;
            if (_membershipUser == null) return;
            _membershipUser.Comment = "Verified";
            Membership.UpdateUser(_membershipUser);
        }
        protected void GetAllUsers()
        {
            allUsersSource.SelectCommand = "Select a.AssignedSupervisor, b.UserName, b.LastActivityDate, c.Email, c.IsApproved, c.IsLockedOut, e.RoleName from MemberAssignments a right outer join aspnet_Users b on a.AssignedUser = b.UserName inner join aspnet_Membership c on b.UserId = c.UserId inner join aspnet_UsersInRoles d on c.UserId = d.UserId inner join aspnet_Roles e on d.RoleId = e.RoleId";
            allUsers.DataBind();
            allUsers.UseAccessibleHeader = true;
            allUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void GetActiveUsers()
        {
            activeUserList.DataSource = Member.CustomGetActiveUsers();
            activeUserList.DataBind();
            activeUserList.UseAccessibleHeader = true;
            activeUserList.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        protected void GetRecentUsers()
        {
            newMembers.DataSource = Member.CustomRecentlyAssigned();
            newMembers.DataBind();
            newMembers.UseAccessibleHeader = true;
            newMembers.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        protected void GetActiveSupervisor()
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
                    MembershipUser newMember = null;
                    // Add user to role
                    switch (managerState.Value)
                    {
                        case "User":
                            newMember = Membership.GetUser(UserName.Text);
                            Roles.AddUserToRole(UserName.Text, "User");
                            Member.AssignToUser(UserName.Text, AssignedTo.SelectedValue);
                            AssignedToLabel.Visible = true;
                            AssignedTo.Visible = true;
                            break;
                        case "Supervisor":
                            newMember = Membership.GetUser(UserName.Text);
                            if (newMember != null) newMember.Comment = null;
                            AssignedTo.SelectedIndex = 0;
                            Roles.AddUserToRole(UserName.Text, "Supervisor");
                            AssignedToLabel.Visible = false;
                            AssignedTo.Visible = false;
                            break;
                    }

                    // Create User
                    Membership.CreateUser(UserName.Text, Password.Text);

                    if (newMember != null)
                    {
                        newMember.Email = Email.Text;
                        Membership.UpdateUser(newMember);
                    }
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