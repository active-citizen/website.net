using System.Collections.Generic;
using System.Linq;
using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ActiveCitizenWeb.Infrastructure.Provider
{
    public interface IUserManagementProvider
    {
        IEnumerable<IdentityRole> GetAllRoles();
        IEnumerable<IdentityRole> GetUserRoles(LdapIdentityUser user);
        IQueryable<LdapIdentityUser> GetUsers();
        IEnumerable<string> CreateUser(LdapIdentityUser ldapIdentityUser, string password);
        LdapIdentityUser GetUserById(string id);
        void DeleteUser(string id);
        IEnumerable<string> UpdateUser(LdapIdentityUser user);
        IEnumerable<string> UpdateUserPassword(string userId, string password);
    }
}