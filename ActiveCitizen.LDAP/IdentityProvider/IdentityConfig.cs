using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.DirectoryServices;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public override bool SupportsUserSecurityStamp
        {
            get
            {
                return false;
            }
        }

        public override bool SupportsUserClaim
        {
            get
            {
                return false;
            }
        }

        private bool Authenticate(string userId, string password)
        {
            var connectionSettings = LdapConnectionSettings.GetDefaults();
            string ldapfilter = "(&(objectclass=person)({0}={1}))";

            try
            {
                string DN = "";
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + connectionSettings.Server + "/" + connectionSettings.BaseDN, connectionSettings.ServiceUserDN, connectionSettings.ServiceUserPassword, AuthenticationTypes.None))
                {
                    DirectorySearcher ds = new DirectorySearcher(entry);
                    ds.SearchScope = SearchScope.Subtree;
                    ds.Filter = string.Format(ldapfilter, connectionSettings.UserIdAttributeName == "DN" ? "entryDN" : connectionSettings.UserIdAttributeName, userId);
                    SearchResult result = ds.FindOne();
                    if (result != null)
                    {
                        DN = result.Path.Replace("LDAP://" + connectionSettings.Server + "/", "");
                    }
                }
                // try logon   
                try
                {
                    using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + connectionSettings.Server + "/" + connectionSettings.BaseDN, DN, password, AuthenticationTypes.None))
                    {
                        DirectorySearcher ds = new DirectorySearcher(entry);
                        ds.SearchScope = SearchScope.Subtree;
                        SearchResult result = ds.FindOne();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex) { throw; }
        }

        protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<ApplicationUser, string> store, ApplicationUser user, string password)
        {
            return Task.FromResult<bool>(Authenticate(user.Id, password));
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var settings = LdapConnectionSettings.GetDefaults();

            var manager = new ApplicationUserManager(new LdapUserStorage(context.Get<ApplicationDbContext>(), settings));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
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
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
