using System;
using System.Collections.Generic;

namespace iPaws.Models
{
    public partial class CompletedTask
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string AssignedUser { get; set; }
        public System.DateTime DateTimeCompleted { get; set; }
        public double TotalTime { get; set; }
        public virtual MemberAssignment MemberAssignment { get; set; }
        public virtual Task Task { get; set; }
    }
}
