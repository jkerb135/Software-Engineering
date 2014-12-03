using SE.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using SE.Classes;
using System.Net;
using System.Web.Security;
using System.Text;
using System.Web.UI;
using System.IO;

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
                PageHeader.InnerText = "Reports";
                ReportOverviewPanel.Visible = false;
                ReportDetailsPanel.Visible = false;
                EmailReportForm.Visible = false;
            }
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (!ReportOverviewPanel.Visible)
            {
                PageHeader.InnerText = "Generate Report";
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
                PageHeader.InnerText = "Email Report";
                EmailReportForm.Visible = true;
                ReportOverviewPanel.Visible = false;
                ReportDetailsPanel.Visible = false;
            }
        }

        protected void EmailFormButton_Click(object sender, EventArgs e)
        {   
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            ReportPreviewControl.RenderControl(hw);
            var html = sb.ToString();

            try 
            {
                ErrorMessage.Text = Methods.SendEmail(FromEmail.Text, ToEmail.Text, html);
            }
            catch(Exception ex){
                ErrorMessage.Text = ex.Message;
            }
            finally
            {
                EmailReportForm.Visible = false;
                ToEmail.Text = String.Empty;
                FromEmail.Text = String.Empty;
                SuccessMessage.Text = "Email has been successfully sent!";
            }
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

        //override the VerifyRenderingInServerForm() to verify the control
        public override void VerifyRenderingInServerForm(Control control)
        {
            //Required to verify that the control is rendered properly on page
        }
    }
}