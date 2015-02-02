using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace SE.Classes
{
    [Serializable]
    public class Category
    {
        #region Properties

        private bool _isActive = true;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CreatedTime { get; set; }
        public List<string> CategoryAssignments { get; set; }
        public bool IsActive
        {
            get
            {
                if (CategoryId <= 0) return _isActive;
                const string queryString = "SELECT IsActive FROM Categories " +
                                           "WHERE CategoryID=@categoryid";

                using (var con = new SqlConnection(
                    Methods.GetConnectionString()))
                {
                    var cmd = new SqlCommand(queryString, con);

                    cmd.Parameters.AddWithValue("@categoryid", CategoryId);

                    con.Open();

                    _isActive = Convert.ToBoolean(cmd.ExecuteScalar());

                    con.Close();
                }

                return _isActive;
            }
            set
            {
                if (CategoryId > 0)
                {
                    const string queryString = "UPDATE Categories " +
                                               "SET IsActive=@isactive " +
                                               "WHERE CategoryID=@categoryid";

                    using (var con = new SqlConnection(
                        Methods.GetConnectionString()))
                    {
                        var cmd = new SqlCommand(queryString, con);

                        cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                        cmd.Parameters.AddWithValue("@isactive", value);

                        con.Open();

                        cmd.ExecuteScalar();

                        con.Close();
                    }
                }

                _isActive = value;
            }
        }

        #endregion

        #region Constructors

        public Category()
        {
            CategoryId = 0;
            CategoryName = String.Empty;
            CategoryAssignments = null;
            IsActive = true;
        }

        public Category(int categoryId = 0, string categoryName = "")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        #endregion

        public void CreateCategory()
        {
            const string queryString = "INSERT INTO Categories (CategoryName, CreatedBy, CreatedTime) " +
                                       "VALUES (@categoryname, @createdby, @createdtime)";

            const string queryString2 = "SELECT MAX(CategoryID) FROM Categories";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryname", CategoryName);
                cmd.Parameters.AddWithValue("@createdby", 
                    System.Web.HttpContext.Current.User.Identity.Name);
                cmd.Parameters.AddWithValue("@createdtime", DateTime.Now);

                con.Open();

                cmd.ExecuteNonQuery();
                CategoryId = Convert.ToInt32(cmd2.ExecuteScalar());

                con.Close();
            }
        }

        public void UpdateCategory()
        {
            const string queryString = "UPDATE Categories " +
                                       "SET CategoryName=@categoryname " +
                                       "WHERE CategoryID=@categoryid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd.Parameters.AddWithValue("@categoryname", CategoryName);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void DeleteCategory()
        {
            const string queryString = "DELETE FROM Categories " +
                                       "WHERE CategoryID=@categoryid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void AssignUserCategories()
        {
            const string queryString = "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                                       "VALUES (@assigneduser,@categoryid)";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                foreach (var catAssign in CategoryAssignments)
                {
                    cmd.Parameters["@assigneduser"].Value = catAssign;
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public void ReAssignUserCategories()
        {
            const string queryString = "DELETE FROM CategoryAssignments " +
                                       "WHERE CategoryID=@categoryid ";

            const string queryString2 = "DELETE FROM TaskAssignments " +
                                        "WHERE CategoryID=@categoryid ";

            const string queryString3 = "INSERT INTO CategoryAssignments (AssignedUser, CategoryID) " +
                                        "VALUES (@assigneduser,@categoryid)";

            const string queryString4 = "INSERT INTO TaskAssignments (CategoryID, TaskID, AssignedUser) " +
                                        "SELECT @categoryid, Tasks.TaskID, @assigneduser " +
                                        "FROM Tasks " +
                                        "WHERE Tasks.CategoryID = @categoryid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);
                var cmd4 = new SqlCommand(queryString4, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);

                cmd2.Parameters.AddWithValue("@categoryid", CategoryId);

                cmd3.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd3.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                cmd4.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd4.Parameters.AddWithValue("@assigneduser", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                foreach (var catAssign in CategoryAssignments)
                {
                    cmd3.Parameters["@assigneduser"].Value = catAssign;
                    cmd3.ExecuteNonQuery();

                    cmd4.Parameters["@assigneduser"].Value = catAssign;
                    cmd4.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        public static List<Category> GetAssignedCategories(string username)
        {
            var assignedCategories = new List<Category>();

            const string queryString = "SELECT * FROM CategoryAssignments " +
                                       "INNER JOIN Categories ON CategoryAssignments.CategoryID=Categories.CategoryID " +
                                       "WHERE CategoryAssignments.AssignedUser=@assigneduser";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assigneduser", username);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var cat = new Category
                    {
                        CategoryId = Convert.ToInt32(dr["CategoryID"]),
                        CategoryName = dr["CategoryName"].ToString()
                    };
                    assignedCategories.Add(cat);
                }

                con.Close();
            }
            
            return assignedCategories;
        }
        public static List<string> GetUsersCategories(string username)
        {
            var assignedCategories = new List<string>();

            const string queryString = "SELECT * FROM CategoryAssignments INNER JOIN Categories ON CategoryAssignments.CategoryID = Categories.CategoryID Where AssignedUser = @user";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", username);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    assignedCategories.Add(Convert.ToString(dr["CategoryName"]));
                }

                con.Close();
            }

            return assignedCategories;
        }
        public static DataSet GetSupervisorCategories(string username)
        {
            var assignedCategories = new DataSet();
            var categories = assignedCategories.Tables.Add("Users");
            categories.Columns.Add("Category Name");
            categories.Columns.Add("Activity");
            categories.Columns.Add("Users In Category");
            categories.Columns.Add("Assign Users");

            const string queryString = "SELECT * FROM Categories " +
                                       "WHERE CreatedBy=@assignedSupervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assignedSupervisor", username);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var row = categories.NewRow();
                    row["Category Name"] = dr["CategoryName"].ToString();
                    var membershipUser = Membership.GetUser();
                    if (membershipUser != null)
                        foreach (var item in Member.UsersAssignedToSupervisorAssignedToCategory(membershipUser.ToString(), Convert.ToInt32(dr["CategoryId"])))
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
            bool categoryIsAssigned;

            const string queryString = "SELECT COUNT(*) " +
                                       "FROM CategoryAssignments " +
                                       "WHERE CategoryID=@categoryid";

            const string queryString2 = "SELECT COUNT(*) " +
                                        "FROM Tasks " +
                                        "WHERE CategoryID=@categoryid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                cmd2.Parameters.AddWithValue("@categoryid", CategoryId);

                con.Open();

                categoryIsAssigned = ((int)cmd.ExecuteScalar() > 0);
                categoryIsAssigned = ((int)cmd2.ExecuteScalar() > 0) || categoryIsAssigned;

                con.Close();
            }

            return categoryIsAssigned;
        }

        public static List<Category> GetAllCategories()
        {
            var allCategories = new List<Category>();

            const string queryString = "SELECT * FROM Categories";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var cat = new Category
                    {
                        CategoryId = Convert.ToInt32(dr["CategoryID"]),
                        CategoryName = dr["CategoryName"].ToString(),
                        CreatedTime = Convert.ToString(dr["CreatedTime"])
                    };
                    allCategories.Add(cat);
                }

                con.Close();
            }

            return allCategories;
        }
        public static int GetCategoryId(string categoryName)
        {
            var id = -1;

            const string queryString = "SELECT CategoryID " +
                                       "FROM Categories " +
                                       "WHERE CategoryName=@categoryName";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryname", categoryName);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["CategoryID"]);
                }

                con.Close();
            }
            return id;
        }

        public static int GetCategoryIdBySupervisor(string categoryName, string creator)
        {
            var id = -1;

            const string queryString = "SELECT CategoryID " +
                                       "FROM Categories " +
                                       "WHERE CategoryName=@categoryName and CreatedBy = @creator";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryname", categoryName);
                cmd.Parameters.AddWithValue("@creator", creator);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    id = Convert.ToInt32(dr["CategoryID"]);
                }

                con.Close();
            }
            return id;
        }
        public static string GetCategoryName(int id)
        {
            var catName = "";
            const string queryString = "SELECT CategoryName " +
                                       "FROM Categories " +
                                       "WHERE CategoryID=@categoryID";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryID", id);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    catName = dr["CategoryName"].ToString();
                }

                con.Close();
            }
            return catName;
        }

        public static bool UserInCategory(string user, int categoryId)
        {
            bool userInCategory;

            const string queryString = "SELECT COUNT(*) " +
                                       "FROM CategoryAssignments " +
                                       "WHERE AssignedUser=@assigneduser " +
                                       "AND CategoryID=@categoryid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assigneduser", user);
                cmd.Parameters.AddWithValue("@categoryid", categoryId);

                con.Open();

                userInCategory = ((int)cmd.ExecuteScalar() > 0);

                con.Close();
            }

            return userInCategory; 
        }
    }
}