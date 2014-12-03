using SE.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using SE.Classes;

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
                EmailReportForm.Visible = false;
            }
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (!ReportOverviewPanel.Visible)
            {
                ReportOverviewPanel.Visible = true;
                EmailReportForm.Visible = false;
                ReportOverview.DataSource = Report.GenerateReport();
                ReportOverview.DataBind();
            }
        }

        protected void EmailReportButton_Click(object sender, EventArgs e)
        {
            if (!EmailReportForm.Visible)
            {
                EmailReportForm.Visible = true;
                ReportOverviewPanel.Visible = false;
                ReportDetailsPanel.Visible = false;
            }
        }

        protected void EmailFormButton_Click(object sender, EventArgs e)
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
                    ReportDetails.DataSource = Report.GenerateDetailedReport(user);
                    ReportDetails.DataBind();
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }
    }
}