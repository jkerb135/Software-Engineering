//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public Task()
        {
            this.MainSteps = new HashSet<MainStep>();
            this.CompletedMainSteps = new HashSet<CompletedMainStep>();
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
        public virtual CompletedTask CompletedTask { get; set; }
        public virtual ICollection<MainStep> MainSteps { get; set; }
        public virtual MemberAssignment MemberAssignment { get; set; }
        public virtual ICollection<CompletedMainStep> CompletedMainSteps { get; set; }
    }
}
