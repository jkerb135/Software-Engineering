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
using System.Web.Services;
namespace SE
{
    public partial class Categories : System.Web.UI.Page
    {
        public int catIDX = 0, taskIDX = 0, mainIDX = 0, deatIDX = 0;
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
                catFilter.Enabled = taskFilter.Enabled = mainFilter.Enabled = detailFilter.Enabled = false;
                catDateSort.Text = "Date \u25B2";
                taskDateSort.Text = "Date \u25B2";
                mainStepSort.Text = "Date \u25B2";
                detailedSort.Text = "Date \u25B2";
                AddNewTask.Attributes.Add("disabled", "true");
                AddNewMainStep.Attributes.Add("disabled", "true");
                AddNewDetailedStep.Attributes.Add("disabled", "true");
                taskDateSort.Attributes.Add("disabled", "true");
                mainStepSort.Attributes.Add("disabled", "true");
                detailedSort.Attributes.Add("disabled", "true");
                UpdateCategory.Attributes.Add("disabled", "true");
                DeleteCategory.Attributes.Add("disabled", "true");
                UpdateTask.Attributes.Add("disabled", "true");
                IsActiveTask.Attributes.Add("disabled", "true");
                UpdateMainStep.Attributes.Add("disabled", "true");
                DeleteMainStep.Attributes.Add("disabled", "true");
                UpdateDetailedStep.Attributes.Add("disabled", "true");
                DeleteDetailedStep.Attributes.Add("disabled", "true");


                BindCategories(catList);

                EditCategoryPanel.Visible = false;

