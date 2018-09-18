using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhaleReport.Startup))]
namespace WhaleReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
