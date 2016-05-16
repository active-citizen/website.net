using Autofac;
using AutoMapper;
using ActiveCitizen.Model.StaticContent.Faq;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq;
using ActiveCitizenWeb.StaticContentCMS.Configuration;
using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ActiveCitizenWeb.Infrastructure.UserManagement;
using System.Data.Entity;
using Autofac.Core;
using ActiveCitizenWeb.Infrastructure.Provider;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public static class CMSInitializer
    {
        public static void Initialize(ContainerBuilder builder, IAppBuilder app)
        {
            // Mapper
            builder.RegisterInstance(InitializeMapping()).SingleInstance();

            // Settings
            builder.RegisterType<AppSettings>().As<IAppSettings>();

            // Ldap infrastructure and settings
            builder.Register(ctx => new LdapConnectionSettingsInitializer().Initialize()).As<ILdapConnectionSettings>();
            builder.RegisterType<LdapConnector>().As<ILdapConnector>();
        }

        static IMapper InitializeMapping()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FaqListItem, QuestionVM>();//.ForMember(vm => vm.CategoryId, li => li.MapFrom(vm => vm.Category.Id));
                    cfg.CreateMap<QuestionVM, FaqListItem>();//.ForMember(li => li.Category, conf => conf.ResolveUsing(vm => new FaqListCategory { Id = vm.CategoryId }));
                });
            return config.CreateMapper();
        }
        
    }
}