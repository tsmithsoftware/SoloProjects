using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WindsorExample.Startup))]
namespace WindsorExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
