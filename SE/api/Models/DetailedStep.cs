using System;
using System.Collections.Generic;

namespace iPaws.Models
{
    public partial class DetailedStep
    {
        public int DetailedStepID { get; set; }
        public int MainStepID { get; set; }
        public string DetailedStepName { get; set; }
        public string DetailedStepText { get; set; }
        public string ImageFilename { get; set; }
        public string ImagePath { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public int ListOrder { get; set; }
        public virtual MainStep MainStep { get; set; }
    }
}
