using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.UserManagement
{
    public class UserCreateModel
    {
        [Display(Name = "Через LDAP сервис")]
        public bool IsLdapUser { get; set; }

        [Required(ErrorMessage = "Укажите {0}")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Укажите правильный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Роли")]
        public IList<SelectListItem> Roles { get; set; }

        public UserCreateModel() { }

        public UserCreateModel(IEnumerable<IdentityRole> allRoles)
        {
            this.Roles = allRoles.Select(role => new SelectListItem { Text = role.Name, Value = role.Name, Selected = false }).ToList();
        }

        public LdapIdentityUser Map(IEnumerable<IdentityRole> allRoles)
        {

            var user =
                new LdapIdentityUser
                {
                    IsLdapUser = this.IsLdapUser,
                    Email = this.Email,
                    UserName = this.UserName,
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
            if (string.IsNullOrWhiteSpace(UserName)) result.Add(new ModelValidationResult { MemberName = "UserName", Message = "Укажите логин" });

            if (!IsLdapUser && string.IsNullOrWhiteSpace(Email)) result.Add(new ModelValidationResult { MemberName = "Email", Message = "Укажите email" });
            if (!IsLdapUser && string.IsNullOrWhiteSpace(Password)) result.Add(new ModelValidationResult { MemberName = "Password", Message = "Укажите пароль" });

            if (IsLdapUser && !string.IsNullOrEmpty(Password)) result.Add(new ModelValidationResult { MemberName = "Password", Message = "Пароль не должен указываться при аутентификации через LDAP" });

            return result;
        }
    }
}