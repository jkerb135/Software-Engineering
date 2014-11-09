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
    
    public class MessageDetail
    {

        public string UserName { get; set; }

        public string Message { get; set; }
    
    }
     public class UserActivityHub : Hub
    {
         private string SessionConnectionID { get; set; }
         public static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        /// <summary>
        /// Sends the update user count to the listening view.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
         public void getOnlineUsers()
         {  
             using (var db = new WebApiEntites())
             {

                 var online = (from r in db.Users where r.Connected == true select new { ConnectionID = r.ConnectionID, UserName = r.UserName }).ToArray();
                 Clients.All.showUsersOnLine(online.ToArray());
             }
             
         }
         public void SendMessageToAll(string userName, string message)
         {
             // store last 100 messages in cache
             AddMessageinCache(userName, message);

             // Broad cast message
             Clients.All.messageReceived(userName, message);
         }

         public void notify(string who)
         {
             string name = Context.User.Identity.Name;
             using (var db = new WebApiEntites())
             {
                 var toUser = db.Users.FirstOrDefault(x => x.UserName == who);
                 if (toUser != null)
                 {
                     string connectionID = toUser.ConnectionID;
                     Clients.Client(connectionID).notifyUser(name + " has requested a category");
                 }
             }
             
         }
         public void SendPrivateMessage(string toUserId, string message)
         {

             string fromUserId = SessionConnectionID;
             using (var db = new WebApiEntites())
             {
                 var toUser = db.Users.FirstOrDefault(x => x.ConnectionID == toUserId);
                 var fromUser = db.Users.FirstOrDefault(x => x.ConnectionID == fromUserId);
                 //var picture = db.Profiles.FirstOrDefault(x => x.Name == fromUser.UserName);
                 if (toUser != null && fromUser != null)
                 {
                     // send to 
                     Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                     // send to caller user
                     Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
                 }
             }

         }
        /// <summary>
        /// The OnConnected event.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override System.Threading.Tasks.Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            using (var db = new WebApiEntites())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = name,
                        Connected = true,
                        ConnectionID = Context.ConnectionId,
                    };
                    SessionConnectionID = Context.ConnectionId;
                    db.Users.Add(user);
                }
                else
                {
                    var connection = db.Users.Find(Context.User.Identity.Name);
                    SessionConnectionID = connection.ConnectionID;
                    connection.Connected = true;
                }
                db.SaveChanges();
            }
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            using (var db = new WebApiEntites())
            {
                var connection = db.Users.Find(Context.User.Identity.Name);
                SessionConnectionID = connection.ConnectionID;
                connection.Connected = true;
                db.SaveChanges();
            }
            return base.OnConnected();
        }
        /// <summary>
        /// The OnDisconnected event.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopcalled)
        {
           using (var db = new WebApiEntites())
           {
               var connection = db.Users.Find(Context.User.Identity.Name);
               connection.Connected = false;
               db.SaveChanges();
           }
           return base.OnDisconnected(stopcalled);
        }

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }
    }

}