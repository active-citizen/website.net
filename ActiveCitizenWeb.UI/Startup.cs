using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ActiveCitizenWeb.UI.Startup))]
namespace ActiveCitizenWeb.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegisterDependencies(app);
            ConfigureAuth(app);
        }
    }
}
