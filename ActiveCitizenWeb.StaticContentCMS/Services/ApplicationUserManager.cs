using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public class ApplicationUserManager : LdapUserManager<LdapIdentityUser>
    {
        public ApplicationUserManager(IUserStore<LdapIdentityUser> store, ILdapConnector ldapConnector)
            : base(store, ldapConnector)
        {
        }
    }
}