using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SE.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SE.Hubs
{
    public class ContactHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        DateTime now = new DateTime();
        public void ContactFormSubmitted(string who,string message)
        {
            string name = Context.User.Identity.Name;
            string connectionID = Context.ConnectionId;
            foreach (var connectionId in _connections.GetConnections(who))
            {
                Clients.Client(connectionId).notifyUsers(now.ToShortTimeString() + connectionID + " " + name + ": " + message);
            }

        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }

}