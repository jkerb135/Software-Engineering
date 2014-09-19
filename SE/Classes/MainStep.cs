using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class MainStep : Step
    {
        #region Properties

        public string AudioPath { get; set; }
        public string VideoPath { get; set; }

        #endregion

        #region Constructors

        public MainStep()
        {
            this.AudioPath = String.Empty;
            this.VideoPath = String.Empty;
        }

        #endregion

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