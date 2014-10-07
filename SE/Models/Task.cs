using System;
using System.Collections.Generic;

namespace SE.Models
{
    public partial class Task
    {
        public Task()
        {
            this.CompletedMainSteps = new List<CompletedMainStep>();
            this.MainSteps = new List<MainStep>();
        }

        public int TaskID { get; set; }
        public int CategoryID { get; set; }
        public string AssignedUser { get; set; }
        public string TaskName { get; set; }
        public Nullable<double> TaskTime { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<CompletedMainStep> CompletedMainSteps { get; set; }
        public virtual CompletedTask CompletedTask { get; set; }
        public virtual ICollection<MainStep> MainSteps { get; set; }
        public virtual MemberAssignment MemberAssignment { get; set; }
    }
}
