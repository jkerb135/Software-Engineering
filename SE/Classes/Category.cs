using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SE.Classes
{
    [Serializable()]
    public class Category
    {
        #region Properties

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CreatedTime { get; set; }
        public List<string> CategoryAssignments { get; set; }

        #endregion

        #region Constructors

        public Category()
        {
            this.CategoryID = 0;
            this.CategoryName = String.Empty;
            this.CategoryAssignments = null;
        }

        public Category(int CategoryID = 0, string CategoryName = "")
        {
            this.CategoryID = CategoryID;
            this.CategoryName = CategoryName;
        }

        #endregion

        public void CreateCategory()
        {
            string queryString =
                "INSERT INTO Categories (CategoryName, CreatedBy, CreatedTime) " +
                "VALUES (@categoryname, @createdby, @createdtime)";

            string queryString2 = "SELECT MAX(CategoryID) FROM Categories";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryname", CategoryName);
                cmd.Parameters.AddWithValue("@createdby", 
                    System.Web.HttpContext.Current.User.Identity.Name);
                cmd.Parameters.AddWithValue("@createdtime", DateTime.Now);

                con.Open();

                cmd.ExecuteNonQuery();
                CategoryID = Convert.ToInt32(cmd2.ExecuteScalar());

                con.Close();
            }
        }

        public void UpdateCategory()
        {
            string queryString =
                "UPDATE Categories " +
                "SET CategoryName=@categoryname " +
                "WHERE CategoryID=@categoryid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                cmd.Parameters.AddWithValue("@categoryname", CategoryName);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void DeleteCategory()
        {
            string queryString =
                "DELETE FROM Categories " +
                "WHERE CategoryID=@categoryid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void AssignUserCategories()
        {
            string queryString =
                "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                "VALUES (@assigneduser,@categoryid)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                cmd.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                foreach (string CatAssign in CategoryAssignments)
                {
                    cmd.Parameters["@assigneduser"].Value = CatAssign;
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public void ReAssignUserCategories()
        {
            string queryString =
                "DELETE FROM CategoryAssignments " +
                "WHERE CategoryID=@categoryid ";

            string queryString2 =
                "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                "VALUES (@assigneduser,@categoryid)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                cmd2.Parameters.AddWithValue("@categoryid", CategoryID);
                cmd2.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

                foreach (string CatAssign in CategoryAssignments)
                {
                    cmd2.Parameters["@assigneduser"].Value = CatAssign;
                    cmd2.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public static List<Category> GetAssignedCategories(string Username)
        {
            List<Category> AssignedCategories = new List<Category>();

            string queryString = 
                "SELECT * FROM CategoryAssignments " +
                "INNER JOIN Categories ON CategoryAssignments.CategoryID=Categories.CategoryID " +
                "WHERE CategoryAssignments.AssignedUser=@assigneduser";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assigneduser", Username);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Category cat = new Category();
                    cat.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                    AssignedCategories.Add(cat);
                }

                con.Close();
            }
            
            return AssignedCategories;
        }
        public static List<string> GetUsersCategories(string Username)
        {
            List<string> AssignedCategories = new List<string>();

            string queryString = "SELECT * FROM CategoryAssignments INNER JOIN Categories ON CategoryAssignments.CategoryID = Categories.CategoryID Where AssignedUser = @user";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", Username);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AssignedCategories.Add(Convert.ToString(dr["CategoryName"]));
                }

                con.Close();
            }

            return AssignedCategories;
        }
        public static DataSet GetSupervisorCategories(string Username)
        {
            DataSet assignedCategories = new DataSet();
            DataTable categories = new DataTable();
            categories = assignedCategories.Tables.Add("Users");
            categories.Columns.Add("Category Name");
            categories.Columns.Add("Activity");
            //categories.Columns.Add("Created Date");
            categories.Columns.Add("Users In Category");
            categories.Columns.Add("Assign Users");

            string queryString =
                "SELECT * FROM Categories " +
                "WHERE CreatedBy=@assignedSupervisor";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assignedSupervisor", Username);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow row;
                    row = categories.NewRow();
                    row["Category Name"] = dr["CategoryName"].ToString();
                    //row["IsActive"] = dr["CategoryName"].ToString();
                    //row["Created Date"] = dr["CreatedTime"].ToString();
                    foreach (string item in Member.UsersAssignedToSupervisorAssignedToCategory(Membership.GetUser().ToString(), Convert.ToInt32(dr["CategoryId"])))
                    {
                        row["Users In Category"] += item + ", ";
                    }
                    categories.Rows.Add(row);
                }

                con.Close();
            }
            return assignedCategories;
        }
        public bool CategoryIsAssigned()
        {
            bool CategoryIsAssigned = false;

            string queryString =
                "SELECT COUNT(*) " +
                "FROM CategoryAssignments " +
                "WHERE CategoryID=@categoryid";

            string queryString2 =
                "SELECT COUNT(*) " +
                "FROM Tasks " +
                "WHERE CategoryID=@categoryid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                cmd2.Parameters.AddWithValue("@categoryid", CategoryID);

                con.Open();

                CategoryIsAssigned = ((int)cmd.ExecuteScalar() > 0) ? true : false;
                CategoryIsAssigned = ((int)cmd2.ExecuteScalar() > 0) ? true : CategoryIsAssigned;

                con.Close();
            }

            return CategoryIsAssigned;
        }

        public static List<Category> GetAllCategories()
        {
            List<Category> AllCategories = new List<Category>();

            string queryString = "SELECT * FROM Categories";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Category cat = new Category();
                    cat.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                    cat.CreatedTime = Convert.ToString(dr["CreatedTime"]);
                    AllCategories.Add(cat);
                }

                con.Close();
            }

            return AllCategories;
        }
        public static int getCategoryID(string CategoryName)
        {
            int id = -1;

            string queryString =
                "SELECT CategoryID " +
                "FROM Categories " +
                "WHERE CategoryName=@categoryName";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryname", CategoryName);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["CategoryID"]);
                }

                con.Close();
            }
            return id;
        }
    }
}