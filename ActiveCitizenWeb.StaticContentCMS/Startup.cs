using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ActiveCitizenWeb.StaticContentCMS.Startup))]

namespace ActiveCitizenWeb.StaticContentCMS
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
