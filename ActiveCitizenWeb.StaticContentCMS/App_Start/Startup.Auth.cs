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
using System.Linq;
using System.Collections.Generic;

namespace ActiveCitizenWeb.StaticContentCMS
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(() => new IdentityDbContext<LdapIdentityUser>("ActiveCitizen.Auth", throwIfV1Schema: false));
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

        public void CreateDefaultUsersAndRoles()
        {
            using (var context = new IdentityDbContext<LdapIdentityUser>("ActiveCitizen.Auth", throwIfV1Schema: false))
            {
                const string adminLogin = "admin";
                const string adminPassword = "secret";
                const string adminRole = "admin";
                using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                {
                    var roles = new List<string> { adminRole, "editor", "user" };
                    if (!roleManager.Roles.Any())
                    {
                        roles.ForEach(roleName => roleManager.Create(new IdentityRole(roleName)));
                    }
                }

                using (var manager = new UserManager<LdapIdentityUser>(new UserStore<LdapIdentityUser>(context)))
                {
                    if (!manager.Users.Any(user=>user.Roles.Any(role => role.RoleId == adminRole)))
                    {
                        manager.Create(new LdapIdentityUser { UserName = adminLogin }, adminPassword);
                        var user = manager.FindByName(adminLogin);
                        manager.AddToRole(user.Id, adminRole);
                    }
                }
            }
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