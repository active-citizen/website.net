using ActiveCitizen.LDAP.IdentityProvider;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ActiveCitizenWeb.StaticContentCMS.Services
{
    public class ApplicationSignInManager : SignInManager<LdapIdentityUser, string>
    {
        public ApplicationSignInManager(UserManager<LdapIdentityUser, string> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}