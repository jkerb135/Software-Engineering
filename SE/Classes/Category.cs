using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SE.Classes
{
    [Serializable()]
    public class Category
    {
        #region Properties

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        #endregion

        #region Constructors

        public Category()
        {
            this.CategoryID = 0;
            this.CategoryName = String.Empty;
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
                "INSERT INTO Categories (CategoryName, CreatedBy) " +
                "VALUES (@categoryname, @createdby)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryname", CategoryName);
                cmd.Parameters.AddWithValue("@createdby", 
                    System.Web.HttpContext.Current.User.Identity.Name);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateCategory()
        {
            string queryString =
                "UPDATE Categories " +
                "SET CategoryName=@categoryname ";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryname", CategoryName);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void DeleteCategory(string CategoryName)
        {
            string queryString =
                "DELETE FROM Categories " +
                "WHERE CategoryName=@categoryname";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryname", CategoryName);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void AssignUserToCategory(string Username)
        {
            string queryString =
                "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                "VALUES (@assigneduser,@categoryid)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assigneduser", Username);
                cmd.Parameters.AddWithValue("@categoryid", CategoryID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UnAssignUserFromCategory(string Username)
        {
            string queryString =
                "DELETE FROM CategoryAssignments " +
                "WHERE AssignedUser=@assigneduser " +
                "AND CategoryID=@categoryid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assigneduser", Username);
                cmd.Parameters.AddWithValue("@categoryid", CategoryID);

                con.Open();

                cmd.ExecuteNonQuery();

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
        public static List<Category> GetSupervisorCategoriesList(string Username)
        {
            List<Category> AssignedCategories = new List<Category>();

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
                    Category cat = new Category();
                    cat.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                    AssignedCategories.Add(cat);
                }
                con.Close();
            }

            return AssignedCategories;
        }
        public static DataSet GetSupervisorCategories(string Username)
        {
            DataSet AssignedCategories = new DataSet();
            DataTable catTable = AssignedCategories.Tables.Add("SupervisorTasks");
            catTable.Columns.Add("Category Name");

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
                    DataRow newRow = catTable.NewRow();
                    newRow["Category Name"] = dr["CategoryName"].ToString();
                    catTable.Rows.Add(newRow);
                }
                con.Close();
            }

            return AssignedCategories;
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