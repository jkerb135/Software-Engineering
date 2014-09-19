﻿using System;
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

                    dt.Rows.Add(dr);
                }
            }
            return ds;
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
                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

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
                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

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
                "DELETE FROM MemberAssignments " +
                "WHERE AssignedUser=@user";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@user", User);

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
    }
}