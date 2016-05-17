using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizenWeb.Infrastructure.Provider;
using Autofac;

namespace ActiveCitizenWeb.Infrastructure.AutofacModules
{
    public class ContentManagementProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StaticContentManagementProvider>().As<IStaticContentManagementProvider>();
            builder.RegisterType<StaticContentDbContext>().As<IStaticContentDbContext>();
        }
    }
}
