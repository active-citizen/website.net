using ActiveCitizen.Model.StaticContent.Faq;

namespace ActiveCitizenWeb.DataAccess.Context
{
    public interface IStaticContentDbContext
    {
        IRepository<int, FaqListCategory> FaqListCategory { get; }

        IRepository<int, FaqListItem> FaqListItem { get; }
    }
}
