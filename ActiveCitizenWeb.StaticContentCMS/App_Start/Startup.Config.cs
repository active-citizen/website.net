using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

using ActiveCitizenWeb.StaticContentCMS.Services;
using System.Web.Mvc;
using System.Web.Http;
using Owin;
using ActiveCitizenWeb.Infrastructure.AutofacModules;

namespace ActiveCitizenWeb.StaticContentCMS
{
    public partial class Startup
    {
        public IContainer RegisterDependencies(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var contentProviderModule = new ContentManagementProvidersModule();
            builder.RegisterModule(contentProviderModule);

            var userManagementModule = new UserManagementModule(app);
            builder.RegisterModule(userManagementModule);

            CMSInitializer.Initialize(builder, app);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            return container;
        }
    }
}