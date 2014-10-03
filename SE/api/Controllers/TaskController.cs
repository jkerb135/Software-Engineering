﻿using System;
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
    public class TaskController : ApiController
    {
        private ipawsTeamBContext db = new ipawsTeamBContext();

        // GET api/Task
        public IQueryable<iPaws.Models.Task> GetTasks()
        {
            return db.Tasks;
        }

        // GET api/Task/5
        [ResponseType(typeof(iPaws.Models.Task))]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            iPaws.Models.Task task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT api/Task/5
        public async Task<IHttpActionResult> PutTask(int id, iPaws.Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.TaskID)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST api/Task
        [ResponseType(typeof(iPaws.Models.Task))]
        public async Task<IHttpActionResult> PostTask(iPaws.Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = task.TaskID }, task);
        }

        // DELETE api/Task/5
        [ResponseType(typeof(iPaws.Models.Task))]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            iPaws.Models.Task task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskID == id) > 0;
        }
    }
}