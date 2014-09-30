﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SE.Classes
{
    [Serializable()]
    public class DetailedStep
    {
        #region Properties

        public int DetailedStepID { get; set; }
        public int MainStepID { get; set; }
        public string DetailedStepName { get; set; }
        public string DetailedStepText { get; set; }
        public string ImageFilename { get; set; }
        public string ImagePath { get; set; }

        #endregion

        #region Constructors

        public DetailedStep()
        {
            this.DetailedStepID = 0;
            this.MainStepID = 0;
            this.DetailedStepName = String.Empty;
            this.DetailedStepText = null;
            this.ImageFilename = null;
            this.ImagePath = null;
        }

        #endregion

        public void CreateDetailedStep()
        {
            string queryString =
                "SELECT MAX(ListOrder) " +
                "AS MaxOf " +
                "FROM DetailedSteps " +
                "WHERE MainStepID=@mainstepid";

            string queryString2 =
                "INSERT INTO DetailedSteps (MainStepID, DetailedStepName, DetailedStepText, ImageFilename, ImagePath, CreatedTime, ListOrder) " +
                "VALUES (@mainstepid, @detailedstepname, @detailedsteptext, @imagefilename, @imagepath, @createdtime, @listorder)";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);

                // Get max number
                cmd.Parameters.AddWithValue("@mainstepid", MainStepID);

                con.Open();

                int MaxNumber = (cmd.ExecuteScalar() != DBNull.Value) ? Convert.ToInt32(cmd.ExecuteScalar()) : 0;

                con.Close();

                // Add Detailed Step
                cmd2.Parameters.AddWithValue("@mainstepid", MainStepID);
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

                cmd2.Parameters.AddWithValue("@listorder", MaxNumber + 1);

                con.Open();

                cmd2.ExecuteNonQuery();

                con.Close();
            }
        }

        public void UpdateDetailedStep()
        {
            string queryString =
                "UPDATE DetailedSteps " +
                "SET DetailedStepName=@detailedstepname " +
                "WHERE DetailedStepID=@detailedstepid";

            string queryString2 =
                "UPDATE DetailedSteps " +
                "SET DetailedStepText=@detailedsteptext " +
                "WHERE DetailedStepID=@DetailedStepID";

            string queryString3 =
                "UPDATE DetailedSteps " +
                "SET ImageFilename=@imagefilename,ImagePath=@imagepath " +
                "WHERE DetailedStepID=@detailedstepid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                SqlCommand cmd3 = new SqlCommand(queryString3, con);

                cmd.Parameters.AddWithValue("@detailedstepid", DetailedStepID);
                cmd.Parameters.AddWithValue("@detailedstepname", DetailedStepName);

                cmd2.Parameters.AddWithValue("@detailedstepid", DetailedStepID);
                cmd2.Parameters.AddWithValue("@detailedsteptext", DetailedStepText);

                cmd3.Parameters.AddWithValue("@detailedstepid", DetailedStepID);
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
            string queryString =
                "DELETE FROM DetailedSteps " +
                "WHERE DetailedStepID=@detailedstepid";

            using (SqlConnection con = new SqlConnection(
                Methods.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@detailedstepid", DetailedStepID);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        public List<DetailedStep> GetDetailedSteps(int StepID)
        {
            List<DetailedStep> DetailedSteps = new List<DetailedStep>();

            return DetailedSteps;
        }

        public int GetNumberOfDetailedStepsUsed()
        {
            int NumberOfDetailedStepsUsed = 0;

            return NumberOfDetailedStepsUsed;
        }
    }
}