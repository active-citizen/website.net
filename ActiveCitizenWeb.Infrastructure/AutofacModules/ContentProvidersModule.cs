using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizenWeb.Infrastructure.Provider;
using Autofac;

namespace ActiveCitizenWeb.Infrastructure.AutofacModules
{
    public class ContentProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StaticContentProvider>().As<IStaticContentProvider>();
            builder.RegisterType<StaticContentDbContext>().As<IStaticContentDbContext>();
        }
    }
}
