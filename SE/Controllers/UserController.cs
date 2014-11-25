using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public new class User
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
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string AssignedUser { get; set; }

        }
        public class Task
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }

        }
        public class Completed
        {
            public int MainStepId { get; set; }
            public int TaskId { get; set; }
            public string MainStepName { get; set; }
            public string AssignedUser { get; set; }
            public DateTime DateTimeComplete { get; set; }
            public float TotalTime { get; set; }
        }
        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns></returns>
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        public IEnumerable<User> GetAllUsers()
        {
            return _db.aspnet_Users.ToList().Where(x => x.aspnet_Roles.Any(tl => tl.RoleName == "User")).Select(tl => new User { ApplicationId = tl.ApplicationId, UserId = tl.UserId, UserName = tl.UserName, IsAnonymous = tl.IsAnonymous, LastActivityDate = tl.LastActivityDate, Password = tl.aspnet_Membership.Password }).AsEnumerable();
        }
        /// <summary>
        /// Gets user by user id
        /// </summary>
        /// <param name="id">System.Guid User ID</param>
        /// <returns></returns>
        public IEnumerable<User> GetUserById(Guid id)
        {
            return _db.aspnet_Users.Where(x => x.UserId == id).Select(tl => new User { ApplicationId = tl.ApplicationId, UserId = tl.UserId, UserName = tl.UserName, IsAnonymous = tl.IsAnonymous, LastActivityDate = tl.LastActivityDate, Password = tl.aspnet_Membership.Password }).AsEnumerable();
        }
        /// <summary>
        /// Gets categories assigned to user by username
        /// </summary>
        /// <param name="id">String Username</param>
        /// <returns></returns>
        public IEnumerable<Category> GetCategoriesByUser(string id)
        {
            return from assignment in _db.CategoryAssignments
                   join cat in _db.Categories on assignment.CategoryID equals cat.CategoryID
                   where assignment.AssignedUser == id
                   select new Category
                   {
                       CategoryId = cat.CategoryID,
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
            return from task in _db.Tasks
                   join cat in _db.Categories on task.CategoryID equals cat.CategoryID
                   join assigned in _db.TaskAssignments on task.TaskID equals assigned.TaskID
                   where assigned.AssignedUser == id
                   select new Task
                   {
                       CategoryId = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       TaskId = task.TaskID,
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

               var complete = _db.CompletedMainSteps.SingleOrDefault(u => u.AssignedUser == mainstep.AssignedUser && u.MainStepID == mainstep.MainStepID && u.MainStepName == mainstep.MainStepName && u.DateTimeComplete == mainstep.DateTimeComplete);
            if (complete != null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "Completed Main Step already exists in database");
            mainstep.DateTimeComplete = DateTime.Now;
            _db.CompletedMainSteps.Add(mainstep);
            _db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Completed Main Step added to database");
        }
        public HttpResponseMessage PostTaskCompleted([FromBody]CompletedTask task)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var complete = _db.CompletedTasks.SingleOrDefault(u => u.AssignedUser == task.AssignedUser && u.TaskID == task.TaskID && u.TaskName == task.TaskName && u.DateTimeCompleted == task.DateTimeCompleted);
            if (complete != null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "Completed Task already exists in database");
            task.DateTimeCompleted = DateTime.Now;
            _db.CompletedTasks.Add(task);
            _db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Completed Task added to database");
        }

    }
}
