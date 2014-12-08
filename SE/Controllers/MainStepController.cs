/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: MainStepController.cs
Purpose			: This is the main class file for the WebAPI that pertains to mainsteps. It handles GET request for all mainsteps and all mainsteps by task id.
*/
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SE.Models;
using System.Web.Http.Cors;

namespace SE.Controllers
{
    
    public class OnlyMainSteps
    {
        public int TaskId { get; set; }
        public int MainStepId { get; set; }
        public string MainStepName { get; set; }
        public string MainStepText { get; set; }
        public string AudioPath { get; set; }
        public string VideoPath { get; set; }
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MainStepController : ApiController
    {
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        /// <summary>
        /// Gets all main steps from the database.
        /// </summary>
        public IEnumerable<OnlyMainSteps> GetAllMainSteps()
        {
            return _db.MainSteps.Select(tl => new OnlyMainSteps { TaskId = tl.TaskID, MainStepId = tl.MainStepID, MainStepName = tl.MainStepName, AudioPath = tl.AudioPath, VideoPath = tl.VideoPath }).AsEnumerable();
        }
        /// <summary>
        /// Gets all main steps from the database pertaining to a task id.
        /// </summary>
        public IEnumerable<OnlyMainSteps> GetMainStepByTaskId(int id)
        {
            return _db.MainSteps.Where(tl => tl.TaskID == id).Select(tl => new OnlyMainSteps { TaskId = tl.TaskID, MainStepId = tl.MainStepID, MainStepName = tl.MainStepName, AudioPath = tl.AudioPath, VideoPath = tl.VideoPath }).AsEnumerable();
        }
    }
}