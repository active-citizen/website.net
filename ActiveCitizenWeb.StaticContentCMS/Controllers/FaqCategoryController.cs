using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActiveCitizenWeb.DataAccess.Provider;
using ActiveCitizen.Model.StaticContent.FAQ;
using System.Web.Http.Description;

namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    public class FaqCategoryController : ApiController
    {
        private readonly StaticContentProvider _staticContentProvider = null;

        public FaqCategoryController(StaticContentProvider provider)
        {
            _staticContentProvider = provider;
        }

        // DELETE: api/FaqCategory/5
        [ResponseType(typeof(FaqListCategory))]
        public IHttpActionResult DeleteFaqCategory(int id)
        {
            if (!_staticContentProvider.IsFaqListCategoryDeletable(id))
            { 
                return Content(HttpStatusCode.Conflict, StaticContentProvider.CannotDelete);
            }

            FaqListCategory item = _staticContentProvider.DeleteFaqCategory(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
         }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _staticContentProvider.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
