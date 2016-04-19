using ActiveCitizenWeb.Factory;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace ActiveCitizenWeb.UI
{
    public partial class Startup
    {
        public void RegisterDependencies(IAppBuilder app)
        {
            IContainer container = RegisterDependencies();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

        }

        private IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            Initializer.Initialize(builder);

            var container = builder.Build();

            return container;
        }
    }
}
