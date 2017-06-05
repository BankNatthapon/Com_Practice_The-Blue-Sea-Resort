using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheBlueSeaResort_v2.Startup))]
namespace TheBlueSeaResort_v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
