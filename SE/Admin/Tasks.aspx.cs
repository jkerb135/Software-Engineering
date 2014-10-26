using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using System.Web.Security;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SE
{
    public partial class Tasks : System.Web.UI.Page
    {
        #region "Variables"

        string UserName = System.Web.HttpContext.Current.User.Identity.Name;
        Task ITask = new Task();
        MainStep IMainStep = new MainStep();
        DetailedStep IDetailedStep = new DetailedStep();

        private enum TaskPage
        {
            NotSet = -1,
            CreateTask = 0,
            EditTask = 1,
            ManageTasks = 2
        }

        #endregion

        #region "LifeCycle"

        protected void Page_Init(object sender, EventArgs e)
        {
            ViewState.Add("Task", ITask);
            ViewState.Add("MainStep", IMainStep);
            ViewState.Add("DetailedStep", IDetailedStep);
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

                    IMainStep = (MainStep)ViewState["MainStep"];
                    IMainStep.TaskID = Convert.ToInt32(Request.QueryString["taskid"]);
                    ViewState.Add("MainStep", IMainStep);

                    TasksMultiView.ActiveViewIndex = (int)TaskPage.EditTask;

                    BindCategories(EditAssignTaskToCategory);
                    BindUsers(EditAssignUserToTask);
                }

                // Manage task page
                else
                {
                    ShowManageTasks();
                }
            }
        }

        #endregion

        #region "Task Management"

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

        protected void ViewMainStepButton_Click(object sender, EventArgs e)
        {
            ManageMainStepPanel.Visible = true;
            EditTaskPanel.Visible = false;
            EditSuccessMessage.Text = "";

            BindMainSteps();
        }

        #endregion

        #region "Main Step Management"

        protected void BackToTask_Click(object sender, EventArgs e)
        {
            EditTaskPanel.Visible = true;
            ManageMainStepPanel.Visible = false;
            EditMainStepPanel.Visible = false;
        }

        protected void AddNewMainStep_Click(Object sender, EventArgs e)
        {
            NewMainStepPanel.Visible = true;
            ManageMainStepPanel.Visible = false;
            EditMainStepPanel.Visible = false;
        }

        protected void MainStepCancel_Click(object sender, EventArgs e)
        {
            ManageMainStepPanel.Visible = true;
            NewMainStepPanel.Visible = false;
        }

        protected void MainStepButton_Click(object sender, EventArgs e)
        {
            IMainStep = (MainStep)ViewState["MainStep"];

            IMainStep.MainStepName = MainStepName.Text;

            IMainStep.MainStepText = 
                !String.IsNullOrEmpty(MainStepText.Text) ? MainStepText.Text : null;

            if (MainStepAudio.HasFile)
            {
                Methods.UploadFile(MainStepAudio, "Audio");

                IMainStep.AudioFilename = MainStepAudio.FileName;
                IMainStep.AudioPath = "~/Uploads/" + MainStepAudio.FileName;
            }

            if (MainStepVideo.HasFile)
            {
                Methods.UploadFile(MainStepVideo, "Video");

                IMainStep.VideoFilename = MainStepVideo.FileName;
                IMainStep.VideoPath = "~/Uploads/" + MainStepVideo.FileName;
            }

            IMainStep.CreateMainStep();

            MainStepName.Text = "";
            MainStepText.Text = "";

            BindMainSteps();
            ManageMainStepPanel.Visible = true;
            NewMainStepPanel.Visible = false;
        }

        protected void MainStepMoveDown_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "" && MainStepList.SelectedIndex != MainStepList.Items.Count-1)
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

                BindMainSteps();
            }
        }

        protected void MainStepMoveUp_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "" && MainStepList.SelectedIndex != 0)
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

                BindMainSteps();
            }
        }

        protected void MainStepEdit_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "" && !EditMainStepPanel.Visible)
            {
                EditMainStepPanel.Visible = true;
            }
            else
            {
                EditMainStepPanel.Visible = false;
            }
        }

        protected void EditMainStepButton_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "")
            {
                IMainStep.MainStepID = Convert.ToInt32(MainStepList.SelectedValue);

                IMainStep.MainStepName = 
                    !String.IsNullOrEmpty(EditMainStepName.Text) ? EditMainStepName.Text : null;
                IMainStep.MainStepText = 
                    !String.IsNullOrEmpty(EditMainStepText.Text) ? EditMainStepText.Text : null;

                if (EditMainStepAudio.HasFile)
                {
                    Methods.UploadFile(EditMainStepAudio, "Audio");

                    IMainStep.AudioFilename = EditMainStepAudio.FileName;
                    IMainStep.AudioPath = "~/Uploads/" + EditMainStepAudio.FileName;
                }

                if (EditMainStepVideo.HasFile)
                {
                    Methods.UploadFile(EditMainStepVideo, "Video");

                    IMainStep.VideoFilename = EditMainStepVideo.FileName;
                    IMainStep.VideoPath = "~/Uploads/" + EditMainStepVideo.FileName;
                }

                IMainStep.UpdateMainStep();

                EditMainStepName.Text = "";
                EditMainStepText.Text = "";

                BindMainSteps();
                EditMainStepPanel.Visible = false;
            }
        }

        protected void MainStepDelete_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "")
            {
                IMainStep.MainStepID = Convert.ToInt32(MainStepList.SelectedValue);
                IMainStep.DeleteMainStep();

                BindMainSteps();
            }
        }

        protected void ViewDetailedSteps_Click(object sender, EventArgs e)
        {
            if (MainStepList.SelectedValue != "")
            {
                IDetailedStep = (DetailedStep)ViewState["DetailedStep"];
                IDetailedStep.MainStepID = Convert.ToInt32(MainStepList.SelectedValue);
                ViewState.Add("DetailedStep", IDetailedStep);

                ManageDetailedStepPanel.Visible = true;
                ManageMainStepPanel.Visible = false;
                EditMainStepPanel.Visible = false;
                BindDetailedSteps();
            }
        }

        #endregion

        #region "Detailed Step Management"

        protected void BackToMainStep_Click(object sender, EventArgs e)
        {
            ManageMainStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            EditDetailedStepPanel.Visible = false;
        }

        protected void AddNewDetailedStep_Click(object sender, EventArgs e)
        {
            NewDetailedStepPanel.Visible = true;
            ManageDetailedStepPanel.Visible = false;
            EditDetailedStepPanel.Visible = false;
        }

        protected void DetailedStepCancel_Click(object sender, EventArgs e)
        {
            ManageDetailedStepPanel.Visible = true;
            NewDetailedStepPanel.Visible = false;
        }

        protected void DetailedStepButton_Click(object sender, EventArgs e)
        {
            IDetailedStep = (DetailedStep)ViewState["DetailedStep"];

            IDetailedStep.DetailedStepName = DetailedStepName.Text;

            IDetailedStep.DetailedStepText = 
                !String.IsNullOrEmpty(DetailedStepText.Text) ? DetailedStepText.Text : null;

            if (DetailedStepImage.HasFile)
            {
                Methods.UploadFile(DetailedStepImage, "Image");

                IDetailedStep.ImageFilename = DetailedStepImage.FileName;
                IDetailedStep.ImagePath = "~/Uploads/" + DetailedStepImage.FileName;
            }

            IDetailedStep.CreateDetailedStep();

            DetailedStepName.Text = "";
            DetailedStepText.Text = "";

            BindDetailedSteps();
            ManageDetailedStepPanel.Visible = true;
            NewDetailedStepPanel.Visible = false;
        }

        protected void DetailedStepMoveDown_Click(object sender, EventArgs e)
        {
            if (DetailedStepList.SelectedValue != "" && DetailedStepList.SelectedIndex != DetailedStepList.Items.Count - 1)
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
                    cmd.Parameters.AddWithValue("@detailedstepid", Convert.ToInt32(DetailedStepList.SelectedValue));

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
                    cmd4.Parameters.AddWithValue("@detailedstepid1", Convert.ToInt32(DetailedStepList.SelectedValue));
                    cmd4.Parameters.AddWithValue("@detailedstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                BindDetailedSteps();
            }
        }

        protected void DetailedStepMoveUp_Click(object sender, EventArgs e)
        {
            if (DetailedStepList.SelectedValue != "" && DetailedStepList.SelectedIndex != 0)
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
                    cmd.Parameters.AddWithValue("@detailedstepid", Convert.ToInt32(DetailedStepList.SelectedValue));

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
                    cmd4.Parameters.AddWithValue("@detailedstepid1", Convert.ToInt32(DetailedStepList.SelectedValue));
                    cmd4.Parameters.AddWithValue("@detailedstepid2", ThirdValue);

                    con.Open();

                    cmd4.ExecuteNonQuery();

                    con.Close();
                }

                BindDetailedSteps();
            }
        }

        protected void DetailedStepEdit_Click(object sender, EventArgs e)
        {
            if (DetailedStepList.SelectedValue != "" && !EditDetailedStepPanel.Visible)
            {
                EditDetailedStepPanel.Visible = true;
            }
            else
            {
                EditDetailedStepPanel.Visible = false;
            }
        }

        protected void EditDetailedStepButton_Click(object sender, EventArgs e)
        {
            if (DetailedStepList.SelectedValue != "")
            {
                IDetailedStep.DetailedStepID = Convert.ToInt32(DetailedStepList.SelectedValue);

                IDetailedStep.DetailedStepName = 
                    !String.IsNullOrEmpty(EditDetailedStepName.Text) ? EditDetailedStepName.Text : null;
                IDetailedStep.DetailedStepText = 
                    !String.IsNullOrEmpty(EditDetailedStepText.Text) ? EditDetailedStepText.Text : null;

                if (EditDetailedStepImage.HasFile)
                {
                    Methods.UploadFile(EditDetailedStepImage, "Image");

                    IDetailedStep.ImageFilename = EditDetailedStepImage.FileName;
                    IDetailedStep.ImagePath = "~/Uploads/" + EditDetailedStepImage.FileName;
                }

                IDetailedStep.UpdateDetailedStep();

                EditDetailedStepName.Text = "";
                EditDetailedStepText.Text = "";

                BindDetailedSteps();
                EditDetailedStepPanel.Visible = false;
            }
        }

        protected void DetailedStepDelete_Click(object sender, EventArgs e)
        {
            if (DetailedStepList.SelectedValue != "")
            {
                IDetailedStep.DetailedStepID = Convert.ToInt32(DetailedStepList.SelectedValue);
                IDetailedStep.DeleteDetailedStep();

                BindDetailedSteps();
            }
        }

        #endregion

        #region "Functions"

        private void BindCategories(DropDownList drp)
        {
            drp.DataSource = CategoryListSource;
            drp.DataBind();
            Methods.AddBlankToDropDownList(drp);
        }

        private void BindUsers(DropDownList drp)
        {
            drp.DataSource = Member.UsersAssignedToSupervisor(UserName);
            drp.DataBind();

            Methods.AddBlankToDropDownList(drp);
        }

        private void BindMainSteps()
        {
            MainStepList.DataSource = MainStepListSource;
            MainStepList.DataBind();
        }

        private void BindDetailedSteps()
        {
            DetailedStepList.DataSource = DetailedStepListSource;
            DetailedStepList.DataBind();
        }

        private void ShowManageTasks()
        {
            TasksMultiView.ActiveViewIndex = (int)TaskPage.ManageTasks;
            TaskList.DataSource = Task.ManageTasksList();
            TaskList.DataBind();
        }

        #endregion
    }
}