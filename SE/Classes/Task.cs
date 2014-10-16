﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace SE.Classes
{
    [Serializable()]
    public class Task
    {
        #region Properties

        private bool isActive = true;

        public int TaskID { get; set; }
        public int CategoryID { get; set; }
        public string AssignedUser { get; set; }
        public string TaskName { get; set; }
        public double TaskTime { get; set; }
        public string CreatedTime { get; set; }
        public bool IsActive
        {
            get
            {
                if(TaskID > 0)
                {
                    string queryString =
                        "SELECT IsActive FROM Tasks " +
                        "WHERE TaskID=@taskid";

                    using (SqlConnection con = new SqlConnection(
                        Methods.GetConnectionString()))
                    {
                        SqlCommand cmd = new SqlCommand(queryString, con);

                        cmd.Parameters.AddWithValue("@taskid", TaskID);

                        con.Open();

                        isActive = (bool)cmd.ExecuteScalar();

                        con.Close();
                    }
                }

                return isActive;
            }
            set
            {
                if (TaskID > 0)
                {
                    string queryString =
                        "UPDATE Tasks " +
                        "SET IsActive=@isactive " +
                        "WHERE TaskID=@taskid";

                    using (SqlConnection con = new SqlConnection(
                        Methods.GetConnectionString()))
                    {
                        SqlCommand cmd = new SqlCommand(queryString, con);

                        cmd.Parameters.AddWithValue("@taskid", TaskID);
                        cmd.Parameters.AddWithValue("@isactive", value);

                        con.Open();

                        cmd.ExecuteScalar();

                        con.Close();
                    }
                }

                isActive = value;
            }
        }

        #endregion

        #region Constructors

        public Task()
        {
            this.TaskID = 0;
            this.CategoryID = 0;
            this.AssignedUser = null;
            this.TaskName = String.Empty;
            this.TaskTime = 0;
            this.IsActive = true;
        }

        #endregion

        public void CreateTask()
        {
            string queryString =
                "INSERT INTO Tasks (CategoryID, AssignedUser, TaskName, TaskTime, IsActive, CreatedTime, CreatedBy) " +
                "VALUES (@categoryid, @assigneduser, @taskname, @tasktime, @isactive, @createdtime, @createdby)";

            string queryString2 = "SELECT MAX(TaskID) FROM Tasks";
          
            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                if (AssignedUser != null){cmd.Parameters.AddWithValue("@assigneduser", AssignedUser);}
                else {cmd.Parameters.AddWithValue("@assigneduser", DBNull.Value);}
                cmd.Parameters.AddWithValue("@taskname", TaskName);
                cmd.Parameters.AddWithValue("@tasktime", TaskTime);
                cmd.Parameters.AddWithValue("@isactive", IsActive);
                cmd.Parameters.AddWithValue("@createdtime", DateTime.Now);
                cmd.Parameters.AddWithValue("@createdby", 
                    System.Web.HttpContext.Current.User.Identity.Name);

                con.Open();

                cmd.ExecuteNonQuery();
                TaskID = Convert.ToInt32(cmd2.ExecuteScalar());

                con.Close();
            }
        }

        public void UpdateTask()
        {
            string queryString =
                "UPDATE Tasks " +
                "SET CategoryID=@categoryid " +
                "WHERE TaskID=@taskid";
    
            string queryString2 =
                "UPDATE Tasks " +
                "SET AssignedUser=@assigneduser " +
                "WHERE TaskID=@taskid";

            string queryString3 =
                "UPDATE Tasks " +
                "SET TaskName=@taskname " +
                "WHERE TaskID=@taskid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                SqlCommand cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@taskid", TaskID);
                cmd.Parameters.AddWithValue("@categoryid", CategoryID);

                cmd2.Parameters.AddWithValue("@taskid", TaskID);
                cmd2.Parameters.AddWithValue("@assigneduser", AssignedUser);

                cmd3.Parameters.AddWithValue("@taskid", TaskID);
                cmd3.Parameters.AddWithValue("@taskname", TaskName);

                con.Open();

                if (CategoryID != 0)
                    cmd.ExecuteScalar();
                if (!String.IsNullOrEmpty(AssignedUser))
                    cmd2.ExecuteScalar();
                if (!String.IsNullOrEmpty(TaskName))
                    cmd3.ExecuteScalar();

                con.Close();
            }
        }

        public void CompleteTask()
        {
        }

        public void AddTimeToTask(double Minutes)
        {
        }

        public int GetNumberOFTasksComplete(string Username)
        {
            int NumberOfTasksComplete = 0;

            return NumberOfTasksComplete;
        }

        public static bool TaskExists(int TaskID)
        {
            bool TaskExists = false;

            string queryString =
                "SELECT COUNT(*) " +
                "FROM Tasks " +
                "WHERE TaskID=@TaskID";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskid", TaskID);

                con.Open();

                TaskExists = ((int)cmd.ExecuteScalar() > 0) ? true : false;

                con.Close();
            }

            return TaskExists;
        }

        public static DataSet ManageTasksList()
        {
            string queryString =
                "SELECT * FROM Tasks " +
                "INNER JOIN Categories ON Tasks.CategoryID=Categories.CategoryID " +
                "WHERE Tasks.CreatedBy=@createdby";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables.Add("Tasks");

            dt.Columns.Add("Task Name", Type.GetType("System.String"));
            dt.Columns.Add("Assigned Category", Type.GetType("System.String"));
            dt.Columns.Add("Assigned User", Type.GetType("System.String"));

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@createdby", 
                    System.Web.HttpContext.Current.User.Identity.Name);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow datarow;

                    datarow = dt.NewRow();

                    datarow["Task Name"] = "<a href='?taskpage=edittask&taskid=" + dr["TaskID"] + "'>" + dr["TaskName"] + "</a>";
                    datarow["Assigned Category"] = dr["CategoryName"];
                    datarow["Assigned User"] = dr["AssignedUser"];

                    dt.Rows.Add(datarow);
                }

                con.Close();
            }

            return ds;
        }

        public List<Task> GetIncompleteTasks(string Username)
        {
            List<Task> IncompleteTasks = new List<Task>();

            return IncompleteTasks;
        }

        public List<Task> GetAssignedTasks(string Username)
        {
            List<Task> AssignedTasks = new List<Task>();

            return AssignedTasks;
        }

        public List<Task> GetTasksInCategory(int CategoryID)
        {
            List<Task> TasksInCategory = new List<Task>();

            return TasksInCategory;
        }

        public List<Task> GetTasksInCategoryAssignedToUser(int CategoryID, string Username)
        {
            List<Task> TasksInCategoryAssignedToUser = new List<Task>();

            return TasksInCategoryAssignedToUser;
        }

        public static int getTaskID(string TaskName)
        {
            int id = -1;

            string queryString =
                "SELECT TaskID " +
                "FROM Tasks " +
                "WHERE TaskName=@taskName";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskName", TaskName);

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
        public static DataSet GetSupervisorTasks(string Username)
        {
            DataSet SupervisorTasks = new DataSet();
            DataTable taskTable = SupervisorTasks.Tables.Add("SupervisorTasks");
            taskTable.Columns.Add("Task Name");
            taskTable.Columns.Add("Activity");
            taskTable.Columns.Add("Created");
            taskTable.Columns.Add("Users In Task");

            string queryString =
                "SELECT * FROM Tasks " +
                "WHERE CreatedBy=@assignedSupervisor";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assignedSupervisor", Username);

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
                    newRow["Task Name"] = Username + " has not created any tasks yet.";
                    taskTable.Rows.Add(newRow);
                }

                con.Close();
            }
            return SupervisorTasks;
        }

        public static Task GetTask(int TaskID)
        {
            Task Task = new Task();

            string queryString =
                "SELECT * " +
                "FROM Tasks " +
                "WHERE TaskID=@TaskID";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@taskid", TaskID);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Task.TaskName = dr["TaskName"].ToString();
                    Task.AssignedUser = dr["AssignedUser"].ToString();
                }

                con.Close();
            }

            return Task;
        }
    }
}