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
                catDateSort.Text = "Date Asc";
                taskDateSort.Text = "Date Asc";
                mainStepSort.Text = "Date Asc";
                detailedSort.Text = "Date Asc";
                taskDateSort.Attributes.Add("disabled", "true");
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

            if (taskList.Items.Count == 0)
                taskList.Attributes.Add("disabled", "true");
            if (mainStep.Items.Count == 0)
                mainStep.Attributes.Add("disabled", "true");
            if (detailedStep.Items.Count == 0)
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
                header.Text = "Management Panel";
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
                header.Text = "Management Panel";
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
                    header.Text = "Edit Task: " + ITask.TaskName;
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
            header.Text = "Management Panel";
        }
        protected void EditMainStepButton_Click(object sender, EventArgs e)
        {
            IMainStep = (MainStep)ViewState["MainStep"];
            string Message = "";

            ErrorMessage.Text = String.Empty;

            if (MainStepName.Text != String.Empty)
            {

                IMainStep.MainStepName = MainStepName.Text;
                IMainStep.MainStepText =
                    !String.IsNullOrEmpty(MainStepText.Text) ? MainStepText.Text : null;

                if (MainStepAudio.HasFile)
                {
                    Message = Methods.UploadFile(MainStepAudio, "Audio");

                    if (Message == "")
                    {
                        IMainStep.AudioFilename = MainStepAudio.FileName;
                        IMainStep.AudioPath = "~/Uploads/" + MainStepAudio.FileName;
                    }
                    else
                    {
                        Message += "<br/>";
                    }
                }

                if (MainStepVideo.HasFile)
                {
                    Message += Methods.UploadFile(MainStepVideo, "Video");

                    if (Message == "")
                    {
                        IMainStep.VideoFilename = MainStepVideo.FileName;
                        IMainStep.VideoPath = "~/Uploads/" + MainStepVideo.FileName;
                    }
                }

                if (MainStepButton.Text == "Add New Main Step")
                {
                    if (Message == "")
                    {
                        IMainStep.CreateMainStep();
                        SuccessMessage.Text = "New main step successfully added.";
                    }
                    else
                    {
                        ErrorMessage.Text = Message;
                    }
                }
                if (MainStepButton.Text == "Update Main Step")
                {
                    if (Message == "")
                    {
                        IMainStep.UpdateMainStep();
                        SuccessMessage.Text = "Main step successfully updated.";
                    }
                    else
                    {
                        ErrorMessage.Text = Message;
                    }
                }

                if (Message == "")
                {
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
            header.Text = "Management Panel";
        }
        protected void EditDetailedStepButton_Click(object sender, EventArgs e)
        {
            IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
            string Message = "";

            ErrorMessage.Text = String.Empty;

            if (DetailedStepName.Text != String.Empty)
            {
                IDetailedStep.DetailedStepName = DetailedStepName.Text;
                IDetailedStep.DetailedStepText =
                    !String.IsNullOrEmpty(DetailedStepText.Text) ? DetailedStepText.Text : null;

                if (DetailedStepImage.HasFile)
                {
                    Message = Methods.UploadFile(DetailedStepImage, "Image");

                    if (Message == "")
                    {
                        IDetailedStep.ImageFilename = DetailedStepImage.FileName;
                        IDetailedStep.ImagePath = "~/Uploads/" + DetailedStepImage.FileName;
                    }
                }

                if (EditDetailedStepButton.Text == "Add New Detailed Step")
                {
                    if (Message == "")
                    {
                        IDetailedStep.CreateDetailedStep();
                        SuccessMessage.Text = "New detailed step successfully added.";
                    }
                    else
                    {
                        ErrorMessage.Text = Message;
                    }
                }
                if (EditDetailedStepButton.Text == "Update Detailed Step")
                {
                    if (Message == "")
                    {
                        IDetailedStep.UpdateDetailedStep();
                        SuccessMessage.Text = "detailed step successfully updated.";
                    }
                    else
                    {
                        ErrorMessage.Text = Message;
                    }
                }

                if (Message == "")
                {
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
            header.Text = "Management Panel";
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
            header.Text = EditCategoryButton.Text = "Add New Category";

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
            header.Text = EditTaskButton.Text = "Add New Task";
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
            header.Text = MainStepButton.Text = "Add New Main Step";
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
            header.Text = EditDetailedStepButton.Text = "Add New Detailed Step";
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
                header.Text = "Update Category: " + catList.SelectedItem.Text;
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
            ITask = Task.GetTask(Convert.ToInt32(taskList.SelectedValue));
            EditTaskName.Text = ITask.TaskName;
            EditAssignUserToTask.Text = ITask.AssignedUser;
            EditTaskButton.Text = "Update Task";
            header.Text = "Update Task: " + taskList.SelectedItem.Text;
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
            IMainStep = MainStep.GetMainStep(Convert.ToInt32(mainStep.SelectedValue));
            MainStepName.Text = IMainStep.MainStepName;
            MainStepText.Text = IMainStep.MainStepText;
            MainStepAudioCurrentLabel.Text = "<audio controls><source src='" + ResolveUrl(IMainStep.AudioPath) + "'></audio>";
            MainStepVideoCurrentLabel.Text = "<video controls><source src='" + ResolveUrl(IMainStep.VideoPath) + "'></video>"; ;
            MainStepButton.Text = "Update Main Step";
            header.Text = "Update Main Step: " + mainStep.SelectedItem.Text;
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
            IDetailedStep = DetailedStep.GetDetailedStep(Convert.ToInt32(detailedStep.SelectedValue));
            DetailedStepName.Text = IDetailedStep.DetailedStepName;
            DetailedStepText.Text = IDetailedStep.DetailedStepText;
            DetailedStepImageCurrentLabel.Text = "<img class='image-preview' src='" + ResolveUrl(IDetailedStep.ImagePath) + "'>";
            EditDetailedStepButton.Text = "Update Detailed Step";
            header.Text = "Update Detailed Step: " + detailedStep.SelectedItem.Text;
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
                UsersInCategory.Items.Remove(UsersInCategory.SelectedItem.Value);
            }
        }

        protected void MoveRight_Click(object sender, EventArgs e)
        {
            if (AllUsers.SelectedItem != null &&
                !UsersInCategory.Items.Contains(AllUsers.SelectedItem))
            {
                UsersInCategory.Items.Add(AllUsers.SelectedItem.Value);
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
            EditCategoryName.Text = String.Empty;
            EditTaskName.Text = String.Empty;
            MainStepName.Text = String.Empty;
            MainStepText.Text = String.Empty;
            MainStepAudioCurrentLabel.Text = String.Empty;
            MainStepVideoCurrentLabel.Text = String.Empty;
            DetailedStepName.Text = String.Empty;
            DetailedStepText.Text = String.Empty;
            DetailedStepImageCurrentLabel.Text = String.Empty;
            header.Text = "Management Panel";

        }
        private void BindCategories(ListControl list)
        {
            CategoryListSource.Select(DataSourceSelectArguments.Empty);
            CategoryListSource.DataBind();

            if ((bool)ViewState["CategoriesExist"])
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

                if (UsersAssignedToSupervisorAssignedToCategory.Count > 0)
                {
                    UsersInCategory.DataSource = UsersAssignedToSupervisorAssignedToCategory;
                    UsersInCategory.DataBind();
                }
            }
        }
        protected void QueryTasks(object sender, EventArgs e)
        {
            taskDateSort.Text = "Date Asc";
            AddNewCategoryPanel.Visible = true;
            TaskManagmentPanel.Visible = MainStepManagement.Visible = DetailedStepManagement.Visible = false;

            if (catList.SelectedItem.Text != "--Add Categories--")
            {
                taskDateSort.Attributes.Remove("disabled");
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
                taskDateSort.Attributes.Add("disabled", "true");
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
            mainStepSort.Text = "Date Asc";
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

            if (mainStep.Items.Count > 2 && mainStep.SelectedValue != "--Add Main Steps--")
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

            if (detailedStep.SelectedItem.Text != "--Add Detailed Steps--")
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

            if (detailedStep.Items.Count > 2)
            {
                DetailedStepMoveUp.Visible = DetailedStepMoveDown.Visible = true;
            }
            else
            {
                DetailedStepMoveUp.Visible = DetailedStepMoveDown.Visible = false;
            }
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

        protected void catDateSort_Click(object sender, EventArgs e)
        {
                string queryString = "";
                switch (catDateSort.Text)
                {
                    case "Date Desc": catDateSort.Text = "Date Asc"; queryString = "SELECT * FROM Categories WHERE CreatedBy=@supervisor ORDER BY CreatedTime ASC"; break;
                    case "Date Asc": catDateSort.Text = "Date Desc"; queryString = "SELECT * FROM Categories WHERE CreatedBy=@supervisor ORDER BY CreatedTime DESC"; break;
                    default: break;
                }
                catList.DataSource = null;
                catList.Items.Clear();
                taskList.Items.Clear();
                taskList.Attributes.Add("disabled", "true");
                mainStep.Items.Clear();
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
                List<Category> sort = new List<Category>();
                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);

                    cmd.Parameters.AddWithValue("@supervisor", Membership.GetUser().UserName);


                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Category cat = new Category();
                        cat.CategoryName = Convert.ToString(dr["CategoryName"]);
                        cat.CategoryID = Convert.ToInt32(dr["CategoryId"]);
                        cat.CreatedTime = Convert.ToString(dr["CreatedTime"]);
                        sort.Add(cat);
                    }

                    con.Close();
                }
                catList.DataSource = sort;
                catList.DataBind();
                catList.Items.Add("--Add Categories--");
        }

        protected void taskDateSort_Click(object sender, EventArgs e)
        {
            if (catList.SelectedIndex != -1)
            {
                string queryString = "";
                switch (taskDateSort.Text)
                {
                    case "Date Desc": taskDateSort.Text = "Date Asc"; queryString = "SELECT * FROM Tasks WHERE CreatedBy=@supervisor AND CategoryID=@catID ORDER BY CreatedTime ASC"; break;
                    case "Date Asc": taskDateSort.Text = "Date Desc"; queryString = "SELECT * FROM Tasks WHERE CreatedBy=@supervisor AND CategoryID=@catID ORDER BY CreatedTime DESC"; break;
                    default: break;
                }
                taskList.DataSource = null;
                taskList.Items.Clear();
                mainStep.Items.Clear();
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
                List<Task> sort = new List<Task>();
                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);

                    cmd.Parameters.AddWithValue("@supervisor", Membership.GetUser().UserName);
                    cmd.Parameters.AddWithValue("@catID", catList.SelectedValue);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Task task = new Task();
                        task.TaskName = Convert.ToString(dr["TaskName"]);
                        task.TaskID = Convert.ToInt32(dr["TaskID"]);
                        task.CreatedTime = Convert.ToString(dr["CreatedTime"]);
                        sort.Add(task);
                    }

                    con.Close();
                }
                taskList.DataSource = sort;
                taskList.DataBind();
                taskList.Items.Add("--Add Tasks--");
            }
        }
        protected void mainStep_Sort(object sender, EventArgs e)
        {
            if (taskList.SelectedIndex != -1)
            {
                string queryString = "";
                switch (mainStepSort.Text)
                {
                    case "Date Desc": mainStepSort.Text = "Date Asc"; queryString = "SELECT * FROM MainSteps WHERE TaskID=@id ORDER BY CreatedTime ASC"; break;
                    case "Date Asc": mainStepSort.Text = "Date Desc"; queryString = "SELECT * FROM MainSteps WHERE TaskID=@id ORDER BY CreatedTime DESC"; break;
                    default: break;
                }
                mainStep.DataSource = null;
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
                List<MainStep> sort = new List<MainStep>();
                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);

                    cmd.Parameters.AddWithValue("@supervisor", Membership.GetUser().UserName);
                    cmd.Parameters.AddWithValue("@id", taskList.SelectedValue);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        MainStep step = new MainStep();
                        step.MainStepName = Convert.ToString(dr["MainStepName"]);
                        step.MainStepID = Convert.ToInt32(dr["MainStepID"]);
                        step.CreatedTime = Convert.ToString(dr["CreatedTime"]);
                        sort.Add(step);
                    }

                    con.Close();
                }
                mainStep.DataSource = sort;
                mainStep.DataBind();
                mainStep.Items.Add("--Add Main Steps--");
            }
        }
        protected void detailedDateSort_Click(object sender, EventArgs e)
        {
            if (mainStep.SelectedIndex != -1)
            {
                string queryString = "";
                switch (detailedSort.Text)
                {
                    case "Date Desc": detailedSort.Text = "Date Asc"; queryString = "SELECT * FROM DetailedSteps WHERE MainStepID=@id ORDER BY CreatedTime ASC"; break;
                    case "Date Asc": detailedSort.Text = "Date Desc"; queryString = "SELECT * FROM DetailedSteps WHERE MainStepID=@id ORDER BY CreatedTime DESC"; break;
                    default: break;
                }
                detailedStep.DataSource = null;
                detailedStep.Items.Clear();
                List<DetailedStep> sort = new List<DetailedStep>();
                using (SqlConnection con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand(queryString, con);

                    cmd.Parameters.AddWithValue("@supervisor", Membership.GetUser().UserName);
                    cmd.Parameters.AddWithValue("@id", mainStep.SelectedValue);


                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DetailedStep step = new DetailedStep();
                        step.DetailedStepName = Convert.ToString(dr["DetailedStepName"]);
                        step.DetailedStepID = Convert.ToInt32(dr["DetailedStepID"]);
                        step.CreatedTime = Convert.ToString(dr["CreatedTime"]);
                        sort.Add(step);
                    }

                    con.Close();
                }
                detailedStep.DataSource = sort;
                detailedStep.DataBind();
                detailedStep.Items.Add("--Add Detailed Steps--");
            }
        }

        protected void catFilter_TextChanged(object sender, EventArgs e)
        {
            BindCategories(catList);
        }

        protected void taskFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshTasks();
        }

        protected void mainFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshMainSteps();
        }

        protected void detailFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshDetailedSteps();
        }
    }
}