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
    public class CompletedMainStepController : ApiController
    {
        private ipawsTeamBContext db = new ipawsTeamBContext();

        // GET api/CompletedMainStep
        public IQueryable<CompletedMainStep> GetCompletedMainSteps()
        {
            return db.CompletedMainSteps;
        }

        // GET api/CompletedMainStep/5
        [ResponseType(typeof(CompletedMainStep))]
        public async Task<IHttpActionResult> GetCompletedMainStep(int id)
        {
            CompletedMainStep completedmainstep = await db.CompletedMainSteps.FindAsync(id);
            if (completedmainstep == null)
            {
                return NotFound();
            }

            return Ok(completedmainstep);
        }

        // PUT api/CompletedMainStep/5
        public async Task<IHttpActionResult> PutCompletedMainStep(int id, CompletedMainStep completedmainstep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != completedmainstep.MainStepID)
            {
                return BadRequest();
            }

            db.Entry(completedmainstep).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletedMainStepExists(id))
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

        // POST api/CompletedMainStep
        [ResponseType(typeof(CompletedMainStep))]
        public async Task<IHttpActionResult> PostCompletedMainStep(CompletedMainStep completedmainstep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompletedMainSteps.Add(completedmainstep);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompletedMainStepExists(completedmainstep.MainStepID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = completedmainstep.MainStepID }, completedmainstep);
        }

        // DELETE api/CompletedMainStep/5
        [ResponseType(typeof(CompletedMainStep))]
        public async Task<IHttpActionResult> DeleteCompletedMainStep(int id)
        {
            CompletedMainStep completedmainstep = await db.CompletedMainSteps.FindAsync(id);
            if (completedmainstep == null)
            {
                return NotFound();
            }

            db.CompletedMainSteps.Remove(completedmainstep);
            await db.SaveChangesAsync();

            return Ok(completedmainstep);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompletedMainStepExists(int id)
        {
            return db.CompletedMainSteps.Count(e => e.MainStepID == id) > 0;
        }
    }
}