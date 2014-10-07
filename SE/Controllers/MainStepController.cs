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
    public class MainStepController : ApiController
    {
        private dpt_seContext db = new dpt_seContext();

        // GET api/MainStep
        public IEnumerable<MainStep> GetMainSteps()
        {
            var mainsteps = db.MainSteps.Include(m => m.Task);
            return mainsteps.AsEnumerable();
        }

        // GET api/MainStep/5
        public MainStep GetMainStep(int id)
        {
            MainStep mainstep = db.MainSteps.Find(id);
            if (mainstep == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,"Not Found"));
            }

            return mainstep;
        }

        // PUT api/MainStep/5
        public HttpResponseMessage PutMainStep(int id, MainStep mainstep)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != mainstep.MainStepID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Bad Request");
            }

            db.Entry(mainstep).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK,"Ok");
        }

        // POST api/MainStep
        public HttpResponseMessage PostMainStep(MainStep mainstep)
        {
            if (ModelState.IsValid)
            {
                db.MainSteps.Add(mainstep);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mainstep);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = mainstep.MainStepID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/MainStep/5
        public HttpResponseMessage DeleteMainStep(int id)
        {
            MainStep mainstep = db.MainSteps.Find(id);
            if (mainstep == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Not Found");
            }

            db.MainSteps.Remove(mainstep);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, mainstep);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}