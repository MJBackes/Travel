using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelSite.Startup))]
namespace TravelSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
