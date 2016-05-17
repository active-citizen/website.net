using ActiveCitizenWeb.Infrastructure.AutofacModules;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace ActiveCitizenWeb.Api.App_Start
{
    public class IocConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var contentProvidersModule = new ContentProvidersModule();
            builder.RegisterModule(contentProvidersModule);

            var container = builder.Build();

            return container;
        }
    }
}