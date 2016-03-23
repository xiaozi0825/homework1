using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(asphomework1.Startup))]
namespace asphomework1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
