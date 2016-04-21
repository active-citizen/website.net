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

        public bool IsFaqListCategoryDeletable(int id)
        {
            bool any = faqDbContext.FaqListItem.Any(e => e.Category.Id == id);

            return !any;
        }
        public static string CannotDelete = @"Нельзя удалить раздел, так как в разделе есть вопросы, вначале удалите все вопросы из раздела, а потом удаляйте раздел.";

        public void PutFaqItem<T>(T item) where T : class
        {
            try
            {
                faqDbContext.Entry<T>(item).State = EntityState.Modified;
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO - create error page with explanation
                throw;
            }
        }

        public void PostFaqItem(FaqListItem item)
        {
            faqDbContext.FaqListItem.Add(item);
            SaveChanges();
        }

        public void PostFaqItem(FaqListCategory item)
        {
            faqDbContext.FaqListCategory.Add(item);
            SaveChanges();
        }

        public FaqListCategory GetCategory(int id)
        {
            return faqDbContext.FaqListCategory.Find(id);
        }

        public FaqListItem DeleteFaqListItem(int id)
        {
            FaqListItem item = GetFaqListItem(id);

            if (item != null)
            {
                faqDbContext.FaqListItem.Remove(item);
                SaveChanges();
            }

            return item;
        }

        public FaqListCategory DeleteFaqCategory(int id)
        {
            FaqListCategory item = GetCategory(id);

            if (item != null)
            {
                faqDbContext.FaqListCategory.Remove(item);
                SaveChanges();
            }

            return item;
        }

        public List<FaqListCategory> GetAllCategories()
        {
            return faqDbContext.FaqListCategory.ToList<FaqListCategory>();
        }

        private void SaveChanges()
        {
            try
            {
                faqDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TODO delete it and check, that context dispose is called 
        public void Dispose()
        {
            faqDbContext.Dispose();
        }
    }
}