using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SE;

[assembly: OwinStartup(typeof (Startup))]

namespace SE
{
    [assembly: OwinStartup(typeof (Startup))]
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