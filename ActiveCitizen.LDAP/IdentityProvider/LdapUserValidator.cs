using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    // Never validate email is present and/or email is unique for LDAP users
    public class LdapUserValidator<TUser> : IIdentityValidator<TUser> where TUser : LdapIdentityUser
    {
        private readonly UserManager<TUser, string> userManager;

        public bool AllowOnlyAlphanumericUserNames { get; set; }
        public bool RequireUniqueEmail { get; set; }

        public LdapUserValidator(UserManager<TUser, string> manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            this.AllowOnlyAlphanumericUserNames = true;
            this.userManager = manager;
        }

        public async Task<IdentityResult> ValidateAsync(TUser item)
        {
            var isLdapUser = item.IsLdapUser;
            var validator = new UserValidator<TUser, string>(userManager)
            {
                AllowOnlyAlphanumericUserNames = isLdapUser ? false : AllowOnlyAlphanumericUserNames,
                RequireUniqueEmail = isLdapUser ? false : RequireUniqueEmail
            };

            return await validator.ValidateAsync(item);
        }
    }
}