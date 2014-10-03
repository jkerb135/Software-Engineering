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
    public class CompletedTaskController : ApiController
    {
        private ipawsTeamBContext db = new ipawsTeamBContext();

        // GET api/CompletedTask
        public IQueryable<CompletedTask> GetCompletedTasks()
        {
            return db.CompletedTasks;
        }

        // GET api/CompletedTask/5
        [ResponseType(typeof(CompletedTask))]
        public async Task<IHttpActionResult> GetCompletedTask(int id)
        {
            CompletedTask completedtask = await db.CompletedTasks.FindAsync(id);
            if (completedtask == null)
            {
                return NotFound();
            }

            return Ok(completedtask);
        }

        // PUT api/CompletedTask/5
        public async Task<IHttpActionResult> PutCompletedTask(int id, CompletedTask completedtask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != completedtask.TaskID)
            {
                return BadRequest();
            }

            db.Entry(completedtask).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletedTaskExists(id))
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

        // POST api/CompletedTask
        [ResponseType(typeof(CompletedTask))]
        public async Task<IHttpActionResult> PostCompletedTask(CompletedTask completedtask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompletedTasks.Add(completedtask);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompletedTaskExists(completedtask.TaskID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = completedtask.TaskID }, completedtask);
        }

        // DELETE api/CompletedTask/5
        [ResponseType(typeof(CompletedTask))]
        public async Task<IHttpActionResult> DeleteCompletedTask(int id)
        {
            CompletedTask completedtask = await db.CompletedTasks.FindAsync(id);
            if (completedtask == null)
            {
                return NotFound();
            }

            db.CompletedTasks.Remove(completedtask);
            await db.SaveChangesAsync();

            return Ok(completedtask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompletedTaskExists(int id)
        {
            return db.CompletedTasks.Count(e => e.TaskID == id) > 0;
        }
    }
}