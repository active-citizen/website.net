using System.Web.Mvc;

namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}