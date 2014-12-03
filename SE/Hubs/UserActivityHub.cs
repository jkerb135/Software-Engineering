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
            if (yourNotifications == null) throw new ArgumentNullException("yourNotifications");
            Clients.Client(toUser.ConnectionID).yourCategoryRequests(yourNotifications.ToArray());
        }
    }

}