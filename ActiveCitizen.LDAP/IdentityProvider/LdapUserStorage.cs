using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.DirectoryServices;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapUserStorage : UserStore<ApplicationUser>
        /*
        IUserStore<ApplicationUser>
        ,IUserPasswordStore<ApplicationUser, string>
        , IUserLockoutStore<ApplicationUser, string>
        ,IUserTwoFactorStore<ApplicationUser, string>
        ,IUserPhoneNumberStore<ApplicationUser, string>
        ,IUserLoginStore<ApplicationUser, string>
        */
    {
        public LdapUserStorage(ApplicationDbContext context, LdapConnectionSettings connectionSettings) : base(context)
        {
            this.connectionSettings = connectionSettings;
        }

        private readonly LdapConnectionSettings connectionSettings;
        
        public override Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        
        public override Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        /*
        public override Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var user = base.FindByIdAsync(userId).Result;

            if (user != null) return Task.FromResult(user);

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

                        user = new ApplicationUser
                        {
                            Id =
                            connectionSettings.UserIdAttributeName == "DN"
                            ? DN
                            : (string)result.Properties[connectionSettings.UserIdAttributeName][0],
                            UserName = (string)result.Properties[connectionSettings.UserNameAttributeName][0]
                        };
                        this.CreateAsync(user).Wait();
                        return base.FindByIdAsync(userId);
                    }
                }
            }
            catch (Exception ex) { Task.FromException(ex); }

            return Task.FromResult<ApplicationUser>(null);
        }
        */
        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = base.FindByNameAsync(userName).Result;

            if (user != null) return Task.FromResult(user);

            string ldapfilter = "(&(objectclass=person)({0}={1}))";

            try
            {
                string DN = "";
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + connectionSettings.Server + "/" + connectionSettings.BaseDN, connectionSettings.ServiceUserDN, connectionSettings.ServiceUserPassword, AuthenticationTypes.None))
                {
                    DirectorySearcher ds = new DirectorySearcher(entry);
                    ds.SearchScope = SearchScope.Subtree;
                    ds.Filter = string.Format(ldapfilter, connectionSettings.UserNameAttributeName, userName);
                    SearchResult result = ds.FindOne();
                    if (result != null)
                    {
                        DN = result.Path.Replace("LDAP://" + connectionSettings.Server + "/", "");

                        user = new ApplicationUser
                        {
                            Id =
                            connectionSettings.UserIdAttributeName == "DN"
                            ? DN
                            : (string)result.Properties[connectionSettings.UserIdAttributeName][0],
                            UserName = (string)result.Properties[connectionSettings.UserNameAttributeName][0]
                        };

                        base.CreateAsync(user).Wait();
                        return base.FindByNameAsync(userName);
                    }
                }
            }
            catch (Exception ex) { Task.FromException(ex); }

            return Task.FromResult<ApplicationUser>(null);
        }
        
        public override Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        protected override  void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.CompletedTask;
        }

        public override Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public override Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            return Task.FromResult(DateTimeOffset.Now.AddYears(-1));
        }

        public override Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            return Task.CompletedTask;
        }

        public override Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.FromResult(0);
        }

        public override Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.CompletedTask;
        }

        public override Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            return Task.FromResult(0);
        }

        public override Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult(false);
        }

        public override Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public override Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public override Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult(false);
        }

        public override Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            return Task.FromResult("");
        }

        public override Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        public override Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public override Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            return Task.FromResult((IList<UserLoginInfo>)(new List<UserLoginInfo> { new UserLoginInfo("LDAP", user.Id) }));
        }

        public override Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
    }
}