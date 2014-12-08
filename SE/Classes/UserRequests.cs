/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: CategoryController.cs
Purpose			: This is the main class file for viewing supervisors categories. This file keeps it from showing categories that have the same name information in them so supervisors cannot request categories they already have.
*/
using System.Data;
using System.Linq;
using SE.Models;
namespace SE.Classes
{
    public class UserRequests
    {
        public static DataTable CategoriesNotOwned(string user, string otherUser)
        {
            var dt = new DataTable();
            dt.Columns.Add("CategoryId");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("CreatedTime");
            using (var db = new ipawsTeamBEntities())
            {
                var userCats = db.Categories.Where(x => x.CreatedBy == user).Select(x => new {x.CategoryID, x.CategoryName, x.CreatedTime }).ToList();
                var otherCats = db.Categories.Where(x => x.CreatedBy == otherUser).Select(x => new { x.CategoryID, x.CategoryName, x.CreatedTime }).ToList();

                var concat = userCats.Concat(otherCats).ToList();
                foreach (var s in userCats)
                {
                    concat.RemoveAll(x => x.CategoryName == s.CategoryName);
                }

                foreach (var s in concat)
                {
                    var row = dt.NewRow();
                    row["CategoryId"] = s.CategoryID;
                    row["CategoryName"] = s.CategoryName;
                    row["CreatedTime"] = s.CreatedTime;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
    }
}