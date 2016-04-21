using System.Data.Entity;
using ActiveCitizen.Model.StaticContent.FAQ;

namespace ActiveCitizenWeb.DataAccess.Context
{
    public interface IFaqContext
    {
        IDbSet<FaqListCategory> FaqListCategory { get; set; }
        IDbSet<FaqListItem> FaqListItem { get; set; }
    }
}