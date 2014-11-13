using SE.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE.Admin
{
    public partial class Requests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RequestSource.SelectCommand = "Select b.CategoryID,b.CategoryName,a.RequestingUser, a.Date From RequestedCategories a inner join Categories b on a.CategoryID = b.CategoryID Where a.CreatedBy = '" + Membership.GetUser().UserName + "' and a.IsApproved = '" + false + "'";
                requests.DataBind();
            }
        }
        protected void users_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var args = e.CommandArgument.ToString().Split(';');
            string CategoryID = args[0];
            string RequestingUser = args[1];
            string queryString = "";
            string queryString2 = ""; 
            string queryString3 = "";
            string catName = "";
            if (e.CommandName == "AcceptRequest")
            {
                queryString = "Update RequestedCategories SET IsApproved= '" + true + "' WHERE RequestingUser = '" + RequestingUser + "' and CategoryID = '" + CategoryID + "'";
                queryString2 = "Select * From Categories Where CategoryID = '" + CategoryID + "'";
            }
            else
            {
                queryString = "Delete From RequestedCategories WHERE RequestingUser = '" + RequestingUser + "' and CategoryID = '" + CategoryID + "'";
            }
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                con.Open();
                cmd.ExecuteScalar();
                if (queryString2 != ""){

                    SqlDataReader dr = cmd2.ExecuteReader();

                    while (dr.Read())
                    {
                        catName = dr["CategoryName"].ToString();
                    }
                }

                con.Close();
            }
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                con.Open();
                queryString3 = "Insert Into Categories (CategoryName,CreatedBy,CreatedTime,IsActive) Values ('" + catName + "','" + RequestingUser + "','" + DateTime.Now + "','" + true + "')";
                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                cmd3.ExecuteNonQuery();
                con.Close();
            }

            addUnderlyingInfo(Convert.ToInt32(CategoryID));
        }
        protected void addUnderlyingInfo(int id)
        {
            string queryString = "Select * FROM Categories INNER JOIN Tasks ON Categories.CategoryID = Tasks.CategoryID INNER JOIN MainSteps ON MainSteps.TaskID = Tasks.TaskID INNER JOIN DetailedSteps ON MainSteps.MainStepID = DetailedSteps.MainStepID Where Categories.CategoryID = '" + id + "'";
            string taskQuery, mainQuery, detailQuery = "";
            using (SqlConnection con = new SqlConnection(Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var a = dr;

                }
            }
        }
    }
}