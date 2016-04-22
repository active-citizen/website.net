using System.Collections.Generic;
using ActiveCitizen.Model.StaticContent.FAQ;
using System;

namespace ActiveCitizenWeb.DataAccess.Provider
{
    public interface IStaticContentProvider : IDisposable
    {
        FaqListCategory DeleteFaqCategory(int id);
        FaqListItem DeleteFaqListItem(int id);
        List<FaqListCategory> GetAllCategories();
        List<FaqListItem> GetAllItems();
        FaqListCategory GetCategory(int id);
        FaqListItem GetFaqListItem(int id);
        bool IsFaqListCategoryDeletable(int id);
        bool IsFaqListItemExists(int id);
        void PostFaqItem(FaqListItem item);
        void PostFaqItem(FaqListCategory item);
        void PutFaqItem<T>(T item) where T : class;
    }
}