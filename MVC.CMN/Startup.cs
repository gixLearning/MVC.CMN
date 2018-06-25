using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.CMN.Startup))]
namespace MVC.CMN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
