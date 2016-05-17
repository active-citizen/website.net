using ActiveCitizenWeb.Infrastructure.Provider;
using System.Linq;
using System.Web.Mvc;

namespace ActiveCitizenWeb.UI.Controllers
{
    public class FaqListController : Controller
    {
        private readonly IStaticContentManagementProvider staticContentProvider;

        public FaqListController(IStaticContentManagementProvider staticContentProvider)
        {
            this.staticContentProvider = staticContentProvider;
        }

        // GET: FaqList
        public string Index()
        {
            return string.Join(",", staticContentProvider.GetAllFaqListItems().Select(listItem => listItem.Question));
        }
    }
}