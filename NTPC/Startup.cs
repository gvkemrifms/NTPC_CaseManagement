using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NTPC.Startup))]
namespace NTPC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
