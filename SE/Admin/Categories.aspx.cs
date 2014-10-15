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
        string UserName = System.Web.HttpContext.Current.User.Identity.Name;
        Task ITask = new Task();
        MainStep IMainStep = new MainStep();
        DetailedStep IDetailedStep = new DetailedStep();

        protected void Page_Init(object sender, EventArgs e)
        {
            ViewState.Add("Category", Cat);
            ViewState.Add("Task", ITask);
            ViewState.Add("MainStep", IMainStep);
            ViewState.Add("DetailedStep", IDetailedStep);
            ViewState.Add("CategoriesExist", false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                catList.Items.Add("--Add Categories--");
                BindCategories(catList);
                AddNewCategoryPanel.Visible = false;
                EditCategoryPanel.Visible = false;
                TaskManagmentPanel.Visible = false;
            }
            else
            {
                ErrorMessage.Text = String.Empty;
                SuccessMessage.Text = String.Empty;
            }

            if(taskList.Items.Count == 0)
                taskList.Attributes.Add("disabled", "true");
            if(mainStep.Items.Count == 0)
                mainStep.Attributes.Add("disabled", "true");
            if(detailedStep.Items.Count == 0)
                detailedStep.Attributes.Add("disabled", "true");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        
        /* Management CRUD Functionality */
        protected void EditCategoryButton_Click(object sender, EventArgs e)
        {
            Cat = (Category)ViewState["Category"];

            if (EditCategoryButton.Text == "Add New Category")
            {
                if (EditCategoryName.Text != String.Empty)
                {
                    Cat.CategoryName = EditCategoryName.Text;
                    Cat.CategoryAssignments = (from l in UsersInCategory.Items.Cast<ListItem>() select l.Value).ToList();

                    Cat.CreateCategory();
                    Cat.AssignUserCategories();
                    BindCategories(catList);

                    SuccessMessage.Text = "New category successfully added.";

                    EditCategoryName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
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
                    Cat.CategoryAssignments = (from l in UsersInCategory.Items.Cast<ListItem>() select l.Value).ToList();

                    Cat.UpdateCategory();
                    Cat.ReAssignUserCategories();
                    BindCategories(catList);

                    SuccessMessage.Text = "Category successfully updated.";

                    EditCategoryName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                }
                else
                {
                    ErrorMessage.Text = "Error Updating Category";
                }
            }
        }
        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            ITask = (Task)ViewState["Task"];

            if (EditTaskButton.Text == "Add New Task")
            {
                if (EditTaskName.Text != String.Empty)
                {
                    ITask.TaskName = EditTaskName.Text;

                    if (!String.IsNullOrEmpty(EditAssignUserToTask.SelectedItem.Text))
                        ITask.AssignedUser = EditAssignUserToTask.SelectedItem.Text;
                    else
                        ITask.AssignedUser = null;

                    ITask.CreateTask();

                    SuccessMessage.Text = "New task successfully added.";

                    EditTaskName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    TaskPanel.Visible = false;
                    RefreshTasks();
                }
                else
                {
                    ErrorMessage.Text = "Error creating new task";
                }
            }
            else if (EditTaskButton.Text == "Update Task")
            {
                if (EditTaskName.Text != String.Empty)
                {
                    ITask.TaskName = EditTaskName.Text;

                    if (!String.IsNullOrEmpty(EditAssignUserToTask.SelectedItem.Text))
                        ITask.AssignedUser = EditAssignUserToTask.SelectedItem.Text;
                    else
                        ITask.AssignedUser = null;

                    ITask.UpdateTask();

                    SuccessMessage.Text = "Task successfully updated.";

                    EditTaskName.Text = String.Empty;
                    AddNewCategoryPanel.Visible = false;
                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    TaskPanel.Visible = false;
                    RefreshTasks();
                }
                else
                {
                    ErrorMessage.Text = "Error Updating Task";
                }
            }
        }
        protected void EditMainStepButton_Click(object sender, EventArgs e)
        {
            IMainStep = (MainStep)ViewState["MainStep"];

            if (MainStepName.Text != String.Empty)
            {
                IMainStep.MainStepName = MainStepName.Text;
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

                if (MainStepButton.Text == "Add New Main Step")
                {
                    IMainStep.CreateMainStep();
                    SuccessMessage.Text = "New main step successfully added.";
                }
                if (MainStepButton.Text == "Update Main Step")
                {
                    IMainStep.UpdateMainStep();
                    SuccessMessage.Text = "Main step successfully updated.";
                }

                MainStepName.Text = String.Empty;
                MainStepText.Text = String.Empty;
                AddNewCategoryPanel.Visible = false;
                ListBoxPanel.Visible = true;
                EditCategoryPanel.Visible = false;
                TaskPanel.Visible = false;
                ManageMainStepPanel.Visible = false;
                ManageDetailedStepPanel.Visible = false;

                RefreshMainSteps();
            }
        }
        protected void EditDetailedStepButton_Click(object sender, EventArgs e)
        {
            IDetailedStep = (DetailedStep)ViewState["DetailedStep"];

            if (DetailedStepName.Text != String.Empty)
            {
                IDetailedStep.DetailedStepName = DetailedStepName.Text;
                IDetailedStep.DetailedStepText =
                    !String.IsNullOrEmpty(DetailedStepText.Text) ? DetailedStepText.Text : null;

                if (DetailedStepImage.HasFile)
                {
                    Methods.UploadFile(DetailedStepImage);

                    IDetailedStep.ImageFilename = DetailedStepImage.FileName;
                    IDetailedStep.ImagePath = "~/Uploads/" + DetailedStepImage.FileName;
                }

                if (EditDetailedStepButton.Text == "Add New Detailed Step")
                {
                    IDetailedStep.CreateDetailedStep();
                    SuccessMessage.Text = "New detailed step successfully added.";
                }
                if (EditDetailedStepButton.Text == "Update Detailed Step")
                {
                    IDetailedStep.UpdateDetailedStep();
                    SuccessMessage.Text = "detailed step successfully updated.";
                }

                DetailedStepName.Text = String.Empty;
                DetailedStepText.Text = String.Empty;
                AddNewCategoryPanel.Visible = false;
                ListBoxPanel.Visible = true;
                EditCategoryPanel.Visible = false;
                TaskPanel.Visible = false;
                ManageMainStepPanel.Visible = false;
                ManageDetailedStepPanel.Visible = false;

                RefreshDetailedSteps();
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
            ManageDetailedStepPanel.Visible = false;
            EditCategoryButton.Text = "Add New Category";

            GenerateUserLists();
        }
        protected void AddNewTask_Click(object sender, EventArgs e)
        {
            BindUsers(EditAssignUserToTask);
            EditTaskName.Text = String.Empty;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = false;
            TaskPanel.Visible = true;
            EditTaskButton.Text = "Add New Task";
        }
        protected void AddNewMainStep_Click(object sender, EventArgs e)
        {
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = false;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            MainStepButton.Text = "Add New Main Step";
        }
        protected void AddNewDetailedStep_Click(object sender, EventArgs e)
        {
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = true;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = true;
            EditDetailedStepButton.Text = "Add New Detailed Step";
        }
        protected void UpdateCategory_Click(object sender, EventArgs e)
        {
            if (catList.SelectedItem.Text != "--Add Categories--")
            {
                Cat = (Category)ViewState["Category"];
                Cat.CategoryID = Convert.ToInt32(catList.SelectedValue);
                ViewState.Add("Category", Cat);

                AddNewCategoryPanel.Visible = false;
                ListBoxPanel.Visible = false;
                EditCategoryPanel.Visible = true;
                TaskPanel.Visible = false;
                EditCategoryName.Text = catList.SelectedItem.Text;
                EditCategoryButton.Text = "Update Category";

                GenerateUserLists();
            }
            else
            {
                ErrorMessage.Text = "Not a valid category.";
            }
        }
        protected void UpdateTask_Click(object sender, EventArgs e)
        {
            BindUsers(EditAssignUserToTask);
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            TaskPanel.Visible = true;
            EditTaskName.Text = taskList.SelectedItem.Text;
            EditTaskButton.Text = "Update Task";
        }
        protected void UpdateMainStep_Click(object sender, EventArgs e)
        {
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = false;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            MainStepName.Text = mainStep.SelectedItem.Text;
            MainStepButton.Text = "Update Main Step";
        }
        protected void UpdateDetailedStep_Click(object sender, EventArgs e)
        {
            MainStepManagement.Visible = false;
            DetailedStepManagement.Visible = true;
            AddNewCategoryPanel.Visible = false;
            ListBoxPanel.Visible = false;
            TaskManagmentPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = true;
            DetailedStepName.Text = detailedStep.SelectedItem.Text;
            EditDetailedStepButton.Text = "Update Detailed Step";
        }
        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            Cat.CategoryID = Convert.ToInt32(catList.SelectedValue);
            Cat.DeleteCategory();
            BindCategories(catList);
            SuccessMessage.Text = "Category successfully deleted.";

            if (catList.Items.Contains(new ListItem("--Add Categories--")))
            {
                taskList.Items.Clear();
                taskList.Attributes.Add("disabled", "true");
            }
        }
        protected void IsActiveTaskButton_Click(object sender, EventArgs e)
        {
            ITask = (Task)ViewState["Task"];

            if (ITask.IsActive)
            {
                ITask.IsActive = false;
                IsActiveTask.Text = "Activate Task";
                IsActiveTask.CssClass = "btn btn-success";
            }
            else
            {
                ITask.IsActive = true;
                IsActiveTask.Text = "Deactivate Task";
                IsActiveTask.CssClass = "btn btn-danger";
            }
        }
        protected void DeleteMainStep_Click(object sender, EventArgs e)
        {
            IMainStep.MainStepID = Convert.ToInt32(mainStep.SelectedValue);
            IMainStep.DeleteMainStep();
            RefreshMainSteps();
            SuccessMessage.Text = "main step successfully deleted.";

            if (mainStep.Items.Contains(new ListItem("No Main Steps")))
            {
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
            }
        }
        protected void DeleteDetailedStep_Click(object sender, EventArgs e)
        {
            IDetailedStep.DetailedStepID = Convert.ToInt32(detailedStep.SelectedValue);
            IDetailedStep.DeleteDetailedStep();
            RefreshDetailedSteps();
            SuccessMessage.Text = "detailed step successfully deleted.";
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
                UsersInCategory.Items.Remove(UsersInCategory.SelectedItem);
            }
        }

        protected void MoveRight_Click(object sender, EventArgs e)
        {
            if (AllUsers.SelectedItem != null &&
                !UsersInCategory.Items.Contains(AllUsers.SelectedItem))
            {
                UsersInCategory.Items.Add(AllUsers.SelectedItem);
            }
        }

        
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = true;
            TaskPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = false;
            catList.SelectedIndex = -1;
            taskList.SelectedIndex = -1;
            mainStep.SelectedIndex = -1;
            detailedStep.SelectedIndex = -1;

        }
        private void BindCategories(ListControl list)
        {
            CategoryListSource.Select(DataSourceSelectArguments.Empty);
            CategoryListSource.DataBind();

            if((bool)ViewState["CategoriesExist"])
            {
                list.Items.Clear();
                list.DataSource = CategoryListSource;
                list.DataBind();
                list.Items.Add("--Add Categories--");
            }
            else
            {
                list.Items.Clear();
                list.Items.Add("--Add Categories--");
            }
        }
        private void GenerateUserLists()
        {
            AllUsers.Items.Clear();
            UsersInCategory.Items.Clear();

            List<string> UsersAssignedToSupervisor = Member.UsersAssignedToSupervisor(UserName);

            if (UsersAssignedToSupervisor.Count > 0)
            {
                AllUsers.DataSource = UsersAssignedToSupervisor;
                AllUsers.DataBind();
            }

            if (EditCategoryButton.Text == "Update Category")
            {
                List<string> UsersAssignedToSupervisorAssignedToCategory = Member.UsersAssignedToSupervisorAssignedToCategory(UserName, Convert.ToInt32(catList.SelectedValue));

                if(UsersAssignedToSupervisorAssignedToCategory.Count > 0)
                {
                    UsersInCategory.DataSource = UsersAssignedToSupervisorAssignedToCategory;
                    UsersInCategory.DataBind();
                }
            }
        }
        protected void QueryTasks(object sender, EventArgs e)
        {
            AddNewCategoryPanel.Visible = true;
            TaskManagmentPanel.Visible = MainStepManagement.Visible = DetailedStepManagement.Visible = false;

            if (catList.SelectedItem.Text != "--Add Categories--")
            {
                ITask = (Task)ViewState["Task"];
                ITask.CategoryID = Convert.ToInt32(Convert.ToInt32(catList.SelectedValue));
                ViewState.Add("Task", ITask);

                taskList.Items.Clear();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                taskList.Items.Add("--Add Tasks--");
                RefreshTasks();

                if (taskList.Items.Count == 0)
                {
                    taskList.Items.Add("--Add Tasks--");
                }

                Update.Visible = Delete.Visible = true;
                taskList.Attributes.Remove("disabled");
            }
            else if (catList.SelectedValue == "--Add Categories--")
            {
                taskList.Items.Clear();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                taskList.Attributes.Add("disabled", "true");
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Attributes.Add("disabled", "true");
                Update.Visible = Delete.Visible = false;
            }
        }
        protected void QueryMainStep(object sender, EventArgs e)
        {
            TaskManagmentPanel.Visible = true;
            AddNewCategoryPanel.Visible = MainStepManagement.Visible = DetailedStepManagement.Visible = false;

            if (taskList.SelectedValue != "--Add Tasks--")
            {
                ITask = (Task)ViewState["Task"];
                ITask.TaskID = Convert.ToInt32(Convert.ToInt32(taskList.SelectedValue));
                ViewState.Add("Task", ITask);

                IMainStep = (MainStep)ViewState["MainStep"];
                IMainStep.TaskID = Convert.ToInt32(Convert.ToInt32(taskList.SelectedValue));
                ViewState.Add("MainStep", IMainStep);

                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                mainStep.Items.Add("--Add Main Steps--");
                RefreshMainSteps();
                
                if (mainStep.Items.Count == 0)
                {
                    mainStep.Items.Add("--Add Main Steps--");
                }

                if (ITask.IsActive)
                {
                    IsActiveTask.Text = "Deactivate Task";
                    IsActiveTask.CssClass = "btn btn-danger";
                }
                else
                {
                    IsActiveTask.Text = "Activate Task";
                    IsActiveTask.CssClass = "btn btn-success";
                }

                UpdateTask.Visible = IsActiveTask.Visible = true;
                mainStep.Attributes.Remove("disabled");
            }
            else if (taskList.SelectedValue == "--Add Tasks--")
            {
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Attributes.Add("disabled", "true");
                UpdateTask.Visible = IsActiveTask.Visible = false;
            }
        }
        protected void QueryDetailedStep(object sender, EventArgs e)
        {

            MainStepManagement.Visible = true;
            TaskManagmentPanel.Visible = AddNewCategoryPanel.Visible = DetailedStepManagement.Visible = false;

            if (mainStep.SelectedItem.Text != "--Add Main Steps--")
            {
                IMainStep = (MainStep)ViewState["MainStep"];
                IMainStep.MainStepID = Convert.ToInt32(Convert.ToInt32(mainStep.SelectedValue));
                ViewState.Add("MainStep", IMainStep);

                IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
                IDetailedStep.MainStepID = Convert.ToInt32(Convert.ToInt32(mainStep.SelectedValue));
                ViewState.Add("DetailedStep", IDetailedStep);

                detailedStep.Items.Clear();
                detailedStep.Items.Add("--Add Detailed Steps--");
                RefreshDetailedSteps();
                
                if (detailedStep.Items.Count == 0)
                {
                    detailedStep.Items.Add("--Add Detailed Steps--");
                }

                UpdateMainStep.Visible = DeleteMainStep.Visible = true;
                detailedStep.Attributes.Remove("disabled");
            }
            else if (mainStep.SelectedValue == "--Add Main Steps--")
            {
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
                UpdateMainStep.Visible = DeleteMainStep.Visible = MainStepMoveUp.Visible = MainStepMoveDown.Visible = false;
            }

            if(mainStep.Items.Count > 1 && mainStep.SelectedValue != "--Add Main Steps--")
            {
                MainStepMoveUp.Visible = MainStepMoveDown.Visible = true;
            }
            else
            {
                MainStepMoveUp.Visible = MainStepMoveDown.Visible = false;
            }
        }
        protected void detailButtons(object sender, EventArgs e)
        {
            DetailedStepManagement.Visible = true;
            MainStepManagement.Visible = TaskManagmentPanel.Visible = AddNewCategoryPanel.Visible = false;

            if(detailedStep.SelectedItem.Text != "--Add Detailed Steps--")
            {
                IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
                IDetailedStep.DetailedStepID = Convert.ToInt32(Convert.ToInt32(detailedStep.SelectedValue));
                ViewState.Add("DetailedStep", IDetailedStep);

                UpdateDetailedStep.Visible = DeleteDetailedStep.Visible = true;
            }
            else
            {
                UpdateDetailedStep.Visible = DeleteDetailedStep.Visible = false;
            }

            if (detailedStep.Items.Count > 1)
            {
                DetailedStepMoveUp.Visible = DetailedStepMoveDown.Visible = true;
            }
            else
            {
                DetailedStepMoveUp.Visible = DetailedStepMoveDown.Visible = false;
            }
        }
        protected void catFilter_TextChanged(object sender, EventArgs e)
        {
            BindCategories(catList);
        }
        
        /*Main Step Function*/
        private void RefreshTasks()
        {
            ITask = (Task)ViewState["Task"];
            TaskListSource.SelectCommand = "SELECT * FROM [Tasks] WHERE ([CategoryID] = @CategoryID)";
            TaskListSource.SelectParameters["CategoryID"].DefaultValue = ITask.CategoryID.ToString();
            taskList.Items.Clear();
            taskList.DataSource = TaskListSource;
            taskList.DataBind();
            taskList.Items.Add("--Add Tasks--");
        }

        private void RefreshMainSteps()
        {
            IMainStep = (MainStep)ViewState["MainStep"];
            MainStepListSource.SelectCommand = "SELECT * FROM [MainSteps] WHERE ([TaskID] = @TaskID) ORDER BY ListOrder";
            MainStepListSource.SelectParameters["TaskID"].DefaultValue = IMainStep.TaskID.ToString();
            mainStep.Items.Clear();
            mainStep.DataSource = MainStepListSource;
            mainStep.DataBind();
            mainStep.Items.Add("--Add Main Steps--");
        }

        private void RefreshDetailedSteps()
        {
            IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
            DetailedStepListSource.SelectCommand = "SELECT * FROM [DetailedSteps] WHERE ([MainStepID] = @MainStepID) ORDER BY ListOrder";
            DetailedStepListSource.SelectParameters["MainStepID"].DefaultValue = IDetailedStep.MainStepID.ToString();
            detailedStep.Items.Clear();
            detailedStep.DataSource = DetailedStepListSource;
            detailedStep.DataBind();
            detailedStep.Items.Add("--Add Detailed Steps--");
        }


        private void BindUsers(DropDownList drp)
        {
            drp.DataSource = Member.UsersAssignedToSupervisor(UserName);
            drp.DataBind();

            Methods.AddBlankToDropDownList(drp);
        }
        protected void MainStepMoveDown_Click(object sender, EventArgs e)
        {
            if (mainStep.SelectedValue != "" && mainStep.SelectedIndex != mainStep.Items.Count - 1)
            {
                IMainStep = (MainStep)ViewState["MainStep"];

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
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    SqlCommand cmd2 = new SqlCommand(queryString2, con);
                    SqlCommand cmd3 = new SqlCommand(queryString3, con);
                    SqlCommand cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@mainstepid", Convert.ToInt32(mainStep.SelectedValue));

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
                    cmd4.Parameters.AddWithValue("@mainstepid1", Convert.ToInt32(mainStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@mainstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshMainSteps();
            }
        }
        protected void MainStepMoveUp_Click(object sender, EventArgs e)
        {
            if (mainStep.SelectedValue != "" && mainStep.SelectedIndex != 0)
            {
                IMainStep = (MainStep)ViewState["MainStep"];

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
                    cmd.Parameters.AddWithValue("@mainstepid", Convert.ToInt32(mainStep.SelectedValue));

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
                    cmd4.Parameters.AddWithValue("@mainstepid1", Convert.ToInt32(mainStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@mainstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshMainSteps();
            }
        }
        protected void DetailedStepMoveDown_Click(object sender, EventArgs e)
        {
            if (detailedStep.SelectedValue != "" && detailedStep.SelectedIndex != detailedStep.Items.Count - 1)
            {
                IDetailedStep = (DetailedStep)ViewState["DetailedStep"];

                string queryString =
                    "SELECT ListOrder " +
                    "FROM DetailedSteps " +
                    "WHERE DetailedStepID=@detailedstepid";

                string queryString2 =
                    "SELECT MIN(ListOrder) " +
                    "FROM DetailedSteps " +
                    "WHERE ListOrder > @listorder " +
                    "AND MainStepID=@mainstepid";

                string queryString3 =
                    "SELECT DetailedStepID " +
                    "FROM DetailedSteps " +
                    "WHERE ListOrder = ( " +
                        "SELECT MIN(ListOrder) " +
                        "FROM DetailedSteps " +
                        "WHERE ListOrder > @listorder " +
                        "AND MainStepID=@mainstepid " +
                    ") " +
                    "AND MainStepID=@mainstepid";

                string queryString4 =
                    "UPDATE DetailedSteps " +
                    "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                    "WHERE DetailedStepID IN (@detailedstepid1, @detailedstepid2)";

                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    SqlCommand cmd2 = new SqlCommand(queryString2, con);
                    SqlCommand cmd3 = new SqlCommand(queryString3, con);
                    SqlCommand cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@detailedstepid", Convert.ToInt32(detailedStep.SelectedValue));

                    con.Open();

                    int FirstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd2.Parameters.AddWithValue("@mainstepid", IDetailedStep.MainStepID);

                    cmd3.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd3.Parameters.AddWithValue("@mainstepid", IDetailedStep.MainStepID);

                    con.Open();

                    int SecondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    int ThirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", FirstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", SecondValue);
                    cmd4.Parameters.AddWithValue("@detailedstepid1", Convert.ToInt32(detailedStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@detailedstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshDetailedSteps();
            }
        }
        protected void DetailedStepMoveUp_Click(object sender, EventArgs e)
        {
            if (detailedStep.SelectedValue != "" && detailedStep.SelectedIndex != 0)
            {
                IDetailedStep = (DetailedStep)ViewState["DetailedStep"];

                string queryString =
                    "SELECT ListOrder " +
                    "FROM DetailedSteps " +
                    "WHERE DetailedStepID=@detailedstepid";

                string queryString2 =
                    "SELECT MAX(ListOrder) " +
                    "FROM DetailedSteps " +
                    "WHERE ListOrder < @listorder " +
                    "AND MainStepID=@mainstepid";

                string queryString3 =
                    "SELECT DetailedStepID " +
                    "FROM DetailedSteps " +
                    "WHERE ListOrder = ( " +
                        "SELECT MAX(ListOrder) " +
                        "FROM DetailedSteps " +
                        "WHERE ListOrder < @listorder " +
                        "AND MainStepID=@mainstepid " +
                    ") " +
                    "AND MainStepID=@mainstepid";

                string queryString4 =
                    "UPDATE DetailedSteps " +
                    "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                    "WHERE DetailedStepID IN (@detailedstepid1, @detailedstepid2)";

                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    SqlCommand cmd2 = new SqlCommand(queryString2, con);
                    SqlCommand cmd3 = new SqlCommand(queryString3, con);
                    SqlCommand cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@detailedstepid", Convert.ToInt32(detailedStep.SelectedValue));

                    con.Open();

                    int FirstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd2.Parameters.AddWithValue("@mainstepid", IDetailedStep.MainStepID);

                    cmd3.Parameters.AddWithValue("@listorder", FirstValue);
                    cmd3.Parameters.AddWithValue("@mainstepid", IDetailedStep.MainStepID);

                    con.Open();

                    int SecondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    int ThirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", FirstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", SecondValue);
                    cmd4.Parameters.AddWithValue("@detailedstepid1", Convert.ToInt32(detailedStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@detailedstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshDetailedSteps();
            }
        }

        protected void CategoryListSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.AffectedRows > 0)
            {
                ViewState["CategoriesExist"] = true;
            }
            else
            {
                ViewState["CategoriesExist"] = false;
            }
        }

        protected void TaskListSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {

        }
    }
}