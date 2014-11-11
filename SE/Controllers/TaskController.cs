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
        iPawsEntities db = new iPawsEntities();
        public class CatTasks
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }
        }
        /// <summary>
        /// Gets all Tasks in the database.
        /// </summary>
        public IEnumerable<CatTasks> GetAllTasks()
        {
            return from task in db.Tasks
                   join cat in db.Categories on task.CategoryID equals cat.CategoryID
                   join assigned in db.TaskAssignments on task.TaskID equals assigned.TaskID
                   select new CatTasks
                   {
                       CategoryID = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       TaskID = task.TaskID,
                       TaskName = task.TaskName,
                       AssignedUser = task.AssignedUser
                   };
        }
        /// <summary>
        /// Gets all tasks from the database pertaining to a category id.
        /// </summary>
       public IEnumerable<CatTasks> GetTaskByCategoryID(int id)
        {
            return from task in db.Tasks
                   join cat in db.Categories on task.CategoryID equals cat.CategoryID
                   join assigned in db.TaskAssignments on task.TaskID equals assigned.TaskID
                   where cat.CategoryID == id
                   select new CatTasks
                   {
                       CategoryID = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       TaskID = task.TaskID,
                       TaskName = task.TaskName,
                       AssignedUser = task.AssignedUser
                   };
        }
       /// <summary>
       /// Gets all detailed steps from the database pertaining to a category id and username.
       /// </summary>
    }
}
