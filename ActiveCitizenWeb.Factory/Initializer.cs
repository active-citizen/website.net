using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizenWeb.DataAccess.Provider;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActiveCitizenWeb.Factory
{
    public class Initializer
    {
        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterType<StaticContentProvider>().As<IStaticContentProvider>();
            builder.RegisterType<FaqContext>().As<IFaqContext>();
        }
    }
}