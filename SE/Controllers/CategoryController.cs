using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SE.Controllers
{

    public class UserCategories
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string assignedUser { get; set; }
    }
    public class CategoryController : ApiController
    {
        ipawsTeamBEntities db = new ipawsTeamBEntities();
        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>All categories in database</returns>
        public IEnumerable<UserCategories> GetAllCategories()
        {
            return from cat in db.Categories
                   join assignment in db.CategoryAssignments on cat.CategoryID equals assignment.CategoryID
                   select new UserCategories
                   {
                       categoryID = cat.CategoryID,
                       categoryName = cat.CategoryName,
                       assignedUser = assignment.AssignedUser
                   };
        }
    }
}

