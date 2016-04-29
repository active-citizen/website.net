using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapUserManager<TUser> : UserManager<TUser> where TUser : LdapIdentityUser
    {
        public const string LdapProviderId = "LDAP";

        private readonly ILdapConnector ldapConnector;

        public LdapUserManager(IUserStore<TUser> store, ILdapConnector ldapConnector)
            : base(store)
        {
            this.ldapConnector = ldapConnector;
        }

        public async Task<IdentityResult> CreateUsingLdapAsync(TUser user)
        {
            var loginName = user.UserName;
            LdapEntry ldapEntry;
            try
            {
                ldapEntry = await Task.Run(() => ldapConnector.SearchByUserName(loginName));
                if (ldapEntry == null)
                {
                    return new IdentityResult(
                        string.Format("Login name {0} is not found in the LDAP catalog.", loginName));
                }
            }
            catch (Exception ex)
            {
                //TODO log exception
                return new IdentityResult("Failed to search user login in LDAP catalog.");
            }

            user.IsLdapUser = true;
            user.UserName = ldapConnector.GetUserUniqueName(ldapEntry.UserName);

            var result = await base.CreateAsync(user);

            if (!result.Succeeded) return result;

            result = await this.AddLoginAsync(user.Id, new UserLoginInfo(LdapProviderId, ldapEntry.UserId));

            return result;
        }

        public override async Task<TUser> FindByNameAsync(string userName)
        {
            // Priority of user search: local users first, then LDAP
            var user = await base.FindByNameAsync(userName);

            if (user != null) return user;

            var ldapUserName = ldapConnector.GetUserUniqueName(userName);

            var userEntry = await Store.FindByNameAsync(ldapUserName);

            if (userEntry == null || !userEntry.IsLdapUser) return null;

            return userEntry;
        }

        private bool Authenticate(string userId, string password)
        {
            return ldapConnector.Authenticate(userId, password);
        }

        protected override async Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, string> store, TUser user, string password)
        {
            if (user.IsLdapUser)
            {
                var ldapUserId = user.Logins
                    .Where(login => login.LoginProvider == LdapProviderId)
                    .Select(login => login.ProviderKey)
                    .FirstOrDefault();

                if (ldapUserId == null)
                {
                    return false;
                }

                return await Task.Run<bool>(() => Authenticate(ldapUserId, password));
            }

            return await base.VerifyPasswordAsync(store, user, password);
        }
    }
}
