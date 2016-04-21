using ActiveCitizen.Model.StaticContent.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCitizenWeb.DataAccess.Context;

namespace ActiveCitizenWeb.DataAccess.Provider
{
    public class StaticContentProvider
    {
        private readonly IFaqContext faqDbContext;

        public StaticContentProvider(IFaqContext faqDbContext)
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

        public List<FaqListCategory> GetAllCategories()
        {
            return faqDbContext.FaqListCategory.ToList<FaqListCategory>();
        }
    }
}