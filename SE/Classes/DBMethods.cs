using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace SE.Classes
{
    public partial class DBMethods
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
        }

        public static string UserAssignedTo(string User)
        {
            string queryString =
                "SELECT AssignedSupervisor " +
                "FROM MemberAssignments " +
                "WHERE AssignedUser=@user";
            string Supervisor = "";

            using (SqlConnection con = new SqlConnection(
                DBMethods.GetConnectionString()))
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
                DBMethods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@supervisor", Supervisor);

                con.Open();

                HasUsers = ((int)cmd.ExecuteScalar() > 0) ? true : false;

                con.Close();
            }

            return HasUsers;
        }
    }
}