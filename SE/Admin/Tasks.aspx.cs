using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using System.Web.Security;
using System.Diagnostics;

namespace SE
{
    public partial class Tasks : System.Web.UI.Page
    {
        string UserName = System.Web.HttpContext.Current.User.Identity.Name;
        Task ITask = new Task();

        private enum TaskPage
        {
            NotSet = -1,
            CreateTask = 0,
            EditTask = 1,
            ManageTasks = 2
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ViewState.Add("Task", ITask);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Show different content based on querystring
            if (!IsPostBack)
            {
                // Create task page
                if (Request.QueryString["taskpage"] == "createtask")
                {
                    TasksMultiView.ActiveViewIndex = (int)TaskPage.CreateTask;

                    if (!IsPostBack)
                    {
                        BindCategories(AssignTaskToCategory);
                        BindUsers(AssignUserToTask);
                    }
                }

                // Edit task page
                else if (Request.QueryString["taskpage"] == "edittask" &&
                    Task.TaskExists(Convert.ToInt32(Request.QueryString["taskid"])))
                {
                    ITask = (Task)ViewState["Task"];
                    ITask.TaskID = Convert.ToInt32(Request.QueryString["taskid"]);
                    ViewState.Add("Task", ITask);

                    TasksMultiView.ActiveViewIndex = (int)TaskPage.EditTask;
                    EditMainStepPanel.Visible = false;
                    MainStepPanel.Visible = false;

                    BindCategories(EditAssignTaskToCategory);
                    BindUsers(EditAssignUserToTask);

                    MainStepList.DataSource = MainStepListSource;
                    MainStepList.DataBind();
                    Methods.AddBlankToDropDownList(MainStepList);
                }

                // Manage task page
                else
                {
                    ShowManageTasks();
                }
            }
        }

        protected void CreateTaskButton_Click(object sender, EventArgs e)
        {
            ITask.CategoryID = Convert.ToInt32(AssignTaskToCategory.SelectedValue);
            ITask.TaskName = TaskName.Text;

            if(!String.IsNullOrEmpty(AssignUserToTask.SelectedValue))
            {
                ITask.AssignedUser = AssignUserToTask.SelectedValue;
            }

            ITask.CreateTask();

            ShowManageTasks();
            SuccessMessage.Text = "Task has been successfully created";
        }

        protected void TaskList_Change(Object sender, DataGridPageChangedEventArgs e)
        {
            SuccessMessage.Text = "";

            // Set CurrentPageIndex to the page the user clicked.
            TaskList.CurrentPageIndex = e.NewPageIndex;

            // Rebind the data. 
            TaskList.DataSource = Task.ManageTasksList();
            TaskList.DataBind();
        }

        protected void EditTaskButton_Click(object sender, EventArgs e)
        {
            string SuccessMessage = "";

            EditSuccessMessage.Text = "";

            ITask = (Task)ViewState["Task"];

            Debug.WriteLine(ITask.TaskID);

            if(!String.IsNullOrEmpty(EditAssignTaskToCategory.SelectedItem.Text))
            {
                ITask.CategoryID = Convert.ToInt32(EditAssignTaskToCategory.SelectedValue);
                SuccessMessage += "Assign Task to Category successfully updated.<br/>";
            }

            if (!String.IsNullOrEmpty(EditAssignUserToTask.SelectedItem.Text))
            {
                ITask.AssignedUser = EditAssignUserToTask.SelectedItem.Text;
                SuccessMessage += "Assign User to Task successfully updated.<br/>";
            }

            if(!String.IsNullOrEmpty(EditTaskName.Text))
            {
                ITask.TaskName = EditTaskName.Text;
                SuccessMessage += "Task Name successfully updated.";
            }

            if (SuccessMessage != "")
            {
                ITask.UpdateTask();
                EditSuccessMessage.Text = SuccessMessage;

                EditAssignTaskToCategory.SelectedIndex = 0;
                EditAssignUserToTask.SelectedIndex = 0;
                EditTaskName.Text = "";
            }
        }
        protected void NewMainStepButton_Click(object sender, EventArgs e)
        {
            EditTaskPanel.Visible = false;
            EditMainStepPanel.Visible = true;
            EditSuccessMessage.Text = "";
        }

        protected void BackToTask_Click(object sender, EventArgs e)
        {
            EditMainStepPanel.Visible = false;
            EditTaskPanel.Visible = true;
            EditSuccessMessage.Text = "";
        }

        protected void MainStepList_SelectedIndexChanged(Object sender, EventArgs e)
        {
        }

        protected void AddNewMainStep_Click(Object sender, EventArgs e)
        {
            EditMainStepPanel.Visible = false;
            MainStepPanel.Visible = true;
        }

        protected void MainStepCancel_Click(object sender, EventArgs e)
        {
            MainStepPanel.Visible = false;
            EditMainStepPanel.Visible = true;
        }

        private void BindCategories(DropDownList drp)
        {
            drp.DataSource = CategoryListSource;
            drp.DataBind();
            Methods.AddBlankToDropDownList(drp);
        }

        private void BindUsers(DropDownList drp)
        {
            if (Roles.IsUserInRole(UserName, "Manager"))
            {
                drp.DataSource = Roles.GetUsersInRole("User");
                drp.DataBind();
            }
            else if (Roles.IsUserInRole(UserName, "Supervisor"))
            {
                drp.DataSource = Member.UsersAssignedToSupervisor(UserName);
                drp.DataBind();
            }

            Methods.AddBlankToDropDownList(drp);
        }

        private void ShowManageTasks()
        {
            TasksMultiView.ActiveViewIndex = (int)TaskPage.ManageTasks;
            TaskList.DataSource = Task.ManageTasksList();
            TaskList.DataBind();
        }
    }
}