using System;
using System.Collections.Generic;

namespace SE.Models
{
    public partial class CompletedMainStep
    {
        public int MainStepID { get; set; }
        public int TaskID { get; set; }
        public string MainStepName { get; set; }
        public string AssignedUser { get; set; }
        public System.DateTime DateTimeComplete { get; set; }
        public double TotalTime { get; set; }
        public virtual MainStep MainStep { get; set; }
        public virtual MemberAssignment MemberAssignment { get; set; }
        public virtual Task Task { get; set; }
    }
}
