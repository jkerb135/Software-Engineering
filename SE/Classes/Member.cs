using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace SE.Classes
{
    public static class Member
    {
        public static bool ValidatePassword(string password, ref string errorMessage)
        {

            // Password is less then required length
            if (password.Length < Membership.MinRequiredPasswordLength)
            {
                errorMessage += "Password must be at least " +
                    Membership.MinRequiredPasswordLength + " characters.<br/>";
                return false;
            }

            // Password does not contain minimum special characters
            if (password.Count(c => !char.IsLetterOrDigit(c)) >= Membership.MinRequiredNonAlphanumericCharacters)
                return true;
            errorMessage += "Password must contain at least " +
                            Membership.MinRequiredNonAlphanumericCharacters +
                            " non-alphanumeric characters.<br/>";

            return false;
        }

        /// <summary>Populates a dataset of all users by username, email and user role
        /// </summary> 
        public static DataTable CustomGetAllUsers()
        {

            var dt = new DataTable();

            MembershipUserCollection muc;
            muc = Membership.GetAllUsers();

            dt.Columns.Add("Username", Type.GetType("System.String"));
            dt.Columns.Add("Email", Type.GetType("System.String"));
            dt.Columns.Add("Password", Type.GetType("System.String"));
            dt.Columns.Add("RoleName", Type.GetType("System.String"));
            dt.Columns.Add("LastLoginDate", Type.GetType("System.String"));
            dt.Columns.Add("AssignedSupervisor", Type.GetType("System.String"));
            dt.Columns.Add("IsApproved", Type.GetType("System.String"));

            /* Here is the list of columns returned of the Membership.GetAllUsers() method
             * UserName, Email, PasswordQuestion, Comment, IsApproved
             * IsLockedOut, LastLockoutDate, CreationDate, LastLoginDate
             * LastActivityDate, LastPasswordChangedDate, IsOnline, ProviderName
             */

            foreach (var mu in muc.Cast<MembershipUser>().Where(mu => !Roles.IsUserInRole(mu.UserName, "Manager")))
            {
                var userIsSupervisor = Roles.IsUserInRole(mu.UserName, "Supervisor");

                var dr = dt.NewRow();
                dr["Username"] = mu.UserName;
                dr["Password"] = "";
                dr["Email"] = mu.Email;
                dr["LastLoginDate"] = mu.LastLoginDate;
                dr["RoleName"] = userIsSupervisor ? "Supervisor" : "User";
                dr["AssignedSupervisor"] = !userIsSupervisor ? UserAssignedTo(mu.UserName) : "";
                dr["IsApproved"] = mu.IsApproved;

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataSet CustomGetActiveUsers()
        {
            var activeUsers = new DataSet();
            var userTable = activeUsers.Tables.Add("Users");
            userTable.Columns.Add("Username", Type.GetType("System.String"));
            var activeUserCollection = Membership.GetAllUsers();
            DataRow row;
            foreach (var membership in from MembershipUser membership in activeUserCollection let membershipUser = Membership.GetUser() where membershipUser != null && (Roles.IsUserInRole(membership.UserName, "User") && (UserAssignedTo(membership.UserName).ToLower() == membershipUser.UserName.ToLower()) && (membership.IsOnline)) select membership)
            {
                row = userTable.NewRow();
                row["Username"] = membership.UserName;
                userTable.Rows.Add(row);
            }
            if (userTable.Rows.Count != 0) return activeUsers;
            row = userTable.NewRow();
            row["Username"] = "No Users Online";
            userTable.Rows.Add(row);
            return activeUsers;
        }

        public static DataSet CustomRecentlyAssigned()
        {
            var recentUsers = new DataSet();
            var users = recentUsers.Tables.Add("Users");
            if (users == null) return recentUsers;
            users.Columns.Add("Username", Type.GetType("System.String"));
            var recentUser = Membership.GetAllUsers();
            var aWeekAgo = DateTime.Now.AddDays(-7);
            foreach (var membership in from MembershipUser membership in recentUser let membershipUser = Membership.GetUser() where membershipUser != null && (Roles.IsUserInRole(membership.UserName, "User") && (membership.CreationDate >= aWeekAgo) && UserAssignedTo(membership.UserName).ToLower() == membershipUser.UserName.ToLower()) select membership)
            {
                var row = users.NewRow();
                row["Username"] = membership.UserName;
                users.Rows.Add(row);
            }
            if (users.Rows.Count != 0) return recentUsers;
            var newRow = users.NewRow();
            newRow["Username"] = "No new users assigned";
            users.Rows.Add(newRow);

            return recentUsers;
        }

        /// <summary>Assign user to supervisor
        /// </summary> 
        public static void AssignToUser(string user, string supervisor)
        {
            const string queryString = "INSERT INTO MemberAssignments (AssignedUser, AssignedSupervisor) " +
                                       "VALUES (@user,@supervisor)";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", user);

                if (!String.IsNullOrEmpty(supervisor))
                    cmd.Parameters.AddWithValue("@supervisor", supervisor);
                else
                    cmd.Parameters.AddWithValue("@supervisor", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Edit assign user to supervisor
        /// </summary> 
        public static void EditAssignToUser(string user, string supervisor)
        {
            const string queryString = "UPDATE MemberAssignments " +
                                       "SET AssignedSupervisor=@supervisor " +
                                       "WHERE AssignedUser=@user";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", user);

                if (!String.IsNullOrEmpty(supervisor))
                    cmd.Parameters.AddWithValue("@supervisor", supervisor);
                else
                    cmd.Parameters.AddWithValue("@supervisor", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        /// <summary>Remove user assignment row
        /// </summary> 
        public static void RemoveAssignedUser(string user)
        {
            const string queryString = "DELETE FROM CategoryAssignments " +
                                       "WHERE AssignedUser=@user";

            const string queryString2 = "UPDATE TasksAssignements " +
                                        "SET AssignedUser=NULL " +
                                        "WHERE AssignedUser=@user";

            const string queryString3 = "DELETE FROM CompletedMainSteps " +
                                        "WHERE AssignedUser=@user";

            const string queryString4 = "DELETE FROM CompletedTasks " +
                                        "WHERE AssignedUser=@user";

            const string queryString5 = "UPDATE MemberAssignments " +
                                        "SET AssignedSupervisor=NULL " +
                                        "WHERE AssignedUser=@user";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);
                var cmd4 = new SqlCommand(queryString4, con);
                var cmd5 = new SqlCommand(queryString5, con);

                cmd.Parameters.AddWithValue("@user", user);
                cmd2.Parameters.AddWithValue("@user", user);
                cmd3.Parameters.AddWithValue("@user", user);
                cmd4.Parameters.AddWithValue("@user", user);
                cmd5.Parameters.AddWithValue("@user", user);

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
        public static void RemoveSupervisor(string supervisor)
        {
            const string queryString = "DELETE FROM Categories " +
                                       "WHERE CreatedBy=@createdby";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@createdby", supervisor);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public static string UserAssignedTo(string user)
        {
            const string queryString = "SELECT AssignedSupervisor " +
                                       "FROM MemberAssignments " +
                                       "WHERE AssignedUser=@user";
            string supervisor;

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", user);

                con.Open();
                try
                {
                    supervisor = cmd.ExecuteScalar().ToString();
                }
                catch
                {
                    supervisor = "Unassigned";
                }

                con.Close();
            }

            return supervisor;
        }

        public static bool SupervisorHasUsers(String supervisor)
        {
            bool hasUsers;

            const string queryString = "SELECT COUNT(*) " +
                                       "FROM MemberAssignments " +
                                       "WHERE AssignedSupervisor=@supervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@supervisor", supervisor);

                con.Open();

                hasUsers = ((int)cmd.ExecuteScalar() > 0);

                con.Close();
            }

            return hasUsers;
        }

        public static List<string> UsersAssignedToSupervisor(string supervisor)
        {
            var usersAssignedToSupervisor = new List<string>();

            const string queryString = "SELECT * FROM MemberAssignments " +
                                       "WHERE AssignedSupervisor=@supervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@supervisor", supervisor);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!usersAssignedToSupervisor.Contains(dr["AssignedUser"].ToString()))
                    {
                        usersAssignedToSupervisor.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return usersAssignedToSupervisor;
        }

        public static List<string> UsersAssignedToCategory(int categoryId)
        {
            var usersAssignedToCategory = new List<string>();

            const string queryString = "SELECT * FROM CategoryAssignments " +
                                       "WHERE CategoryID=@categoryid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", categoryId);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!usersAssignedToCategory.Contains(dr["AssignedUser"].ToString()))
                    {
                        usersAssignedToCategory.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return usersAssignedToCategory;
        }

        public static List<string> UsersAssignedToSupervisorAssignedToCategory(
            string supervisor, int categoryId)
        {
            var usersAssignedToSupervisorAssignedToCategory = new List<string>();

            const string queryString = "SELECT * FROM MemberAssignments " +
                                       "INNER JOIN CategoryAssignments ON MemberAssignments.AssignedUser=CategoryAssignments.AssignedUser " +
                                       "WHERE CategoryAssignments.CategoryID=@categoryid " +
                                       "AND MemberAssignments.AssignedSupervisor=@assignedsupervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@categoryid", categoryId);
                cmd.Parameters.AddWithValue("@assignedsupervisor", supervisor);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!usersAssignedToSupervisorAssignedToCategory.Contains(dr["AssignedUser"].ToString()))
                    {
                        usersAssignedToSupervisorAssignedToCategory.Add(dr["AssignedUser"].ToString());
                    }
                }

                con.Close();
            }

            return usersAssignedToSupervisorAssignedToCategory;
        }
        public static DataSet CustomGetActiveSupervisor()
        {
            var activeUsers = new DataSet();
            var userTable = activeUsers.Tables.Add("Supervisor");
            userTable.Columns.Add("Username", Type.GetType("System.String"));
            var activeUserCollection = Membership.GetAllUsers();
            DataRow row;
            foreach (var membership in activeUserCollection.Cast<MembershipUser>().Where(membership =>
            {
                var membershipUser = Membership.GetUser();
                return membershipUser != null && (Roles.IsUserInRole(membership.UserName, "Supervisor") && (!String.Equals(membership.UserName, membershipUser.UserName, StringComparison.CurrentCultureIgnoreCase)));
            }))
            {
                row = userTable.NewRow();
                row["Username"] = "<a class='signalRUser' id= " + membership.UserName + " href='Profile.aspx?userName=" + membership.UserName + "'>" + membership.UserName + "</a>";
                userTable.Rows.Add(row);
            }
            if (userTable.Columns.Count != 1) return activeUsers;
            row = userTable.NewRow();
            row["Username"] = "No other supervisors in the system.";
            if (userTable.Rows.Count != 0) return activeUsers;
            row = userTable.NewRow();
            row["Username"] = "No other supervisors in the system";
            userTable.Rows.Add(row);
            return activeUsers;
        }
        public static DataSet CustomGetSupervisorsUsers(string username)
        {
            var supervisorUsers = new DataSet();
            var users = supervisorUsers.Tables.Add("Users");
            users.Columns.Add("Users", Type.GetType("System.String"));
            users.Columns.Add("Activity");
            users.Columns.Add("Assigned Categories");
            Membership.GetAllUsers();

            const string queryString = "SELECT AssignedUser FROM MemberAssignments " +
                                       "WHERE AssignedSupervisor=@assignedSupervisor";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@assignedSupervisor", username);

                con.Open();

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var row = users.NewRow();
                    row["Users"] = dr["AssignedUser"].ToString();

                    var userCategories = Category.GetUsersCategories(Convert.ToString(dr["AssignedUser"]));
                    foreach (var item in userCategories)
                    {
                        row["Assigned Categories"] += item + ",";
                    }
                    users.Rows.Add(row);
                }

                con.Close();
            }


            return supervisorUsers;
        }

        public static DataTable GetAllSupervisors()
        {
            var dt = new DataTable();

            var muc = Membership.GetAllUsers();

            dt.Columns.Add("Username");
            foreach (var mu in muc.Cast<MembershipUser>().Where(mu => !Roles.IsUserInRole(mu.UserName, "Manager")))
            {
                if (!Roles.IsUserInRole(mu.UserName, "Supervisor")) continue;
                var row = dt.NewRow();
                row["Username"] = mu.UserName;
                dt.Rows.Add(row);
            }

            return dt;
        } 

    }
}