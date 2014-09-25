using System;
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

        public int TaskID { get; set; }
        public int CategoryID { get; set; }
        public string AssignedUser { get; set; }
        public string TaskName { get; set; }
        public double TaskTime { get; set; }
        public int IsActive { get; set; }

        #endregion

        #region Constructors

        public Task()
        {
            this.TaskID = 0;
            this.CategoryID = 0;
            this.AssignedUser = null;
            this.TaskName = String.Empty;
            this.TaskTime = 0;
            this.IsActive = 1;
        }

        #endregion

        public void CreateTask()
        {
            string queryString = "";

            queryString =
                "INSERT INTO Tasks (CategoryID, AssignedUser, TaskName, TaskTime, IsActive) " +
                "VALUES (@categoryid, @assigneduser, @taskname, @tasktime, @IsActive)";

            Debug.WriteLine(AssignedUser);
          
            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                if (AssignedUser != null){cmd.Parameters.AddWithValue("@assigneduser", AssignedUser);}
                else {cmd.Parameters.AddWithValue("@assigneduser", DBNull.Value);}
                cmd.Parameters.AddWithValue("@taskname", TaskName);
                cmd.Parameters.AddWithValue("@tasktime", TaskTime);
                cmd.Parameters.AddWithValue("@isactive", IsActive);

                con.Open();

                cmd.ExecuteNonQuery();

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
                "INNER JOIN Categories ON Tasks.CategoryID=Categories.CategoryID";

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
    }
}