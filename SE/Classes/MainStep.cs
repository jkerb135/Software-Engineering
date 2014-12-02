using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SE.Classes
{
    [Serializable]
    public class MainStep
    {
        #region Properties

        public int MainStepId { get; set; }
        public int TaskId { get; set; }
        public string MainStepName { get; set; }
        public string MainStepText { get; set; }
        public double MainStepTime { get; set; }
        public string AudioFilename { get; set; }
        public string AudioPath { get; set; }
        public string VideoFilename { get; set; }
        public string VideoPath { get; set; }
        public string CreatedTime { get; set; }

        #endregion

        #region Constructors

        public MainStep()
        {
            MainStepId = 0;
            TaskId = 0;
            MainStepName = String.Empty;
            MainStepText = null;
            MainStepTime = 0;
            AudioFilename = null;
            AudioPath = null;
            VideoFilename = null;
            VideoPath = null;
        }

        #endregion

        public void CreateMainStep()
        {
            const string queryString = "SELECT MAX(ListOrder) " +
                                       "AS MaxOf " +
                                       "FROM MainSteps " +
                                       "WHERE TaskID=@taskid";

            const string queryString2 =
                "INSERT INTO MainSteps (TaskID, MainStepName, MainStepText, AudioFileName, AudioPath, VideoFilename, VideoPath, CreatedTime, ListOrder) " +
                "VALUES (@taskid, @mainstepname, @mainsteptext, @audiofilename, @audiopath, @videofilename, @videopath, @createdtime, @listorder)";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);

                // Get max number
                cmd.Parameters.AddWithValue("@taskid", TaskId);

                con.Open();

                int maxNumber = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                con.Close();

                // Add Main Step
                cmd2.Parameters.AddWithValue("@taskid", TaskId);
                cmd2.Parameters.AddWithValue("@mainstepname", MainStepName);

                if (MainStepText != null)
                    cmd2.Parameters.AddWithValue("@mainsteptext", MainStepText);
                else
                    cmd2.Parameters.AddWithValue("@mainsteptext", DBNull.Value);

                if (AudioPath != null)
                {
                    cmd2.Parameters.AddWithValue("@audiofilename", AudioFilename);
                    cmd2.Parameters.AddWithValue("@audiopath", AudioPath);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@audiofilename", DBNull.Value);
                    cmd2.Parameters.AddWithValue("@audiopath", DBNull.Value);
                }

                if (VideoPath != null)
                {
                    cmd2.Parameters.AddWithValue("@videofilename", VideoFilename);
                    cmd2.Parameters.AddWithValue("@videopath", VideoPath);
                }
                else
                {
                    cmd2.Parameters.AddWithValue("@videofilename", DBNull.Value);
                    cmd2.Parameters.AddWithValue("@videopath", DBNull.Value);
                }

                cmd2.Parameters.AddWithValue("@createdtime", DateTime.Now);

                cmd2.Parameters.AddWithValue("@listorder", maxNumber + 1);

                con.Open();

                cmd2.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateMainStep()
        {
            const string queryString = "UPDATE MainSteps " +
                                       "SET MainStepName=@mainstepname " +
                                       "WHERE MainStepID=@mainstepid";

            const string queryString2 = "UPDATE MainSteps " +
                                        "SET MainStepText=@mainsteptext " +
                                        "WHERE MainStepID=@MainStepID";

            const string queryString3 = "UPDATE MainSteps " +
                                        "SET AudioFilename=@audiofilename,AudioPath=@audiopath " +
                                        "WHERE MainStepID=@mainstepid";

            const string queryString4 = "UPDATE MainSteps " +
                                        "SET VideoFilename=@videofilename,VideoPath=@videopath " +
                                        "WHERE MainStepID=@mainstepid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);
                var cmd2 = new SqlCommand(queryString2, con);
                var cmd3 = new SqlCommand(queryString3, con);
                var cmd4 = new SqlCommand(queryString4, con);

                cmd.Parameters.AddWithValue("@mainstepid", MainStepId);
                cmd.Parameters.AddWithValue("@mainstepname", MainStepName);

                cmd2.Parameters.AddWithValue("@mainstepid", MainStepId);
                cmd2.Parameters.AddWithValue("@mainsteptext", MainStepText);

                cmd3.Parameters.AddWithValue("@mainstepid", MainStepId);
                cmd3.Parameters.AddWithValue("@audiofilename", AudioFilename);
                cmd3.Parameters.AddWithValue("@audiopath", AudioPath);

                cmd4.Parameters.AddWithValue("@mainstepid", MainStepId);
                cmd4.Parameters.AddWithValue("@videofilename", VideoFilename);
                cmd4.Parameters.AddWithValue("@videopath", VideoPath);

                con.Open();

                if (!String.IsNullOrEmpty(MainStepName))
                    cmd.ExecuteScalar();
                if (!String.IsNullOrEmpty(MainStepText))
                    cmd2.ExecuteScalar();
                if (!String.IsNullOrEmpty(AudioPath))
                    cmd3.ExecuteNonQuery();
                if (!String.IsNullOrEmpty(VideoPath))
                    cmd4.ExecuteNonQuery();

                con.Close();
            }
        }

        public void DeleteMainStep()
        {
            const string queryString = "DELETE FROM MainSteps " +
                                       "WHERE MainStepID=@mainstepid";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@mainstepid", MainStepId);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public void CompleteMainStep()
        {
        }

        public List<MainStep> GetMainSteps(int taskId)
        {
            var mainSteps = new List<MainStep>();

            return mainSteps;
        }

        public void AddTimeToMainStep(double minutes)
        {
        }

        public int GetNumberOfMainStepsComplete(int taskId, string username)
        {
            const int numberOfMainStepsComplete = 0;

            return numberOfMainStepsComplete;
        }

        public static MainStep GetMainStep(int mainStepId)
        {
            var mainStep = new MainStep();

            const string queryString = "SELECT * " +
                                       "FROM MainSteps " +
                                       "WHERE MainStepID=@MainStepID";

            using (var con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                var cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@MainStepID", mainStepId);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mainStep.MainStepName = dr["MainStepName"].ToString();
                    mainStep.MainStepText = dr["MainStepText"].ToString();
                    mainStep.AudioFilename = dr["AudioFilename"].ToString();
                    mainStep.AudioPath = dr["AudioPath"].ToString();
                    mainStep.VideoFilename = dr["VideoFilename"].ToString();
                    mainStep.VideoPath = dr["VideoPath"].ToString();
                }

                con.Close();
            }

            return mainStep;
        }
    }
}