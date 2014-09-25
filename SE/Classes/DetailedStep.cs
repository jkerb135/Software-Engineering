using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class DetailedStep
    {
        #region Properties

        public int DetailedStepID { get; set; }
        public string DetailedStepName { get; set; }
        public string DetailedStepText { get; set; }
        public string ImagePath { get; set; }

        #endregion

        #region Constructors

        public DetailedStep()
        {
            DetailedStepID = 0;
            DetailedStepName = String.Empty;
            DetailedStepText = String.Empty;
            this.ImagePath = String.Empty;
        }

        #endregion

        public void CreateDetailedStep()
        {
        }

        public void UpdateDetailedStep()
        {
        }

        public void DeleteDetailedStep()
        {
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