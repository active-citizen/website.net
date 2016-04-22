using System.Collections.Generic;
using ActiveCitizen.Model.StaticContent.FAQ;

namespace ActiveCitizenWeb.DataAccess.Provider
{
    public interface IStaticContentProvider
    {
        List<FaqListCategory> GetAllCategories();
        List<FaqListItem> GetAllItems();
        FaqListItem GetFaqListItem(int id);
    }
}