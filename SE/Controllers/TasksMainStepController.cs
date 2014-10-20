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
        public string mainStepName { get; set; }
        public string mainStepText { get; set; }
        public string audioPath { get; set; }
        public string videoPath { get; set; }
    }
    public class TasksMainStepController : ApiController
    {
        dpt_seContext db = new dpt_seContext();

        public IEnumerable<MainStep> GetMainSteps()
        {
            return db.MainSteps;
        }

        // GET api/MainStep/5
        public IEnumerable<OnlyMainSteps> GetMainStep(string id)
        {
            var task = db.Tasks.SingleOrDefault(name => name.TaskName == id);
            if (task == null){
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,"Not Found"));
            }
            return db.MainSteps.Where(tl => tl.TaskID == task.TaskID).Select( tl => new OnlyMainSteps{ mainStepName = tl.MainStepName, audioPath = tl.AudioPath, videoPath = tl.VideoPath }).AsEnumerable<OnlyMainSteps>();
        }
    }
}
