using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SE.Controllers
{
    public class GetCategoryTasksController : ApiController
    {
        WebApiEntites db = new WebApiEntites();
        public class CatTasks
        {
            public int CategoryID { get; set; }
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }
        }
        public IEnumerable<CatTasks> GetAllTasks()
        {
            return db.Tasks.Select(tl => new CatTasks { AssignedUser = tl.AssignedUser, CategoryID = tl.CategoryID, TaskName = tl.TaskName, TaskID = tl.TaskID }).AsEnumerable<CatTasks>();
        }
       public IEnumerable<CatTasks> GetTaskByID(int id)
        {
            return db.Tasks.Where(tl => tl.CategoryID == id).Select(tl => new CatTasks { AssignedUser = tl.AssignedUser, CategoryID = tl.CategoryID, TaskID = tl.TaskID, TaskName = tl.TaskName }).AsEnumerable<CatTasks>();
        }
       public IEnumerable<CatTasks> GetTaskByUser(int id,string user)
       {
           return db.Tasks.Where(tl => tl.CategoryID == id && tl.AssignedUser == user).Select(tl => new CatTasks { AssignedUser = tl.AssignedUser, CategoryID = tl.CategoryID, TaskID = tl.TaskID, TaskName = tl.TaskName }).AsEnumerable<CatTasks>();
       }
    }
}
