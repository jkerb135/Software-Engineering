using System;
using System.Collections.Generic;

namespace iPaws.Models
{
    public partial class MemberAssignment
    {
        public MemberAssignment()
        {
            this.CompletedMainSteps = new List<CompletedMainStep>();
            this.CompletedTasks = new List<CompletedTask>();
            this.Tasks = new List<Task>();
            this.Categories = new List<Category>();
        }

        public string AssignedUser { get; set; }
        public string AssignedSupervisor { get; set; }
        public virtual ICollection<CompletedMainStep> CompletedMainSteps { get; set; }
        public virtual ICollection<CompletedTask> CompletedTasks { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
