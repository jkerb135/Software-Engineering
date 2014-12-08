/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: DetailedStepController.cs
Purpose			: This is the main class file for the WebAPI that pertains to tasks. It handles GET request for all detailed steps and all detailed steps by main step id.
*/
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SE.Models;
using System.Web.Http.Cors;

namespace SE.Controllers
{

    public class OnlyDetailedSteps{
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
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        /// <summary>
        /// Gets all detailed steps from the database.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetAllDetailedSteps()
        {
            return _db.DetailedSteps.Select(tl => new OnlyDetailedSteps {MainStepId = tl.MainStepID, DetailedStepId = tl.DetailedStepID, DetailedStepName = tl.DetailedStepName, DetailedStepText = tl.DetailedStepText, ImagePath = tl.ImagePath, ImageName = tl.ImageFilename }).AsEnumerable();
        }

        /// <summary>
        /// Gets all detailed steps from the database pertaining to a main step id.
        /// </summary>
        public IEnumerable<OnlyDetailedSteps> GetDetailedStepById(int id)
        {
            return _db.DetailedSteps.Where(tl => tl.MainStepID == id).Select(tl => new OnlyDetailedSteps { MainStepId = tl.MainStepID, DetailedStepId = tl.DetailedStepID, DetailedStepName = tl.DetailedStepName, DetailedStepText = tl.DetailedStepText, ImagePath = tl.ImagePath, ImageName = tl.ImageFilename }).AsEnumerable();
        }
    }
}
