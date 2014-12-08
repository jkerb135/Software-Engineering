/*
Author			: Josh Kerbaugh\Daniel Talley
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: ReportPreview.aspx.cs
Purpose			: This file contains the backend code for previewing a report.
*/
using SE.Classes;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE.UserControls
{
    public partial class ReportPreview : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmailGrid.DataSource = Report.GenerateReport();
            EmailGrid.DataBind();

            var dr = (SqlDataReader)UserDataSource.Select(DataSourceSelectArguments.Empty);
            while (dr.Read())
            {
                var userData = Report.GenerateDetailedReport(dr["AssignedUser"].ToString());
                var gv = new GridView();

                if (userData.Tables[0].Rows.Count == 0) continue;
                EmailContents.Controls.Add(new LiteralControl("<h2>Details About " +
                                                              dr["AssignedUser"].ToString() + "</h2>"));

                gv.CssClass = "report table table-bordered";
                gv.DataSource = userData;
                EmailContents.Controls.Add(gv);
            }
            EmailContents.DataBind();
            dr.Close();
        }
    }
}