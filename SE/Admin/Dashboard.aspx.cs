using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Newtonsoft.Json;
using SE.Classes;
using SE.Controllers;
using SE.Models;

namespace SE.Admin
{
    public class FrontEndUser
    {
        public string Username { get; set; }
    }
    public partial class Dashboard : Page
    {
        private static Label _lbl;
        private readonly MembershipUser _membershipUser = Membership.GetUser();
        private static readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        
        private enum DashView
        {
            Supervisor = 1, Manager = 0
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            SqlDataSource2.SelectParameters.Add("@AssignedSupervisor", _membershipUser.UserName);
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            Session["DataSource"] = Member.CustomGetAllUsers();
            BindUserAccounts();
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();
                }
                DashboardView.ActiveViewIndex = (int)DashView.Supervisor;
                GetRecentUsers();
                GetActiveSupervisor();
                GetActiveUsers();
            }
            else
            {
                var user = Membership.GetUser();
                if (user == null || !Roles.IsUserInRole(user.UserName, "Manager")) return;
                DashboardView.ActiveViewIndex = (int)DashView.Manager;
            }
        }

        protected void SaveState_Click(object sender, EventArgs e)
        {
            if (!ShowAgain.Checked) return;
            if (_membershipUser == null) return;
            _membershipUser.Comment = "Verified";
            Membership.UpdateUser(_membershipUser);
        }
        protected void GetActiveUsers()
        {
            var users = (_db.Users.Join(_db.MemberAssignments, user => user.UserName, memb => memb.AssignedUser,
                (user, memb) => new {user, memb})
                .Where(@t => @t.memb.AssignedSupervisor == _membershipUser.UserName && @t.user.Connected)
                .Select(@t => new
                {
                    @t.user.UserName
                })).ToList();
            activeUserList.DataSource = users;
            activeUserList.DataBind();
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
        
        private void BindUserAccounts()
        {
            GridView1.DataSource = Session["DataSource"];
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.PageIndex = e.NewPageIndex;
            BindUserAccounts();
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            var dataTable = (DataTable)Session["DataSource"];
            dataTable.DefaultView.Sort = String.Format("{0} {1}", e.SortExpression, GetSortingDirection());
            Session["DataSource"] = dataTable;
            GridView1.DataSource = Session["DataSource"];
            GridView1.DataBind();
        }
        protected string GetSortingDirection()
        {
            if (ViewState["SortDirection"] == null)
                ViewState["SortDirection"] = "ASC";
            else if ((string) ViewState["SortDirection"] == "ASC")
                ViewState["SortDirection"] = "DESC";
            else
                ViewState["SortDirection"] = "ASC";

            return (string) ViewState["SortDirection"];
        }
        protected void NewInsert_Click(object sender, EventArgs e)
        {
                var username = (TextBox) GridView1.FooterRow.FindControl("UsernameTxt");
                var password = (TextBox) GridView1.FooterRow.FindControl("PasswordTxt");
                var email = (TextBox) GridView1.FooterRow.FindControl("EmailTxt");
                var role = (DropDownList) GridView1.FooterRow.FindControl("RoleDrp");
                var assigned = (DropDownList) GridView1.FooterRow.FindControl("AssignDrp");
                var lockOut = (DropDownList) GridView1.FooterRow.FindControl("LockOutDrp");
                var emailRegex = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");
            try
            {
                if (!emailRegex.IsMatch(email.Text))
                    throw new EmailException("Email is not in the correct format");
                Membership.CreateUser(username.Text, password.Text);
                var newMember = Membership.GetUser(username.Text);
                if (newMember != null)
                {
                    if (newMember.ProviderUserKey != null)
                        Roles.AddUserToRole(newMember.UserName, role.SelectedValue.ToLower());
                    newMember.Email = email.Text;
                    newMember.IsApproved = Convert.ToBoolean(lockOut.SelectedValue);
                    if (assigned.Enabled && assigned.Visible)
                    {
                        Member.AssignToUser(username.Text, assigned.SelectedValue);
                    }
                    Membership.UpdateUser(newMember);
                }

                username.Text = password.Text = email.Text = String.Empty;
                assigned.Enabled = true;
                assigned.Visible = false;
                Session["DataSource"] = Member.CustomGetAllUsers();
                GridView1.DataSource = Member.CustomGetAllUsers();
                ScriptManager.RegisterStartupScript(this, GetType(), "script", "newUserToast();", true);
            }
            catch (MembershipCreateUserException e1)
            {
                var error = e1.Message;
                if (error ==
                    "The password supplied is invalid.  Passwords must conform to the password strength requirements configured for the default provider.")
                {
                    error = String.Empty;
                    Member.ValidatePassword(password.Text, ref error);
                }
                ScriptManager.RegisterStartupScript(this, typeof (string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
            catch (MembershipPasswordException)
            {
                var error = "";
                Member.ValidatePassword(password.Text, ref error);
                ScriptManager.RegisterStartupScript(this, typeof (string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
            catch(EmailException e1)
            {
                var error = e1.Message;
                ScriptManager.RegisterStartupScript(this, typeof(string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
          
        }

        protected void RoleDrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var role = (DropDownList)GridView1.FooterRow.FindControl("RoleDrp");
            var assigned = (DropDownList)GridView1.FooterRow.FindControl("AssignDrp");
            assigned.Visible = role.Text == "User";
        }
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            userSearch.Text = String.Empty;
            Session["DataSource"] = Member.CustomGetAllUsers();
            GridView1.DataSource = Member.CustomGetAllUsers();
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var username = (Label)GridView1.Rows[e.RowIndex].FindControl("UserLabel");
            var password = (TextBox)GridView1.Rows[e.RowIndex].FindControl("PasswordTxt");
            var email = (TextBox)GridView1.Rows[e.RowIndex].FindControl("EmailTxt");
            var assigned = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("AssignDrp");
            var lockOut = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("LockOutDrp");
            var emailRegex = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");
            try
            {

                if (!emailRegex.IsMatch(email.Text))
                    throw new EmailException("Email is not in the correct format");
                var newMember = Membership.GetUser(username.Text);
                if (newMember == null) return;
                if (password.Text != String.Empty)
                {
                    newMember.ChangePassword(newMember.ResetPassword(), password.Text);
                }
                if (email.Text != String.Empty || email.Text != newMember.Email)
                {
                    newMember.Email = email.Text;
                }
                newMember.IsApproved = Convert.ToBoolean(lockOut.SelectedValue);
                if (assigned.Enabled && assigned.Visible &&
                    !String.Equals(Member.UserAssignedTo(username.Text).Trim(), assigned.SelectedValue.Trim()))
                {
                    Member.EditAssignToUser(username.Text, assigned.SelectedValue);
                }
                Membership.UpdateUser(newMember);
                GridView1.EditIndex = -1;
                Session["DataSource"] = Member.CustomGetAllUsers();
                BindUserAccounts();
                ScriptManager.RegisterStartupScript(this, GetType(), "script", "successToast();", true);
            }
            catch (MembershipCreateUserException e1)
            {
                var error = e1.Message;
                ScriptManager.RegisterStartupScript(this, typeof (string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
            catch (EmailException e1)
            {
                var error = e1.Message;
                ScriptManager.RegisterStartupScript(this, typeof (string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
            catch (ArgumentException)
            {
                var error = "";
                Member.ValidatePassword(password.Text, ref error);
                ScriptManager.RegisterStartupScript(this, typeof(string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
            catch (Exception e1)
            {
                var error = e1.GetType();
                ScriptManager.RegisterStartupScript(this, typeof(string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            _lbl = (Label)GridView1.Rows[e.NewEditIndex].FindControl("AssignLbl");
            GridView1.EditIndex = e.NewEditIndex;
            BindUserAccounts();
            var role = (Label)GridView1.Rows[e.NewEditIndex].FindControl("RoleLbl");
            var assigned = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("AssignDrp");
            if (role.Text != "User") return;
            assigned.Visible = true;
            assigned.SelectedValue = _lbl.Text;
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindUserAccounts();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Footer:
                    {
                        var assigned = (DropDownList)e.Row.FindControl("AssignDrp");
                        assigned.DataSource = Member.GetAllSupervisors();
                        assigned.DataBind();
                        var unassigned = new ListItem("Unassigned", "Unassigned");
                        assigned.Items.Add(unassigned);
                    }
                    break;
                case DataControlRowType.DataRow:
                    {
                        if ((e.Row.RowState & DataControlRowState.Edit) <= 0) return;
                        var assigned = (DropDownList)e.Row.FindControl("AssignDrp");
                        assigned.DataSource = Member.GetAllSupervisors();
                        assigned.DataBind();
                        assigned.SelectedValue = _lbl.Text;
                        var unassigned = new ListItem("Unassigned", "Unassigned");
                        assigned.Items.Add(unassigned);
                    }
                    break;
            }
        }

        protected void userSearch_OnTextChanged(object sender, EventArgs e)
        {
            var dt = (DataTable)Session["DataSource"];

            dt.DefaultView.RowFilter = string.Format("UserName LIKE '%{0}%'",
                   userSearch.Text);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var username = (Label)GridView1.Rows[e.RowIndex].FindControl("UserLabel");
            var deleteUser = Membership.GetUser(username.Text);
            try
            {
                if (deleteUser != null) Membership.DeleteUser(deleteUser.UserName,true);
            }
            catch (Exception e1)
            {
                var error = e1.GetType();
                ScriptManager.RegisterStartupScript(this, typeof(string), "Registering",
                    String.Format("errorToast('{0}');", error), true);
            }
            GridView1.DataSource = Member.CustomGetAllUsers();
            GridView1.DataBind();
        }

        protected void HelpBtn_OnClick(object sender, EventArgs e)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var formatUsername = textInfo.ToTitleCase(_membershipUser.UserName);
            lblModalTitle.Text = "Welcome " + formatUsername + "!";
            lblModalBody.Text =
                "This is your portal to the interactive personal assistant web-app system (or just iPAWS for short). This first page is your Dashboard where you can see if you have any active users online, if you have any newly assigned users or what other supervisors are using the system. The Dashboard is just a staging ground to keep you up to date on what is going on. If you would like to create, update or delete a Category or task go to the Task Manager on the left hand menu. If you would like to view what users are assigned to what task or category or if you would like to update who is assigned to what go to User Assignment. If you would like to review requests from other supervisors go to the Supervisor Requests. Lastly if you would like to request or view reports go to the Reports. We hope you enjoy the web-app.";
            lblSincere.Text = "Cheers,";
            From.Text = "iPAWS Team B";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        }
    }

    public class EmailException : Exception
    {
        public EmailException()
        {
        }

        public EmailException(string message)
            : base(message)
        {
        }

        public EmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}