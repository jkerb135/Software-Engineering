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
        iPawsEntities db = new iPawsEntities();

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
           using (var db = new iPawsEntities())
           {
               var connection = db.Users.Find(Context.User.Identity.Name);
               connection.Connected = false;
               db.SaveChanges();
           }
           return base.OnDisconnected(stopcalled);
        }

    }

}