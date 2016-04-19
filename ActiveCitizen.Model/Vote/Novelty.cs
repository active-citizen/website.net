using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;

namespace ActiveCitizen.Model.Vote
{
    //city novelty
    class Novelty : BaseVote
    {
        public override FeedItemStyle FeedStyle { get { return FeedItemStyle.Novelty; } }
    }
}
