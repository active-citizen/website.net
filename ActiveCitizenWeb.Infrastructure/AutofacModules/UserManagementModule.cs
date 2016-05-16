using ActiveCitizen.LDAP.IdentityProvider;
using ActiveCitizenWeb.Infrastructure.Provider;
using ActiveCitizenWeb.Infrastructure.UserManagement;
using Autofac;
using Autofac.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Data.Entity;

namespace ActiveCitizenWeb.Infrastructure.AutofacModules
{
    public class UserManagementModule : Module
    {
        private readonly IAppBuilder appBuilder;

        public UserManagementModule(IAppBuilder appBuilder)
        {
            this.appBuilder = appBuilder;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Identity DB context
            builder.RegisterType<ApplicationIdentityDbContext>().Named<DbContext>("Identity");

            // Role manager - a separate instance is utilized in UserManagementProvider
            builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().WithParameter(ResolvedParameter.ForNamed<DbContext>("Identity"));
            builder.RegisterType<ApplicationRoleManager>().AsSelf();

            // User manager - a separate instance is utilized in UserManagementProvider
            // (Data protection provider is required in User manager implementation to perform password updates)
            var dataProtectionProvider = appBuilder.GetDataProtectionProvider();
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
    }
}
