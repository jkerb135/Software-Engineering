using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class Report
    {
        #region Properties

        public int ReportID { get; set; }

        #endregion

        #region Constructors

        public Report()
        {
            this.ReportID = 0;
        }

        #endregion

        public void GenerateReport()
        {

        }
    }
}