using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapUserStorage : UserStore<ApplicationUser>
    {
        private readonly LdapConnector ldapConnector;

        public LdapUserStorage(ApplicationDbContext context, LdapConnector ldapConnector) : base(context)
        {
            this.ldapConnector = ldapConnector;
        }

        public override Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        
        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = base.FindByNameAsync(userName).Result;

            if (user != null) return Task.FromResult(user);

            var userEntry = ldapConnector.SearchByUserName(userName);

            if (userEntry == null) return Task.FromResult<ApplicationUser>(null);

            user = new ApplicationUser
            {
                Id = userEntry.UserId,
                UserName = userEntry.UserName
            };

            base.CreateAsync(user).Wait();
            return base.FindByNameAsync(userName);
        }
        
        public override Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.CompletedTask;
        }

        public override Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

        public override Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(false);
        }
    }
}