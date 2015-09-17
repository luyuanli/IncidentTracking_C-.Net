using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LL_Lab6.Startup))]
namespace LL_Lab6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
