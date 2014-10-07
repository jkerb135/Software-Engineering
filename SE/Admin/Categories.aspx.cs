using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace SE
{
    public partial class Categories : System.Web.UI.Page
    {
        Category Cat = new Category();
        DataTable CategoriesTable = new DataTable();
        protected void Page_Init(object sender, EventArgs e)
        {
            ViewState.Add("Category", Cat);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                GenerateList();
                AddNewCategoryPanel.Visible = false;
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
                AddNewCategoryPanel.Visible = false;
                EditCategoryPanel.Visible = false;
            }
            else
            {
                string UserName = System.Web.HttpContext.Current.User.Identity.Name;

                List<string> UsersAssignedToSupervisorAssignedToCategory = Member.UsersAssignedToSupervisorAssignedToCategory(UserName, Convert.ToInt32(CategoryList.SelectedValue));

                AllUsers.DataSource = Member.UsersAssignedToSupervisor(UserName);
                AllUsers.DataBind();

                if (UsersAssignedToSupervisorAssignedToCategory.Count > 0)
                {
                    UsersInCategory.DataSource = UsersAssignedToSupervisorAssignedToCategory;
                    UsersInCategory.DataBind();
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

            Cat.DeleteCategory();
            BindCategories();
            SuccessMessage.Text = "Category successfully deleted.";
        }

        private void BindCategories()
        {
            CategoryList.DataSource = CategoryListSource;
            CategoryList.DataBind();
            Methods.AddBlankToDropDownList(CategoryList);
        }
        private void GenerateList()
        {
            CategoriesTable.Columns.Add("Name", Type.GetType("System.String"));
            foreach (Category cat in Category.GetSupervisorCategories(Membership.GetUser().UserName.ToLower()))
            {
                DataRow row;
                row = CategoriesTable.NewRow();
                row["Name"] = cat.CategoryName;
                CategoriesTable.Rows.Add(row);
            }
            catList.DataTextField = "Name";
            catList.DataSource = CategoriesTable;
            catList.DataBind();
            if (catList.Items.Count == 0)
            {
                catList.Items.Add("No Categories");
                catList.Attributes.Add("disabled", "true");
                taskList.Attributes.Add("disabled", "true");
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Attributes.Add("disabled", "true");
            }
            Button1.Text = "Create Category";
        }

        protected void QueryTasks(object sender, EventArgs e)
        {
            Button1.Text = "Create Task";
            taskList.Attributes.Remove("disabled");
            mainStep.Attributes.Remove("disabled");
            detailedStep.Attributes.Remove("disabled");
            string queryString = "SELECT CategoryID FROM Categories WHERE CategoryName ='" + catList.SelectedItem.Text + "'";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    queryString = "SELECT TaskName FROM Tasks WHERE CategoryID ='" + Convert.ToString(dr["CategoryID"]) + "'";
                }
                con.Close();
                cmd = new SqlCommand(queryString, con);
                con.Open();
                dr = cmd.ExecuteReader();
                taskList.Items.Clear();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                while (dr.Read())
                {
                    taskList.Items.Add(Convert.ToString(dr["TaskName"]));
                }
                if (taskList.Items.Count == 0)
                {
                    taskList.Items.Add("No Tasks");
                    taskList.Attributes.Add("disabled", "true");
                    mainStep.Attributes.Add("disabled", "true");
                    detailedStep.Attributes.Add("disabled", "true");
                }
            }
        }
        protected void QueryMainStep(object sender, EventArgs e)
        {
            Button1.Text = "Create Main Step";
            mainStep.Attributes.Remove("disabled");
            detailedStep.Attributes.Remove("disabled");
            string queryString = "SELECT TaskID FROM Tasks WHERE TaskName ='" + taskList.SelectedItem.Text + "'";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    queryString = "SELECT MainStepName FROM MainSteps WHERE TaskID ='" + Convert.ToString(dr["TaskID"]) + "'";
                }
                con.Close();
                cmd = new SqlCommand(queryString, con);
                con.Open();
                dr = cmd.ExecuteReader();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                while (dr.Read())
                {
                    mainStep.Items.Add(Convert.ToString(dr["MainStepName"]));
                }
                if (mainStep.Items.Count == 0)
                {
                    mainStep.Items.Add("No Main Steps");
                    mainStep.Attributes.Add("disabled", "true");
                    detailedStep.Attributes.Add("disabled", "true");
                }
            }
        }
        protected void QueryDetailedStep(object sender, EventArgs e)
        {
            Button1.Text = "Create Detailed Step";
            detailedStep.Attributes.Remove("disabled");
            string queryString = "SELECT MainStepID FROM MainSteps WHERE MainStepName ='" + mainStep.SelectedItem.Text + "'";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    queryString = "SELECT DetailedStepName FROM DetailedSteps WHERE MainStepID ='" + Convert.ToString(dr["MainStepID"]) + "'";
                }
                con.Close();
                cmd = new SqlCommand(queryString, con);
                con.Open();
                dr = cmd.ExecuteReader();
                detailedStep.Items.Clear();
                while (dr.Read())
                {
                    detailedStep.Items.Add(Convert.ToString(dr["DetailedStepName"]));
                }
                if (detailedStep.Items.Count == 0)
                {
                    detailedStep.Items.Add("No Detailed Steps");
                    detailedStep.Attributes.Add("disabled", "true");
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Text = "Clicked";
            ListBoxPanel.Visible = true;
        }
        protected void catFilter_TextChanged(object sender, EventArgs e)
        {
            DataView Dv = CategoriesTable.DefaultView;
            Dv.RowFilter = "Name like '" + catFilter.Text + "%'";
            catList.DataSource = Dv;
            catList.DataBind();
        }
    }
}