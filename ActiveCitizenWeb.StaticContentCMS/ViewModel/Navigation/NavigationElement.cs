using Microsoft.Owin;
using System.Linq;

namespace ActiveCitizenWeb.StaticContentCMS.ViewModel.Navigation
{
    public class NavigationElement
    {
        public string MenuLabel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] ShowForRoles { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public bool IsVisible(IOwinContext context)
        {
            return ShowForRoles.Any(roleName => context.Authentication.User.IsInRole(roleName));
        }
    }
}