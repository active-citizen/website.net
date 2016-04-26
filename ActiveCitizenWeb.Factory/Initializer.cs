using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizenWeb.Infrastructure.Provider;
using Autofac;

namespace ActiveCitizenWeb.Factory
{
    public class Initializer
    {
        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterType<StaticContentProvider>().As<IStaticContentProvider>();
            builder.RegisterType<StaticContentDbContext>().As<IStaticContentDbContext>();
        }
    }
}