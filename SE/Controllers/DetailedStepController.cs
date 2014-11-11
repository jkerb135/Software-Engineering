using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SE.Models;

namespace SE.Controllers
{
    public class OnlyDetailedSteps{
        public int mainStepId { get; set; }
        public string detailedStepName { get; set; }
        public string detailedStepText { get; set; }
        public string imagePath { get; set; }
    }
    public class DetailedStepController : ApiController
    {
        iPawsEntities db = new iPawsEntities();
        /// <summary>
        /// Gets all detailed steps from the database.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetAllDetailedSteps()
        {
            return db.DetailedSteps.Select(tl => new OnlyDetailedSteps {mainStepId = tl.MainStepID, detailedStepName = tl.DetailedStepName, detailedStepText = tl.DetailedStepText, imagePath = tl.ImagePath }).AsEnumerable<OnlyDetailedSteps>();
        }

        /// <summary>
        /// Gets all detailed steps from the database pertaining to a main step id.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetDetailedStepById(int id)
        {
            return db.DetailedSteps.Where(tl => tl.MainStepID == id).Select(tl => new OnlyDetailedSteps { mainStepId = tl.MainStepID, detailedStepName = tl.DetailedStepName, detailedStepText = tl.DetailedStepText, imagePath = tl.ImagePath }).AsEnumerable<OnlyDetailedSteps>();
        }
    }
}
