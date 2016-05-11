﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

using ActiveCitizenWeb.StaticContentCMS.Services;
using ActiveCitizenWeb.Factory;
using System.Web.Mvc;
using System.Web.Http;
using Owin;

namespace ActiveCitizenWeb.StaticContentCMS
{
    public partial class Startup
    {
        public IContainer RegisterDependencies(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            Initializer.Initialize(builder);
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