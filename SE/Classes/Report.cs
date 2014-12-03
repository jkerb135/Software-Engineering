using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace SE.Classes
{
    public class Report
    {
        #region Properties

        public int ReportID { get; set; }

        #endregion

        #region Constructors

        public Report()
        {
            this.ReportID = 0;
        }

        #endregion

        public static DataSet GenerateReport()
        {
            var ReportDataSet = new DataSet();
            var ReportTable = ReportDataSet.Tables.Add("Overview");
            var AllUsers = new List<string>();

            ReportTable.Columns.Add("User");
            ReportTable.Columns.Add("Tasks Complete");
            ReportTable.Columns.Add("Tasks Incomplete");

            const string queryString = "SELECT AssignedUser FROM MemberAssignments";
            const string queryString2 = "SELECT COUNT(*) FROM CompletedTasks WHERE AssignedUser=@assigneduser";
            const string queryString3 = "SELECT COUNT(*) FROM TaskAssignments WHERE AssignedUser=@assigneduser";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);

                cmd2.Parameters.AddWithValue("@assigneduser", DBNull.Value);
                cmd3.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AllUsers.Add(dr["AssignedUser"].ToString());
                }

                dr.Close();

                foreach (string user in AllUsers)
                {
                    var row = ReportTable.NewRow();
                    cmd2.Parameters["@assigneduser"].Value = cmd3.Parameters["@assigneduser"].Value = user;

                    row["User"] = user;
                    row["Tasks Complete"] = Convert.ToInt32(cmd2.ExecuteScalar());
                    row["Tasks Incomplete"] = Convert.ToInt32(cmd3.ExecuteScalar());

                    ReportTable.Rows.Add(row);
                }

                con.Close();
            }

            return ReportDataSet;
        }

        public static DataSet GenerateDetailedReport(string user)
        {
            var ReportDataSet = new DataSet();
            var ReportTable = ReportDataSet.Tables.Add("Detailed");

            ReportTable.Columns.Add("Task Name");
            ReportTable.Columns.Add("Main Steps Complete");
            ReportTable.Columns.Add("Detailed Steps Used");
            ReportTable.Columns.Add("Time Spent on Task");
            ReportTable.Columns.Add("Date Completed");

            const string queryString = "SELECT * FROM Tasks " +
                                        "INNER JOIN TaskAssignments ON Tasks.TaskID = TaskAssignments.TaskID " +
                                        "WHERE TaskAssignments.AssignedUser=@assigneduser";

            const string queryString2 = "SELECT * FROM Tasks " +
                                        "INNER JOIN CompletedTasks ON Tasks.TaskID = CompletedTasks.TaskID " +
                                        "WHERE CompletedTasks.AssignedUser=@assigneduser";

            const string queryString3 = "SELECT COUNT(*) FROM CompletedMainSteps WHERE AssignedUser=@assigneduser AND TaskID=@taskid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@assigneduser", user);

                cmd2.Parameters.AddWithValue("@assigneduser", user);
                cmd2.Parameters.AddWithValue("@taskid", DBNull.Value);

                cmd3.Parameters.AddWithValue("@assigneduser", user);
                cmd3.Parameters.AddWithValue("@taskid", DBNull.Value);

                con.Open();

                // tasks in progress
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var row = ReportTable.NewRow();
                    cmd3.Parameters["@taskid"].Value = Convert.ToInt32(dr["TaskID"]);

                    row["Task Name"] = dr["TaskName"].ToString();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = dr["DetailedStepsUsed"].ToString();
                    row["Time Spent on Task"] = dr["TaskTime"].ToString() + " minutes";
                    row["Date Completed"] = "In Progress";
                    ReportTable.Rows.Add(row);
                }
                dr.Close();

                // completed tasks
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    var row = ReportTable.NewRow();
                    cmd3.Parameters["@taskid"].Value = Convert.ToInt32(dr["TaskID"]);

                    row["Task Name"] = dr["TaskName"].ToString();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = dr["TotalDetailedStepsUsed"].ToString();
                    row["Time Spent on Task"] = dr["TotalTime"].ToString() + " minutes";
                    row["Date Completed"] = dr["DateTimeCompleted"].ToString();
                    ReportTable.Rows.Add(row);
                }
                dr.Close();

                con.Close();
            }

            return ReportDataSet;
        }
    }
}