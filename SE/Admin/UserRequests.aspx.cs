using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SE.Classes;
using SE.Models;
using Category = SE.Classes.Category;
using Task = SE.Models.Task;

namespace SE.Admin
{
    public partial class UserRequests : System.Web.UI.Page
    {
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        private readonly string _mem = Membership.GetUser().UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindUserRequests();
        }

        protected void requests_OnRowCommand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AcceptRequest")
            {
                var gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                var idx = gvr.RowIndex;

                var name = requests.Rows[idx].Cells[1].Text;
                var user = requests.Rows[idx].Cells[3].Text;

                UsernameTxt.Text = user;
                TaskNameTxt.Text = name;
                upModal.Update();

                BindCategories();

                taskrequestid.Value = (e.CommandArgument.ToString());
                requestUpdatePanel.Update();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#taskRequestModal').modal();", true);
            }
        }

        protected void requests_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            requests.EditIndex = -1;
            requests.PageIndex = e.NewPageIndex;
            BindUserRequests();
        }

        protected void BindUserRequests()
        {
            Session["DataSource"] = RequestClass.GetTaskRequests();
            requests.DataSource = Session["DataSource"];
            requests.DataBind();

        }

        private void BindCategories()
        {
            var linq = _db.Categories.Where(x => x.CreatedBy == _mem);

            foreach (var cat in linq)
            {
                CategoryDrp.Items.Add(new ListItem(cat.CategoryName, cat.CategoryID.ToString()));
            }
        }


        protected void AddTask_OnClick(object sender, EventArgs e)
        {
            if (CategoryText.Text == String.Empty && CategoryDrp.SelectedValue != "null")
            {
                var task = new Task
                {
                    TaskName = TaskNameTxt.Text,
                    CategoryID = Convert.ToInt32(CategoryDrp.SelectedValue),
                    CreatedBy = _mem,
                    CreatedTime = DateTime.Now,
                    IsActive = true,
                };
                _db.Tasks.Add(task);

                var assign = new TaskAssignment
                {
                    TaskID = task.TaskID,
                    CategoryID = Convert.ToInt32(CategoryDrp.SelectedValue),
                    AssignedUser = UsernameTxt.Text
                };

                _db.TaskAssignments.Add(assign);

                var remove = _db.UserTaskRequests.Find(Convert.ToInt32(taskrequestid.Value));
                _db.UserTaskRequests.Remove(remove);
                _db.SaveChanges();

                BindUserRequests();
                requestUpdatePanel.Update();
                ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("submitUserRequest('{0}');", "Successfully Created The Task!"), true);
            }
            else if (CategoryText.Text != String.Empty && CategoryDrp.SelectedValue == "null")
            {
                
            }

        }

        protected void requests_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = ((Label)requests.Rows[e.RowIndex].FindControl("ID")).Text;

            var delete = _db.UserTaskRequests.Find(Convert.ToInt32(id));

            _db.UserTaskRequests.Remove(delete);
            _db.SaveChanges();


            BindUserRequests();
            requestUpdatePanel.Update();
            ScriptManager.RegisterStartupScript(this, typeof(string), "Registering", String.Format("submitUserRequest('{0}');", "Successfully Deleted The Task!"), true);
        }
    }
}