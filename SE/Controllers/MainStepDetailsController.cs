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
        public string stepName { get; set; }
        public string stepText { get; set; }
        public string imagePath { get; set; }
    }
    public class MainStepDetailsController : ApiController
    {
        dpt_seContext db = new dpt_seContext();

        public IEnumerable<DetailedStep> GetDetailedSteps()
        {
            var detailedsteps = db.DetailedSteps.Include(d => d.MainStep);
            return detailedsteps.AsEnumerable();
        }

        // GET api/MainStep/5
        public IEnumerable<OnlyDetailedSteps> GetDetailedStepInfo(string id)
        {
            var task = db.MainSteps.SingleOrDefault(name => name.MainStepName == id);
            if (task == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "Not Found"));
            }
            return db.DetailedSteps.Where(tl => tl.MainStepID == task.MainStepID).Select(tl => new OnlyDetailedSteps { stepName = tl.DetailedStepName, stepText = tl.DetailedStepText, imagePath = tl.ImagePath }).AsEnumerable<OnlyDetailedSteps>();
        }
    }
}
