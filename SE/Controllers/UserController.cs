/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: UserController.cs
Purpose			: This is the main class file for the WebApiClass that pertains to a specific user. It returns all users, a user by name, a users categories, and a users tasks. This file also
                  handles posting completed mainsteps and competed tasks, users logged in state and ipaddress, and their task requests. Also written is a SaveChanges(DbContext) function that outputs  
                  any problems saving database changes in an inner exception I used this for debugging posting.
*/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using SE.Classes;
using SE.Models;
using Category = SE.Models.Category;
using Task = SE.Models.Task;

namespace SE.Controllers
{

    [ValidateModelState]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {

        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns></returns>
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        public IEnumerable<WebApiClass.User> GetAllUsers()
        {
            return _db.aspnet_Users.ToList().Where(x => x.aspnet_Roles.Any(tl => tl.RoleName == "User")).Select(tl => new WebApiClass.User { ApplicationId = tl.ApplicationId, UserId = tl.UserId, UserName = tl.UserName, IsAnonymous = tl.IsAnonymous, LastActivityDate = tl.LastActivityDate, Password = tl.aspnet_Membership.Password }).AsEnumerable();
        }
        /// <summary>
        /// Gets user by user id
        /// </summary>
        /// <param name="id">System.Guid User ID</param>
        /// <returns></returns>
        public IEnumerable<WebApiClass.User> GetUserById(Guid id)
        {
            return _db.aspnet_Users.Where(x => x.UserId == id).Select(tl => new WebApiClass.User { ApplicationId = tl.ApplicationId, UserId = tl.UserId, UserName = tl.UserName, IsAnonymous = tl.IsAnonymous, LastActivityDate = tl.LastActivityDate, Password = tl.aspnet_Membership.Password }).AsEnumerable();
        }

        /// <summary>
        /// Gets categories assigned to user by username
        /// </summary>
        /// <param name="id">String Username</param>
        /// <returns></returns>
        public IEnumerable<Category> GetCategoriesByUser(string id)
        {
            return from a in _db.Categories
                join b in _db.CategoryAssignments on a.CategoryID equals b.CategoryID
                where b.AssignedUser == id
                select new Category();
        }

        public IList<WebApiClass.CategoryClass> GetByUser(string id)
        {
            return (from a in _db.Categories
                    join b in _db.CategoryAssignments on a.CategoryID equals b.CategoryID
                    where b.AssignedUser == id
                    select new WebApiClass.CategoryClass
                    {
                        CategoryId = a.CategoryID,
                        CategoryName = a.CategoryName,
                        Tasks = a.Tasks.ToList().Select(t => new WebApiClass.TaskClass
                        {
                            TaskId = t.TaskID,
                            TaskName = t.TaskName,
                        }).ToList()
                    }).ToList();
        }

        public IList<WebApiClass.MainStepClass> GetTaskDetails(int id)
        {
<<<<<<< HEAD
            List<WebApiClass.NewMainStep> list = new List<WebApiClass.NewMainStep>();
            foreach (WebApiClass.NewMainStep step in (_db.MainSteps.Where(a => a.TaskID == id).Select(a => new WebApiClass.NewMainStep
            {
                MainStepId = a.MainStepID, MainStepName = a.MainStepName, MainStepText = a.MainStepText, AudioPath = a.AudioPath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"), VideoPath = a.VideoPath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"), DetailedStep = a.DetailedSteps.ToList().Select(t => new WebApiClass.NewDetailedStep
=======
            return (from a in _db.MainSteps
                where a.TaskID == id
                select new WebApiClass.MainStepClass
>>>>>>> origin/master
                {
                    MainStepID = a.MainStepID,
                    MainStepName = a.MainStepName,
                    MainStepText = a.MainStepText,
                    AudioPath = a.AudioPath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"),
                    VideoPath = a.VideoPath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"),
                    SortOrder = a.ListOrder,
                    DetailedSteps = a.DetailedSteps.ToList().Select(d => new WebApiClass.DetailedStepClass
                    {
                        DetailedStepID = d.DetailedStepID,
                        DetailedStepName = d.DetailedStepName,
                        DetailedStepText = d.DetailedStepText,
                        ImagePath = d.ImagePath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu")
                    }).ToList()
                }).OrderBy(x => x.SortOrder).ToList();
        }

        public IList<WebApiClass.DetailedStepClass> GetMainStepDetails(int id)
        {
            return (from d in _db.DetailedSteps
                where d.MainStepID == id
                select new WebApiClass.DetailedStepClass
                {
                    DetailedStepID = d.DetailedStepID,
                    DetailedStepName = d.DetailedStepName,
                    DetailedStepText = d.DetailedStepText,
                    ImagePath = d.ImagePath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"),
                    SortOrder = d.ListOrder,
                }).OrderBy(x => x.SortOrder).ToList();
        }

        public IEnumerable<WebApiClass.CompleteStep> GetAllCompletedSteps(string id, int val)
        {
            return _db.CompletedMainSteps.Where(@u => @u.AssignedUser == id && @u.TaskID == val).Select(@t => new WebApiClass.CompleteStep() { MainStepId = @t.MainStepID, DateTimeComplete = @t.DateTimeComplete, MainStepName = @t.MainStepName, TotalTime = @t.TotalTime }); 
        }

        [HttpPost]
        public HttpResponseMessage PostMainStepCompleted([FromBody]CompletedMainStep mainstep)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var complete =
                _db.CompletedMainSteps.Where(
                    x => x.AssignedUser == mainstep.AssignedUser && x.MainStepID == mainstep.MainStepID)
                    .Select(x => x)
                    .FirstOrDefault();
            if (complete != null)
            {
                complete.DateTimeComplete = DateTime.Now;
                complete.TotalTime = mainstep.TotalTime;
            }
            else
            {
                mainstep.DateTimeComplete = DateTime.Now;
                _db.CompletedMainSteps.Add(mainstep);
            }
            _db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Completed Main Step added to database");
        }

        public HttpResponseMessage PostTaskCompleted([FromBody]CompletedTask task)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var complete =
                _db.CompletedTasks.Where(
                    x => x.AssignedUser == task.AssignedUser && x.TaskID == task.TaskID)
                    .Select(x => x)
                    .FirstOrDefault();
            if (complete != null)
            {
                complete.DateTimeCompleted = DateTime.Now;
                complete.TotalDetailedStepsUsed = task.TotalDetailedStepsUsed;
                complete.TotalTime = task.TotalTime;
            }
            else
            {
                task.DateTimeCompleted = DateTime.Now;
                _db.CompletedTasks.Add(task);
            }

            SaveChanges(_db);
            return Request.CreateResponse(HttpStatusCode.OK, "Completed Task added to database");
        }

        public IEnumerable<CompletedTask> GetTasksCompleted(string id)
        {
            return _db.CompletedTasks.Where(x => x.AssignedUser == id).Select(x => x);
        }

        public HttpResponseMessage PostLoggedInIp([FromBody]WebApiClass.SendUser user)
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

        public IEnumerable<WebApiClass.UserRequest> GetAllRequests()
        {
            return (_db.MemberAssignments.Join(_db.UserTaskRequests, mem => mem.AssignedUser, req => req.UserName,
                (mem, req) =>
                    new WebApiClass.UserRequest
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

        private static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

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
                    sb, ex
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
