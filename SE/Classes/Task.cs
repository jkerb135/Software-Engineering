using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SE.Classes
{
    [Serializable]
    public class Task
    {
        #region Properties

        private bool _isActive = true;

        public int TaskId { get; set; }
        public int CategoryId { get; set; }
        public string AssignedUser { get; set; }
        public string TaskName { get; set; }
        public double TaskTime { get; set; }
        public string CreatedTime { get; set; }
        public List<string> TaskAssignments { get; set; }

        public bool IsActive
        {
            get
            {
                if (TaskId > 0)
                {
                    const string queryString = "SELECT IsActive FROM Tasks " +
                                               "WHERE TaskID=@taskid";

                    using (var con = new SqlConnection(
                        Methods.GetConnectionString()))
                    {
                        var cmd = new SqlCommand(queryString, con);

                        cmd.Parameters.AddWithValue("@taskid", TaskId);

                        con.Open();

                        _isActive = (bool) cmd.ExecuteScalar();

                        con.Close();
                    }
                }

                return _isActive;
            }
            set
            {
                if (TaskId > 0)
                {
                    const string queryString = "UPDATE Tasks " +
                                               "SET IsActive=@isactive " +
                                               "WHERE TaskID=@taskid";

                    using (var con = new SqlConnection(
                        Methods.GetConnectionString()))
                    {
                        var cmd = new SqlCommand(queryString, con);

                        cmd.Parameters.AddWithValue("@taskid", TaskId);
                        cmd.Parameters.AddWithValue("@isactive", value);

                        con.Open();

                        cmd.ExecuteScalar();

                        con.Close();
                    }
                }

                _isActive = value;
            }
        }

        #endregion

        #region Constructors

        public Task()
        {
            TaskId = 0;
            CategoryId = 0;
            AssignedUser = null;
            TaskName = String.Empty;
            TaskTime = 0;
            IsActive = true;
            TaskAssignments = null;
        }

        #endregion

        public void CreateTask()
        {
            const string queryString =
                "INSERT INTO Tasks (CategoryID, AssignedUser, TaskName, TaskTime, IsActive, CreatedTime, CreatedBy) " +
                "VALUES (@categoryid, @assigneduser, @taskname, @tasktime, @isactive, @createdtime, @createdby)";

            const string queryString2 = "SELECT MAX(TaskID) FROM Tasks";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                if (AssignedUser != null)
                {
                    cmd.Parameters.AddWithValue("@assigneduser", AssignedUser);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@assigneduser", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@taskname", TaskName);
                cmd.Parameters.AddWithValue("@tasktime", TaskTime);
                cmd.Parameters.AddWithValue("@isactive", true);
                cmd.Parameters.AddWithValue("@createdtime", DateTime.Now);
                cmd.Parameters.AddWithValue("@createdby",
                    HttpContext.Current.User.Identity.Name);

                con.Open();

                cmd.ExecuteNonQuery();
                TaskId = Convert.ToInt32(cmd2.ExecuteScalar());

                con.Close();
            }
        }

        public void UpdateTask()
        {
            const string queryString = "UPDATE Tasks " +
                                       "SET CategoryID=@categoryid " +
                                       "WHERE TaskID=@taskid";

            const string queryString2 = "UPDATE Tasks " +
                                        "SET AssignedUser=@assigneduser " +
                                        "WHERE TaskID=@taskid";

            const string queryString3 = "UPDATE Tasks " +
                                        "SET TaskName=@taskname " +
                                        "WHERE TaskID=@taskid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@taskid", TaskId);
                cmd.Parameters.AddWithValue("@categoryid", CategoryId);

                cmd2.Parameters.AddWithValue("@taskid", TaskId);
                cmd2.Parameters.AddWithValue("@assigneduser", AssignedUser);

                cmd3.Parameters.AddWithValue("@taskid", TaskId);
                cmd3.Parameters.AddWithValue("@taskname", TaskName);

                con.Open();

                if (CategoryId != 0)
                    cmd.ExecuteScalar();
                if (!String.IsNullOrEmpty(AssignedUser))
                    cmd2.ExecuteScalar();
                if (!String.IsNullOrEmpty(TaskName))
                    cmd3.ExecuteScalar();

                con.Close();
            }
        }

        public void AssignUserTasks()
        {
            const string queryString = "INSERT INTO TaskAssignments (CategoryID, AssignedUser, TaskID) " +
                                       "VALUES (@categoryid, @assigneduser,@taskid)";

            const string queryString2 = "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                                        "VALUES (@assigneduser,@categoryid)";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd.Parameters.AddWithValue("@taskid", TaskId);
                cmd.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                cmd2.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd2.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                foreach (string taskAssign in TaskAssignments)
                {
                    cmd.Parameters["@assigneduser"].Value = taskAssign;
                    cmd.ExecuteNonQuery();

                    if (Category.UserInCategory(taskAssign, CategoryId)) continue;
                    cmd2.Parameters["@assigneduser"].Value = taskAssign;
                    cmd2.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public void ReAssignUserTasks()
        {
            const string queryString = "DELETE FROM TaskAssignments " +
                                       "WHERE TaskID=@taskid ";

            const string queryString2 = "INSERT INTO TaskAssignments (CategoryID, AssignedUser, TaskID) " +
                                        "VALUES (@categoryid, @assigneduser,@taskid)";

            const string queryString3 = "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                                        "VALUES (@assigneduser,@categoryid)";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@taskid", TaskId);

                cmd2.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd2.Parameters.AddWithValue("@taskid", TaskId);
                cmd2.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                cmd3.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd3.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

                foreach (string taskAssign in TaskAssignments)
                {
                    cmd2.Parameters["@assigneduser"].Value = taskAssign;
                    cmd2.ExecuteNonQuery();

                    if (Category.UserInCategory(taskAssign, CategoryId)) continue;
                    cmd3.Parameters["@assigneduser"].Value = taskAssign;
                    cmd3.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public void CompleteTask()
        {
        }

        public void AddTimeToTask(double minutes)
        {
        }

        public int GetNumberOfTasksComplete(string username)
        {
            const int numberOfTasksComplete = 0;

            return numberOfTasksComplete;
        }

        public static bool TaskExists(int taskId)
        {
            bool taskExists;

            const string queryString = "SELECT COUNT(*) " +
                                       "FROM Tasks " +
                                       "WHERE TaskID=@TaskID";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskid", taskId);

                con.Open();

                taskExists = ((int) cmd.ExecuteScalar() > 0);

                con.Close();
            }

            return taskExists;
        }

        public static DataSet ManageTasksList()
        {
            const string queryString = "SELECT * FROM Tasks " +
                                       "INNER JOIN Categories ON Tasks.CategoryID=Categories.CategoryID " +
                                       "WHERE Tasks.CreatedBy=@createdby";

            var ds = new DataSet();
            DataTable dt = ds.Tables.Add("Tasks");

            dt.Columns.Add("Task Name", Type.GetType("System.String"));
            dt.Columns.Add("Assigned Category", Type.GetType("System.String"));
            dt.Columns.Add("Assigned User", Type.GetType("System.String"));

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@createdby",
                    HttpContext.Current.User.Identity.Name);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow datarow = dt.NewRow();

                    datarow["Task Name"] = "<a href='?taskpage=edittask&taskid=" + dr["TaskID"] + "'>" + dr["TaskName"] +
                                           "</a>";
                    datarow["Assigned Category"] = dr["CategoryName"];
                    datarow["Assigned User"] = dr["AssignedUser"];

                    dt.Rows.Add(datarow);
                }

                con.Close();
            }

            return ds;
        }

        public List<Task> GetIncompleteTasks(string username)
        {
            var incompleteTasks = new List<Task>();

            return incompleteTasks;
        }

        public List<Task> GetAssignedTasks(string username)
        {
            var assignedTasks = new List<Task>();

            return assignedTasks;
        }

        public List<Task> GetTasksInCategory(int categoryId)
        {
            var tasksInCategory = new List<Task>();

            return tasksInCategory;
        }

        public List<Task> GetTasksInCategoryAssignedToUser(int categoryId, string username)
        {
            var tasksInCategoryAssignedToUser = new List<Task>();

            return tasksInCategoryAssignedToUser;
        }

        public static int GetTaskIdBySupervisor(string taskName, string creator)
        {
            int id = -1;

            const string queryString = "SELECT TaskID " +
                                       "FROM Tasks " +
                                       "WHERE TaskName=@taskName and CreatedBy = @creator";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskName", taskName);
                cmd.Parameters.AddWithValue("@creator", creator);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["TaskID"]);
                }

                con.Close();
            }
            return id;
        }

        public static int GetTaskId(string taskName)
        {
            int id = -1;

            const string queryString = "SELECT TaskID " +
                                       "FROM Tasks " +
                                       "WHERE TaskName=@taskName";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskName", taskName);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["TaskID"]);
                }

                con.Close();
            }
            return id;
        }

        public static DataSet GetSupervisorTasks(string username)
        {
            var supervisorTasks = new DataSet();
            DataTable taskTable = supervisorTasks.Tables.Add("SupervisorTasks");
            taskTable.Columns.Add("Task Name");
            taskTable.Columns.Add("Activity");
            taskTable.Columns.Add("Created");
            taskTable.Columns.Add("Users In Task");

            const string queryString = "SELECT * FROM Tasks " +
                                       "WHERE CreatedBy=@assignedSupervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assignedSupervisor", username);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow newRow = taskTable.NewRow();
                    newRow["Task Name"] = dr["TaskName"].ToString();
                    if (Convert.ToBoolean(dr["IsActive"]))
                    {
                        newRow["Activity"] = "Active";
                    }
                    else
                    {
                        newRow["Activity"] = "Inactive";
                    }
                    DateTime created = Convert.ToDateTime(Convert.ToString(dr["CreatedTime"]));
                    newRow["Created"] = created.ToShortDateString();
                    newRow["Users In Task"] = Convert.ToString(dr["AssignedUser"]);
                    taskTable.Rows.Add(newRow);
                }
                if (taskTable.Rows.Count == 0)
                {
                    DataRow newRow = taskTable.NewRow();
                    newRow["Task Name"] = username + " has not created any tasks yet.";
                    taskTable.Rows.Add(newRow);
                }

                con.Close();
            }
            return supervisorTasks;
        }

        public static Task GetTask(int taskId)
        {
            var task = new Task();

            const string queryString = "SELECT * " +
                                       "FROM Tasks " +
                                       "WHERE TaskID=@TaskID";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskid", taskId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    task.TaskName = dr["TaskName"].ToString();
                    task.AssignedUser = dr["AssignedUser"].ToString();
                }

                con.Close();
            }

            return task;
        }

        public static string GetTaskName(int taskId)
        {
            string taskName = "";

            const string queryString = "SELECT TaskName " +
                                       "FROM Tasks " +
                                       "WHERE TaskID=@TaskID";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskid", taskId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    taskName = dr["TaskName"].ToString();
                }

                con.Close();
            }

            return taskName;
        }

        public static List<string> UsersAssignedToSupervisorAssignedToTask(
            string supervisor, int taskId)
        {
            var usersAssignedToSupervisorAssignedToTask = new List<string>();

            const string queryString = "SELECT * FROM MemberAssignments " +
                                       "INNER JOIN TaskAssignments ON MemberAssignments.AssignedUser=TaskAssignments.AssignedUser " +
                                       "WHERE TaskAssignments.TaskID=@taskid " +
                                       "AND MemberAssignments.AssignedSupervisor=@assignedsupervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskid", taskId);
                cmd.Parameters.AddWithValue("@assignedsupervisor", supervisor);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!usersAssignedToSupervisorAssignedToTask.Contains(dr["AssignedUser"].ToString()))
                    {
                        usersAssignedToSupervisorAssignedToTask.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return usersAssignedToSupervisorAssignedToTask;
        }
    }
}