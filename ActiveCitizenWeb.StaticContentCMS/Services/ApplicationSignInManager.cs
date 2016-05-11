using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ActiveCitizenWeb.Infrastructure.UserManagement;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public class ApplicationSignInManager : SignInManager<LdapIdentityUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}