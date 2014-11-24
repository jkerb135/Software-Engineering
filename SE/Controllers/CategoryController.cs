using SE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserCategories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string AssignedUser { get; set; }
    }
    public class CategoryController : ApiController
    {
        readonly ipawsTeamBEntities db = new ipawsTeamBEntities();
        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>All categories in database</returns>
        public IEnumerable<UserCategories> GetAllCategories()
        {
            return from cat in db.Categories
                   join assignment in db.CategoryAssignments on cat.CategoryID equals assignment.CategoryID into user
                   from b in user.DefaultIfEmpty()
                   select new UserCategories
                   {
                       CategoryId = cat.CategoryID,
                       CategoryName = cat.CategoryName,
                       AssignedUser = b.AssignedUser
                   };
        }
    }
}

