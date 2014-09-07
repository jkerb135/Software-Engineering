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
        public enum UserPage
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
            else if (Request.QueryString["userpage"] == "edituser")
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.EditUser;
            }
            else
            {
                UsersMultiView.ActiveViewIndex = (int) UserPage.ManageUsers;
            }
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            Roles.AddUserToRole(CreateUserWizard.UserName, UserRole.SelectedValue);
        }
    }
}