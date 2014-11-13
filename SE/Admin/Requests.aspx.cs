using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using SE.Models;

namespace SE.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Requests : Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
                RequestSource.SelectCommand =
                    "Select b.CategoryID,b.CategoryName,a.RequestingUser, a.Date From RequestedCategories a inner join Categories b on a.CategoryID = b.CategoryID Where a.CreatedBy = '" +
                    membershipUser.UserName + "' and a.IsApproved = '" + false + "'";
            requests.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void users_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            requestUpdatePanel.Update();
            var args = e.CommandArgument.ToString().Split(';');
            var categoryId = args[0];
            if (categoryId == null) throw new ArgumentNullException("categoryId");
            var requestingUser = args[1];
            if (requestingUser == null) throw new ArgumentNullException("requestingUser");
            string queryString, queryString2, catName = "";
            if (e.CommandName == "AcceptRequest")
            {
                queryString = "Update RequestedCategories SET IsApproved= '" + true + "' WHERE RequestingUser = '" +
                              requestingUser + "' and CategoryID = '" + categoryId + "'";
                queryString2 = "Select * From Categories Where CategoryID = '" + categoryId + "'";
            }
            else
            {
                queryString = "Delete From RequestedCategories WHERE RequestingUser = '" + requestingUser +
                              "' and CategoryID = '" + categoryId + "'";
                queryString2 = "";
            }
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                con.Open();
                cmd.ExecuteScalar();
                if (queryString2 != "")
                {
                    var dr = cmd2.ExecuteReader();

                    while (dr.Read())
                    {
                        catName = dr["CategoryName"].ToString();
                    }
                }

                con.Close();
            }
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                con.Open();
                var queryString3 = "Insert Into Categories (CategoryName,CreatedBy,CreatedTime,IsActive) Values ('" +
                                      catName + "','" + requestingUser + "','" + DateTime.Now + "','" + true + "')";
                var cmd3 = new SqlCommand(queryString3, con);
                cmd3.ExecuteNonQuery();
                con.Close();
            }

            AddTasks(catName, categoryId, requestingUser);
        }

        /// <summary>
        /// </summary>
        /// <param name="catName"></param>
        /// <param name="previousId"></param>
        /// <param name="requestingUser"></param>
        private static void AddTasks(string catName, string previousId, string requestingUser)
        {
            var db = new ipawsTeamBEntities();
            var newCatId = db.Categories.Where(find => find.CategoryName == catName && find.CreatedBy == requestingUser).Select(r => r.CategoryID).FirstOrDefault();
            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                con.Open();
                const string taskQuery = "Insert Into Tasks (CategoryID,TaskName,TaskTime,IsActive,CreatedTime,CreatedBy) Select @id, TaskName, TaskTime, IsActive, CreatedTime, @supervisor From Tasks Where CategoryID = @prevID";
                var cmd2 = new SqlCommand(taskQuery, con);
                cmd2.Parameters.AddWithValue("@supervisor", requestingUser);
                cmd2.Parameters.AddWithValue("@id", newCatId);
                cmd2.Parameters.AddWithValue("@prevID", previousId);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
