using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SE.Models;

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