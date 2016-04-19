using System;
using System.Collections.Generic;

namespace ActiveCitizen.Model.StaticContent.FAQ
{
    public class FaqListCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public IEnumerable<FaqListItem> Items { get; set; }
    }
}
