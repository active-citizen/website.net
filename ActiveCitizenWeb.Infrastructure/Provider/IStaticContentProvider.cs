using System.Collections.Generic;
using ActiveCitizen.Model.StaticContent.Faq.Api;

namespace ActiveCitizenWeb.Infrastructure.Provider
{
    public interface IStaticContentProvider
    {
        IEnumerable<FaqListCategory> GetFaqList();
    }
}