using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.UserManagement
{
    public class UserListEntryModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }

        [DisplayName("Логин")]
        public string UserName { get; set; }

        [DisplayName("Пользователь LDAP")]
        public bool IsLdapUser { get; set; }

        [DisplayName("Роли")]
        public string Roles { get; set; }

        public UserListEntryModel(LdapIdentityUser user, IEnumerable<IdentityRole> userRoles)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.IsLdapUser = user.IsLdapUser;
            this.Roles = string.Join(", ", userRoles.Select(role => role.Name));
        }
    }
}