using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SE
{
    public partial class Users : System.Web.UI.Page
    {
        public DropDownList UserRole;
        public enum ShowPage
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
            if (Request.QueryString["CreateUser"] == "1")
            {
                UsersMultiView.ActiveViewIndex = (int) ShowPage.CreateUser;
            }
            else if (Request.QueryString["EditUser"] == "1")
            {
                UsersMultiView.ActiveViewIndex = (int) ShowPage.EditUser;
            }
            else
            {
                UsersMultiView.ActiveViewIndex = (int) ShowPage.ManageUsers;
            }
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            Roles.AddUserToRole(CreateUserWizard.UserName, UserRole.SelectedValue);
        }
    }
}