using Microsoft.AspNet.Identity.EntityFramework;

namespace ActiveCitizen.LDAP.IdentityProvider
{
    public class LdapIdentityUser : IdentityUser
    {
        public bool IsLdapUser { get; set; }
    }
}