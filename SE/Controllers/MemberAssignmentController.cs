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
    public class MemberAssignmentController : ApiController
    {
        private dpt_seContext db = new dpt_seContext();

        // GET api/MemberAssignment
        public IEnumerable<MemberAssignment> GetMemberAssignments()
        {
            return db.MemberAssignments.AsEnumerable();
        }

        // GET api/MemberAssignment/5
        public MemberAssignment GetMemberAssignment(string id)
        {
            MemberAssignment memberassignment = db.MemberAssignments.Find(id);
            if (memberassignment == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "Not Found"));
            }

            return memberassignment;
        }

        // PUT api/MemberAssignment/5
        public HttpResponseMessage PutMemberAssignment(string id, MemberAssignment memberassignment)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != memberassignment.AssignedUser)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Bad Request");
            }

            db.Entry(memberassignment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK,"OK");
        }

        // POST api/MemberAssignment
        public HttpResponseMessage PostMemberAssignment(MemberAssignment memberassignment)
        {
            if (ModelState.IsValid)
            {
                db.MemberAssignments.Add(memberassignment);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, memberassignment);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = memberassignment.AssignedUser }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/MemberAssignment/5
        public HttpResponseMessage DeleteMemberAssignment(string id)
        {
            MemberAssignment memberassignment = db.MemberAssignments.Find(id);
            if (memberassignment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"null");
            }

            db.MemberAssignments.Remove(memberassignment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, memberassignment);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}