//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskAssignment
    {
        public int CategoryID { get; set; }
        public int TaskID { get; set; }
        public string AssignedUser { get; set; }
        public double TaskTime { get; set; }
        public int DetailedStepsUsed { get; set; }
        public int id { get; set; }
    }
}
