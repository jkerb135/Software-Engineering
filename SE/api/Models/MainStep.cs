using System;
using System.Collections.Generic;

namespace iPaws.Models
{
    public partial class MainStep
    {
        public MainStep()
        {
            this.CompletedMainSteps = new List<CompletedMainStep>();
            this.DetailedSteps = new List<DetailedStep>();
        }

        public int MainStepID { get; set; }
        public int TaskID { get; set; }
        public string MainStepName { get; set; }
        public string MainStepText { get; set; }
        public Nullable<double> MainStepTime { get; set; }
        public string AudioFilename { get; set; }
        public string AudioPath { get; set; }
        public string VideoFilename { get; set; }
        public string VideoPath { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public int ListOrder { get; set; }
        public virtual ICollection<CompletedMainStep> CompletedMainSteps { get; set; }
        public virtual ICollection<DetailedStep> DetailedSteps { get; set; }
        public virtual Task Task { get; set; }
    }
}
