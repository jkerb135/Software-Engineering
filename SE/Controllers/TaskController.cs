using SE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
namespace SE.Controllers
{
    public class TaskController : ApiController
    {
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        public class CatTasks
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }
        }
        /// <summary>
        /// Gets all Tasks in the database.
        /// </summary>
        public IEnumerable<CatTasks> GetAllTasks()
        {
            return from task in _db.Tasks
                   join cat in _db.Categories on task.CategoryID equals cat.CategoryID
                   join assigned in _db.TaskAssignments on task.TaskID equals assigned.TaskID into user
                   from b in user.DefaultIfEmpty()
                   select new CatTasks
                   {
                       CategoryId = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       TaskId = task.TaskID,
                       TaskName = task.TaskName,
                       AssignedUser = b.AssignedUser
                   };
        }
        /// <summary>
        /// Gets all tasks from the database pertaining to a category id.
        /// </summary>
       public IEnumerable<CatTasks> GetTaskByCategoryId(int id)
        {
            return from task in _db.Tasks
                   join cat in _db.Categories on task.CategoryID equals cat.CategoryID
                   join assigned in _db.TaskAssignments on task.TaskID equals assigned.TaskID into user
                   from b in user.DefaultIfEmpty()
                   where cat.CategoryID == id
                   select new CatTasks
                   {
                       CategoryId = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       TaskId = task.TaskID,
                       TaskName = task.TaskName,
                       AssignedUser = b.AssignedUser
                   };
        }
       /// <summary>
       /// Gets all detailed steps from the database pertaining to a category id and username.
       /// </summary>
    }
}
