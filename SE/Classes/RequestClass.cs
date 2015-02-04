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

using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using SE.Models;
namespace SE.Classes
{
    public class RequestClass
    {
        private static readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        private static readonly string _mem = Membership.GetUser().UserName;
        public static DataTable CategoriesNotOwned(string user, string otherUser)
        {
            var dt = new DataTable();
            dt.Columns.Add("CategoryId");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("CreatedTime");

                var userCats = _db.Categories.Where(x => x.CreatedBy == user).Select(x => new {x.CategoryID, x.CategoryName, x.CreatedTime }).ToList();
                var otherCats = _db.Categories.Where(x => x.CreatedBy == otherUser).Select(x => new { x.CategoryID, x.CategoryName, x.CreatedTime }).ToList();

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
            return dt;
        }

        public static DataTable GetTaskRequests(string supervisor)
        {
            var dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("TaskName");
            dt.Columns.Add("TaskDescription");
            dt.Columns.Add("UserName");
            dt.Columns.Add("DateCompleted");

            var linq = (_db.MemberAssignments.Join(_db.UserTaskRequests, mem => mem.AssignedUser, req => req.UserName,
                (mem, req) => new { mem, req }).Where(@t => @t.mem.AssignedSupervisor == supervisor).Select(@t => new
                {
                    @t.req.ID,
                    @t.req.TaskName,
                    @t.req.TaskDescription,
                    @t.req.UserName,
                    @t.req.DateCompleted
                })).ToList().Select(c => new UserTaskRequest
                {
                    ID = c.ID,
                    TaskName = c.TaskName,
                    TaskDescription = c.TaskDescription,
                    UserName = c.UserName,
                    DateCompleted = c.DateCompleted
                }).ToList();

            

            for (int i = 0, len = linq.Count; i < len; i++)
            {
                var dr = dt.NewRow();
                dr["ID"] = linq[i].ID;
                dr["TaskName"] = linq[i].TaskName;
                dr["TaskDescription"] = linq[i].TaskDescription;
                dr["UserName"] = linq[i].UserName;
                dr["DateCompleted"] = linq[i].DateCompleted;

                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}