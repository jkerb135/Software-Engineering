using SE.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace SE
{
    public partial class Reports : System.Web.UI.Page
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

        protected void OverviewRDB(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(ReportOverview, "Select$" + e.Row.RowIndex);
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
            var ReportTable = ReportDataSet.Tables.Add("Overview");
            var AllUsers = new List<string>();

            ReportTable.Columns.Add("User");
            ReportTable.Columns.Add("Tasks Complete");
            ReportTable.Columns.Add("Tasks Incomplete");

            const string queryString = "SELECT AssignedUser FROM MemberAssignments";
            const string queryString2 = "SELECT COUNT(*) FROM CompletedTasks WHERE AssignedUser=@assigneduser";
            const string queryString3 = "SELECT COUNT(*) FROM Tasks WHERE AssignedUser=@assigneduser";

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

            ReportOverview.DataSource = ReportDataSet;
            ReportOverview.DataBind();
        }

        private void GenerateDetailedReport(string user)
        {
            var ReportDataSet = new DataSet();
            var ReportTable = ReportDataSet.Tables.Add("Detailed");
            var AllTasks = new List<int>();
            string TotalTime = null;
            string DateComplete = null;

            ReportTable.Columns.Add("Task Name");
            ReportTable.Columns.Add("Main Steps Complete");
            ReportTable.Columns.Add("Detailed Steps Used");
            ReportTable.Columns.Add("Time Spent on Task");
            ReportTable.Columns.Add("Date Completed");

            const string queryString = "SELECT TaskID FROM Tasks WHERE AssignedUser=@assigneduser";
            const string queryString2 = "SELECT TaskName FROM Tasks WHERE TaskID=@taskid";
            const string queryString3 = "SELECT COUNT(*) FROM CompletedMainSteps WHERE AssignedUser=@assigneduser AND TaskID=@taskid";
            const string queryString4 = "SELECT MainStepID FROM MainSteps WHERE TaskID=@taskid";
            const string queryString5 = "SELECT COUNT(*) FROM DetailedSteps WHERE MainStepID=@mainstepid";
            const string queryString6 = "SELECT TaskTime FROM Tasks WHERE TaskID=@taskid";
            const string queryString7 = "SELECT TotalTime FROM CompletedTasks WHERE TaskID=@taskid AND AssignedUser=@assigneduser";
            const string queryString8 = "SELECT DateTimeCompleted FROM CompletedTasks WHERE TaskID=@taskid AND AssignedUser=@assigneduser";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);
                var cmd4 = new SqlCommand(queryString4, con);
                var cmd5 = new SqlCommand(queryString5, con);
                var cmd6 = new SqlCommand(queryString6, con);
                var cmd7 = new SqlCommand(queryString7, con);
                var cmd8 = new SqlCommand(queryString8, con);

                cmd.Parameters.AddWithValue("@assigneduser", user);

                cmd2.Parameters.AddWithValue("@taskid", DBNull.Value);

                cmd3.Parameters.AddWithValue("@assigneduser", user);
                cmd3.Parameters.AddWithValue("@taskid", DBNull.Value);

                cmd4.Parameters.AddWithValue("@taskid", DBNull.Value);

                cmd5.Parameters.AddWithValue("@mainstepid", DBNull.Value);

                cmd6.Parameters.AddWithValue("@taskid", DBNull.Value);

                cmd7.Parameters.AddWithValue("@assigneduser", user);
                cmd7.Parameters.AddWithValue("@taskid", DBNull.Value);

                cmd8.Parameters.AddWithValue("@assigneduser", user);
                cmd8.Parameters.AddWithValue("@taskid", DBNull.Value);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AllTasks.Add(Convert.ToInt32(dr["TaskID"]));
                }

                dr.Close();

                foreach (int taskid in AllTasks)
                {
                    var row = ReportTable.NewRow();

                    cmd2.Parameters["@taskid"].Value = cmd3.Parameters["@taskid"].Value = cmd4.Parameters["@taskid"].Value = cmd6.Parameters["@taskid"].Value = cmd7.Parameters["@taskid"].Value = cmd8.Parameters["@taskid"].Value = taskid;
                    cmd5.Parameters["@mainstepid"].Value = Convert.ToInt32(cmd4.ExecuteScalar());

                    if (cmd7.ExecuteScalar() != null)
                    {
                        TotalTime = cmd7.ExecuteScalar().ToString();
                        DateComplete = cmd8.ExecuteScalar().ToString();
                    }
                    else
                    {
                        TotalTime = null;
                        DateComplete = null;
                    }

                    row["Task Name"] = cmd2.ExecuteScalar();
                    row["Main Steps Complete"] = cmd3.ExecuteScalar().ToString();
                    row["Detailed Steps Used"] = cmd5.ExecuteScalar().ToString();
                    row["Time Spent on Task"] = (TotalTime != null) ? TotalTime : cmd6.ExecuteScalar().ToString() + " Minutes";
                    row["Date Completed"] = (DateComplete != null) ? DateComplete : "In Progress";

                    ReportTable.Rows.Add(row);
                }

                con.Close();
            }

            ReportDetails.DataSource = ReportDataSet;
            ReportDetails.DataBind();

            if (AllTasks.Count < 1)
            {
                ReportDetailsMessage.Text = "There is no task progress for " + user + " at this time";
            }
            else
            {
                ReportDetailsHeading.InnerHtml = "Details About " + user;
            }
        }
    }
}