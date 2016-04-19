using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;

namespace ActiveCitizen.Model.Vote
{
    //TODO: it should be integrated with lizaalert.org
    class LizaAlert : Poll
    {
        public override FeedItemStyle FeedStyle { get { return FeedItemStyle.PollItemStyle; } }
    }
}
