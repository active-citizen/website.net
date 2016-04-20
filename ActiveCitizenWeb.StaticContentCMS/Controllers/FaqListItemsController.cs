using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ActiveCitizen.Model.StaticContent.FAQ;
using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizenWeb.DataAccess.Provider;

namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    public class FaqListItemsController : ApiController
    {
        private readonly StaticContentProvider _staticContentProvider = null;

        public FaqListItemsController(StaticContentProvider provider)
        {
            _staticContentProvider = provider;
        }

        // GET: api/FaqListItems
        public IQueryable<FaqListItem> GetFaqListItem()
        {
            return _staticContentProvider.GetAllItems().AsQueryable();
        }

        // GET: api/FaqListItems/5
        [ResponseType(typeof(FaqListItem))]
        public IHttpActionResult GetFaqListItem(int id)
        {
            FaqListItem faqListItem = _staticContentProvider.GetFaqListItem(id);
            if (faqListItem == null)
            {
                return NotFound();
            }

            return Ok(faqListItem);
        }

        // PUT: api/FaqListItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFaqListItem(int id, FaqListItem faqListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faqListItem.Id)
            {
                return BadRequest();
            }

            if (!_staticContentProvider.IsFaqListItemExists(id))
            {
                return NotFound();
            }

            _staticContentProvider.PutFaqListItem(faqListItem);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FaqListItems
        [ResponseType(typeof(FaqListItem))]
        public IHttpActionResult PostFaqListItem(FaqListItem faqListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _staticContentProvider.PostFaqListItem(faqListItem);

            return CreatedAtRoute("DefaultApi", new { id = faqListItem.Id }, faqListItem);
        }

        // DELETE: api/FaqListItems/5
        [ResponseType(typeof(FaqListItem))]
        public IHttpActionResult DeleteFaqListItem(int id)
        {
            FaqListItem item = _staticContentProvider.DeleteFaqListItem(id);
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