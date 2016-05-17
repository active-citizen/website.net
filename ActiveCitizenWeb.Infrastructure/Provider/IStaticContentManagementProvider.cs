using System.Collections.Generic;
using ActiveCitizen.Model.StaticContent.Faq;

namespace ActiveCitizenWeb.Infrastructure.Provider
{
    public interface IStaticContentManagementProvider
    {
        List<FaqListCategory> GetAllFaqListCategories();

        List<FaqListItem> GetAllFaqListItems();

        FaqListCategory GetFaqListCategory(int id);

        FaqListItem GetFaqListItem(int id);

        bool IsFaqListCategoryDeletable(int id);

        bool IsFaqListItemExists(int id);

        void PostFaqListItem(FaqListItem item);
        void PostFaqListCategory(FaqListCategory category);

        void PutFaqListItem(FaqListItem item);
        void PutFaqListCategory(FaqListCategory category);

        void DeleteFaqListCategory(int id);
        void DeleteFaqListItem(int id);
    }
}