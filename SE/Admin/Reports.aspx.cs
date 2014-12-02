using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;

namespace SE
{
    public partial class Reports : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportOverviewPanel.Visible = false;
                ReportDetailsPanel.Visible = false;
            }
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (!ReportOverviewPanel.Visible)
            {
                ReportOverviewPanel.Visible = true;
                GenerateReport();
            }
        }

        protected void EmailReportButton_Click(object sender, EventArgs e)
        {
        }

        protected void PrintReportButton_Click(object sender, EventArgs e)
        {
        }

        protected void OverviewRDB(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(ReportOverview,
                    "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void OverviewSIC(object sender, EventArgs e)
        {
            if (!ReportDetailsPanel.Visible)
            {
                ReportDetailsPanel.Visible = true;
            }

            foreach (GridViewRow row in ReportOverview.Rows)
            {
                if (row.RowIndex == ReportOverview.SelectedIndex)
                {
                    string user = row.Cells[0].Text;
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    ReportDetailsHeading.InnerHtml = String.Empty;
                    ReportDetailsMessage.Text = String.Empty;
                    GenerateDetailedReport(user);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        private void GenerateReport()
        {
            var ReportDataSet = new DataSet();
            DataTable ReportTable = ReportDataSet.Tables.Add("Overview");
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

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AllUsers.Add(dr["AssignedUser"].ToString());
                }

                dr.Close();

                foreach (string user in AllUsers)
                {
                    DataRow row = ReportTable.NewRow();
                    cmd2.Parameters["@assigneduser"].Value = cmd3.Parameters["@assigneduser"].Value = user;

                    row["User"] = user;
                    row["Tasks Complete"] = Convert.ToInt32(cmd2.ExecuteScalar());
                    row["Tasks Incomplete"] = Convert.ToInt32(cmd3.ExecuteScalar());

                    ReportTable.Rows.Add(row);
                }

                con.Close();
            }

            ReportOverview.DataSource = ReportDataSet;
            ReportOverview.DataBind();
        }

        private void GenerateDetailedReport(string user)
        {
            var ReportDataSet = new DataSet();
            DataTable ReportTable = ReportDataSet.Tables.Add("Detailed");

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

            const string queryString3 =
                "SELECT COUNT(*) FROM CompletedMainSteps WHERE AssignedUser=@assigneduser AND TaskID=@taskid";

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
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow row = ReportTable.NewRow();
                    cmd3.Parameters["@taskid"].Value = Convert.ToInt32(dr["TaskID"]);

                    row["Task Name"] = dr["TaskName"].ToString();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = dr["DetailedStepsUsed"].ToString();
                    row["Time Spent on Task"] = dr["TaskTime"] + " minutes";
                    row["Date Completed"] = "In Progress";
                    ReportTable.Rows.Add(row);
                }
                dr.Close();

                // completed tasks
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    DataRow row = ReportTable.NewRow();
                    cmd3.Parameters["@taskid"].Value = Convert.ToInt32(dr["TaskID"]);

                    row["Task Name"] = dr["TaskName"].ToString();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = dr["TotalDetailedStepsUsed"].ToString();
                    row["Time Spent on Task"] = dr["TotalTime"] + " minutes";
                    row["Date Completed"] = dr["DateTimeCompleted"].ToString();
                    ReportTable.Rows.Add(row);
                }
                dr.Close();

                con.Close();
            }
            ReportDetails.DataSource = ReportDataSet;
            ReportDetails.DataBind();
        }
    }
}