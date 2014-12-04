using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;

namespace SE.Admin
{
    public partial class Reports : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            PageHeader.InnerText = "Reports";
            ReportOverviewPanel.Visible = false;
            ReportDetailsPanel.Visible = false;
            EmailReportForm.Visible = false;
            FromEmail.Text = "ipawsteamb@gmail.com";
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (ReportOverviewPanel.Visible) return;
            PageHeader.InnerText = "Generate Report";
            SuccessMessage.Text = String.Empty;
            ErrorMessage.Text = String.Empty;
            ReportOverviewPanel.Visible = true;
            EmailReportForm.Visible = false;
            ReportOverview.DataSource = Report.GenerateReport();
            ReportOverview.DataBind();
        }

        protected void EmailReportButton_Click(object sender, EventArgs e)
        {
            if (EmailReportForm.Visible) return;
            PageHeader.InnerText = "Email Report";
            SuccessMessage.Text = String.Empty;
            ErrorMessage.Text = String.Empty;
            EmailReportForm.Visible = true;
            ReportOverviewPanel.Visible = false;
            ReportDetailsPanel.Visible = false;
        }

        protected void EmailFormButton_Click(object sender, EventArgs e)
        {   
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            var hw = new HtmlTextWriter(tw);
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
                FromEmail.Text = "ipawsteamb@gmail.com";
                ToEmail.Text = String.Empty;
                SuccessMessage.Text = "Email has been successfully sent!";
            }
        }

        protected void OverviewRdb(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(ReportOverview, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }

        protected void OverviewSic(object sender, EventArgs e)
        {
            if (!ReportDetailsPanel.Visible)
            {
                ReportDetailsPanel.Visible = true;
                
            }

            foreach (GridViewRow row in ReportOverview.Rows)
            {
                if (row.RowIndex == ReportOverview.SelectedIndex)
                {
                    var user = row.Cells[0].Text;
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