                MainStepMoveUp.Attributes.Add("disabled", "true");
                MainStepMoveDown.Attributes.Add("disabled", "true");
                DetailedStepMoveUp.Attributes.Add("disabled", "true");
                DetailedStepMoveDown.Attributes.Add("disabled", "true");
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

                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                }
                else
                {
                    ErrorMessage.Text = "Error Updating Category";
                }
                header.Text = "Management Panel";
            }
            if (catList.Items.Count > 1) { catList.SelectedIndex = catIDX; };
        }
        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            ITask = (Task)ViewState["Task"];

            if (EditTaskButton.Text == "Add New Task")
            {
                if (EditTaskName.Text != String.Empty)
                {
                    ITask.TaskName = EditTaskName.Text;

                    ITask.TaskAssignments = (from l in UsersAssignedToTask.Items.Cast<ListItem>() select l.Value).ToList();

                    ITask.CreateTask();
                    ITask.AssignUserTasks();

                    SuccessMessage.Text = "New task successfully added.";

                    EditTaskName.Text = String.Empty;

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

                    ITask.TaskAssignments = (from l in UsersAssignedToTask.Items.Cast<ListItem>() select l.Value).ToList();

                    ITask.UpdateTask();
                    ITask.ReAssignUserTasks();

                    SuccessMessage.Text = "Task successfully updated.";

                    EditTaskName.Text = String.Empty;

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
            if (taskList.Items.Count > 1) { taskList.SelectedIndex = taskIDX; };
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

                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    TaskPanel.Visible = false;
                    ManageMainStepPanel.Visible = false;
                    ManageDetailedStepPanel.Visible = false;

                    RefreshMainSteps();
                }
            }
            header.Text = "Management Panel";
            if (mainStep.Items.Count > 1) { mainStep.SelectedIndex = mainIDX; };
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

                    ListBoxPanel.Visible = true;
                    EditCategoryPanel.Visible = false;
                    TaskPanel.Visible = false;
                    ManageMainStepPanel.Visible = false;
                    ManageDetailedStepPanel.Visible = false;

                    RefreshDetailedSteps();
                }
            }
            header.Text = "Management Panel";
            if (detailedStep.Items.Count > 1) { detailedStep.SelectedIndex = deatIDX; };
        }
        protected void AddNewCategory_Click(object sender, EventArgs e)
        {
            EditCategoryName.Text = String.Empty;

            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = true;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = false;
            header.Text = EditCategoryButton.Text = "Add New Category";

            GenerateUserLists();
        }
        protected void AddNewTask_Click(object sender, EventArgs e)
        {
            EditTaskName.Text = String.Empty;

            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;

            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = false;
            TaskPanel.Visible = true;
            header.Text = EditTaskButton.Text = "Add New Task";

            GenerateUserLists();
        }
        protected void AddNewMainStep_Click(object sender, EventArgs e)
        {

            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            header.Text = MainStepButton.Text = "Add New Main Step";
        }
        protected void AddNewDetailedStep_Click(object sender, EventArgs e)
        {



            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = true;
            header.Text = EditDetailedStepButton.Text = "Add New Detailed Step";
        }
        protected void UpdateCategory_Click(object sender, EventArgs e)
        {
            if (catList.SelectedItem.Text != "No Categories")
            {
                Cat = (Category)ViewState["Category"];
                Cat.CategoryID = Convert.ToInt32(catList.SelectedValue);
                ViewState.Add("Category", Cat);
                catFilter.Enabled = true;

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
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;

            TaskPanel.Visible = true;
            ITask = Task.GetTask(Convert.ToInt32(taskList.SelectedValue));
            EditTaskName.Text = ITask.TaskName;
            EditTaskButton.Text = "Update Task";
            header.Text = "Update Task: " + taskList.SelectedItem.Text;
            GenerateUserLists();
        }
        protected void UpdateMainStep_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            IMainStep = MainStep.GetMainStep(Convert.ToInt32(mainStep.SelectedValue));
            MainStepName.Text = IMainStep.MainStepName;
            MainStepText.Text = IMainStep.MainStepText;

            if (IMainStep.AudioPath.Length > 0)
                MainStepAudioCurrentLabel.Text = "<audio controls><source src='" + ResolveUrl(IMainStep.AudioPath) + "'></audio>";
            else
                MainStepAudioCurrentLabel.Text = "";

            if (IMainStep.VideoPath.Length > 0)
                MainStepVideoCurrentLabel.Text = "<video controls><source src='" + ResolveUrl(IMainStep.VideoPath) + "'></video>";
            else
                MainStepVideoCurrentLabel.Text = "";

            MainStepButton.Text = "Update Main Step";
            header.Text = "Update Main Step: " + mainStep.SelectedItem.Text;
        }
        protected void UpdateDetailedStep_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = true;
            IDetailedStep = DetailedStep.GetDetailedStep(Convert.ToInt32(detailedStep.SelectedValue));
            DetailedStepName.Text = IDetailedStep.DetailedStepName;
            DetailedStepText.Text = IDetailedStep.DetailedStepText;

            if (IDetailedStep.ImagePath.Length > 0)
                DetailedStepImageCurrentLabel.Text = "<img class='image-preview' src='" + ResolveUrl(IDetailedStep.ImagePath) + "'>";
            else
                DetailedStepImageCurrentLabel.Text = "";

            EditDetailedStepButton.Text = "Update Detailed Step";
            header.Text = "Update Detailed Step: " + detailedStep.SelectedItem.Text;
        }
        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            Cat.CategoryID = Convert.ToInt32(catList.SelectedValue);

            if (Cat.IsActive)
            {
                Cat.IsActive = false;
                DeleteCategory.Text = "Activate";
                DeleteCategory.CssClass = "btn btn-success form-control";
            }
            else
            {
                Cat.IsActive = true;
                DeleteCategory.Text = "Deactivate";
                DeleteCategory.CssClass = "btn btn-danger form-control";
            }

            BindCategories(catList);
        }
        protected void IsActiveTaskButton_Click(object sender, EventArgs e)
        {
            ITask = (Task)ViewState["Task"];

            if (ITask.IsActive)
            {
                ITask.IsActive = false;
                IsActiveTask.Text = "Activate";
                IsActiveTask.CssClass = "btn btn-success form-control";
            }
            else
            {
                ITask.IsActive = true;
                IsActiveTask.Text = "Deactivate";
                IsActiveTask.CssClass = "btn btn-danger form-control";
            }
        }
        protected void DeleteMainStep_Click(object sender, EventArgs e)
        {
            IMainStep.MainStepID = Convert.ToInt32(mainStep.SelectedValue);
            IMainStep.DeleteMainStep();
            RefreshMainSteps();
            SuccessMessage.Text = "main step successfully deleted.";

            if (mainStep.Items.Contains(new ListItem("No Main Steps in " + taskList.SelectedItem.Text)))
            {
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
            }
            if (mainStep.Items[0].Text == "No Main Steps in " + taskList.SelectedItem.Text)
            {
                mainStep.Items[0].Attributes.Add("disabled", "disabled");
            }

        }
        protected void DeleteDetailedStep_Click(object sender, EventArgs e)
        {
            IDetailedStep.DetailedStepID = Convert.ToInt32(detailedStep.SelectedValue);
            IDetailedStep.DeleteDetailedStep();
            RefreshDetailedSteps();
            SuccessMessage.Text = "detailed step successfully deleted.";
            if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text)
            {
                detailedStep.Items[0].Attributes.Add("disabled", "disabled");
            }
        }

        /*Button Binding Events*/
        protected void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryList.SelectedValue != "")
            {
                AllUsers.Items.Clear();
                UsersInCategory.Items.Clear();

                EditCategoryPanel.Visible = true;
                MainStepMoveUp.Attributes.Add("disabled", "true");
                MainStepMoveDown.Attributes.Add("disabled", "true");
                DetailedStepMoveUp.Attributes.Add("disabled", "true");
                DetailedStepMoveDown.Attributes.Add("disabled", "true");
            }
        }

        protected void MoveLeft_Click(object sender, EventArgs e)
        {
            ListBox control = EditTaskPanel.Visible ? UsersAssignedToTask : UsersInCategory;

            if (control.SelectedItem != null)
            {
                control.Items.Remove(control.SelectedItem.Value);
            }
        }

        protected void MoveRight_Click(object sender, EventArgs e)
        {
            ListBox control = EditTaskPanel.Visible ? AllUsersTask : AllUsers;
            ListBox control2 = EditTaskPanel.Visible ? UsersAssignedToTask : UsersInCategory;

            if (control.SelectedItem != null &&
                !control2.Items.Contains(control.SelectedItem))
            {
                control2.Items.Add(control.SelectedItem.Value);
            }
        }


        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = true;
            TaskPanel.Visible = false;
            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = false;
            EditCategoryName.Text = String.Empty;
            EditTaskName.Text = String.Empty;
            MainStepName.Text = String.Empty;
            MainStepText.Text = String.Empty;
            MainStepAudioCurrentLabel.Text = String.Empty;
            MainStepVideoCurrentLabel.Text = String.Empty;
            DetailedStepName.Text = String.Empty;
            DetailedStepText.Text = String.Empty;
            DetailedStepImageCurrentLabel.Text = String.Empty;
            if (catList.Items.Count == 1 && catList.Items[0].Text == "No Categories") { catList.Items[0].Attributes.Add("disabled", "disabled"); }
            if (taskList.Items.Count == 1 && taskList.Items[0].Text == "No Tasks in " + catList.SelectedItem.Text) { taskList.Items[0].Attributes.Add("disabled", "disabled"); }
            if (mainStep.Items.Count == 1 && mainStep.Items[0].Text == "No Main Steps in " + taskList.SelectedItem.Text) { mainStep.Items[0].Attributes.Add("disabled", "disabled"); }
            if (detailedStep.Items.Count == 1 && detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text) { detailedStep.Items[0].Attributes.Add("disabled", "disabled"); }
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
                catFilter.Enabled = true;
            }
            else
            {
                list.Items.Clear();
                list.Items.Add("No Categories");
                UpdateCategory.Attributes.Add("disabled", "disabled");
                DeleteCategory.Attributes.Add("disabled", "disabled");
                list.Items[0].Attributes.Add("disabled", "disabled");
            }
        }
        private void GenerateUserLists()
        {
            ListBox control = EditTaskPanel.Visible ? AllUsersTask : AllUsers;
            ListBox control2 = EditTaskPanel.Visible ? UsersAssignedToTask : UsersInCategory;

            control.Items.Clear();
            control2.Items.Clear();

            List<string> UsersAssignedToSupervisor = Member.UsersAssignedToSupervisor(UserName);

            if (UsersAssignedToSupervisor.Count > 0)
            {
                control.DataSource = UsersAssignedToSupervisor;
                control.DataBind();
            }

            if (EditCategoryButton.Text == "Update Category" && EditCategoryPanel.Visible)
            {
                List<string> UsersAssignedToSupervisorAssignedToCategory = Member.UsersAssignedToSupervisorAssignedToCategory(UserName, Convert.ToInt32(catList.SelectedValue));

                if (UsersAssignedToSupervisorAssignedToCategory.Count > 0)
                {
                    UsersInCategory.DataSource = UsersAssignedToSupervisorAssignedToCategory;
                    UsersInCategory.DataBind();
                }
            }
            else if (EditTaskButton.Text == "Update Task" && EditTaskPanel.Visible)
            {
                List<string> UsersAssignedToSupervisorAssignedToTask = Task.UsersAssignedToSupervisorAssignedToTask(UserName, Convert.ToInt32(taskList.SelectedValue));

                if (UsersAssignedToSupervisorAssignedToTask.Count > 0)
                {
                    UsersAssignedToTask.DataSource = UsersAssignedToSupervisorAssignedToTask;
                    UsersAssignedToTask.DataBind();
                }
            }
        }
        protected void QueryTasks(object sender, EventArgs e)
        {
            catIDX = catList.SelectedIndex;
            taskDateSort.Text = "Date \u25B2";
            UpdateCategory.Attributes.Remove("disabled");
            DeleteCategory.Attributes.Remove("disabled");
            mainStep.Attributes.Add("disabled", "true");
            detailedStep.Attributes.Add("disabled", "true");
            AddNewMainStep.Attributes.Add("disabled", "true");
            AddNewDetailedStep.Attributes.Add("disabled", "true");
            MainStepMoveUp.Attributes.Add("disabled", "true");
            MainStepMoveDown.Attributes.Add("disabled", "true");
            DetailedStepMoveUp.Attributes.Add("disabled", "true");
            DetailedStepMoveDown.Attributes.Add("disabled", "true");
            UpdateCategory.Attributes.Remove("disabled");
            DeleteCategory.Attributes.Remove("disabled");
            UpdateTask.Attributes.Add("disabled", "true");
            IsActiveTask.Attributes.Add("disabled", "true");
            UpdateMainStep.Attributes.Add("disabled", "true");
            DeleteMainStep.Attributes.Add("disabled", "true");
            UpdateDetailedStep.Attributes.Add("disabled", "true");
            DeleteDetailedStep.Attributes.Add("disabled", "true");
            if (catList.SelectedItem.Text != "No Categories")
            {
                taskFilter.Enabled = true;
                mainFilter.Enabled = detailFilter.Enabled = false;
                taskDateSort.Attributes.Remove("disabled");
                mainStepSort.Attributes.Add("disabled", "true");
                detailedSort.Attributes.Add("disabled", "true");

                Cat = (Category)ViewState["Category"];
                Cat.CategoryID = Convert.ToInt32(Convert.ToInt32(catList.SelectedValue));
                ViewState.Add("Category", Cat);

                ITask = (Task)ViewState["Task"];
                ITask.CategoryID = Convert.ToInt32(Convert.ToInt32(catList.SelectedValue));
                ViewState.Add("Task", ITask);

                taskList.Items.Clear();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                RefreshTasks();

                if (taskList.Items.Count == 0)
                {
                    ListItem li = new ListItem();
                    li.Text = "No Tasks in " + catList.SelectedItem.Text;
                    li.Attributes.Add("disabled", "disabled");
                    taskList.Items.Add(li);
                    taskFilter.Enabled = false;
                }

                if (Cat.IsActive)
                {
                    DeleteCategory.Text = "Deactivate";
                    DeleteCategory.CssClass = "btn btn-danger form-control";
                }
                else
                {
                    DeleteCategory.Text = "Activate";
                    DeleteCategory.CssClass = "btn btn-success form-control";
                }

                taskList.Attributes.Remove("disabled");
                AddNewTask.Attributes.Remove("disabled");

            }
            else
            {
                taskDateSort.Attributes.Add("disabled", "true");
                taskList.Items.Clear();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                taskList.Attributes.Add("disabled", "true");
                mainStep.Attributes.Add("disabled", "true");
                detailedStep.Attributes.Add("disabled", "true");

            }
        }
        protected void QueryMainStep(object sender, EventArgs e)
        {
            taskIDX = taskList.SelectedIndex;
            mainStepSort.Text = "Date \u25B2";
            detailedStep.Attributes.Add("disabled", "true");
            AddNewDetailedStep.Attributes.Add("disabled", "true");
            DetailedStepMoveUp.Attributes.Add("disabled", "true");
            DetailedStepMoveDown.Attributes.Add("disabled", "true");
            UpdateTask.Attributes.Remove("disabled");
            IsActiveTask.Attributes.Remove("disabled");
            UpdateMainStep.Attributes.Add("disabled", "true");
            DeleteMainStep.Attributes.Add("disabled", "true");
            UpdateDetailedStep.Attributes.Add("disabled", "true");
            DeleteDetailedStep.Attributes.Add("disabled", "true");
            if (taskList.SelectedValue != "No Tasks")
            {
                mainFilter.Enabled = true;
                detailFilter.Enabled = false;
                mainStepSort.Attributes.Remove("disabled");
                detailedSort.Attributes.Add("disabled", "true");
                ITask = (Task)ViewState["Task"];
                ITask.TaskID = Convert.ToInt32(Convert.ToInt32(taskList.SelectedValue));
                ViewState.Add("Task", ITask);

                IMainStep = (MainStep)ViewState["MainStep"];
                IMainStep.TaskID = Convert.ToInt32(Convert.ToInt32(taskList.SelectedValue));
                ViewState.Add("MainStep", IMainStep);

                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                RefreshMainSteps();

                if (mainStep.Items[0].Text == "No Main Steps in " + taskList.SelectedItem.Text)
                {
                    mainStep.Items[0].Attributes.Add("disabled", "disabled");
                    mainFilter.Enabled = false;
                }

                if (ITask.IsActive)
                {
                    IsActiveTask.Text = "Deactivate";
                    IsActiveTask.CssClass = "btn btn-danger form-control";
                }
                else
                {
                    IsActiveTask.Text = "Activate";
                    IsActiveTask.CssClass = "btn btn-success form-control";
                }

                UpdateTask.Visible = IsActiveTask.Visible = true;
                mainStep.Attributes.Remove("disabled");
                AddNewMainStep.Attributes.Remove("disabled");
            }
            else
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
            mainIDX = mainStep.SelectedIndex;
            UpdateMainStep.Attributes.Remove("disabled");
            DeleteMainStep.Attributes.Remove("disabled");
            UpdateDetailedStep.Attributes.Add("disabled", "true");
            DeleteDetailedStep.Attributes.Add("disabled", "true");

            if (mainStep.SelectedItem.Text != "No Main Steps in" + taskList.SelectedItem.Value)
            {
                detailFilter.Enabled = true;
                detailedSort.Attributes.Remove("disabled");
                IMainStep = (MainStep)ViewState["MainStep"];
                IMainStep.MainStepID = Convert.ToInt32(Convert.ToInt32(mainStep.SelectedValue));
                ViewState.Add("MainStep", IMainStep);

                IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
                IDetailedStep.MainStepID = Convert.ToInt32(Convert.ToInt32(mainStep.SelectedValue));
                ViewState.Add("DetailedStep", IDetailedStep);

                detailedStep.Items.Clear();
                RefreshDetailedSteps();

                if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text)
                {
                    detailedStep.Items[0].Attributes.Add("disabled", "disabled");
                    detailFilter.Enabled = false;
                }

                UpdateMainStep.Visible = DeleteMainStep.Visible = true;
                detailedStep.Attributes.Remove("disabled");
                AddNewDetailedStep.Attributes.Remove("disabled");
            }
            else
            {
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "true");
                UpdateMainStep.Visible = DeleteMainStep.Visible = false;
            }

            if (mainStep.Items.Count > 1 && mainStep.SelectedValue != "No Main Steps")
            {
                MainStepMoveUp.Attributes.Remove("disabled");
                MainStepMoveDown.Attributes.Remove("disabled");
            }
            else
            {
                MainStepMoveUp.Attributes.Add("disabled", "true");
                MainStepMoveDown.Attributes.Add("disabled", "true");
            }
        }
        protected void detailButtons(object sender, EventArgs e)
        {
            deatIDX = detailedStep.SelectedIndex;
            UpdateDetailedStep.Attributes.Remove("disabled");
            DeleteDetailedStep.Attributes.Remove("disabled");
            if (detailedStep.SelectedItem.Text != "No Detailed Steps")
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
                DetailedStepMoveUp.Attributes.Remove("disabled");
                DetailedStepMoveDown.Attributes.Remove("disabled");
            }
            else
            {
                DetailedStepMoveUp.Attributes.Add("disabled", "true");
                DetailedStepMoveDown.Attributes.Add("disabled", "true");
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
        }

        private void RefreshMainSteps()
        {
            IMainStep = (MainStep)ViewState["MainStep"];
            MainStepListSource.SelectCommand = "SELECT * FROM [MainSteps] WHERE ([TaskID] = @TaskID) ORDER BY ListOrder";
            MainStepListSource.SelectParameters["TaskID"].DefaultValue = IMainStep.TaskID.ToString();
            mainStep.Items.Clear();
            mainStep.DataSource = MainStepListSource;
            mainStep.DataBind();
            if (mainStep.Items.Count == 0)
            {
                ListItem li = new ListItem();
                li.Text = "No Main Steps in " + taskList.SelectedItem.Text;
                mainStep.Items.Add(li);
                mainFilter.Enabled = false;
            }
        }

        private void RefreshDetailedSteps()
        {
            IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
            DetailedStepListSource.SelectCommand = "SELECT * FROM [DetailedSteps] WHERE ([MainStepID] = @MainStepID) ORDER BY ListOrder";
            DetailedStepListSource.SelectParameters["MainStepID"].DefaultValue = IDetailedStep.MainStepID.ToString();
            detailedStep.Items.Clear();
            detailedStep.DataSource = DetailedStepListSource;
            detailedStep.DataBind();
            if (detailedStep.Items.Count == 0)
            {
                ListItem li = new ListItem();
                li.Text = "No Detailed Steps in " + mainStep.SelectedItem.Text;
                detailedStep.Items.Add(li);
                detailFilter.Enabled = false;

            }
        }


        private void BindUsers(DropDownList drp)
        {
            drp.DataSource = Member.UsersAssignedToSupervisor(UserName);
            drp.DataBind();

            Methods.AddBlankToDropDownList(drp);
        }
        protected void MainStepMoveDown_Click(object sender, EventArgs e)
        {
            string value = mainStep.SelectedValue;
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
            mainStep.SelectedValue = value;
            if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text)
            {
                detailedStep.Items[0].Attributes.Add("disabled", "disabled");
            }
            
        }
        protected void MainStepMoveUp_Click(object sender, EventArgs e)
        {
            string value = mainStep.SelectedValue;
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
            mainStep.SelectedValue = value;
            if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text)
            {
                detailedStep.Items[0].Attributes.Add("disabled", "disabled");
            }
        }
        protected void DetailedStepMoveDown_Click(object sender, EventArgs e)
        {
            string value = detailedStep.SelectedValue;
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
            detailedStep.SelectedValue = value;
        }
        protected void DetailedStepMoveUp_Click(object sender, EventArgs e)
        {
            string value = detailedStep.SelectedValue;
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
            detailedStep.SelectedValue = value;
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
                case "Date \u25BC": catDateSort.Text = "Date \u25B2"; queryString = "SELECT * FROM Categories WHERE CreatedBy=@supervisor ORDER BY CreatedTime ASC"; break;
                case "Date \u25B2": catDateSort.Text = "Date \u25BC"; queryString = "SELECT * FROM Categories WHERE CreatedBy=@supervisor ORDER BY CreatedTime DESC"; break;
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
        }

        protected void taskDateSort_Click(object sender, EventArgs e)
        {
            if (catList.SelectedIndex != -1)
            {
                string queryString = "";
                switch (taskDateSort.Text)
                {
                    case "Date \u25BC": taskDateSort.Text = "Date \u25B2"; queryString = "SELECT * FROM Tasks WHERE CreatedBy=@supervisor AND CategoryID=@catID ORDER BY CreatedTime ASC"; break;
                    case "Date \u25B2": taskDateSort.Text = "Date \u25BC"; queryString = "SELECT * FROM Tasks WHERE CreatedBy=@supervisor AND CategoryID=@catID ORDER BY CreatedTime DESC"; break;
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
            }
        }
        protected void mainStep_Sort(object sender, EventArgs e)
        {
            if (taskList.SelectedIndex != -1)
            {
                string queryString = "";
                switch (mainStepSort.Text)
                {
                    case "Date \u25BC": mainStepSort.Text = "Date \u25B2"; queryString = "SELECT * FROM MainSteps WHERE TaskID=@id ORDER BY CreatedTime ASC"; break;
                    case "Date \u25B2": mainStepSort.Text = "Date \u25BC"; queryString = "SELECT * FROM MainSteps WHERE TaskID=@id ORDER BY CreatedTime DESC"; break;
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
            }
        }
        protected void detailedDateSort_Click(object sender, EventArgs e)
        {
            if (mainStep.SelectedIndex != -1)
            {
                string queryString = "";
                switch (detailedSort.Text)
                {
                    case "Date \u25BC": detailedSort.Text = "Date \u25B2"; queryString = "SELECT * FROM DetailedSteps WHERE MainStepID=@id ORDER BY CreatedTime ASC"; break;
                    case "Date \u25B2": detailedSort.Text = "Date \u25BC"; queryString = "SELECT * FROM DetailedSteps WHERE MainStepID=@id ORDER BY CreatedTime DESC"; break;
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