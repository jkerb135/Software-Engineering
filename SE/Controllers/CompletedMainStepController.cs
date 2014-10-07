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
    public class CompletedMainStepController : ApiController
    {
        private dpt_seContext db = new dpt_seContext();

        // GET api/CompletedMainStep
        public IEnumerable<CompletedMainStep> GetCompletedMainSteps()
        {
            var completedmainsteps = db.CompletedMainSteps.Include(c => c.MainStep).Include(c => c.MemberAssignment).Include(c => c.Task);
            return completedmainsteps.AsEnumerable();
        }

        // GET api/CompletedMainStep/5
        public CompletedMainStep GetCompletedMainStep(int id)
        {
            CompletedMainStep completedmainstep = db.CompletedMainSteps.Find(id);
            if (completedmainstep == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,"Not Found"));
            }

            return completedmainstep;
        }

        // PUT api/CompletedMainStep/5
        public HttpResponseMessage PutCompletedMainStep(int id, CompletedMainStep completedmainstep)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != completedmainstep.MainStepID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Bad Request");
            }

            db.Entry(completedmainstep).State = EntityState.Modified;

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

        // POST api/CompletedMainStep
        public HttpResponseMessage PostCompletedMainStep(CompletedMainStep completedmainstep)
        {
            if (ModelState.IsValid)
            {
                db.CompletedMainSteps.Add(completedmainstep);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, completedmainstep);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = completedmainstep.MainStepID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CompletedMainStep/5
        public HttpResponseMessage DeleteCompletedMainStep(int id)
        {
            CompletedMainStep completedmainstep = db.CompletedMainSteps.Find(id);
            if (completedmainstep == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Not Found");
            }

            db.CompletedMainSteps.Remove(completedmainstep);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, completedmainstep);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}