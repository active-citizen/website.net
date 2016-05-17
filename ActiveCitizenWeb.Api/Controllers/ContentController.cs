using ActiveCitizen.Model.StaticContent.Faq.Api;
using ActiveCitizenWeb.Infrastructure.Provider;
using System.Collections.Generic;
using System.Web.Http;

namespace ActiveCitizenWeb.Api.Controllers
{
    [RoutePrefix("api/content")]
    public class ContentController : ApiController
    {
        private readonly IStaticContentProvider staticContentProvider;

        public ContentController(IStaticContentProvider staticContentProvider)
        {
            this.staticContentProvider = staticContentProvider;
        }

        [Route("faq")]
        public IEnumerable<FaqListCategory> GetFaqList()
        {
            return staticContentProvider.GetFaqList();
        }
    }
}
