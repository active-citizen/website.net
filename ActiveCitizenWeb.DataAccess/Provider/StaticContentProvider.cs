using ActiveCitizen.Model.StaticContent.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCitizenWeb.DataAccess.Context;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ActiveCitizenWeb.DataAccess.Provider
{
    public class StaticContentProvider : IDisposable
    {
        private readonly FaqContext faqDbContext;

        public StaticContentProvider(FaqContext faqDbContext)
        {
            this.faqDbContext = faqDbContext;
        }

        public List<FaqListItem> GetAllItems()
        {
            return faqDbContext.FaqListItem.ToList<FaqListItem>();
        }

        public FaqListItem GetFaqListItem(int id)
        {
            return faqDbContext.FaqListItem.Find(id);
        }

        public bool IsFaqListItemExists(int id)
        {
            return faqDbContext.FaqListItem.Count(e => e.Id == id) > 0;
        }

        public void PutFaqListItem(FaqListItem item)
        {
            try
            {
                faqDbContext.Entry(item).State = EntityState.Modified;
                faqDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO - create error page with explanation
                throw;
            }
        }

        public void PostFaqListItem(FaqListItem item)
        {
            faqDbContext.FaqListItem.Add(item);
            faqDbContext.SaveChanges();
        }

        public FaqListItem DeleteFaqListItem(int id)
        {
            FaqListItem item = GetFaqListItem(id);

            if (item != null)
            {
                faqDbContext.FaqListItem.Remove(item);
                faqDbContext.SaveChanges();
            }

            return item;
        }

        public List<FaqListCategory> GetAllCategories()
        {
            return faqDbContext.FaqListCategory.ToList<FaqListCategory>();
        }

        public void Dispose()
        {
            faqDbContext.Dispose();
        }
    }
}