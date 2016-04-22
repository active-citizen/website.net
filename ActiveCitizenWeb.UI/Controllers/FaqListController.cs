using ActiveCitizenWeb.DataAccess;
using ActiveCitizenWeb.DataAccess.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActiveCitizenWeb.UI.Controllers
{
    public class FaqListController : Controller
    {
        private readonly IStaticContentProvider staticContentProvider;

        public FaqListController(IStaticContentProvider staticContentProvider)
        {
            this.staticContentProvider = staticContentProvider;
        }

        // GET: FaqList
        public string Index()
        {
            return string.Join(",", staticContentProvider.GetAllItems().Select(listItem => listItem.Question));
        }
    }
}