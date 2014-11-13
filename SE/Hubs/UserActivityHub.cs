using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using SE.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SE.Hubs
{
    
     public class UserActivityHub : Hub
    {
         ipawsTeamBEntities db = new ipawsTeamBEntities();
        public override System.Threading.Tasks.Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            var user = db.Users.Find(name);
            if (user == null)
            {
                var newUser = new User
                {
                    Connected = true,
                    ConnectionID = Context.ConnectionId,
                    UserName = name,
                };
                db.Users.Add(newUser);

            }
            else
            {
                user.Connected = true;
                user.ConnectionID = Context.ConnectionId;
                user.UserName = name;
            }
            db.SaveChanges();
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            var connection = db.Users.Find(Context.User.Identity.Name);
            connection.Connected = true;
            db.SaveChanges();

            return base.OnReconnected();
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopcalled)
        {
           using (var db = new ipawsTeamBEntities())
           {
               var connection = db.Users.Find(Context.User.Identity.Name);
               connection.Connected = false;
               db.SaveChanges();
           }
           return base.OnDisconnected(stopcalled);
        }
        public void getCategoryNotifications(string userName)
        {
            var toUser = db.Users.FirstOrDefault(find => find.UserName == userName);
            var yourNotifications = from r in db.RequestedCategories
                                    join c in db.Categories on r.CategoryID equals c.CategoryID
                                    where r.CreatedBy == userName
                                    orderby r.Date descending
                                    select new
                                    {
                                        CategoryName = c.CategoryName,
                                        Requester = r.RequestingUser,
                                        Date = r.Date,
                                    };
            Clients.Client(toUser.ConnectionID).yourCategoryRequests(yourNotifications.ToArray());
        }
        public void sendCategoryNotifications(string userName)
        {
            var toUser = db.Users.FirstOrDefault(find => find.UserName == userName);
            var yourNotifications = from r in db.RequestedCategories
                                    join c in db.Categories on r.CategoryID equals c.CategoryID
                                    where r.CreatedBy == userName
                                    orderby r.Date descending
                                    select new
                                    {
                                        CategoryName = c.CategoryName,
                                        Requester = r.RequestingUser,
                                        Date = r.Date,
                                    };
            Clients.Client(toUser.ConnectionID).sendYourCategoryRequests(yourNotifications.ToArray());
        }

    }

}