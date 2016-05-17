using System;
using System.Collections.Generic;
using System.Linq;
using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizen.Model.StaticContent.Faq.Api;

namespace ActiveCitizenWeb.Infrastructure.Provider
{
    public class StaticContentProvider : IStaticContentProvider
    {
        private readonly IStaticContentDbContext staticContentDbContext;

        public StaticContentProvider(IStaticContentDbContext staticContentDbContext)
        {
            this.staticContentDbContext = staticContentDbContext;
        }

       public IEnumerable<FaqListCategory> GetFaqList()
        {
            var categories = staticContentDbContext.FaqListCategory.GetAll()
                .OrderBy(category => category.Order)
                .Select(category => new FaqListCategory
                {
                    Name = category.Name,
                    Items = category.Items.OrderBy(item => item.Order).
                        Select(item => new FaqListItem { Answer = item.Answer, Question = item.Question }).ToList()
                }).ToList();

            return categories;
        }
    }
}