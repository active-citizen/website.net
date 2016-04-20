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
        private readonly LdapConnector ldapConnector;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, LdapConnector ldapConnector)
            : base(store)
        {
            this.ldapConnector = ldapConnector;
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
            return ldapConnector.Authenticate(userId, password);
                    }

        protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<ApplicationUser, string> store, ApplicationUser user, string password)
        {
            return Task.FromResult<bool>(Authenticate(user.Id, password));
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var ldapConnector = new LdapConnector(LdapConnectionSettings.GetDefaults());

            var manager = new ApplicationUserManager(new LdapUserStorage(context.Get<ApplicationDbContext>(), ldapConnector), ldapConnector);
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
}
