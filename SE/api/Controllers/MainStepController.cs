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
    public class MainStepController : ApiController
    {
        private ipawsTeamBContext db = new ipawsTeamBContext();

        // GET api/MainStep
        public IQueryable<MainStep> GetMainSteps()
        {
            return db.MainSteps;
        }

        // GET api/MainStep/5
        [ResponseType(typeof(MainStep))]
        public async Task<IHttpActionResult> GetMainStep(int id)
        {
            MainStep mainstep = await db.MainSteps.FindAsync(id);
            if (mainstep == null)
            {
                return NotFound();
            }

            return Ok(mainstep);
        }

        // PUT api/MainStep/5
        public async Task<IHttpActionResult> PutMainStep(int id, MainStep mainstep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainstep.MainStepID)
            {
                return BadRequest();
            }

            db.Entry(mainstep).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainStepExists(id))
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

        // POST api/MainStep
        [ResponseType(typeof(MainStep))]
        public async Task<IHttpActionResult> PostMainStep(MainStep mainstep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MainSteps.Add(mainstep);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mainstep.MainStepID }, mainstep);
        }

        // DELETE api/MainStep/5
        [ResponseType(typeof(MainStep))]
        public async Task<IHttpActionResult> DeleteMainStep(int id)
        {
            MainStep mainstep = await db.MainSteps.FindAsync(id);
            if (mainstep == null)
            {
                return NotFound();
            }

            db.MainSteps.Remove(mainstep);
            await db.SaveChangesAsync();

            return Ok(mainstep);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainStepExists(int id)
        {
            return db.MainSteps.Count(e => e.MainStepID == id) > 0;
        }
    }
}