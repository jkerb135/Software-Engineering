using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;

namespace SE
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Categories : Page
    {
        public int CatIdx = 0;
        public int TaskIdx = 0;
        public int MainIdx = 0;
        public int DeatIdx = 0;
        Category _cat = new Category();
        readonly string _userName = System.Web.HttpContext.Current.User.Identity.Name;
        Task _task = new Task();
        MainStep _mainStep = new MainStep();
        DetailedStep _detailedStep = new DetailedStep();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            ViewState.Add("Category", _cat);
            ViewState.Add("Task", _task);
            ViewState.Add("MainStep", _mainStep);
            ViewState.Add("DetailedStep", _detailedStep);
            ViewState.Add("CategoriesExist", false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                catFilter.Enabled = taskFilter.Enabled = mainFilter.Enabled = detailFilter.Enabled = PreviewTask.Enabled = false;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }
        /* Management CRUD Functionality */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditCategoryButton_Click(object sender, EventArgs e)
        {
            _cat = (Category)ViewState["Category"];

            switch (EditCategoryButton.Text)
            {
                case "Add New Category":
                    if (EditCategoryName.Text != String.Empty)
                    {
                        taskList.Items.Clear();
                        taskList.Attributes.Add("disabled","disabled");
                        mainStep.Items.Clear();
                        mainStep.Attributes.Add("disabled", "disabled");
                        detailedStep.Items.Clear();
                        detailedStep.Attributes.Add("disabled", "disabled");

                        _cat.CategoryName = EditCategoryName.Text;
                        _cat.CategoryAssignments = (from l in UsersInCategory.Items.Cast<ListItem>() select l.Value).ToList();

                        _cat.CreateCategory();
                        _cat.AssignUserCategories();
                        BindCategories(catList);

                        const string message = "New category successfully added.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "success"), true);

                        EditCategoryName.Text = String.Empty;

                        ListBoxPanel.Visible = true;
                        EditCategoryPanel.Visible = false;
                    }
                    else
                    {
                        const string message = "Error Creating New Category";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                    header.Text = "Management Panel";
                    break;
                case "Update Category":
                    if (EditCategoryName.Text != String.Empty)
                    {
                        _cat.CategoryName = EditCategoryName.Text;
                        _cat.CategoryAssignments = (from l in UsersInCategory.Items.Cast<ListItem>() select l.Value).ToList();

                        _cat.UpdateCategory();
                        _cat.ReAssignUserCategories();
                        BindCategories(catList);

                        const string message = "Category successfully updated.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "success"), true);

                        EditCategoryName.Text = String.Empty;

                        ListBoxPanel.Visible = true;
                        EditCategoryPanel.Visible = false;
                    }
                    else
                    {
                        const string message = "Error Updating Category.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);

                    }
                    header.Text = "Management Panel";
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            _task = (Task)ViewState["Task"];

            switch (EditTaskButton.Text)
            {
                case "Add New Task":
                    if (EditTaskName.Text != String.Empty)
                    {
                        mainStep.Items.Clear();
                        mainStep.Attributes.Add("disabled", "disabled");
                        detailedStep.Items.Clear();
                        detailedStep.Attributes.Add("disabled", "disabled");

                        _task.TaskName = EditTaskName.Text;

                        _task.TaskAssignments = (from l in UsersAssignedToTask.Items.Cast<ListItem>() select l.Value).ToList();

                        _task.CreateTask();
                        _task.AssignUserTasks();

                        const string message = "New Task Added";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "success"), true);

                        EditTaskName.Text = String.Empty;

                        ListBoxPanel.Visible = true;
                        EditCategoryPanel.Visible = false;
                        TaskPanel.Visible = false;
                        RefreshTasks();
                    }
                    else
                    {
                        const string message = "Error Creating Task.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                    break;
                case "Update Task":
                    if (EditTaskName.Text != String.Empty)
                    {
                        _task.TaskName = EditTaskName.Text;
                        header.Text = "Edit Task: " + _task.TaskName;

                        _task.TaskAssignments = (from l in UsersAssignedToTask.Items.Cast<ListItem>() select l.Value).ToList();

                        _task.UpdateTask();
                        _task.ReAssignUserTasks();

                        const string message = "Task Updated.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "success"), true);

                        EditTaskName.Text = String.Empty;

                        ListBoxPanel.Visible = true;
                        EditCategoryPanel.Visible = false;
                        TaskPanel.Visible = false;
                        RefreshTasks();
                    }
                    else
                    {
                        const string message = "Error Updating Task.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                    break;
            }
            header.Text = "Management Panel";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditMainStepButton_Click(object sender, EventArgs e)
        {
            _mainStep = (MainStep)ViewState["MainStep"];
            var message = "";
            var btn = (Button)sender;

            ErrorMessage.Text = String.Empty;

            if (MainStepName.Text != String.Empty)
            {
                detailedStep.Items.Clear();
                detailedStep.Attributes.Add("disabled", "disabled");

                _mainStep.MainStepName = MainStepName.Text;
                _mainStep.MainStepText =
                    !String.IsNullOrEmpty(MainStepText.Text) ? MainStepText.Text : null;

                if (MainStepAudio.HasFile)
                    message = Methods.UploadFile(MainStepAudio, "Audio");

                if (message == "")
                {
                    _mainStep.AudioFilename =
                        MainStepAudio.HasFile ? MainStepAudio.FileName : null;
                    _mainStep.AudioPath =
                        MainStepAudio.HasFile ? ("~/Uploads/" + MainStepAudio.FileName) : null;
                }
                else
                {
                    message += "<br/>";
                }

                if (MainStepVideo.HasFile)
                    message += Methods.UploadFile(MainStepVideo, "Video");

                if (message == "")
                {
                    _mainStep.VideoFilename =
                        MainStepVideo.HasFile ? MainStepVideo.FileName : null;
                    _mainStep.VideoPath =
                        MainStepVideo.HasFile ? ("~/Uploads/" + MainStepVideo.FileName) : null;
                }

                if (MainStepButton.Text == "Add New Main Step")
                {
                    if (message == "")
                    {
                        _mainStep.CreateMainStep();
                        const string succ = "Main Step Added";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", succ, ",", "success"), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                }
                if (MainStepButton.Text == "Update Main Step")
                {
                    if (message == "")
                    {
                        _mainStep.UpdateMainStep();
                        const string succ = "Updated Main Step.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", succ, ",", "success"), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                }

                if (message == "")
                {
                    MainStepName.Text = String.Empty;
                    MainStepText.Text = String.Empty;
                    MainStepAudio = null;
                    MainStepVideo = null;
                    RefreshMainSteps();

                    if (btn.Text != "+ Main Step")
                    {
                        ListBoxPanel.Visible = true;
                        EditCategoryPanel.Visible = false;
                        TaskPanel.Visible = false;
                        ManageMainStepPanel.Visible = false;
                        ManageDetailedStepPanel.Visible = false;
                        header.Text = "Management Panel";
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditDetailedStepButton_Click(object sender, EventArgs e)
        {
            _detailedStep = (DetailedStep)ViewState["DetailedStep"];
            var message = "";
            var btn = (Button)sender;

            ErrorMessage.Text = String.Empty;

            if (DetailedStepName.Text != String.Empty)
            {
                _detailedStep.DetailedStepName = DetailedStepName.Text;
                _detailedStep.DetailedStepText =
                    !String.IsNullOrEmpty(DetailedStepText.Text) ? DetailedStepText.Text : null;

                if (DetailedStepImage.HasFile)
                    message = Methods.UploadFile(DetailedStepImage, "Image");

                if (message == "")
                {
                    _detailedStep.ImageFilename =
                        DetailedStepImage.HasFile ? DetailedStepImage.FileName : null;
                    _detailedStep.ImagePath =
                        DetailedStepImage.HasFile ? ("~/Uploads/" + DetailedStepImage.FileName) : null;
                }

                if (EditDetailedStepButton.Text == "Add New Detailed Step")
                {
                    if (message == "")
                    {
                        _detailedStep.CreateDetailedStep();
                        const string succ = "Detailed Step Added";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", succ, ",", "success"), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                }
                if (EditDetailedStepButton.Text == "Update Detailed Step")
                {
                    if (message == "")
                    {
                        _detailedStep.UpdateDetailedStep();
                        const string succ = "Updated Detailed Step.";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", succ, ",", "success"), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("showNotification('{0}'{1}'{2}');", message, ",", "error"), true);
                    }
                }

                if (message == "")
                {
                    DetailedStepName.Text = String.Empty;
                    DetailedStepText.Text = String.Empty;
                    DetailedStepImage = null;
                    RefreshDetailedSteps();

                    if (btn.Text != "+ Detailed Step")
                    {
                        ListBoxPanel.Visible = true;
                        EditCategoryPanel.Visible = false;
                        TaskPanel.Visible = false;
                        ManageMainStepPanel.Visible = false;
                        ManageDetailedStepPanel.Visible = false;
                        header.Text = "Management Panel";
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewMainStep_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            MainStepButtonNew.Visible = true;
            header.Text = MainStepButton.Text = "Add New Main Step";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewDetailedStep_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = true;
            EditDetailedStepButtonNew.Visible = true;
            header.Text = EditDetailedStepButton.Text = "Add New Detailed Step";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateCategory_Click(object sender, EventArgs e)
        {
            if (catList.SelectedItem.Text != "No Categories")
            {
                _cat = (Category)ViewState["Category"];
                _cat.CategoryId = Convert.ToInt32(catList.SelectedValue);
                ViewState.Add("Category", _cat);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateTask_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;
            EditCategoryPanel.Visible = false;

            TaskPanel.Visible = true;
            _task = Task.GetTask(Convert.ToInt32(taskList.SelectedValue));
            EditTaskName.Text = _task.TaskName;
            EditTaskButton.Text = "Update Task";
            header.Text = "Update Task: " + taskList.SelectedItem.Text;
            GenerateUserLists();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateMainStep_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            _mainStep = MainStep.GetMainStep(Convert.ToInt32(mainStep.SelectedValue));
            MainStepName.Text = _mainStep.MainStepName;
            MainStepText.Text = _mainStep.MainStepText;

            if (!String.IsNullOrEmpty(_mainStep.AudioFilename))
                MainStepAudioCurrentLabel.Text = "<audio controls><source src='" + ResolveUrl(_mainStep.AudioPath) + "'></audio>";
            else
                MainStepAudioCurrentLabel.Text = "";

            if (!String.IsNullOrEmpty(_mainStep.VideoFilename))
                MainStepVideoCurrentLabel.Text = "<video controls><source src='" + ResolveUrl(_mainStep.VideoPath) + "'></video>";
            else
                MainStepVideoCurrentLabel.Text = "";

            MainStepButton.Text = "Update Main Step";
            MainStepButtonNew.Visible = false;
            header.Text = "Update Main Step: " + mainStep.SelectedItem.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateDetailedStep_Click(object sender, EventArgs e)
        {
            ListBoxPanel.Visible = false;

            EditCategoryPanel.Visible = false;
            ManageMainStepPanel.Visible = false;
            ManageDetailedStepPanel.Visible = true;
            _detailedStep = DetailedStep.GetDetailedStep(Convert.ToInt32(detailedStep.SelectedValue));
            DetailedStepName.Text = _detailedStep.DetailedStepName;
            DetailedStepText.Text = _detailedStep.DetailedStepText;

            if (!String.IsNullOrEmpty(_detailedStep.ImageFilename))
                DetailedStepImageCurrentLabel.Text = "<img class='image-preview' src='" + ResolveUrl(_detailedStep.ImagePath) + "'>";
            else
                DetailedStepImageCurrentLabel.Text = "";

            EditDetailedStepButton.Text = "Update Detailed Step";
            EditDetailedStepButtonNew.Visible = false;
            header.Text = "Update Detailed Step: " + detailedStep.SelectedItem.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            _cat.CategoryId = Convert.ToInt32(catList.SelectedValue);
            string value = catList.SelectedValue;
            if (_cat.IsActive)
            {
                _cat.IsActive = false;
                DeleteCategory.Text = "Activate";
                DeleteCategory.CssClass = "btn btn-success form-control";
                SuccessMessage.Text = "Category has been deactivated";
            }
            else
            {
                _cat.IsActive = true;
                DeleteCategory.Text = "Deactivate";
                DeleteCategory.CssClass = "btn btn-danger form-control";
                SuccessMessage.Text = "Category has been activated";
            }
            catList.SelectedValue = value;
            BindCategories(catList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsActiveTaskButton_Click(object sender, EventArgs e)
        {
            _task = (Task)ViewState["Task"];

            if (_task.IsActive)
            {
                _task.IsActive = false;
                IsActiveTask.Text = "Activate";
                IsActiveTask.CssClass = "btn btn-success form-control";
                SuccessMessage.Text = "Task has been deactivated";
            }
            else
            {
                _task.IsActive = true;
                IsActiveTask.Text = "Deactivate";
                IsActiveTask.CssClass = "btn btn-danger form-control";
                SuccessMessage.Text = "Task has been activated";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteMainStep_Click(object sender, EventArgs e)
        {
            _mainStep.MainStepId = Convert.ToInt32(mainStep.SelectedValue);
            _mainStep.DeleteMainStep();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteDetailedStep_Click(object sender, EventArgs e)
        {
            _detailedStep.DetailedStepId = Convert.ToInt32(detailedStep.SelectedValue);
            _detailedStep.DeleteDetailedStep();
            RefreshDetailedSteps();
            SuccessMessage.Text = "detailed step successfully deleted.";
            if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text.Substring(mainStep.SelectedItem.Text.IndexOf(':') + 1))
            {
                detailedStep.Items[0].Attributes.Add("disabled", "disabled");
            }
        }

        /*Button Binding Events*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MoveLeft_Click(object sender, EventArgs e)
        {
            ListBox control = EditTaskPanel.Visible ? AllUsersTask : AllUsers;
            ListBox control2 = EditTaskPanel.Visible ? UsersAssignedToTask : UsersInCategory;

            if (control2.SelectedItem != null)
            {
                control.Items.Add(control2.SelectedItem.Value);
                control2.Items.Remove(control2.SelectedItem.Value);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MoveRight_Click(object sender, EventArgs e)
        {
            ListBox control = EditTaskPanel.Visible ? AllUsersTask : AllUsers;
            ListBox control2 = EditTaskPanel.Visible ? UsersAssignedToTask : UsersInCategory;

            if (control.SelectedItem != null &&
                !control2.Items.Contains(control.SelectedItem))
            {
                control2.Items.Add(control.SelectedItem.Value);
                control.Items.Remove(control.SelectedItem.Value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (detailedStep.Items.Count == 1 && detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text.Substring(mainStep.SelectedItem.Text.IndexOf(':') + 1)) { detailedStep.Items[0].Attributes.Add("disabled", "disabled"); }
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

            List<string> usersAssignedToSupervisor = Member.UsersAssignedToSupervisor(_userName);

            if (usersAssignedToSupervisor.Count > 0)
            {
                control.DataSource = usersAssignedToSupervisor;
                control.DataBind();
            }

            if (EditCategoryButton.Text == "Update Category" && EditCategoryPanel.Visible)
            {
                List<string> usersAssignedToSupervisorAssignedToCategory = Member.UsersAssignedToSupervisorAssignedToCategory(_userName, Convert.ToInt32(catList.SelectedValue));

                if (usersAssignedToSupervisorAssignedToCategory.Count > 0)
                {
                    UsersInCategory.DataSource = usersAssignedToSupervisorAssignedToCategory;
                    UsersInCategory.DataBind();

                    foreach (string user in usersAssignedToSupervisorAssignedToCategory)
                    {
                        control.Items.Remove(user);
                    }
                }
            }
            else if (EditTaskButton.Text == "Update Task" && EditTaskPanel.Visible)
            {
                List<string> usersAssignedToSupervisorAssignedToTask = Task.UsersAssignedToSupervisorAssignedToTask(_userName, Convert.ToInt32(taskList.SelectedValue));

                if (usersAssignedToSupervisorAssignedToTask.Count > 0)
                {
                    UsersAssignedToTask.DataSource = usersAssignedToSupervisorAssignedToTask;
                    UsersAssignedToTask.DataBind();

                    foreach (string user in usersAssignedToSupervisorAssignedToTask)
                    {
                        control.Items.Remove(user);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void QueryTasks(object sender, EventArgs e)
        {
            PreviewTask.Enabled = false;
            CatIdx = catList.SelectedIndex;
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

                _cat = (Category)ViewState["Category"];
                _cat.CategoryId = Convert.ToInt32(Convert.ToInt32(catList.SelectedValue));
                ViewState.Add("Category", _cat);

                _task = (Task)ViewState["Task"];
                _task.CategoryId = Convert.ToInt32(Convert.ToInt32(catList.SelectedValue));
                ViewState.Add("Task", _task);

                taskList.Items.Clear();
                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                RefreshTasks();

                if (taskList.Items.Count == 0)
                {
                    var li = new ListItem { Text = "No Tasks in " + catList.SelectedItem.Text };
                    li.Attributes.Add("disabled", "disabled");
                    taskList.Items.Add(li);
                    taskFilter.Enabled = false;
                }

                if (_cat.IsActive)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void QueryMainStep(object sender, EventArgs e)
        {
            TaskIdx = taskList.SelectedIndex;
            PreviewTask.Enabled = true;
            taskIDValue.Value = taskList.SelectedValue;
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
                _task = (Task)ViewState["Task"];
                _task.TaskId = Convert.ToInt32(Convert.ToInt32(taskList.SelectedValue));
                ViewState.Add("Task", _task);

                _mainStep = (MainStep)ViewState["MainStep"];
                _mainStep.TaskId = Convert.ToInt32(Convert.ToInt32(taskList.SelectedValue));
                ViewState.Add("MainStep", _mainStep);

                mainStep.Items.Clear();
                detailedStep.Items.Clear();
                RefreshMainSteps();

                if (mainStep.Items[0].Text == "No Main Steps in " + taskList.SelectedItem.Text)
                {
                    mainStep.Items[0].Attributes.Add("disabled", "disabled");
                    mainFilter.Enabled = false;
                }

                if (_task.IsActive)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void QueryDetailedStep(object sender, EventArgs e)
        {
            MainIdx = mainStep.SelectedIndex;
            UpdateMainStep.Attributes.Remove("disabled");
            DeleteMainStep.Attributes.Remove("disabled");
            UpdateDetailedStep.Attributes.Add("disabled", "true");
            DeleteDetailedStep.Attributes.Add("disabled", "true");

            if (mainStep.SelectedItem.Text != "No Main Steps in" + taskList.SelectedItem.Value)
            {
                detailFilter.Enabled = true;
                detailedSort.Attributes.Remove("disabled");
                _mainStep = (MainStep)ViewState["MainStep"];
                _mainStep.MainStepId = Convert.ToInt32(Convert.ToInt32(mainStep.SelectedValue));
                ViewState.Add("MainStep", _mainStep);

                _detailedStep = (DetailedStep)ViewState["DetailedStep"];
                _detailedStep.MainStepId = Convert.ToInt32(Convert.ToInt32(mainStep.SelectedValue));
                ViewState.Add("DetailedStep", _detailedStep);

                detailedStep.Items.Clear();
                RefreshDetailedSteps();

                if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text.Substring(mainStep.SelectedItem.Text.IndexOf(':') + 1))
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailButtons(object sender, EventArgs e)
        {
            DeatIdx = detailedStep.SelectedIndex;
            UpdateDetailedStep.Attributes.Remove("disabled");
            DeleteDetailedStep.Attributes.Remove("disabled");
            if (detailedStep.SelectedItem.Text != "No Detailed Steps")
            {
                _detailedStep = (DetailedStep)ViewState["DetailedStep"];
                _detailedStep.DetailedStepId = Convert.ToInt32(Convert.ToInt32(detailedStep.SelectedValue));
                ViewState.Add("DetailedStep", _detailedStep);

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
            _task = (Task)ViewState["Task"];
            TaskListSource.SelectCommand = "SELECT * FROM [Tasks] WHERE ([CategoryID] = @CategoryID)";
            TaskListSource.SelectParameters["CategoryID"].DefaultValue = _task.CategoryId.ToString(CultureInfo.InvariantCulture);
            taskList.Items.Clear();
            taskList.DataSource = TaskListSource;
            taskList.DataBind();
        }

        private void RefreshMainSteps()
        {
            _mainStep = (MainStep)ViewState["MainStep"];
            MainStepListSource.SelectCommand = "SELECT * FROM [MainSteps] WHERE ([TaskID] = @TaskID) ORDER BY ListOrder";
            MainStepListSource.SelectParameters["TaskID"].DefaultValue = _mainStep.TaskId.ToString(CultureInfo.InvariantCulture);
            mainStep.Items.Clear();
            mainStep.DataSource = MainStepListSource;
            mainStep.DataBind();

            //Display the step number
            int i = 1;

            foreach (ListItem ms in mainStep.Items)
            {
                ms.Text = "Step " + i + ": " + ms.Text;
                i++;
            }

            if (mainStep.Items.Count != 0) return;
            var li = new ListItem { Text = "No Main Steps in " + taskList.SelectedItem.Text };
            mainStep.Items.Add(li);
            mainFilter.Enabled = false;
        }

        private void RefreshDetailedSteps()
        {
            _detailedStep = (DetailedStep)ViewState["DetailedStep"];
            DetailedStepListSource.SelectCommand = "SELECT * FROM [DetailedSteps] WHERE ([MainStepID] = @MainStepID) ORDER BY ListOrder";
            DetailedStepListSource.SelectParameters["MainStepID"].DefaultValue = _detailedStep.MainStepId.ToString(CultureInfo.InvariantCulture);
            detailedStep.Items.Clear();
            detailedStep.DataSource = DetailedStepListSource;
            detailedStep.DataBind();

            //Display the step number
            int i = 1;

            foreach (ListItem ds in detailedStep.Items)
            {
                ds.Text = "Step " + i + ": " + ds.Text;
                i++;
            }

            if (detailedStep.Items.Count != 0) return;
            var li = new ListItem
            {
                Text =
                    "No Detailed Steps in " +
                    mainStep.SelectedItem.Text.Substring(mainStep.SelectedItem.Text.IndexOf(':') + 1)
            };
            detailedStep.Items.Add(li);
            detailFilter.Enabled = false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MainStepMoveDown_Click(object sender, EventArgs e)
        {
            var value = mainStep.SelectedValue;
            if (mainStep.SelectedValue != "" && mainStep.SelectedIndex != mainStep.Items.Count - 1)
            {
                _mainStep = (MainStep)ViewState["MainStep"];

                const string queryString = "SELECT ListOrder " +
                                           "FROM MainSteps " +
                                           "WHERE MainStepID=@mainstepid";

                const string queryString2 = "SELECT MIN(ListOrder) " +
                                            "FROM MainSteps " +
                                            "WHERE ListOrder > @listorder " +
                                            "AND TaskID=@taskid";

                const string queryString3 = "SELECT MainStepID " +
                                            "FROM MainSteps " +
                                            "WHERE ListOrder = ( " +
                                            "SELECT MIN(ListOrder) " +
                                            "FROM MainSteps " +
                                            "WHERE ListOrder > @listorder " +
                                            "AND TaskID=@taskid " +
                                            ") " +
                                            "AND TaskID=@taskid";

                const string queryString4 = "UPDATE MainSteps " +
                                            "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                                            "WHERE MainStepID IN (@mainstepid1, @mainstepid2)";

                using (var con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    var cmd2 = new SqlCommand(queryString2, con);
                    var cmd3 = new SqlCommand(queryString3, con);
                    var cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@mainstepid", Convert.ToInt32(mainStep.SelectedValue));

                    con.Open();

                    var firstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", firstValue);
                    cmd2.Parameters.AddWithValue("@taskid", _mainStep.TaskId);

                    cmd3.Parameters.AddWithValue("@listorder", firstValue);
                    cmd3.Parameters.AddWithValue("@taskid", _mainStep.TaskId);

                    con.Open();

                    var secondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    var thirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", firstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", secondValue);
                    cmd4.Parameters.AddWithValue("@mainstepid1", Convert.ToInt32(mainStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@mainstepid2", thirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshMainSteps();
            }
            mainStep.SelectedValue = value;
            if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text.Substring(mainStep.SelectedItem.Text.IndexOf(':') + 1))
            {
                detailedStep.Items[0].Attributes.Add("disabled", "disabled");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MainStepMoveUp_Click(object sender, EventArgs e)
        {
            var value = mainStep.SelectedValue;
            if (mainStep.SelectedValue != "" && mainStep.SelectedIndex != 0)
            {
                _mainStep = (MainStep)ViewState["MainStep"];

                const string queryString = "SELECT ListOrder " +
                                           "FROM MainSteps " +
                                           "WHERE MainStepID=@mainstepid";

                const string queryString2 = "SELECT MAX(ListOrder) " +
                                            "FROM MainSteps " +
                                            "WHERE ListOrder < @listorder " +
                                            "AND TaskID=@taskid";

                const string queryString3 = "SELECT MainStepID " +
                                            "FROM MainSteps " +
                                            "WHERE ListOrder = ( " +
                                            "SELECT MAX(ListOrder) " +
                                            "FROM MainSteps " +
                                            "WHERE ListOrder < @listorder " +
                                            "AND TaskID=@taskid " +
                                            ") " +
                                            "AND TaskID=@taskid";

                const string queryString4 = "UPDATE MainSteps " +
                                            "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                                            "WHERE MainStepID IN (@mainstepid1, @mainstepid2)";

                using (var con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    var cmd2 = new SqlCommand(queryString2, con);
                    var cmd3 = new SqlCommand(queryString3, con);
                    var cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@mainstepid", Convert.ToInt32(mainStep.SelectedValue));

                    con.Open();

                    var firstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", firstValue);
                    cmd2.Parameters.AddWithValue("@taskid", _mainStep.TaskId);

                    cmd3.Parameters.AddWithValue("@listorder", firstValue);
                    cmd3.Parameters.AddWithValue("@taskid", _mainStep.TaskId);

                    con.Open();

                    var secondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    var thirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", firstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", secondValue);
                    cmd4.Parameters.AddWithValue("@mainstepid1", Convert.ToInt32(mainStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@mainstepid2", thirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshMainSteps();
            }
            mainStep.SelectedValue = value;
            if (detailedStep.Items[0].Text == "No Detailed Steps in " + mainStep.SelectedItem.Text.Substring(mainStep.SelectedItem.Text.IndexOf(':') + 1))
            {
                detailedStep.Items[0].Attributes.Add("disabled", "disabled");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailedStepMoveDown_Click(object sender, EventArgs e)
        {
            var value = detailedStep.SelectedValue;
            if (detailedStep.SelectedValue != "" && detailedStep.SelectedIndex != detailedStep.Items.Count - 1)
            {
                _detailedStep = (DetailedStep)ViewState["DetailedStep"];

                const string queryString = "SELECT ListOrder " +
                                           "FROM DetailedSteps " +
                                           "WHERE DetailedStepID=@detailedstepid";

                const string queryString2 = "SELECT MIN(ListOrder) " +
                                            "FROM DetailedSteps " +
                                            "WHERE ListOrder > @listorder " +
                                            "AND MainStepID=@mainstepid";

                const string queryString3 = "SELECT DetailedStepID " +
                                            "FROM DetailedSteps " +
                                            "WHERE ListOrder = ( " +
                                            "SELECT MIN(ListOrder) " +
                                            "FROM DetailedSteps " +
                                            "WHERE ListOrder > @listorder " +
                                            "AND MainStepID=@mainstepid " +
                                            ") " +
                                            "AND MainStepID=@mainstepid";

                const string queryString4 = "UPDATE DetailedSteps " +
                                            "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                                            "WHERE DetailedStepID IN (@detailedstepid1, @detailedstepid2)";

                using (var con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    var cmd2 = new SqlCommand(queryString2, con);
                    var cmd3 = new SqlCommand(queryString3, con);
                    var cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@detailedstepid", Convert.ToInt32(detailedStep.SelectedValue));

                    con.Open();

                    var firstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", firstValue);
                    cmd2.Parameters.AddWithValue("@mainstepid", _detailedStep.MainStepId);

                    cmd3.Parameters.AddWithValue("@listorder", firstValue);
                    cmd3.Parameters.AddWithValue("@mainstepid", _detailedStep.MainStepId);

                    con.Open();

                    var secondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    var thirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", firstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", secondValue);
                    cmd4.Parameters.AddWithValue("@detailedstepid1", Convert.ToInt32(detailedStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@detailedstepid2", thirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshDetailedSteps();
            }
            detailedStep.SelectedValue = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailedStepMoveUp_Click(object sender, EventArgs e)
        {
            var value = detailedStep.SelectedValue;
            if (detailedStep.SelectedValue != "" && detailedStep.SelectedIndex != 0)
            {
                _detailedStep = (DetailedStep)ViewState["DetailedStep"];

                const string queryString = "SELECT ListOrder " +
                                           "FROM DetailedSteps " +
                                           "WHERE DetailedStepID=@detailedstepid";

                const string queryString2 = "SELECT MAX(ListOrder) " +
                                            "FROM DetailedSteps " +
                                            "WHERE ListOrder < @listorder " +
                                            "AND MainStepID=@mainstepid";

                const string queryString3 = "SELECT DetailedStepID " +
                                            "FROM DetailedSteps " +
                                            "WHERE ListOrder = ( " +
                                            "SELECT MAX(ListOrder) " +
                                            "FROM DetailedSteps " +
                                            "WHERE ListOrder < @listorder " +
                                            "AND MainStepID=@mainstepid " +
                                            ") " +
                                            "AND MainStepID=@mainstepid";

                const string queryString4 = "UPDATE DetailedSteps " +
                                            "SET ListOrder=@listorder1 + @listorder2 - ListOrder " +
                                            "WHERE DetailedStepID IN (@detailedstepid1, @detailedstepid2)";

                using (var con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);
                    var cmd2 = new SqlCommand(queryString2, con);
                    var cmd3 = new SqlCommand(queryString3, con);
                    var cmd4 = new SqlCommand(queryString4, con);

                    // Get First Value
                    cmd.Parameters.AddWithValue("@detailedstepid", Convert.ToInt32(detailedStep.SelectedValue));

                    con.Open();

                    var firstValue = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                    con.Close();

                    // Get Second Value
                    cmd2.Parameters.AddWithValue("@listorder", firstValue);
                    cmd2.Parameters.AddWithValue("@mainstepid", _detailedStep.MainStepId);

                    cmd3.Parameters.AddWithValue("@listorder", firstValue);
                    cmd3.Parameters.AddWithValue("@mainstepid", _detailedStep.MainStepId);

                    con.Open();

                    int secondValue = (cmd2.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd2.ExecuteScalar()) : 0;
                    int thirdValue = (cmd3.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd3.ExecuteScalar()) : 0;

                    con.Close();

                    // Swap Values
                    cmd4.Parameters.AddWithValue("@listorder1", firstValue);
                    cmd4.Parameters.AddWithValue("@listorder2", secondValue);
                    cmd4.Parameters.AddWithValue("@detailedstepid1", Convert.ToInt32(detailedStep.SelectedValue));
                    cmd4.Parameters.AddWithValue("@detailedstepid2", thirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                RefreshDetailedSteps();
            }
            detailedStep.SelectedValue = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void catDateSort_Click(object sender, EventArgs e)
        {
            var queryString = "";
            switch (catDateSort.Text)
            {
                case "Date \u25BC": catDateSort.Text = "Date \u25B2"; queryString = "SELECT * FROM Categories WHERE CreatedBy=@supervisor ORDER BY CreatedTime ASC"; break;
                case "Date \u25B2": catDateSort.Text = "Date \u25BC"; queryString = "SELECT * FROM Categories WHERE CreatedBy=@supervisor ORDER BY CreatedTime DESC"; break;
            }
            catList.DataSource = null;
            catList.Items.Clear();
            taskList.Items.Clear();
            taskList.Attributes.Add("disabled", "true");
            mainStep.Items.Clear();
            mainStep.Attributes.Add("disabled", "true");
            detailedStep.Items.Clear();
            detailedStep.Attributes.Add("disabled", "true");
            var sort = new List<Category>();
            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                    cmd.Parameters.AddWithValue("@supervisor", membershipUser.UserName);


                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var cat = new Category
                    {
                        CategoryName = Convert.ToString(dr["CategoryName"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        CreatedTime = Convert.ToString(dr["CreatedTime"])
                    };
                    sort.Add(cat);
                }

                con.Close();
            }
            catList.DataSource = sort;
            catList.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void taskDateSort_Click(object sender, EventArgs e)
        {
            if (catList.SelectedIndex == -1) return;
            var queryString = "";
            switch (taskDateSort.Text)
            {
                case "Date \u25BC": taskDateSort.Text = "Date \u25B2"; queryString = "SELECT * FROM Tasks WHERE CreatedBy=@supervisor AND CategoryID=@catID ORDER BY CreatedTime ASC"; break;
                case "Date \u25B2": taskDateSort.Text = "Date \u25BC"; queryString = "SELECT * FROM Tasks WHERE CreatedBy=@supervisor AND CategoryID=@catID ORDER BY CreatedTime DESC"; break;
            }
            taskList.DataSource = null;
            taskList.Items.Clear();
            mainStep.Items.Clear();
            mainStep.Attributes.Add("disabled", "true");
            detailedStep.Items.Clear();
            detailedStep.Attributes.Add("disabled", "true");
            var sort = new List<Task>();
            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                    cmd.Parameters.AddWithValue("@supervisor", membershipUser.UserName);
                cmd.Parameters.AddWithValue("@catID", catList.SelectedValue);
                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var task = new Task
                    {
                        TaskName = Convert.ToString(dr["TaskName"]),
                        TaskId = Convert.ToInt32(dr["TaskID"]),
                        CreatedTime = Convert.ToString(dr["CreatedTime"])
                    };
                    sort.Add(task);
                }

                con.Close();
            }
            taskList.DataSource = sort;
            taskList.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mainStep_Sort(object sender, EventArgs e)
        {
            if (taskList.SelectedIndex == -1) return;
            var queryString = "";
            switch (mainStepSort.Text)
            {
                case "Date \u25BC": mainStepSort.Text = "Date \u25B2"; queryString = "SELECT * FROM MainSteps WHERE TaskID=@id ORDER BY CreatedTime ASC"; break;
                case "Date \u25B2": mainStepSort.Text = "Date \u25BC"; queryString = "SELECT * FROM MainSteps WHERE TaskID=@id ORDER BY CreatedTime DESC"; break;
            }
            mainStep.DataSource = null;
            mainStep.Items.Clear();
            detailedStep.Items.Clear();
            detailedStep.Attributes.Add("disabled", "true");
            var sort = new List<MainStep>();
            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                    cmd.Parameters.AddWithValue("@supervisor", membershipUser.UserName);
                cmd.Parameters.AddWithValue("@id", taskList.SelectedValue);
                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var step = new MainStep
                    {
                        MainStepName = Convert.ToString(dr["MainStepName"]),
                        MainStepId = Convert.ToInt32(dr["MainStepID"]),
                        CreatedTime = Convert.ToString(dr["CreatedTime"])
                    };
                    sort.Add(step);
                }

                con.Close();
            }
            mainStep.DataSource = sort;
            mainStep.DataBind();

            //Display the step number
            var i = 1;

            foreach (ListItem ms in mainStep.Items)
            {
                ms.Text = "Step " + i + ": " + ms.Text;
                i++;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void detailedDateSort_Click(object sender, EventArgs e)
        {
            if (mainStep.SelectedIndex == -1) return;
            var queryString = "";
            switch (detailedSort.Text)
            {
                case "Date \u25BC": detailedSort.Text = "Date \u25B2"; queryString = "SELECT * FROM DetailedSteps WHERE MainStepID=@id ORDER BY CreatedTime ASC"; break;
                case "Date \u25B2": detailedSort.Text = "Date \u25BC"; queryString = "SELECT * FROM DetailedSteps WHERE MainStepID=@id ORDER BY CreatedTime DESC"; break;
            }
            detailedStep.DataSource = null;
            detailedStep.Items.Clear();
            var sort = new List<DetailedStep>();
            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                    cmd.Parameters.AddWithValue("@supervisor", membershipUser.UserName);
                cmd.Parameters.AddWithValue("@id", mainStep.SelectedValue);


                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var step = new DetailedStep
                    {
                        DetailedStepName = Convert.ToString(dr["DetailedStepName"]),
                        DetailedStepId = Convert.ToInt32(dr["DetailedStepID"]),
                        CreatedTime = Convert.ToString(dr["CreatedTime"])
                    };
                    sort.Add(step);
                }

                con.Close();
            }
            detailedStep.DataSource = sort;
            detailedStep.DataBind();

            //Display the step number
            var i = 1;

            foreach (ListItem ds in detailedStep.Items)
            {
                ds.Text = "Step " + i + ": " + ds.Text;
                i++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void catFilter_TextChanged(object sender, EventArgs e)
        {
            BindCategories(catList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void taskFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshTasks();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mainFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshMainSteps();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void detailFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshDetailedSteps();
        }
        protected void HelpBtn_OnClick(object sender, EventArgs e)
        {
            lblModalTitle.Text = "Help!";
            lblModalBody.Text =
                "This page is used to add Categories, Tasks, Main Steps, and Detailed Steps with their respective tables. Each table (shown as a column) can be used to add, update, or deactivate/reactivate its contents. To get to detailed and main steps, you must first move from left to right selecting the root category, then moving down the tree (moving right across the screen) through tasks to find the desired step. Note the 'Move Up/Down' buttons which quickly allow you to re-order the selected step. You are also able to preview a task by clicking the 'Preview Task' button. This will open another tab which will allow you to view the selected task from a User's perspective.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        }
    }
}