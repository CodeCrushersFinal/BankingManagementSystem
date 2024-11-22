using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BMSMVC.Startup))]
namespace BMSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
