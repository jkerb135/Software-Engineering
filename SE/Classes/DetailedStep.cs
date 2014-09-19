using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class DetailedStep : Step
    {
        #region Properties

        public string ImagePath { get; set; }

        #endregion

        #region Constructors

        public DetailedStep()
        {
            this.ImagePath = String.Empty;
        }

        #endregion

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