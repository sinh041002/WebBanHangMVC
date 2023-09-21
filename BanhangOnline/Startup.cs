using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanhangOnline.Startup))]
namespace BanhangOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
