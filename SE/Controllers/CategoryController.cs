/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: CategoryController.cs
Purpose			: This is the main class file for the WebAPI that pertains to Categories. It handles GET request for all Categories.
*/
using SE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SE.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class CategoryController : ApiController
    {
        public class UserCategories
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string AssignedUser { get; set; }
        }
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>All categories in database</returns>
        public IEnumerable<UserCategories> GetAllCategories()
        {
            return
                _db.Categories.GroupJoin(_db.CategoryAssignments, cat => cat.CategoryID,
                    assignment => assignment.CategoryID, (cat, user) => new {cat, user})
                    .SelectMany(@t => @t.user.DefaultIfEmpty(), (@t, b) => new UserCategories
                    {
                        CategoryId = @t.cat.CategoryID,
                        CategoryName = @t.cat.CategoryName,
                        AssignedUser = b.AssignedUser
                    });
        }
    }
}

