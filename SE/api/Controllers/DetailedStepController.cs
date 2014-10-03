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
    public class DetailedStepController : ApiController
    {
        private ipawsTeamBContext db = new ipawsTeamBContext();

        // GET api/DetailedStep
        public IQueryable<DetailedStep> GetDetailedSteps()
        {
            return db.DetailedSteps;
        }

        // GET api/DetailedStep/5
        [ResponseType(typeof(DetailedStep))]
        public async Task<IHttpActionResult> GetDetailedStep(int id)
        {
            DetailedStep detailedstep = await db.DetailedSteps.FindAsync(id);
            if (detailedstep == null)
            {
                return NotFound();
            }

            return Ok(detailedstep);
        }

        // PUT api/DetailedStep/5
        public async Task<IHttpActionResult> PutDetailedStep(int id, DetailedStep detailedstep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detailedstep.DetailedStepID)
            {
                return BadRequest();
            }

            db.Entry(detailedstep).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailedStepExists(id))
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

        // POST api/DetailedStep
        [ResponseType(typeof(DetailedStep))]
        public async Task<IHttpActionResult> PostDetailedStep(DetailedStep detailedstep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetailedSteps.Add(detailedstep);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = detailedstep.DetailedStepID }, detailedstep);
        }

        // DELETE api/DetailedStep/5
        [ResponseType(typeof(DetailedStep))]
        public async Task<IHttpActionResult> DeleteDetailedStep(int id)
        {
            DetailedStep detailedstep = await db.DetailedSteps.FindAsync(id);
            if (detailedstep == null)
            {
                return NotFound();
            }

            db.DetailedSteps.Remove(detailedstep);
            await db.SaveChangesAsync();

            return Ok(detailedstep);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetailedStepExists(int id)
        {
            return db.DetailedSteps.Count(e => e.DetailedStepID == id) > 0;
        }
    }
}