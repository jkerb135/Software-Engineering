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
    
    public partial class Category
    {
        public Category()
        {
            this.RequestedCategories = new HashSet<RequestedCategory>();
            this.Tasks = new HashSet<Task>();
        }
    
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
    
        public virtual CategoryAssignment CategoryAssignment { get; set; }
        public virtual ICollection<RequestedCategory> RequestedCategories { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
