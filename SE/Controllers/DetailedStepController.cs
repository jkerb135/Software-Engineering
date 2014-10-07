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
    public class DetailedStepController : ApiController
    {
        private dpt_seContext db = new dpt_seContext();

        // GET api/DetailedStep
        public IEnumerable<DetailedStep> GetDetailedSteps()
        {
            var detailedsteps = db.DetailedSteps.Include(d => d.MainStep);
            return detailedsteps.AsEnumerable();
        }

        // GET api/DetailedStep/5
        public DetailedStep GetDetailedStep(int id)
        {
            DetailedStep detailedstep = db.DetailedSteps.Find(id);
            if (detailedstep == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,"Not Found"));
            }

            return detailedstep;
        }

        // PUT api/DetailedStep/5
        public HttpResponseMessage PutDetailedStep(int id, DetailedStep detailedstep)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != detailedstep.DetailedStepID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Bad Request");
            }

            db.Entry(detailedstep).State = EntityState.Modified;

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

        // POST api/DetailedStep
        public HttpResponseMessage PostDetailedStep(DetailedStep detailedstep)
        {
            if (ModelState.IsValid)
            {
                db.DetailedSteps.Add(detailedstep);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, detailedstep);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = detailedstep.DetailedStepID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/DetailedStep/5
        public HttpResponseMessage DeleteDetailedStep(int id)
        {
            DetailedStep detailedstep = db.DetailedSteps.Find(id);
            if (detailedstep == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Not Found");
            }

            db.DetailedSteps.Remove(detailedstep);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, detailedstep);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}