using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using SE.Models;
using Category = SE.Models.Category;
using Task = SE.Models.Task;

namespace SE.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Requests : Page
    {
        readonly MembershipUser _membershipUser = Membership.GetUser();
        static readonly ipawsTeamBEntities _db = new ipawsTeamBEntities(); 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (_membershipUser == null) return;
            Session["DataSource"] = Member.GetSupervisorRequests(_membershipUser.UserName);
            BindRequests();
        }

        private void BindRequests()
        {
            requests.DataSource = Session["DataSource"];
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
            if (e.CommandName == "AcceptRequest")
            {
                var args = e.CommandArgument.ToString().Split(';');
                var categoryId = args[0];
                if (categoryId == null) throw new ArgumentNullException("sender");
                var requestingUser = args[1];
                if (requestingUser == null) throw new ArgumentNullException("sender");
                var catName = "";
                var catId = Convert.ToInt32(categoryId);

                var update =
                    _db.RequestedCategories.Select(x => x).FirstOrDefault(x => x.RequestingUser == requestingUser && x.CategoryID == catId);

                if (update != null) update.IsApproved = true;

                _db.SaveChanges();

                catName = _db.Categories.Where(x => x.CategoryID == catId).Select(x => x.CategoryName).FirstOrDefault();

                var obj = new Category
                {
                    CategoryName = catName,
                    CreatedBy = requestingUser,
                    CreatedTime = DateTime.Now,
                    IsActive = true
                };

                _db.Categories.Add(obj);
                _db.SaveChanges();

                AddTasks(catName, catId, requestingUser);

                var message = _membershipUser + " has accepted your request for " + catName;
                ScriptManager.RegisterStartupScript(this, typeof(string), "Registering",
                    String.Format("evaluateRequests('{0}'{1}'{2}'{3}'{4}');", message, ",", requestingUser, ",", "success"), true);
            }
            else if (e.CommandName == "RejectRequest")
            {
                var args = e.CommandArgument.ToString().Split(';');
                var categoryId = args[0];
                if (categoryId == null) throw new ArgumentNullException("sender");
                var requestingUser = args[1];
                if (requestingUser == null) throw new ArgumentNullException("sender");


                var catId = Convert.ToInt32(categoryId);

                var delete =
                    _db.RequestedCategories.Select(x => x).FirstOrDefault(x => x.CategoryID == catId && x.RequestingUser == requestingUser);
                if (delete != null)
                {
                    var catName = delete.Category.CategoryName;

                    _db.RequestedCategories.Remove(delete);
                    _db.SaveChanges();

                    var message = _membershipUser + " has rejected your request for " + catName;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Registering",
                        String.Format("evaluateRequests('{0}'{1}'{2}'{3}'{4}');", message, ",", requestingUser, ",", "error"), true);
                }
            }
            Session["DataSource"] = Member.GetSupervisorRequests(_membershipUser.UserName);
            BindRequests();
        }

        /// <summary>
        /// </summary>
        /// <param name="catName"></param>
        /// <param name="previousId"></param>
        /// <param name="requestingUser"></param>
        private static void AddTasks(string catName, int previousId, string requestingUser)
        {
            
            var newCatId = _db.Categories.Where(find => find.CategoryName == catName && find.CreatedBy == requestingUser).Select(r => r.CategoryID).FirstOrDefault();

            using (var con = new SqlConnection(Methods.GetConnectionString()))
            {
                con.Open();
                const string taskQuery = "Insert Into Tasks (CategoryID,TaskName,IsActive,CreatedTime,CreatedBy) Select @id, TaskName, IsActive, CreatedTime, @supervisor From Tasks Where CategoryID = @prevID";
                var cmd2 = new SqlCommand(taskQuery, con);
                cmd2.Parameters.AddWithValue("@supervisor", requestingUser);
                cmd2.Parameters.AddWithValue("@id", newCatId);
                cmd2.Parameters.AddWithValue("@prevID", previousId);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
        }
        protected void HelpBtn_OnClick(object sender, EventArgs e)
        {
            lblModalTitle.Text = "Help!";
            lblModalBody.Text =
                "This page provides two simple tables which show both Category and Task Requests respectively. You can view the Name, Requesting User, and Date for each request that you have.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        }

        protected void requests_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            requests.EditIndex = -1;
            requests.PageIndex = e.NewPageIndex;
            BindRequests();
        }
    }
}
