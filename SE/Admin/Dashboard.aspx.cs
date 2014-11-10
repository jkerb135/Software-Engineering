using SE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            if (!IsPostBack)
            {
                if (Roles.IsUserInRole(Membership.GetUser().UserName, "Supervisor"))
                {
                    DashboardView.ActiveViewIndex = (int)DashView.Supervisor;
                    getActiveUsers();
                    getRecentUsers();
                    getActiveSupervisor();
                }
                else if (Roles.IsUserInRole(Membership.GetUser().UserName, "Manager"))
                {

                    DashboardView.ActiveViewIndex = (int)DashView.Manager;
                    getAllUsers();
                }
            }
        }
        protected void getAllUsers()
        {
            allUsersSource.SelectCommand = "Select a.AssignedSupervisor, b.UserName, b.LastActivityDate, c.Email, c.IsApproved, c.IsLockedOut from MemberAssignments a right outer join aspnet_Users b on a.AssignedUser = b.UserName inner join aspnet_Membership c on b.UserId = c.UserId";
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
            string ErrorMessage = "";

            if (Member.ValidatePassword(Password.Text, ref ErrorMessage))
            {
                if (Membership.GetUser(UserName.Text) == null)
                {
                    // Add user to role

                    // Assign the user to supervisor

                    // Create User
                    Membership.CreateUser(UserName.Text, Password.Text);
                    MembershipUser NewMember = Membership.GetUser(UserName.Text);
                    NewMember.Email = Email.Text;
                    Membership.UpdateUser(NewMember);

                }
                else
                {
                    ErrorMessage = "Username already exists";
                }
            }

            CreateUserErrorMessage.Text = ErrorMessage;
        }

        

        private void BindSupervisors(DropDownList drp)
        {
            drp.DataSource = Roles.GetUsersInRole("Supervisor");
            drp.DataBind();

            Methods.AddBlankToDropDownList(drp);
        }
    }
}