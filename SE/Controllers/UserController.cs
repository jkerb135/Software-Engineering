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
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }

        }
        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns></returns>
        WebApiEntites db = new WebApiEntites();
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
            return from assignment in db.TaskAssignments
                   join task in db.Tasks on assignment.TaskID equals task.TaskID
                   where assignment.AssignedUser == id
                   select new Task
                   {
                       TaskID = task.TaskID,
                       TaskName = task.TaskName,
                       AssignedUser = assignment.AssignedUser
                   };
        }

    }
}
