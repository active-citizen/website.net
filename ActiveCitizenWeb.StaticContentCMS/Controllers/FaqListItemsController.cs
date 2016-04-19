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

namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    public class FaqListItemsController : ApiController
    {
        private readonly FaqContext db = null;

        public FaqListItemsController(FaqContext context)
        {
            db = context;
        }

        // GET: api/FaqListItems
        public IQueryable<FaqListItem> GetFaqListItem()
        {
            return db.FaqListItem;
        }

        // GET: api/FaqListItems/5
        [ResponseType(typeof(FaqListItem))]
        public IHttpActionResult GetFaqListItem(int id)
        {
            FaqListItem faqListItem = db.FaqListItem.Find(id);
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

            db.Entry(faqListItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaqListItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

            db.FaqListItem.Add(faqListItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = faqListItem.Id }, faqListItem);
        }

        // DELETE: api/FaqListItems/5
        [ResponseType(typeof(FaqListItem))]
        public IHttpActionResult DeleteFaqListItem(int id)
        {
            FaqListItem faqListItem = db.FaqListItem.Find(id);
            if (faqListItem == null)
            {
                return NotFound();
            }

            db.FaqListItem.Remove(faqListItem);
            db.SaveChanges();

            return Ok(faqListItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FaqListItemExists(int id)
        {
            return db.FaqListItem.Count(e => e.Id == id) > 0;
        }
    }
}