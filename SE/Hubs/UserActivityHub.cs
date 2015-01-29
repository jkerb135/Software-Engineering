/*
Author			: Josh Kerbaugh
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: UserActivityHub.cs
Purpose			: This is the class file that handles realtime messaging for task requests between supervisor/supervisor and user/supervisor. 
*/
using System;
using Microsoft.AspNet.SignalR;
using SE.Models;
using System.Linq;

namespace SE.Hubs
{
    
     public class UserActivityHub : Hub
    {
         readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();
        public override System.Threading.Tasks.Task OnConnected()
        {
            var name = Context.User.Identity.Name.ToLower();
            var user = _db.Users.Find(name);
            if (user == null)
            {
                var newUser = new User
                {
                    Connected = true,
                    ConnectionID = Context.ConnectionId,
                    UserName = name,
                };
                _db.Users.Add(newUser);

            }
            else
            {
                user.Connected = true;
                user.ConnectionID = Context.ConnectionId;
                user.UserName = name;
            }
            _db.SaveChanges();
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            var connection = _db.Users.Find(Context.User.Identity.Name.ToLower());
            connection.Connected = true;
            _db.SaveChanges();

            return base.OnReconnected();
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopcalled)
        {
               var connection = _db.Users.Find(Context.User.Identity.Name.ToLower());
               connection.Connected = false;
               _db.SaveChanges();
           return base.OnDisconnected(stopcalled);
        }
        public void GetCategoryNotifications(string userName)
        {
            var toUser = _db.Users.FirstOrDefault(find => find.UserName == userName.ToLower());
            var yourNotifications =
                _db.RequestedCategories.Join(_db.Categories, r => r.CategoryID, c => c.CategoryID, (r, c) => new {r, c})
                    .Where(@t => @t.r.CreatedBy == userName && @t.r.IsApproved == false)
                    .OrderByDescending(@t => @t.r.Date)
                    .Select(@t => new
                    {
                        @t.c.CategoryName,
                        Requester = @t.r.RequestingUser, @t.r.Date,
                    });
            if (yourNotifications == null) throw new ArgumentNullException("userName");
            if (toUser != null) Clients.Client(toUser.ConnectionID).yourCategoryRequests(yourNotifications.ToArray());
        }


         public void GetTaskRequests(string userName)
        {
            var toUser = _db.Users.FirstOrDefault(find => find.UserName == userName.ToLower());
             var taskRequests =
                 _db.UserTaskRequests.Join(_db.MemberAssignments, request => request.UserName, user => user.AssignedUser,
                     (request, user) => new {request, user})
                     .Where(@t => @t.user.AssignedSupervisor == userName)
                     .Select(@t => new
                     {
                         @t.request.UserName,
                         @t.request.TaskName,
                         @t.request.TaskDescription,
                         @t.request.DateCompleted,
                     });
             if (taskRequests == null) throw new ArgumentNullException("userName");
             if (toUser != null) Clients.Client(toUser.ConnectionID).yourTaskRequests(taskRequests.ToArray());
        }

         public void SendMessage(string userName,string message)
         {
             var toUser = _db.Users.FirstOrDefault(find => find.UserName == userName.ToLower());
             if (toUser != null) Clients.Client(toUser.ConnectionID).recieve(message);
         }
    }

}