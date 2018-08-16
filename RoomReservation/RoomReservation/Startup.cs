using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Security.Claims;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(RoomReservation.Startup))]
namespace RoomReservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
