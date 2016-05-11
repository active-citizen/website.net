using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ActiveCitizenWeb.Infrastructure.UserManagement
{
    public class ApplicationIdentityDbContext : IdentityDbContext<LdapIdentityUser>
    {
        public ApplicationIdentityDbContext() : base("ActiveCitizen.Auth", throwIfV1Schema: false)
        {
        }
    }
}