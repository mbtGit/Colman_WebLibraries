using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcModels.Startup))]
namespace MvcModels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
