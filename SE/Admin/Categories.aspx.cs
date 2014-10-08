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
        string UserName = System.Web.HttpContext.Current.User.Identity.Name;
        Task ITask = new Task();
        MainStep IMainStep = new MainStep();
        DetailedStep IDetailedStep = new DetailedStep();
        int catIDX, taskIDX, mainIDX, detIDX = 0;

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
                EditCategoryPanel.Visible = false;
                TaskManagmentPanel.Visible = false;
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
            EditCategoryName.Text = String.Empty;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = true;
            EditCategoryButton.Text = "Add New Category";
        }
        protected void AddNewTask_Click(object sender, EventArgs e)
        {
            EditTaskName.Text = String.Empty;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            TaskPanel.Visible = true;
            EditTaskButton.Text = "Add New Task";
        }
        protected void UpdateCategory_Click(object sender, EventArgs e)
        {
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = true;
            TaskPanel.Visible = false;
            EditCategoryName.Text = catList.SelectedValue;
            EditCategoryButton.Text = "Update Category";
        }
        protected void UpdateTask_Click(object sender, EventArgs e)
        {
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            TaskPanel.Visible = true;
            EditTaskName.Text = taskList.SelectedValue;
            EditTaskButton.Text = "Update Task";
        }
        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            Cat.DeleteCategory(catList.SelectedValue);
            BindCategories();
            SuccessMessage.Text = "Category successfully deleted.";
            GenerateList();
        }
        protected void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            ITask.DeleteTask(taskList.SelectedValue);
            SuccessMessage.Text = "Task successfully deleted.";
            refreshTasks();
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
            if (EditCategoryButton.Text == "Add New Category")
            {
                if (EditCategoryName.Text != String.Empty)
                {
                    Cat.CategoryName = EditCategoryName.Text;

                    Cat.CreateCategory();
                    BindCategories();

                    SuccessMessage.Text = "New category successfully added.";

                    EditCategoryName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    GenerateList();
                }
                else
                {
                    ErrorMessage.Text = "Error creating new category";
                }
            }
            else if (EditCategoryButton.Text == "Update Category")
            {
                if (EditCategoryName.Text != String.Empty)
                {
                    Cat.CategoryName = EditCategoryName.Text;

                    Cat.UpdateCategory();
                    BindCategories();

                    SuccessMessage.Text = "Category successfully updated.";

                    EditCategoryName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    GenerateList();
                }
                else
                {
                    ErrorMessage.Text = "Error Updating Category";
                }
            }
        }

        protected void EditCategoryCancel_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = true;
            TaskPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            catList.SelectedIndex = -1;
            taskList.SelectedIndex = -1;
            mainStep.SelectedIndex = -1;
            detailedStep.SelectedIndex = -1;

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
            }
        }
        protected void QueryTasks(object sender, EventArgs e)
        {
            catIDX = catList.SelectedIndex;
            AddNewCategoryPanel.Visible = true;
            TaskManagmentPanel.Visible = false;
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
                    mainStep.Attributes.Add("disabled", "true");
                    detailedStep.Attributes.Add("disabled", "true");
                }
            }
        }
        protected void QueryMainStep(object sender, EventArgs e)
        {
            taskIDX = taskList.SelectedIndex;
            catList.SelectedIndex = -1;
            AddNewCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = true;
            if (taskList.SelectedValue != "No Tasks")
            {
                mainStep.Attributes.Remove("disabled");
                detailedStep.Attributes.Remove("disabled");
            }
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
                if (mainStep.Items.Count == 0 && taskList.SelectedValue != "No Tasks")
                {
                    mainStep.Items.Add("No Main Steps");
                    detailedStep.Attributes.Add("disabled", "true");
                }
            }
        }
        protected void QueryDetailedStep(object sender, EventArgs e)
        {
            mainIDX = mainStep.SelectedIndex;
            taskList.SelectedIndex = -1;
            AddNewCategoryPanel.Visible = false;
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
        protected void detailButtons(object sender, EventArgs e)
        {
            detIDX = detailedStep.SelectedIndex;
            mainStep.SelectedIndex = -1;
            AddNewCategoryPanel.Visible = false;
        }
        protected void catFilter_TextChanged(object sender, EventArgs e)
        {
            DataView Dv = CategoriesTable.DefaultView;
            Dv.RowFilter = "Name like '" + catFilter.Text + "%'";
            catList.DataSource = Dv;
            catList.DataBind();
        }
        protected void refreshTasks()
        {
            string queryString = "SELECT CategoryID FROM Categories WHERE CategoryName ='" + catList.Items[catIDX].Text + "'";
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
                    mainStep.Attributes.Add("disabled", "true");
                    detailedStep.Attributes.Add("disabled", "true");
                }
            }
        }
        /*Task Function*/
        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            if (EditTaskButton.Text == "Add New Task")
            {
                Cat.CategoryName = catList.Items[catIDX].Text;
                if (EditTaskName.Text != String.Empty)
                {
                    ITask.TaskName = EditTaskName.Text;
                    ITask.CategoryID = Category.getCategoryID(Cat.CategoryName);
                    ITask.CreateTask();

                    SuccessMessage.Text = "New task successfully added.";

                    EditTaskName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    TaskPanel.Visible = false;
                    refreshTasks();
                }
                else
                {
                    ErrorMessage.Text = "Error creating new task";
                }
            }
            else if (EditTaskButton.Text == "Update Task")
            {
                if (EditCategoryName.Text != String.Empty)
                {
                    ITask.TaskName = EditTaskName.Text;
                    ITask.UpdateTask();

                    SuccessMessage.Text = "Task successfully updated.";

                    EditTaskName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    TaskPanel.Visible = false;
                    refreshTasks();
                }
                else
                {
                    ErrorMessage.Text = "Error Updating Task";
                }
            }
        }
        
    }
}