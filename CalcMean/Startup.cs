using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalcMean.Startup))]
namespace CalcMean
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
