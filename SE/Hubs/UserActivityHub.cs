using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SE.Models;
using Task = System.Threading.Tasks.Task;

namespace SE.Hubs
{
    public class UserActivityHub : Hub
    {
        private readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name.ToLower();
            User user = _db.Users.Find(name);
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

        public override Task OnReconnected()
        {
            User connection = _db.Users.Find(Context.User.Identity.Name.ToLower());
            connection.Connected = true;
            _db.SaveChanges();

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopcalled)
        {
            User connection = _db.Users.Find(Context.User.Identity.Name.ToLower());
            connection.Connected = false;
            _db.SaveChanges();
            return base.OnDisconnected(stopcalled);
        }

        public void GetCategoryNotifications(string userName)
        {
            User toUser = _db.Users.FirstOrDefault(find => find.UserName == userName.ToLower());
            var yourNotifications =
                _db.RequestedCategories.Join(_db.Categories, r => r.CategoryID, c => c.CategoryID, (r, c) => new {r, c})
                    .Where(@t => @t.r.CreatedBy == userName && @t.r.IsApproved == false)
                    .OrderByDescending(@t => @t.r.Date)
                    .Select(@t => new
                    {
                        @t.c.CategoryName,
                        Requester = @t.r.RequestingUser,
                        @t.r.Date,
                    });
            if (yourNotifications == null) throw new ArgumentNullException("yourNotifications");
            Clients.Client(toUser.ConnectionID).yourCategoryRequests(yourNotifications.ToArray());
        }
    }
}