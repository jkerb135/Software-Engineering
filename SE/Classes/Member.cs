using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SE.Classes
{
    public static class Member
    {
        public static bool ValidatePassword(string Password, ref string ErrorMessage)
        {
            bool Valid = true;

            // Password is less then required length
            if (Password.Length < Membership.MinRequiredPasswordLength)
            {
                ErrorMessage += "Password must be at least " +
                    Membership.MinRequiredPasswordLength + " characters.<br/>";
                Valid = false;
            }

            // Password does not contain minimum special characters
            if (Password.Count(c => !char.IsLetterOrDigit(c)) <
                Membership.MinRequiredNonAlphanumericCharacters)
            {
                ErrorMessage += "Password must contain at least " +
                    Membership.MinRequiredNonAlphanumericCharacters +
                    " non-alphanumeric characters.<br/>";
                Valid = false;
            }

            return Valid;
        }

        /// <summary>Populates a dataset of all users by username, email and user role
        /// </summary> 
        public static DataSet CustomGetAllUsers()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = ds.Tables.Add("Users");

            MembershipUserCollection muc;
            muc = Membership.GetAllUsers();

            bool UserIsSupervisor;

            dt.Columns.Add("Username", Type.GetType("System.String"));
            dt.Columns.Add("Email", Type.GetType("System.String"));
            dt.Columns.Add("User Role", Type.GetType("System.String"));
            dt.Columns.Add("Assigned To", Type.GetType("System.String"));
            dt.Columns.Add("Status", Type.GetType("System.String"));

            /* Here is the list of columns returned of the Membership.GetAllUsers() method
             * UserName, Email, PasswordQuestion, Comment, IsApproved
             * IsLockedOut, LastLockoutDate, CreationDate, LastLoginDate
             * LastActivityDate, LastPasswordChangedDate, IsOnline, ProviderName
             */

            foreach (MembershipUser mu in muc)
            {
                if (!Roles.IsUserInRole(mu.UserName, "Manager"))
                {
                    DataRow dr;
                    UserIsSupervisor = Roles.IsUserInRole(mu.UserName, "Supervisor");

                    dr = dt.NewRow();
                    dr["Username"] = "<a href='?userpage=edituser&username=" + mu.UserName + "'>" + mu.UserName + "</a>";
                    dr["Email"] = mu.Email;
                    dr["User Role"] = UserIsSupervisor ? "Supervisor" : "User";
                    dr["Assigned To"] = !UserIsSupervisor ? UserAssignedTo(mu.UserName) : "";
                    dr["Status"] = mu.IsApproved ? "Active" : "Inactive";

                    dt.Rows.Add(dr);
                }
            }
            return ds;
        }

        public static DataSet CustomGetActiveUsers()
        {
            DataSet activeUsers = new DataSet();
            DataTable userTable = new DataTable();
            userTable = activeUsers.Tables.Add("Users");
            userTable.Columns.Add("Username", Type.GetType("System.String"));
            MembershipUserCollection activeUserCollection;
            activeUserCollection = Membership.GetAllUsers();
            foreach (MembershipUser membership in activeUserCollection)
            {
                if (Roles.IsUserInRole(membership.UserName, "User") && (UserAssignedTo(membership.UserName).ToLower() == Membership.GetUser().UserName.ToLower()) && (membership.IsOnline))
                {
                    DataRow row;
                    row = userTable.NewRow();
                    row["Username"] = membership.UserName;
                    userTable.Rows.Add(row);
                }
            }
            if (userTable.Rows.Count == 0){
                DataRow row = userTable.NewRow();
                row["Username"] = "No Users Online";
                userTable.Rows.Add(row);
            }
            return activeUsers;
        }

        public static DataSet CustomRecentlyAssigned()
        {
            DataSet recentUsers = new DataSet();
            DataTable users = new DataTable();
            users = recentUsers.Tables.Add("Users");
            users.Columns.Add("Username", Type.GetType("System.String"));
            MembershipUserCollection recentUser = Membership.GetAllUsers();
            DateTime aWeekAgo = DateTime.Now.AddDays(-7);
            foreach (MembershipUser membership in recentUser)
            {
                if (Roles.IsUserInRole(membership.UserName, "User") && (membership.CreationDate >= aWeekAgo) && UserAssignedTo(membership.UserName).ToLower() == Membership.GetUser().UserName.ToLower())
                {
                    DataRow row;
                    row = users.NewRow();
                    row["Username"] = membership.UserName;
                    users.Rows.Add(row);
                }
            }
            if (users.Rows.Count == 0)
            {
                DataRow newRow;
                newRow = users.NewRow();
                newRow["Username"] = "No new users assigned";
                users.Rows.Add(newRow);
            }

            return recentUsers;
        }

        /// <summary>Assign user to supervisor
        /// </summary> 
        public static void AssignToUser(string User, string Supervisor)
        {
            string queryString =
                "INSERT INTO MemberAssignments (AssignedUser, AssignedSupervisor) " +
                "VALUES (@user,@supervisor)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);

                if (!String.IsNullOrEmpty(Supervisor))
                    cmd.Parameters.AddWithValue("@supervisor", Supervisor);
                else
                    cmd.Parameters.AddWithValue("@supervisor", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Edit assign user to supervisor
        /// </summary> 
        public static void EditAssignToUser(string User, string Supervisor)
        {
            string queryString =
                "UPDATE MemberAssignments " +
                "SET AssignedSupervisor=@supervisor " +
                "WHERE AssignedUser=@user";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);
                
                if(!String.IsNullOrEmpty(Supervisor))
                    cmd.Parameters.AddWithValue("@supervisor", Supervisor);
                else
                    cmd.Parameters.AddWithValue("@supervisor", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Remove user assignment row
        /// </summary> 
        public static void RemoveAssignedUser(string User)
        {
            string queryString =
                "DELETE FROM CategoryAssignments " +
                "WHERE AssignedUser=@user";

            string queryString2 =
                "UPDATE Tasks " +
                "SET AssignedUser=NULL " +
                "WHERE AssignedUser=@user";

            string queryString3 =
                "DELETE FROM CompletedMainSteps " +
                "WHERE AssignedUser=@user";

            string queryString4 =
                "DELETE FROM CompletedTasks " +
                "WHERE AssignedUser=@user";

            string queryString5 =
                "UPDATE MemberAssignments " +
                "SET AssignedSupervisor=NULL " +
                "WHERE AssignedUser=@user";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                SqlCommand cmd4 = new SqlCommand(queryString4, con);
                SqlCommand cmd5 = new SqlCommand(queryString5, con);

                cmd.Parameters.AddWithValue("@user", User);
                cmd2.Parameters.AddWithValue("@user", User);
                cmd3.Parameters.AddWithValue("@user", User);
                cmd4.Parameters.AddWithValue("@user", User);
                cmd5.Parameters.AddWithValue("@user", User);

                con.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Remove Supervisor
        /// </summary> 
        public static void RemoveSupervisor(string Supervisor)
        {
            string queryString =
                "DELETE FROM Categories " +
                "WHERE CreatedBy=@createdby";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@createdby", Supervisor);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public static string UserAssignedTo(string User)
        {
            string queryString =
                "SELECT AssignedSupervisor " +
                "FROM MemberAssignments " +
                "WHERE AssignedUser=@user";
            string Supervisor = "";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);

                con.Open();

                Supervisor = cmd.ExecuteScalar().ToString();

                con.Close();
            }

            return Supervisor;
        }

        public static bool SupervisorHasUsers(String Supervisor)
        {
            bool HasUsers = false;

            string queryString =
                "SELECT COUNT(*) " +
                "FROM MemberAssignments " +
                "WHERE AssignedSupervisor=@supervisor";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

                con.Open();

                HasUsers = ((int)cmd.ExecuteScalar() > 0) ? true : false;

                con.Close();
            }

            return HasUsers;
        }

        public static List<string> UsersAssignedToSupervisor(string Supervisor)
        {
            List<string> UsersAssignedToSupervisor = new List<string>();

            string queryString =
                "SELECT * FROM MemberAssignments " +
                "WHERE AssignedSupervisor=@supervisor";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!UsersAssignedToSupervisor.Contains(dr["AssignedUser"].ToString()))
                    {
                        UsersAssignedToSupervisor.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return UsersAssignedToSupervisor;
        }

        public static List<string> UsersAssignedToCategory(int CategoryID)
        {
            List<string> UsersAssignedToCategory = new List<string>();

            string queryString =
                "SELECT * FROM CategoryAssignments " +
                "WHERE CategoryID=@categoryid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!UsersAssignedToCategory.Contains(dr["AssignedUser"].ToString()))
                    {
                        UsersAssignedToCategory.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return UsersAssignedToCategory;
        }

        public static List<string> UsersAssignedToSupervisorAssignedToCategory(
            string Supervisor, int CategoryID)
        {
            List<string> UsersAssignedToSupervisorAssignedToCategory = new List<string>();

            string queryString =
                "SELECT * FROM MemberAssignments " +
                "INNER JOIN CategoryAssignments ON MemberAssignments.AssignedUser=CategoryAssignments.AssignedUser " +
                "WHERE CategoryAssignments.CategoryID=@categoryid " +
                "AND MemberAssignments.AssignedSupervisor=@assignedsupervisor";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", CategoryID);
                cmd.Parameters.AddWithValue("@assignedsupervisor", Supervisor);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!UsersAssignedToSupervisorAssignedToCategory.Contains(dr["AssignedUser"].ToString()))
                    {
                        UsersAssignedToSupervisorAssignedToCategory.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return UsersAssignedToSupervisorAssignedToCategory;
        }
        public static DataSet CustomGetActiveSupervisor()
        {
            DataSet activeUsers = new DataSet();
            DataTable userTable = new DataTable();
            userTable = activeUsers.Tables.Add("Supervisor");
            userTable.Columns.Add("Username", Type.GetType("System.String"));
            MembershipUserCollection activeUserCollection;
            activeUserCollection = Membership.GetAllUsers();
            foreach (MembershipUser membership in activeUserCollection)
            {
                if (Roles.IsUserInRole(membership.UserName, "Supervisor") && (membership.UserName.ToUpper() != Membership.GetUser().UserName.ToUpper()))
                {
                    DataRow row;
                    row = userTable.NewRow();
                    row["Username"] = "<a class='signalRUser' id= " + membership.UserName + " href='Profile.aspx?userName=" + membership.UserName + "'>" + membership.UserName + "</a>";
                    userTable.Rows.Add(row);
                }
            }
<<<<<<< HEAD
            if (userTable.Columns.Count == 1)
            {
                DataRow row;
                row = userTable.NewRow();
                row["Username"] = "No other supervisors in the system.";
=======
            if (userTable.Rows.Count == 0)
            {
                DataRow row;
                row = userTable.NewRow();
                row["Username"] = "No other supervisors in the system";
>>>>>>> 87aeda0e422fc1d94415677ad98e28f3d1821f66
                userTable.Rows.Add(row);
            }
            return activeUsers;
        }
        public static DataSet CustomGetSupervisorsUsers(string Username)
        {
            DataSet supervisorUsers = new DataSet();
            DataTable users = new DataTable();
            users = supervisorUsers.Tables.Add("Users");
            users.Columns.Add("Users", Type.GetType("System.String"));
            users.Columns.Add("Activity");
            users.Columns.Add("Assigned Categories");
            MembershipUserCollection activeUserCollection;
            activeUserCollection = Membership.GetAllUsers();

            string queryString =
                "SELECT AssignedUser FROM MemberAssignments " +
                "WHERE AssignedSupervisor=@assignedSupervisor";

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
                    row = users.NewRow();
                    row["Users"] = dr["AssignedUser"].ToString();
                    MembershipUser user = Membership.GetUser(dr["AssignedUser"].ToString());

                    List<string> userCategories = Category.GetUsersCategories(Convert.ToString(dr["AssignedUser"]));
                    foreach (string item in userCategories)
                    {
                        row["Assigned Categories"] += item + ",";
                    }
                    users.Rows.Add(row);
                }

                con.Close();
            }


            return supervisorUsers;
        }

    }
}