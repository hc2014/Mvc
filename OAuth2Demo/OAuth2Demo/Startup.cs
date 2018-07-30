using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OAuth2Demo.Startup))]
namespace OAuth2Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
