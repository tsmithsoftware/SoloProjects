using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SelectJquery.Startup))]
namespace SelectJquery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
