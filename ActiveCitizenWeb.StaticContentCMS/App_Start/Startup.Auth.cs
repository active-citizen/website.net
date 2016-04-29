using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;
using ActiveCitizenWeb.StaticContentCMS.Services;
using Autofac.Core.Lifetime;

namespace ActiveCitizenWeb.StaticContentCMS
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => new IdentityDbContext<LdapIdentityUser>("ActiveCitizen", throwIfV1Schema: false));
            app.CreatePerOwinContext<ApplicationUserManager>(CreateAndSetupUserManager);
            app.CreatePerOwinContext<ApplicationSignInManager>(CreateSignInManager);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, LdapIdentityUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });            
        }

        private ApplicationSignInManager CreateSignInManager(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        private ApplicationUserManager CreateAndSetupUserManager(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var scope = (LifetimeScope)context.Environment["autofac:OwinLifetimeScope"];
            ILdapConnectionSettings ldapSettings = (ILdapConnectionSettings)scope.GetService(typeof(ILdapConnectionSettings));

            var ldapConnector = new LdapConnector(ldapSettings);

            var manager = new ApplicationUserManager(new UserStore<LdapIdentityUser>(context.Get<IdentityDbContext<LdapIdentityUser>>()), ldapConnector);
            
            // Configure validation logic for usernames
            manager.UserValidator = new LdapUserValidator<LdapIdentityUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<LdapIdentityUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}