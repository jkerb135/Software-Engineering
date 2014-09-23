using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using System.Web.Security;

namespace SE
{
    public partial class Categories : System.Web.UI.Page
    {
        Category Cat = new Category();

        protected void Page_Init(object sender, EventArgs e)
        {
            ViewState.Add("Category", Cat);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
            }
            else
            {
                ErrorMessage.Text = String.Empty;
                SuccessMessage.Text = String.Empty;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (CategoryList.SelectedValue == "")
            {
                AddNewCategoryPanel.Visible = true;
                EditCategoryPanel.Visible = false;
            }
            else
            {
                string UserName = System.Web.HttpContext.Current.User.Identity.Name;
                
                if(Roles.IsUserInRole(UserName, "Manager"))
                {
                    List<string> UsersAssignedToCategory = Member.UsersAssignedToCategory(Convert.ToInt32(CategoryList.SelectedValue));

                    AllUsers.DataSource = Roles.GetUsersInRole("User");
                    AllUsers.DataBind();

                    if (UsersAssignedToCategory.Count > 0)
                    {
                        UsersInCategory.DataSource = UsersAssignedToCategory;
                        UsersInCategory.DataBind();
                    }
                }
                else if(Roles.IsUserInRole(UserName, "Supervisor"))
                {
                    List<string> UsersAssignedToSupervisorAssignedToCategory = Member.UsersAssignedToSupervisorAssignedToCategory(UserName, Convert.ToInt32(CategoryList.SelectedValue));

                    AllUsers.DataSource = Member.UsersAssignedToSupervisor(UserName);
                    AllUsers.DataBind();

                    if (UsersAssignedToSupervisorAssignedToCategory.Count > 0)
                    {
                        UsersInCategory.DataSource = UsersAssignedToSupervisorAssignedToCategory;
                        UsersInCategory.DataBind();
                    }
                }

                Cat = (Category)ViewState["Category"];
                Cat.CategoryID = Convert.ToInt32(CategoryList.SelectedValue);
                ViewState.Add("Category", Cat);
            }
        }

        protected void AddNewCategory_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(AddNewCategoryName.Text))
            {
                Cat.CategoryName = AddNewCategoryName.Text;

                Cat.CreateCategory();
                BindCategories();

                SuccessMessage.Text = "New category successfully added.";

                AddNewCategoryName.Text = String.Empty;
            }
            else
            {
                ErrorMessage.Text = "Please enter a category name.";
            }
        }

        protected void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryList.SelectedValue != "")
            {
                AllUsers.Items.Clear();
                UsersInCategory.Items.Clear();
                AddNewCategoryPanel.Visible = false;
                EditCategoryPanel.Visible = true;
            }
        }

        protected void MoveLeft_Click(object sender, EventArgs e)
        {
            if (UsersInCategory.SelectedItem != null)
            {
                Cat = (Category)ViewState["Category"];
                Cat.UnAssignUserFromCategory(UsersInCategory.SelectedValue.ToString());
                UsersInCategory.Items.Remove(UsersInCategory.SelectedItem);
            }
        }

        protected void MoveRight_Click(object sender, EventArgs e)
        {
            if (AllUsers.SelectedItem != null && 
                !UsersInCategory.Items.Contains(AllUsers.SelectedItem))
            {
                Cat = (Category)ViewState["Category"];
                Cat.AssignUserToCategory(AllUsers.SelectedValue.ToString());
                UsersInCategory.Items.Add(AllUsers.SelectedValue.ToString());
            }
        }

        protected void EditCategoryButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(EditCategoryName.Text))
            {
                Cat = (Category)ViewState["Category"];

                Cat.CategoryName = EditCategoryName.Text;

                Cat.UpdateCategory();
                BindCategories();

                SuccessMessage.Text = "Category successfully updated.";

                EditCategoryName.Text = String.Empty;
            }
            else
            {
                ErrorMessage.Text = "Please enter a category name.";
            }
        }

        protected void EditCategoryCancel_Click(object sender, EventArgs e)
        {
            BindCategories();
        }

        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            Cat = (Category)ViewState["Category"];

            if (!Cat.CategoryIsAssigned())
            {
                Cat.DeleteCategory();
                BindCategories();
                SuccessMessage.Text = "Category successfully deleted.";
            }
            else
            {
                ErrorMessage.Text = "Category has assigned users or tasks please reassign before deleting.";
            }
        }

        private void BindCategories()
        {
            CategoryList.DataSource = CategoryListSource;
            CategoryList.DataBind();
            Methods.AddBlankToDropDownList(CategoryList);
        }
    }
}