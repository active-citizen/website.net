using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.UserManagement
{
    public class UserUpdateModel
    {
        public string Id { get; set; }

        [Display(Name = "Через LDAP сервис")]
        public bool IsLdapUser { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [EmailAddress(ErrorMessage = "Укажите правильный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Роли")]
        public IList<SelectListItem> Roles { get; set; }

        public UserUpdateModel() { }

        public UserUpdateModel(LdapIdentityUser user, IEnumerable<IdentityRole> allRoles)
        {
            this.Id = user.Id;
            this.Login = user.UserName;
            this.Email = user.Email;
            this.IsLdapUser = user.IsLdapUser;
            this.Roles = allRoles.Select(role =>
                    new SelectListItem
                    {
                        Text = role.Name,
                        Value = role.Name,
                        Selected = user.Roles.Any(userRole => role.Id == userRole.RoleId)
                    }).ToList();
        }

        public LdapIdentityUser Map(IEnumerable<IdentityRole> allRoles)
        {
            var user = new LdapIdentityUser
            {
                Id = this.Id,
                Email = this.Email
            };

            var rolesMap = allRoles.ToDictionary(role => role.Name);

            user.Roles.Clear();
            foreach (var roleModel in this.Roles)
            {
                if (!roleModel.Selected) continue;

                user.Roles.Add(
                    new IdentityUserRole { UserId = user.Id, RoleId = rolesMap[roleModel.Value].Id });
            }

            return user;
        }

        public IEnumerable<ModelValidationResult> Validate()
        {
            var result = new List<ModelValidationResult>();
            if (!IsLdapUser && string.IsNullOrWhiteSpace(Email)) result.Add(new ModelValidationResult { MemberName = "Email", Message = "Укажите email" });
            if (IsLdapUser && !string.IsNullOrEmpty(Password)) result.Add(new ModelValidationResult { MemberName = "Password", Message = "Пароль не должен указываться при аутентификации через LDAP" });

            return result;
        }
    }
}