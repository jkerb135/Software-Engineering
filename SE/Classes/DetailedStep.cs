using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SE.Classes
{
    [Serializable]
    public class DetailedStep
    {
        #region Properties

        public int DetailedStepId { get; set; }
        public int MainStepId { get; set; }
        public string DetailedStepName { get; set; }
        public string DetailedStepText { get; set; }
        public string ImageFilename { get; set; }
        public string ImagePath { get; set; }
        public string CreatedTime { get; set; }

        #endregion

        #region Constructors

        public DetailedStep()
        {
            DetailedStepId = 0;
            MainStepId = 0;
            DetailedStepName = String.Empty;
            DetailedStepText = null;
            ImageFilename = null;
            ImagePath = null;
        }

        #endregion

        public void CreateDetailedStep()
        {
            const string queryString = "SELECT MAX(ListOrder) " +
                                       "AS MaxOf " +
                                       "FROM DetailedSteps " +
                                       "WHERE MainStepID=@mainstepid";

            const string queryString2 =
                "INSERT INTO DetailedSteps (MainStepID, DetailedStepName, DetailedStepText, ImageFilename, ImagePath, CreatedTime, ListOrder) " +
                "VALUES (@mainstepid, @detailedstepname, @detailedsteptext, @imagefilename, @imagepath, @createdtime, @listorder)";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);

                // Get max number
                cmd.Parameters.AddWithValue("@mainstepid", MainStepId);

                con.Open();

                int maxNumber = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;


                con.Close();

                // Add Detailed Step
                cmd2.Parameters.AddWithValue("@mainstepid", MainStepId);
                cmd2.Parameters.AddWithValue("@detailedstepname", DetailedStepName);

                if (DetailedStepText != null)
                    cmd2.Parameters.AddWithValue("@detailedsteptext", DetailedStepText);
                else
                    cmd2.Parameters.AddWithValue("@detailedsteptext", DBNull.Value);

                if (ImagePath != null)
                {
                    cmd2.Parameters.AddWithValue("@imagefilename", ImageFilename);
                    cmd2.Parameters.AddWithValue("@imagepath", ImagePath);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@imagefilename", DBNull.Value);
                    cmd2.Parameters.AddWithValue("@imagepath", DBNull.Value);
                }

                cmd2.Parameters.AddWithValue("@createdtime", DateTime.Now);

                cmd2.Parameters.AddWithValue("@listorder", maxNumber + 1);

                con.Open();

                cmd2.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateDetailedStep()
        {
            const string queryString = "UPDATE DetailedSteps " +
                                       "SET DetailedStepName=@detailedstepname " +
                                       "WHERE DetailedStepID=@detailedstepid";

            const string queryString2 = "UPDATE DetailedSteps " +
                                        "SET DetailedStepText=@detailedsteptext " +
                                        "WHERE DetailedStepID=@DetailedStepID";

            const string queryString3 = "UPDATE DetailedSteps " +
                                        "SET ImageFilename=@imagefilename,ImagePath=@imagepath " +
                                        "WHERE DetailedStepID=@detailedstepid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@detailedstepid", DetailedStepId);
                cmd.Parameters.AddWithValue("@detailedstepname", DetailedStepName);

                cmd2.Parameters.AddWithValue("@detailedstepid", DetailedStepId);
                cmd2.Parameters.AddWithValue("@detailedsteptext", DetailedStepText);

                cmd3.Parameters.AddWithValue("@detailedstepid", DetailedStepId);
                cmd3.Parameters.AddWithValue("@imagefilename", ImageFilename);
                cmd3.Parameters.AddWithValue("@imagepath", ImagePath);

                con.Open();

                if (!String.IsNullOrEmpty(DetailedStepName))
                    cmd.ExecuteScalar();
                if (!String.IsNullOrEmpty(DetailedStepText))
                    cmd2.ExecuteScalar();
                if (!String.IsNullOrEmpty(ImagePath))
                    cmd3.ExecuteNonQuery();

                con.Close();
            }
        }

        public void DeleteDetailedStep()
        {
            const string queryString = "DELETE FROM DetailedSteps " +
                                       "WHERE DetailedStepID=@detailedstepid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@detailedstepid", DetailedStepId);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public List<DetailedStep> GetDetailedSteps(int stepId)
        {
            var detailedSteps = new List<DetailedStep>();

            return detailedSteps;
        }

        public int GetNumberOfDetailedStepsUsed()
        {
            const int numberOfDetailedStepsUsed = 0;

            return numberOfDetailedStepsUsed;
        }

        public static DetailedStep GetDetailedStep(int detailedStepId)
        {
            var detailedStep = new DetailedStep();

            const string queryString = "SELECT * " +
                                       "FROM DetailedSteps " +
                                       "WHERE DetailedStepID=@DetailedStepID";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@DetailedStepID", detailedStepId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    detailedStep.DetailedStepName = dr["DetailedStepName"].ToString();
                    detailedStep.DetailedStepText = dr["DetailedStepText"].ToString();
                    detailedStep.ImageFilename = dr["ImageFilename"].ToString();
                    detailedStep.ImagePath = dr["ImagePath"].ToString();
                }

                con.Close();
            }

            return detailedStep;
        }
    }
}