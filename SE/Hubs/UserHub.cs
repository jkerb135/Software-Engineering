using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using SE.Models;
using Task = System.Threading.Tasks.Task;

namespace SE.Hubs
{
    public class UserHub : Hub
    {
        readonly ipawsTeamBEntities _db = new ipawsTeamBEntities();

        public override Task OnConnected()
        {
            Clients.Caller.notify(Context.ConnectionId);
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopcalled)
        {
            return base.OnDisconnected(stopcalled);
        }

        public void AddUser(string username)
        {
            var user = _db.Users.Select(x => x).FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
            if (user == null)
            {
                var newUser = new User
                {
                    Connected = true,
                    ConnectionID = Context.ConnectionId,
                    UserName = username,
                };
                _db.Users.Add(newUser);
            }
            else
            {
                user.Connected = true;
                user.ConnectionID = Context.ConnectionId;
                user.UserName = username;
            }

            _db.SaveChanges();
        }
        public void RemoveUser(string username)
        {
            var user = _db.Users.Where(x => x.UserName == username).Select(x => x).FirstOrDefault();
            user.Connected = false;
            _db.SaveChanges();

        }
        public void SendTaskRequest(string user, string message)
        {
            var supervisor = _db.MemberAssignments.FirstOrDefault(x => x.AssignedUser == user);
            var toUser = _db.Users.FirstOrDefault(find => find.UserName == supervisor.AssignedSupervisor.ToLower());
            if (toUser != null) Clients.Client(toUser.ConnectionID).taskRequest(message);
        }

        public void RefreshUsers(string userName, string message)
        {
            var supervisor = _db.MemberAssignments.FirstOrDefault(x => x.AssignedUser == userName);
            var toUser = _db.Users.FirstOrDefault(find => find.UserName == supervisor.AssignedSupervisor.ToLower());
            var users = (_db.Users.Join(_db.MemberAssignments, user => user.UserName, memb => memb.AssignedUser,
                (user, memb) => new { user, memb })
                .Where(@t => @t.memb.AssignedSupervisor == toUser.UserName && @t.user.Connected)
                .Select(@t => new
                {
                    @t.user.UserName
                })).ToList();
            if (toUser != null) Clients.Client(toUser.ConnectionID).refresh(users, message);
        }
    }
}