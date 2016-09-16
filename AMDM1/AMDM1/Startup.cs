using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AMDM1.Startup))]
namespace AMDM1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
