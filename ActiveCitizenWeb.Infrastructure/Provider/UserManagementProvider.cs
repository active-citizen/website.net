using ActiveCitizen.LDAP.IdentityProvider;
using ActiveCitizenWeb.Infrastructure.UserManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace ActiveCitizenWeb.Infrastructure.Provider
{
    public class UserManagementProvider : IUserManagementProvider
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;

        public UserManagementProvider(ApplicationUserManager userManager,  ApplicationRoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IQueryable<LdapIdentityUser> GetUsers()
        {
            return userManager.Users;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return roleManager.Roles;
        }

        public IEnumerable<IdentityRole> GetUserRoles(LdapIdentityUser user)
        {
            return user.Roles.ToList().Select(role => roleManager.FindById(role.RoleId));
        }

        public IEnumerable<string> CreateUser(LdapIdentityUser user, string password)
        {
            IdentityResult result;
            if (user.IsLdapUser)
            {
                result = userManager.CreateUsingLdap(user);
            }
            else
            {
                result = userManager.Create(user, password);
            }

            return result.Errors;
        }

        public LdapIdentityUser GetUserById(string id)
        {
            return userManager.FindById(id);
        }

        public void DeleteUser(string id)
        {
            var user = userManager.FindById(id);

            var result = userManager.Delete(user);
        }

        public IEnumerable<string> UpdateUser(LdapIdentityUser user)
        {
            var existingUser = userManager.FindById(user.Id);

            existingUser.Email = user.Email;
            existingUser.Roles.Clear();
            foreach (var role in user.Roles)
            {
                existingUser.Roles.Add(role);
            }

            var result = userManager.Update(existingUser);
            return result.Errors;
        }

        public IEnumerable<string> UpdateUserPassword(string userId, string password)
        {
            var resetToken = userManager.GeneratePasswordResetToken(userId);
            var results = userManager.ResetPassword(userId, resetToken, password);

            return results.Errors;
        }
    }
}
