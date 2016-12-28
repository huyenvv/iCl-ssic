using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iClassic.Startup))]
namespace iClassic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
