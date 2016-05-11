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

            // Identity DB context
            builder.RegisterType<ApplicationIdentityDbContext>().Named<DbContext>("Identity");

            // Role manager - a separate instance is utilized in UserManagementProvider
            builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().WithParameter(ResolvedParameter.ForNamed<DbContext>("Identity"));
            builder.RegisterType<ApplicationRoleManager>().AsSelf();

            // User manager - a separate instance is utilized in UserManagementProvider
            // (Data protection provider is required in User manager implementation to perform password updates)
            var dataProtectionProvider = app.GetDataProtectionProvider();
            builder.RegisterInstance(dataProtectionProvider).As<IDataProtectionProvider>();

            builder.RegisterType<UserStore<LdapIdentityUser>>().As<IUserStore<LdapIdentityUser>>().WithParameter(ResolvedParameter.ForNamed<DbContext>("Identity"));
            builder.RegisterType<ApplicationUserManager>().AsSelf().OnActivated(handler =>
                {
                    handler.Instance.UserTokenProvider = 
                        new DataProtectorTokenProvider<LdapIdentityUser>(
                            handler.Context.Resolve<IDataProtectionProvider>().Create("ASP.NET Identity"));
                });

            // BL infrastructure
            builder.RegisterType<UserManagementProvider>().As<IUserManagementProvider>();
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