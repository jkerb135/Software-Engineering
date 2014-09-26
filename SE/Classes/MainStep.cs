using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SE.Classes
{
    [Serializable()]
    public class MainStep
    {
        #region Properties

        public int MainStepID { get; set; }
        public int TaskID { get; set; }
        public string MainStepName { get; set; }
        public string MainStepText { get; set; }
        public double MainStepTime { get; set; }
        public string AudioFilename { get; set; }
        public string AudioPath { get; set; }
        public string VideoFilename { get; set; }
        public string VideoPath { get; set; }

        #endregion

        #region Constructors

        public MainStep()
        {
            this.MainStepID = 0;
            this.TaskID = 0;
            this.MainStepName = String.Empty;
            this.MainStepText = null;
            this.MainStepTime = 0;
            this.AudioFilename = null;
            this.AudioPath = null;
            this.VideoFilename = null;
            this.VideoPath = null;
        }

        #endregion

        public void CreateMainStep()
        {
            string queryString =
                "SELECT MAX(ListOrder) " +
                "AS MaxOf " +
                "FROM MainSteps " +
                "WHERE TaskID=@taskid";

            string queryString2 =
                "INSERT INTO MainSteps (TaskID, MainStepName, MainStepText, AudioFileName, AudioPath, VideoFilename, VideoPath, CreatedTime, ListOrder) " +
                "VALUES (@taskid, @mainstepname, @mainsteptext, @audiofilename, @audiopath, @videofilename, @videopath, @createdtime, @listorder)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);

                // Get max number
                cmd.Parameters.AddWithValue("@taskid", TaskID);

                con.Open();

                int MaxNumber = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                con.Close();

                // Add Main Step
                cmd2.Parameters.AddWithValue("@taskid", TaskID);
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

                cmd2.Parameters.AddWithValue("@listorder", MaxNumber+1);

                con.Open();

                cmd2.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateMainStep()
        {
            string queryString =
                "UPDATE MainSteps " +
                "SET MainStepName=@mainstepname " +
                "WHERE MainStepID=@mainstepid";

            string queryString2 =
                "UPDATE MainSteps " +
                "SET MainStepText=@mainsteptext " +
                "WHERE MainStepID=@MainStepID";

            string queryString3 =
                "UPDATE MainSteps " +
                "SET AudioFilename=@audiofilename,AudioPath=@audiopath " +
                "WHERE MainStepID=@mainstepid";

            string queryString4 =
                "UPDATE MainSteps " +
                "SET VideoFilename=@videofilename,VideoPath=@videopath " +
                "WHERE MainStepID=@mainstepid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                SqlCommand cmd4 = new SqlCommand(queryString4, con);

                cmd.Parameters.AddWithValue("@mainstepid", MainStepID);
                cmd.Parameters.AddWithValue("@mainstepname", MainStepName);

                cmd2.Parameters.AddWithValue("@mainstepid", MainStepID);
                cmd2.Parameters.AddWithValue("@mainsteptext", MainStepText);

                cmd3.Parameters.AddWithValue("@mainstepid", MainStepID);
                cmd3.Parameters.AddWithValue("@audiofilename", AudioFilename);
                cmd3.Parameters.AddWithValue("@audiopath", AudioPath);

                cmd4.Parameters.AddWithValue("@mainstepid", MainStepID);
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
            string queryString =
                "DELETE FROM DetailedSteps " +
                "WHERE MainStepID=@mainstepid";

            string queryString2 =
                "DELETE FROM CompletedMainSteps " +
                "WHERE MainStepID=@mainstepid";

            string queryString3 =
                "DELETE FROM MainSteps " +
                "WHERE MainStepID=@mainstepid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                SqlCommand cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@mainstepid", MainStepID);
                cmd2.Parameters.AddWithValue("@mainstepid", MainStepID);
                cmd3.Parameters.AddWithValue("@mainstepid", MainStepID);

                con.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();

                con.Close();
            }
        }

        public void CompleteMainStep()
        {

        }

        public List<MainStep> GetMainSteps(int TaskID)
        {
            List<MainStep> MainSteps = new List<MainStep>();

            return MainSteps;
        }

        public void AddTimeToMainStep(double Minutes)
        {

        }

        public int GetNumberOfMainStepsComplete(int TaskID, string Username)
        {
            int NumberOfMainStepsComplete = 0;

            return NumberOfMainStepsComplete;
        }
    }
}