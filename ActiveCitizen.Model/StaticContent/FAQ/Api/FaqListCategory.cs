using System.Collections.Generic;

namespace ActiveCitizen.Model.StaticContent.Faq.Api
{
    public class FaqListCategory
    {
        public string Name { get; set; }
        public ICollection<FaqListItem> Items { get; set; }
    }
}