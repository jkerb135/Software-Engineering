using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class Step
    {
        #region Properties

        public int StepID { get; set; }
        public string StepName { get; set; }
        public string StepText { get; set; }

        #endregion

        #region Constructors

        public Step()
        {
            this.StepID = 0;
            this.StepName = String.Empty;
            this.StepText = String.Empty;
        }

        #endregion

        public void CreateStep()
        {

        }

        public void UpdateStep()
        {

        }

        public void DeleteStep()
        {

        }
    }
}