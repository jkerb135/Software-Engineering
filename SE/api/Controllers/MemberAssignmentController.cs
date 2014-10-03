using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using iPaws.Models;

namespace iPaws.Controllers
{
    public class MemberAssignmentController : ApiController
    {
        private ipawsTeamBContext db = new ipawsTeamBContext();

        // GET api/MemberAssignment
        public IQueryable<MemberAssignment> GetMemberAssignments()
        {
            return db.MemberAssignments;
        }

        // GET api/MemberAssignment/5
        [ResponseType(typeof(MemberAssignment))]
        public async Task<IHttpActionResult> GetMemberAssignment(string id)
        {
            MemberAssignment memberassignment = await db.MemberAssignments.FindAsync(id);
            if (memberassignment == null)
            {
                return NotFound();
            }

            return Ok(memberassignment);
        }

        // PUT api/MemberAssignment/5
        public async Task<IHttpActionResult> PutMemberAssignment(string id, MemberAssignment memberassignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != memberassignment.AssignedUser)
            {
                return BadRequest();
            }

            db.Entry(memberassignment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberAssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/MemberAssignment
        [ResponseType(typeof(MemberAssignment))]
        public async Task<IHttpActionResult> PostMemberAssignment(MemberAssignment memberassignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MemberAssignments.Add(memberassignment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberAssignmentExists(memberassignment.AssignedUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = memberassignment.AssignedUser }, memberassignment);
        }

        // DELETE api/MemberAssignment/5
        [ResponseType(typeof(MemberAssignment))]
        public async Task<IHttpActionResult> DeleteMemberAssignment(string id)
        {
            MemberAssignment memberassignment = await db.MemberAssignments.FindAsync(id);
            if (memberassignment == null)
            {
                return NotFound();
            }

            db.MemberAssignments.Remove(memberassignment);
            await db.SaveChangesAsync();

            return Ok(memberassignment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberAssignmentExists(string id)
        {
            return db.MemberAssignments.Count(e => e.AssignedUser == id) > 0;
        }
    }
}