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
        static int catIDX, taskIDX, mainIDX, detIDX;

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

        /* Management CRUD Functionality */
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
        protected void EditMainStepButton_Click(object sender, EventArgs e)
        {
            ITask.TaskName = taskList.Items[taskIDX].Text;
            if (Button2.Text == "Add New Main Step")
            {
                ITask.TaskID = Task.getTaskID(ITask.TaskName);
                if (MainStepName.Text != String.Empty)
                {
                    IMainStep.MainStepName = MainStepName.Text;
                    IMainStep.TaskID = ITask.TaskID;
                    IMainStep.MainStepText =
                        !String.IsNullOrEmpty(MainStepText.Text) ? MainStepText.Text : null;

                    if (MainStepAudio.HasFile)
                    {
                        Methods.UploadFile(MainStepAudio);

                        IMainStep.AudioFilename = MainStepAudio.FileName;
                        IMainStep.AudioPath = "~/Uploads/" + MainStepAudio.FileName;
                    }

                    if (MainStepVideo.HasFile)
                    {
                        Methods.UploadFile(MainStepVideo);

                        IMainStep.VideoFilename = MainStepVideo.FileName;
                        IMainStep.VideoPath = "~/Uploads/" + MainStepVideo.FileName;
                    }

                    IMainStep.CreateMainStep();

                    MainStepName.Text = "";
                    MainStepText.Text = "";
                    populateListBox();
                }
            }
        }
        protected void AddNewCategory_Click(object sender, EventArgs e)
        {
            EditCategoryName.Text = String.Empty;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = true;
            ManageMainStepPanel.Visible = false;
            EditCategoryButton.Text = "Add New Category";
        }
        protected void AddNewTask_Click(object sender, EventArgs e)
        {
            EditTaskName.Text = String.Empty;
            AddNewCategoryPanel.Visible = false;
            mainStep.Attributes.Add("disabled", "true");
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            TaskPanel.Visible = true;
            EditTaskButton.Text = "Add New Task";
        }
        protected void AddNewMainStep_Click(object sender, EventArgs e)
        {
            MainStepManagement.Visible = false;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            MainStepButton.Text = "Add New Main Step";
            populateListBox();
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

        /*Button Binding Events*/
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


        protected void EditCategoryCancel_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = true;
            TaskPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
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

        /* Listbox Events */
        private void GenerateList()
        {
            CategoriesTable.Columns.Add("Name", Type.GetType("System.String"));
            foreach (Category cat in Category.GetSupervisorCategoriesList(Membership.GetUser().UserName.ToLower()))
            {
                DataRow row;
                row = CategoriesTable.NewRow();
                row["Name"] = cat.CategoryName;
                CategoriesTable.Rows.Add(row);
            }
            catList.DataTextField = "Name";
            catList.DataSource = CategoriesTable;
            catList.DataBind();
            taskList.Attributes.Add("disabled", "true");
            mainStep.Attributes.Add("disabled", "true");
            detailedStep.Attributes.Add("disabled", "true");
            if (catList.Items.Count == 0)
            {
                catList.Items.Add("No Categories");
                taskList.Attributes.Add("disabled", "true");
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Attributes.Add("disabled", "true");
            }
        }
        protected void QueryTasks(object sender, EventArgs e)
        {
            catIDX = catList.SelectedIndex;
            AddNewCategoryPanel.Visible = true;
            TaskManagmentPanel.Visible = false;
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = false;
            if (catList.Items[0].Value != "No Categories")
            {
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
                    mainStep.Attributes.Add("disabled", "true");
                    detailedStep.Attributes.Add("disabled", "true");
                    if (taskList.Items.Count == 0)
                    {
                        taskList.Items.Add("No Tasks");
                        mainStep.Attributes.Add("disabled", "true");
                        detailedStep.Attributes.Add("disabled", "true");
                    }
                }
            }
        }
        protected void QueryMainStep(object sender, EventArgs e)
        {
            taskIDX = taskList.SelectedIndex;
            AddNewCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = true;
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = false;
            if (taskList.SelectedValue != "No Tasks")
            {
                mainStep.Attributes.Remove("disabled");
                detailedStep.Attributes.Remove("disabled");
            }
            detailedStep.Attributes.Add("disabled", "true");
            string queryString = "SELECT TaskID FROM Tasks WHERE TaskName ='" + taskList.SelectedItem.Text + "'";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    queryString = "SELECT * FROM MainSteps WHERE TaskID ='" + Convert.ToString(dr["TaskID"]) + "' Order By TaskID ASC";
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
            AddNewCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            MainStepManagement.Visible = true;
            DetailedStepManagement.Visible = false;
            if (mainStep.Items[0].Value != "No Main Steps")
            {
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
                    }
                }
            }
        }
        protected void detailButtons(object sender, EventArgs e)
        {
            detIDX = detailedStep.SelectedIndex;
            AddNewCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = true;
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
        protected void refreshMainSteps()
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

        /*Main Step Function*/
        protected void MainStepMoveDown_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "" && MainStepList.SelectedIndex != MainStepList.Items.Count - 1)
            {

                string queryString =
                    "SELECT ListOrder " +
                    "FROM MainSteps " +
                    "WHERE MainStepID=@mainstepid";

                string queryString2 =
                    "SELECT MIN(ListOrder) " +
                    "FROM MainSteps " +
                    "WHERE ListOrder > @listorder " +
                    "AND TaskID=@taskid";

                string queryString3 =
                    "SELECT MainStepID " +
                    "FROM MainSteps " +
                    "WHERE ListOrder = ( " +
                        "SELECT MIN(ListOrder) " +
                        "FROM MainSteps " +
                        "WHERE ListOrder > @listorder " +
                        "AND TaskID=@taskid " +
                    ") " +
                    "AND TaskID=@taskid";

                string queryString4 =
                    "UPDATE MainSteps " +
                    "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                    "WHERE MainStepID IN (@mainstepid1, @mainstepid2)";

                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    IMainStep.TaskID = Task.getTaskID(taskList.Items[taskIDX].Value);
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    SqlCommand cmd2 = new SqlCommand(queryString2, con);
                    SqlCommand cmd3 = new SqlCommand(queryString3, con);
                    SqlCommand cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@mainstepid", Convert.ToInt32(MainStepList.SelectedValue));

                    con.Open();

                    int FirstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd2.Parameters.AddWithValue("@taskid", IMainStep.TaskID);

                    cmd3.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd3.Parameters.AddWithValue("@taskid", IMainStep.TaskID);

                    con.Open();

                    int SecondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    int ThirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", FirstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", SecondValue);
                    cmd4.Parameters.AddWithValue("@mainstepid1", Convert.ToInt32(MainStepList.SelectedValue));
                    cmd4.Parameters.AddWithValue("@mainstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                    populateListBox();
                }

            }
        }
        protected void MainStepMoveUp_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "" && MainStepList.SelectedIndex != 0)
            {
                string queryString =
                    "SELECT ListOrder " +
                    "FROM MainSteps " +
                    "WHERE MainStepID=@mainstepid";

                string queryString2 =
                    "SELECT MAX(ListOrder) " +
                    "FROM MainSteps " +
                    "WHERE ListOrder < @listorder " +
                    "AND TaskID=@taskid";

                string queryString3 =
                    "SELECT MainStepID " +
                    "FROM MainSteps " +
                    "WHERE ListOrder = ( " +
                        "SELECT MAX(ListOrder) " +
                        "FROM MainSteps " +
                        "WHERE ListOrder < @listorder " +
                        "AND TaskID=@taskid " +
                    ") " +
                    "AND TaskID=@taskid";

                string queryString4 =
                    "UPDATE MainSteps " +
                    "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                    "WHERE MainStepID IN (@mainstepid1, @mainstepid2)";

                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    SqlCommand cmd2 = new SqlCommand(queryString2, con);
                    SqlCommand cmd3 = new SqlCommand(queryString3, con);
                    SqlCommand cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@mainstepid", Convert.ToInt32(MainStepList.SelectedValue));

                    con.Open();

                    int FirstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd2.Parameters.AddWithValue("@taskid", IMainStep.TaskID);

                    cmd3.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd3.Parameters.AddWithValue("@taskid", IMainStep.TaskID);

                    con.Open();

                    int SecondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    int ThirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", FirstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", SecondValue);
                    cmd4.Parameters.AddWithValue("@mainstepid1", Convert.ToInt32(MainStepList.SelectedValue));
                    cmd4.Parameters.AddWithValue("@mainstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
        protected void MainStepDelete_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "")
            {
                IMainStep.MainStepID = Convert.ToInt32(MainStepList.SelectedValue);
                IMainStep.DeleteMainStep();

            }
        }
        protected void populateListBox()
        {
            int taskID = Task.getTaskID(taskList.Items[taskIDX].Value);
            MainStepList.DataSource = MainStep.GetMainSteps(taskID);
            MainStepList.DataBind();
        }
    }
}