using SE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
namespace SE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            return
                _db.Tasks.Join(_db.Categories, task => task.CategoryID, cat => cat.CategoryID,
                    (task, cat) => new {task, cat})
                    .GroupJoin(_db.TaskAssignments, @t => @t.task.TaskID, assigned => assigned.TaskID,
                        (@t, user) => new {@t, user})
                    .SelectMany(@t => @t.user.DefaultIfEmpty(), (@t, b) => new CatTasks
                    {
                        CategoryId = @t.@t.cat.CategoryID,
                        CategoryName = @t.@t.cat.CategoryName,
                        TaskId = @t.@t.task.TaskID,
                        TaskName = @t.@t.task.TaskName,
                        AssignedUser = b.AssignedUser
                    });
        }
        /// <summary>
        /// Gets all tasks from the database pertaining to a category id.
        /// </summary>
       public IEnumerable<CatTasks> GetTaskByCategoryId(int id)
        {
            return
                _db.Tasks.Join(_db.Categories, task => task.CategoryID, cat => cat.CategoryID,
                    (task, cat) => new {task, cat})
                    .GroupJoin(_db.TaskAssignments, @t => @t.task.TaskID, assigned => assigned.TaskID,
                        (@t, user) => new {@t, user})
                    .SelectMany(@t => @t.user.DefaultIfEmpty(), (@t, b) => new {@t, b})
                    .Where(@t => @t.@t.@t.cat.CategoryID == id)
                    .Select(@t => new CatTasks
                    {
                        CategoryId = @t.@t.@t.cat.CategoryID,
                        CategoryName = @t.@t.@t.cat.CategoryName,
                        TaskId = @t.@t.@t.task.TaskID,
                        TaskName = @t.@t.@t.task.TaskName,
                        AssignedUser = @t.b.AssignedUser
                    });
        }
       /// <summary>
       /// Gets all detailed steps from the database pertaining to a category id and username.
       /// </summary>
    }
}
