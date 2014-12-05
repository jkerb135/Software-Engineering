using System.Runtime.InteropServices.ComTypes;
using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Text;

namespace SE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ValidateModelState]
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

        public class UserRequest
        {
            public string AssignedSupervisor { get; set; }
            public string User { get; set; }
            public string TaskName { get; set; }
            public string TaskDesc { get; set; }
            public DateTime CompleteBy { get; set; }
        }
        public class SendUser
        { 
            public string Username { get; set; }
            public string IpAddress { get; set; }
            public bool SignedIn {get;set;}
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
            return
                _db.CategoryAssignments.Join(_db.Categories, assignment => assignment.CategoryID, cat => cat.CategoryID,
                    (assignment, cat) => new {assignment, cat})
                    .Where(@t => @t.assignment.AssignedUser == id)
                    .Select(@t => new Category
                    {
                        CategoryId = @t.cat.CategoryID,
                        CategoryName = @t.cat.CategoryName,
                        AssignedUser = @t.assignment.AssignedUser
                    });
        }
        /// <summary>
        /// Gets tasks assigned to user by username
        /// </summary>
        /// <param name="id">String Username</param>
        /// <returns></returns>
        public IEnumerable<Task> GetTasksByUser(string id)
        {
            return
                _db.Tasks.Join(_db.Categories, task => task.CategoryID, cat => cat.CategoryID,
                    (task, cat) => new {task, cat})
                    .Join(_db.TaskAssignments, @t => @t.task.TaskID, assigned => assigned.TaskID,
                        (@t, assigned) => new {@t, assigned})
                    .Where(@t => @t.assigned.AssignedUser == id)
                    .Select(@t => new Task
                    {
                        CategoryId = @t.@t.cat.CategoryID,
                        CategoryName = @t.@t.cat.CategoryName,
                        TaskId = @t.@t.task.TaskID,
                        TaskName = @t.@t.task.TaskName,
                        AssignedUser = @t.assigned.AssignedUser
                    });
        }
        [HttpPost]
        public HttpResponseMessage PostMainStepCompleted([FromBody]CompletedMainStep mainstep)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

               var complete = _db.CompletedMainSteps.FirstOrDefault(u => u.AssignedUser == mainstep.AssignedUser && u.MainStepID == mainstep.MainStepID && u.MainStepName == mainstep.MainStepName && u.DateTimeComplete == mainstep.DateTimeComplete);
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

            var complete = _db.CompletedTasks.FirstOrDefault(u => u.AssignedUser == task.AssignedUser && u.TaskID == task.TaskID && u.TaskName == task.TaskName && u.DateTimeCompleted == task.DateTimeCompleted);
            if (complete != null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "Completed Task already exists in database");
            task.DateTimeCompleted = DateTime.Now;
            _db.CompletedTasks.Add(task);
            _db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Completed Task added to database");
        }
        public HttpResponseMessage PostLoggedInIp([FromBody]SendUser user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
                var getUser = _db.MemberAssignments.SingleOrDefault(x => x.AssignedUser == user.Username);
                if (getUser != null)
                {
                    getUser.UsersIp = user.IpAddress;
                    getUser.IsUserLoggedIn = user.SignedIn;
                }
                SaveChanges(_db);
            return Request.CreateResponse(HttpStatusCode.OK, "User Updated");
        }
        public IEnumerable<UserRequest> GetAllRequests()
        {
            return (_db.MemberAssignments.Join(_db.UserTaskRequests, mem => mem.AssignedUser, req => req.UserName,
                (mem, req) =>
                    new UserRequest
                    {
                        AssignedSupervisor = mem.AssignedSupervisor,
                        User = req.UserName,
                        TaskName = req.TaskName,
                        TaskDesc = req.TaskDescription,
                        CompleteBy = req.DateCompleted
                    }));
        }
        public HttpResponseMessage RequestTask([FromBody] UserTaskRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var exists = _db.UserTaskRequests.FirstOrDefault(x => x.UserName == userRequest.UserName && x.TaskName == userRequest.TaskName);
            if (exists != null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "You have requested a task by that name");
            _db.UserTaskRequests.Add(userRequest);
            SaveChanges(_db);
            return Request.CreateResponse(HttpStatusCode.OK, "Task Requested");
        }
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }

    }
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
       HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}
