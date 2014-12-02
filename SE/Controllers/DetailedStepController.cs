using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using SE.Models;

namespace SE.Controllers
{
    public class OnlyDetailedSteps
    {
        public int MainStepId { get; set; }
        public int DetailedStepId { get; set; }
        public string DetailedStepName { get; set; }
        public string DetailedStepText { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DetailedStepController : ApiController
    {
        private readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();

        /// <summary>
        ///     Gets all detailed steps from the database.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetAllDetailedSteps()
        {
            return
                _db.DetailedSteps.Select(
                    tl =>
                        new OnlyDetailedSteps
                        {
                            MainStepId = tl.MainStepID,
                            DetailedStepId = tl.DetailedStepID,
                            DetailedStepName = tl.DetailedStepName,
                            DetailedStepText = tl.DetailedStepText,
                            ImagePath = tl.ImagePath,
                            ImageName = tl.ImageFilename
                        }).AsEnumerable();
        }

        /// <summary>
        ///     Gets all detailed steps from the database pertaining to a main step id.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetDetailedStepById(int id)
        {
            return
                _db.DetailedSteps.Where(tl => tl.MainStepID == id)
                    .Select(
                        tl =>
                            new OnlyDetailedSteps
                            {
                                MainStepId = tl.MainStepID,
                                DetailedStepId = tl.DetailedStepID,
                                DetailedStepName = tl.DetailedStepName,
                                DetailedStepText = tl.DetailedStepText,
                                ImagePath = tl.ImagePath,
                                ImageName = tl.ImageFilename
                            })
                    .AsEnumerable();
        }
    }
}