﻿/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: UserController.cs
Purpose			: This is the main class file for the WebAPI that pertains to a specific user. It returns all users, a user by name, a users categories, and a users tasks. This file also
                  handles posting completed mainsteps and competed tasks, users logged in state and ipaddress, and their task requests. Also written is a SaveChanges(DbContext) function that outputs  
                  any problems saving database changes in an inner exception I used this for debugging posting.
*/
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
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Http.ModelBinding;
using Microsoft.Ajax.Utilities;
using SE.Classes;
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

        public IList<WebApiClass.NewCategory> GetByUser(string id)
        {
            List<WebApiClass.NewCategory> list = new List<WebApiClass.NewCategory>();
            foreach (WebApiClass.NewCategory category in (_db.Categories.Join(_db.CategoryAssignments, a => a.CategoryID, b => b.CategoryID, (a, b) => new { a, b }).Where(@t1 => @t1.b.AssignedUser == id).Select(@t1 => new WebApiClass.NewCategory
            {
                CategoryId = @t1.a.CategoryID,
                CategoryName = @t1.a.CategoryName,
                Tasks = @t1.a.Tasks.ToList().Select(t => new WebApiClass.UserTasks
                {
                    TaskId = t.TaskID,
                    TaskName = t.TaskName
                }).ToList()
            })))
                list.Add(category);
            return list;
        }

        public IList<WebApiClass.NewMainStep> GetTaskDetails(int id)
        {
            List<WebApiClass.NewMainStep> list = new List<WebApiClass.NewMainStep>();
            foreach (WebApiClass.NewMainStep step in (_db.MainSteps.Where(a => a.TaskID == id).Select(a => new WebApiClass.NewMainStep
            {
                MainStepId = a.MainStepID,
                MainStepName = a.MainStepName,
                MainStepText = a.MainStepText,
                AudioPath = a.AudioPath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"),
                VideoPath = a.VideoPath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"),
                DetailedStep = a.DetailedSteps.ToList().Select(t => new WebApiClass.NewDetailedStep
                {
                    DetailedStepId = t.DetailedStepID,
                    DetailedStepName = t.DetailedStepName,
                    DetailedStepText = t.DetailedStepText,
                    ImagePath = t.ImagePath.Replace("~", "http://ipawsteamb.csweb.kutztown.edu"),
                }).ToList()
            })))
                list.Add(step);
            return list;
        }

        /// <summary>
        /// Gets tasks assigned to user by username
        /// </summary>
        /// <param name="id">String Username</param>
        /// <returns></returns>
        public IEnumerable<WebApiClass.Task> GetTasksByUser(string id)
        {
            return
                _db.Tasks.Join(_db.Categories, task => task.CategoryID, cat => cat.CategoryID,
                    (task, cat) => new { task, cat })
                    .Join(_db.TaskAssignments, @t => @t.task.TaskID, assigned => assigned.TaskID,
                        (@t, assigned) => new { @t, assigned })
                    .Where(@t => @t.assigned.AssignedUser == id)
                    .Select(@t => new WebApiClass.Task
                    {
                        CategoryId = @t.@t.cat.CategoryID,
                        CategoryName = @t.@t.cat.CategoryName,
                        TaskId = @t.@t.task.TaskID,
                        TaskName = @t.@t.task.TaskName,
                        AssignedUser = @t.assigned.AssignedUser
                    });
        }

        public IEnumerable<WebApiClass.CompleteStep> GetAllCompletedSteps()
        {
            return _db.CompletedMainSteps.Select(@t => new WebApiClass.CompleteStep() { AssignedUser = @t.AssignedUser, DateTimeComplete = @t.DateTimeComplete, MainStepName = @t.MainStepName, TotalTime = @t.TotalTime });
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
        public IEnumerable<CompletedTask> GetAllCompletedTasks()
        {
            return _db.CompletedTasks.AsEnumerable();
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
            SaveChanges(_db);
            return Request.CreateResponse(HttpStatusCode.OK, "Completed Task added to database");
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