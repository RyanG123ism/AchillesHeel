using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AchillesHeel_RG.Startup))]
namespace AchillesHeel_RG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
