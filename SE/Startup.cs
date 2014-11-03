using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(SE.Startup))]

namespace SE
{
    [assembly: OwinStartup(typeof(SE.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR("/signalr", hubConfiguration);
        }
    }
}
