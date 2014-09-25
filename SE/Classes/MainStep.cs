using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class MainStep
    {
        #region Properties

        public int MainStepID { get; set; }
        public string MainStepName { get; set; }
        public string MainStepText { get; set; }
        public string AudioPath { get; set; }
        public string VideoPath { get; set; }

        #endregion

        #region Constructors

        public MainStep()
        {
            this.MainStepID = 0;
            this.MainStepName = String.Empty;
            this.MainStepText = String.Empty;
            this.AudioPath = String.Empty;
            this.VideoPath = String.Empty;
        }

        #endregion

        public void CreateMainStep()
        {
        }

        public void UpdateMainStep()
        {
        }

        public void DeleteMainStep()
        {
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