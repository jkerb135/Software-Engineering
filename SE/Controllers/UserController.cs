using SE.Areas.HelpPage.ModelDescriptions;
using SE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Security;

namespace SE.Controllers
{
    public class UserController : ApiController
    {
        public class User
        {
            public Guid ApplicationId { get; set; }
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public bool IsAnonymous { get; set; }
            public DateTime LastActivityDate { get; set; }
            public string Password { get; set; }
        }
        public class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string AssignedUser { get; set; }

        }
        public class Task
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }

        }
        public class Completed
        {
            public int MainStepID { get; set; }
            public int TaskID { get; set; }
            public string MainStepName { get; set; }
            public string AssignedUser { get; set; }
            public DateTime DateTimeComplete { get; set; }
            public float TotalTime { get; set; }
        }
        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns></returns>
       ipawsTeamBEntities db = new ipawsTeamBEntities();
        public IEnumerable<User> GetAllUsers()
        {
            return db.aspnet_Users.ToList().Where(x => x.aspnet_Roles.Any(tl => tl.RoleName == "User")).Select(tl => new User { ApplicationId = tl.ApplicationId, UserId = tl.UserId, UserName = tl.UserName, IsAnonymous = tl.IsAnonymous, LastActivityDate = tl.LastActivityDate, Password = tl.aspnet_Membership.Password }).AsEnumerable<User>();
        }
        /// <summary>
        /// Gets user by user id
        /// </summary>
        /// <param name="id">System.Guid User ID</param>
        /// <returns></returns>
        public IEnumerable<User> GetUserById(Guid id)
        {
            return db.aspnet_Users.Where(x => x.UserId == id).Select(tl => new User { ApplicationId = tl.ApplicationId, UserId = tl.UserId, UserName = tl.UserName, IsAnonymous = tl.IsAnonymous, LastActivityDate = tl.LastActivityDate, Password = tl.aspnet_Membership.Password }).AsEnumerable<User>();
        }
        /// <summary>
        /// Gets categories assigned to user by username
        /// </summary>
        /// <param name="id">String Username</param>
        /// <returns></returns>
        public IEnumerable<Category> GetCategoriesByUser(string id)
        {
            return from assignment in db.CategoryAssignments
                   join cat in db.Categories on assignment.CategoryID equals cat.CategoryID
                   where assignment.AssignedUser == id
                   select new Category
                   {
                       CategoryID = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       AssignedUser = assignment.AssignedUser
                   };
        }
        /// <summary>
        /// Gets tasks assigned to user by username
        /// </summary>
        /// <param name="id">String Username</param>
        /// <returns></returns>
        public IEnumerable<Task> GetTasksByUser(string id)
        {
            return from task in db.Tasks
                   join cat in db.Categories on task.CategoryID equals cat.CategoryID
                   join assigned in db.TaskAssignments on task.TaskID equals assigned.TaskID
                   where assigned.AssignedUser == id
                   select new Task
                   {
                       CategoryID = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       TaskID = task.TaskID,
                       TaskName = task.TaskName,
                       AssignedUser = assigned.AssignedUser
                   };
        }
        [HttpPost]
        public HttpResponseMessage PostMainStepCompleted([FromBody]CompletedMainStep mainstep)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

               var complete = db.CompletedMainSteps.SingleOrDefault(u => u.AssignedUser == mainstep.AssignedUser && u.MainStepID == mainstep.MainStepID);
               if (complete == null)
               {
                   mainstep.DateTimeComplete = DateTime.Now;
                   db.CompletedMainSteps.Add(mainstep);
                   db.SaveChanges();
                   return Request.CreateResponse(HttpStatusCode.OK, "Completed Main Step added to database");
               }
               else
               {
                   return Request.CreateResponse(HttpStatusCode.Conflict, "Completed Main Step already exists in database");
               }
            
        }
        public HttpResponseMessage PostTaskCompleted([FromBody]CompletedTask task)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var complete = db.CompletedTasks.SingleOrDefault(u => u.AssignedUser == task.AssignedUser && u.TaskID == task.TaskID);
            if (complete == null)
            {
                task.DateTimeCompleted = DateTime.Now;
                db.CompletedTasks.Add(task);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Completed Task added to database");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Completed Task already exists in database");
            }

        }

    }
}
