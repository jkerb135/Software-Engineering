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
        public int mainStepId { get; set; }
        public string mainStepName { get; set; }
        public string mainStepText { get; set; }
        public string audioPath { get; set; }
        public string videoPath { get; set; }
    }
    public class GetMainStepsController : ApiController
    {
        WebApiEntites db = new WebApiEntites();

        public IEnumerable<OnlyMainSteps> GetAllMainSteps()
        {
            return db.MainSteps.Select(tl => new OnlyMainSteps { mainStepId = tl.MainStepID, mainStepName = tl.MainStepName, audioPath = tl.AudioPath, videoPath = tl.VideoPath }).AsEnumerable<OnlyMainSteps>();
        }

        public IEnumerable<OnlyMainSteps> GetMainStepByID(int id)
        {
            return db.MainSteps.Where(tl => tl.TaskID == id).Select( tl => new OnlyMainSteps{mainStepId = tl.MainStepID, mainStepName = tl.MainStepName, audioPath = tl.AudioPath, videoPath = tl.VideoPath }).AsEnumerable<OnlyMainSteps>();
        }
    }
}
