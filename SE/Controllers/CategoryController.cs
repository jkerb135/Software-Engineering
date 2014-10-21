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
        WebApiEntites db = new WebApiEntites();
        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>All categories in database</returns>
        public IEnumerable<UserCategories> GetAllCategories()
        {
            return db.Categories.Join(db.CategoryAssignments, category => category.CategoryID, assignments => assignments.CategoryID, (category, assignments) => new UserCategories { categoryID = assignments.CategoryID, categoryName = category.CategoryName, assignedUser = assignments.AssignedUser}).AsEnumerable<UserCategories>();
        }
        /// <summary>
        /// Gets all Categories for the inputed user.
        /// </summary>
        /// <param name="id">The string value of the username</param>
        /// <returns>All Categories assigned to the passed user</returns>
        public IEnumerable<UserCategories> GetCategoriesByUsername(string id)
        {
           return  from assignment in db.CategoryAssignments
                        join cat in db.Categories on assignment.CategoryID equals cat.CategoryID
                   where assignment.AssignedUser == id
                        select new UserCategories
                        {
                            categoryID = cat.CategoryID,
                            categoryName = cat.CategoryName,
                            assignedUser = assignment.AssignedUser
                        };
        }
    }
}

