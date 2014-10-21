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
    }
    public class GetUserCatergoriesController : ApiController
    {
        WebApiEntites db = new WebApiEntites();
        public IEnumerable<Category> GetAllCategories(){

            return db.Categories;
        }
        public IEnumerable<UserCategories> GetCategoriesByID(string id)
        {

           var items = from assignment in db.CategoryAssignments
                        join cat in db.Categories on assignment.CategoryID equals cat.CategoryID
                       where assignment.AssignedUser == id
                        select new UserCategories
                        {
                            categoryID = cat.CategoryID,
                            categoryName = cat.CategoryName,
                        };

            return items;
        }
    }
}

