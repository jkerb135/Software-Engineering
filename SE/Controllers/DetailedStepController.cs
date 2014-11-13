using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SE.Models;

namespace SE.Controllers
{
    public class OnlyDetailedSteps{
        public int MainStepId { get; set; }
        public string DetailedStepName { get; set; }
        public string DetailedStepText { get; set; }
        public string ImagePath { get; set; }
    }
    public class DetailedStepController : ApiController
    {
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        /// <summary>
        /// Gets all detailed steps from the database.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetAllDetailedSteps()
        {
            return _db.DetailedSteps.Select(tl => new OnlyDetailedSteps {MainStepId = tl.MainStepID, DetailedStepName = tl.DetailedStepName, DetailedStepText = tl.DetailedStepText, ImagePath = tl.ImagePath }).AsEnumerable();
        }

        /// <summary>
        /// Gets all detailed steps from the database pertaining to a main step id.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetDetailedStepById(int id)
        {
            return _db.DetailedSteps.Where(tl => tl.MainStepID == id).Select(tl => new OnlyDetailedSteps { MainStepId = tl.MainStepID, DetailedStepName = tl.DetailedStepName, DetailedStepText = tl.DetailedStepText, ImagePath = tl.ImagePath }).AsEnumerable();
        }
    }
}
