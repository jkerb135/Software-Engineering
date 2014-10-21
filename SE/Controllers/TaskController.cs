using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SE.Controllers
{
    public class TaskController : ApiController
    {
        WebApiEntites db = new WebApiEntites();
        public class CatTasks
        {
            public int CategoryID { get; set; }
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }
        }
        /// <summary>
        /// Gets all Tasks in the database.
        /// </summary>
        public IEnumerable<CatTasks> GetAllTasks()
        {
            return db.Tasks.Select(tl => new CatTasks { AssignedUser = tl.AssignedUser, CategoryID = tl.CategoryID, TaskName = tl.TaskName, TaskID = tl.TaskID }).AsEnumerable<CatTasks>();
        }
        /// <summary>
        /// Gets all tasks from the database pertaining to a category id.
        /// </summary>
       public IEnumerable<CatTasks> GetTaskByCategoryID(int id)
        {
            return db.Tasks.Where(tl => tl.CategoryID == id).Select(tl => new CatTasks { AssignedUser = tl.AssignedUser, CategoryID = tl.CategoryID, TaskID = tl.TaskID, TaskName = tl.TaskName }).AsEnumerable<CatTasks>();
        }
       /// <summary>
       /// Gets all detailed steps from the database pertaining to a category id and username.
       /// </summary>
       public IEnumerable<CatTasks> GetTaskByIDAndUser(int id, string username)
       {
           return db.Tasks.Where(tl => tl.CategoryID == id && tl.AssignedUser == username).Select(tl => new CatTasks { AssignedUser = tl.AssignedUser, CategoryID = tl.CategoryID, TaskID = tl.TaskID, TaskName = tl.TaskName }).AsEnumerable<CatTasks>();
       }
    }
}
