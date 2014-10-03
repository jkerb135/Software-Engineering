using System;
using System.Collections.Generic;

namespace iPaws.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Tasks = new List<Task>();
            this.MemberAssignments = new List<MemberAssignment>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<MemberAssignment> MemberAssignments { get; set; }
    }
}
