using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using SE.Models;
using Category = SE.Classes.Category;

namespace SE.Admin
{
    public partial class MakeARequest : System.Web.UI.Page
    {
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        private readonly string _mem = Membership.GetUser().UserName;
        public static bool PendingRequest;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            QueryGridView.DataBind();

        }
        protected void Search_OnClick(object sender, EventArgs e)
        {

            var dt = new DataTable();
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("CreatedBy");
            dt.Columns.Add("CreatedTime");

            var queryvalue = SearchTxt.Text;
            var mine = _db.Categories.Where(x => x.CreatedBy == _mem).ToList();
            var all = _db.Categories.Where(x =>(x.CategoryName.Contains(queryvalue) || x.CreatedBy.Contains(queryvalue)) && x.CreatedBy != _mem && x.IsPublished).ToList();

            if (all.Count == 0)
            {
                QueryGridView.EmptyDataText = "Found 0 Results.";
                QueryGridView.DataSource = all;
                return;
            }

            for (var i = 0; i < all.Count; i++)
            {
                var i1 = i;
                foreach (var cat in mine.Where(cat => cat.CategoryName == all[i1].CategoryName))
                {
                    all.Remove(all[i]);
                }
            }
            foreach (var result in all)
            {
                var row = dt.NewRow();
                row["CategoryID"] = result.CategoryID;
                row["CategoryName"] = result.CategoryName;
                row["CreatedBy"] = "<a class='signalRUser' id= " + result.CreatedBy +
                                   " href='Profile.aspx?userName=" + result.CreatedBy + "'>" + result.CreatedBy +
                                   "</a>";
                row["CreatedTime"] = result.CreatedTime;

                dt.Rows.Add(row);
            }

            Session["DataSource"] = dt;
            QueryGridView.DataSource = Session["DataSource"];
            QueryGridView.DataBind();
            QueryCatRequestStatus();
        }

        protected void QueryGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gvr = (GridViewRow) (((Button) e.CommandSource).NamingContainer);
            var idx = gvr.RowIndex;

            var _otherUser = Regex.Replace(((HyperLink)QueryGridView.Rows[idx].Cells[1].Controls[0]).Text, @"<[^>]+>|&nbsp;", "").Trim();

            const string queryString =
                "Insert Into RequestedCategories (CategoryID, IsApproved, RequestingUser,CreatedBy,Date) Values (@id, @bool, @user,@owner,@date)";
            const string queryString2 =
                "Select count(*) from RequestedCategories Where CategoryID = @id and RequestingUser=@user";
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd.Parameters.AddWithValue("@user", _mem);
                cmd.Parameters.AddWithValue("@bool", false);
                cmd.Parameters.AddWithValue("@owner", _otherUser);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                var cmd2 = new SqlCommand(queryString2, con);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(e.CommandArgument));
                cmd2.Parameters.AddWithValue("@user", _mem);


                con.Open();
                PendingRequest = false;

                var count = (Int32)cmd2.ExecuteScalar();
                if (count == 0)
                {
                    cmd.ExecuteNonQuery();
                    PendingRequest = true;
                }
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("sendRequest2('{0}');", _otherUser), true);
            QueryCatRequestStatus();
            requestUpdatePanel.Update();
        }

        public void QueryCatRequestStatus()
        {
            var catStatus = _db.RequestedCategories.Where(x => x.RequestingUser == _mem);

            foreach (var status in catStatus)
            {
                for (int i = 0, len = QueryGridView.Rows.Count; i < len; i++)
                {
                    var row = QueryGridView.Rows[i];
                    var approved = status.IsApproved;
                    var request = (Button) row.FindControl("RequestCat");
                    var user =
                        Regex.Replace(((HyperLink) QueryGridView.Rows[i].Cells[1].Controls[0]).Text, @"<[^>]+>|&nbsp;",
                            "").Trim();
                    var catId = Convert.ToInt32(status.CategoryID);
                    var otherCatId = Category.GetCategoryIdBySupervisor(row.Cells[0].Text, user);
                    if (approved || catId != otherCatId) continue;
                    request.Text = "Pending";
                    request.CssClass = "btn btn-warning form-control";
                    request.Enabled = false;
                }

            }
        }
    }
}