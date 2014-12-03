using SE.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE.UserControls
{
    public partial class ReportPreview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmailGrid.DataSource = Report.GenerateReport();
            EmailGrid.DataBind();

            var dr = (SqlDataReader)UserDataSource.Select(DataSourceSelectArguments.Empty);
            while (dr.Read())
            {
                DataSet UserData = Report.GenerateDetailedReport(dr["AssignedUser"].ToString());
                GridView gv = new GridView();

                if (UserData.Tables[0].Rows.Count != 0)
                {
                    EmailContents.Controls.Add(new LiteralControl("<h2>Details About " +
                        dr["AssignedUser"].ToString() + "</h2>"));

                    gv.CssClass = "report table table-bordered";
                    gv.DataSource = UserData;
                    EmailContents.Controls.Add(gv);
                }
            }
            EmailContents.DataBind();
            dr.Close();
        }
    }
}