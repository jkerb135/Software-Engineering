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
    public class OnlyMainSteps
    {
        public int taskId { get; set; }
        public int mainStepId { get; set; }
        public string mainStepName { get; set; }
        public string mainStepText { get; set; }
        public string audioPath { get; set; }
        public string videoPath { get; set; }
    }
    public class MainStepController : ApiController
    {
        iPawsEntities db = new iPawsEntities();
        /// <summary>
        /// Gets all main steps from the database.
        /// </summary>
        public IEnumerable<OnlyMainSteps> GetAllMainSteps()
        {
            return db.MainSteps.Select(tl => new OnlyMainSteps { taskId = tl.TaskID, mainStepId = tl.MainStepID, mainStepName = tl.MainStepName, audioPath = tl.AudioPath, videoPath = tl.VideoPath }).AsEnumerable<OnlyMainSteps>();
        }
        /// <summary>
        /// Gets all main steps from the database pertaining to a task id.
        /// </summary>
        public IEnumerable<OnlyMainSteps> GetMainStepByTaskID(int id)
        {
            return db.MainSteps.Where(tl => tl.TaskID == id).Select(tl => new OnlyMainSteps { taskId = tl.TaskID, mainStepId = tl.MainStepID, mainStepName = tl.MainStepName, audioPath = tl.AudioPath, videoPath = tl.VideoPath }).AsEnumerable<OnlyMainSteps>();
        }
    }
}