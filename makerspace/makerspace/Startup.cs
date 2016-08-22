using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(makerspace.Startup))]
namespace makerspace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
