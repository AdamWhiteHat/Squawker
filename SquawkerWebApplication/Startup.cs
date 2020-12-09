using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SquawkerWebApplication.Startup))]
namespace SquawkerWebApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
