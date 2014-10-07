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
    public class CompletedTaskController : ApiController
    {
        private dpt_seContext db = new dpt_seContext();

        // GET api/CompletedTask
        public IEnumerable<CompletedTask> GetCompletedTasks()
        {
            var completedtasks = db.CompletedTasks.Include(c => c.MemberAssignment).Include(c => c.Task);
            return completedtasks.AsEnumerable();
        }

        // GET api/CompletedTask/5
        public CompletedTask GetCompletedTask(int id)
        {
            CompletedTask completedtask = db.CompletedTasks.Find(id);
            if (completedtask == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,"Not Found"));
            }

            return completedtask;
        }

        // PUT api/CompletedTask/5
        public HttpResponseMessage PutCompletedTask(int id, CompletedTask completedtask)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != completedtask.TaskID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Bad Request");
            }

            db.Entry(completedtask).State = EntityState.Modified;

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

        // POST api/CompletedTask
        public HttpResponseMessage PostCompletedTask(CompletedTask completedtask)
        {
            if (ModelState.IsValid)
            {
                db.CompletedTasks.Add(completedtask);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, completedtask);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = completedtask.TaskID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CompletedTask/5
        public HttpResponseMessage DeleteCompletedTask(int id)
        {
            CompletedTask completedtask = db.CompletedTasks.Find(id);
            if (completedtask == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Not Found");
            }

            db.CompletedTasks.Remove(completedtask);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, completedtask);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}