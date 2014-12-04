using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace SE.Classes
{
    public class Report
    {
        #region Properties

        public int ReportId { get; set; }

        #endregion

        #region Constructors

        public Report()
        {
            ReportId = 0;
        }

        #endregion

        public static DataSet GenerateReport()
        {
            var reportDataSet = new DataSet();
            var reportTable = reportDataSet.Tables.Add("Overview");
            var allUsers = new List<string>();

            reportTable.Columns.Add("User");
            reportTable.Columns.Add("Tasks Complete");
            reportTable.Columns.Add("Tasks Incomplete");

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
                    allUsers.Add(dr["AssignedUser"].ToString());
                }

                dr.Close();

                foreach (var user in allUsers)
                {
                    var row = reportTable.NewRow();
                    cmd2.Parameters["@assigneduser"].Value = cmd3.Parameters["@assigneduser"].Value = user;

                    row["User"] = user;
                    row["Tasks Complete"] = Convert.ToInt32(cmd2.ExecuteScalar());
                    row["Tasks Incomplete"] = Convert.ToInt32(cmd3.ExecuteScalar());

                    reportTable.Rows.Add(row);
                }

                con.Close();
            }

            return reportDataSet;
        }

        public static DataSet GenerateDetailedReport(string user)
        {
            var reportDataSet = new DataSet();
            var reportTable = reportDataSet.Tables.Add("Detailed");

            reportTable.Columns.Add("Task Name");
            reportTable.Columns.Add("Main Steps Complete");
            reportTable.Columns.Add("Detailed Steps Used");
            reportTable.Columns.Add("Time Spent on Task");
            reportTable.Columns.Add("Date Completed");

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
                    var row = reportTable.NewRow();
                    cmd3.Parameters["@taskid"].Value = Convert.ToInt32(dr["TaskID"]);

                    row["Task Name"] = dr["TaskName"].ToString();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = dr["DetailedStepsUsed"].ToString();
                    row["Time Spent on Task"] = dr["TaskTime"] + " minutes";
                    row["Date Completed"] = "In Progress";
                    reportTable.Rows.Add(row);
                }
                dr.Close();

                // completed tasks
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    var row = reportTable.NewRow();
                    cmd3.Parameters["@taskid"].Value = Convert.ToInt32(dr["TaskID"]);

                    row["Task Name"] = dr["TaskName"].ToString();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = dr["TotalDetailedStepsUsed"].ToString();
                    row["Time Spent on Task"] = dr["TotalTime"] + " minutes";
                    row["Date Completed"] = dr["DateTimeCompleted"].ToString();
                    reportTable.Rows.Add(row);
                }
                dr.Close();

                con.Close();
            }

            return reportDataSet;
        }
    }
}