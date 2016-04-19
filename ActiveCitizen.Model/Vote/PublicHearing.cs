using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;

namespace ActiveCitizen.Model.Vote
{
    //public hearing https://ru.wikipedia.org/wiki/%D0%9F%D1%83%D0%B1%D0%BB%D0%B8%D1%87%D0%BD%D1%8B%D0%B5_%D1%81%D0%BB%D1%83%D1%88%D0%B0%D0%BD%D0%B8%D1%8F
    class PublicHearing : Poll
    {
        public override FeedItemStyle FeedStyle { get { return FeedItemStyle.PublicHearingItemStyle; } }
    }
}
