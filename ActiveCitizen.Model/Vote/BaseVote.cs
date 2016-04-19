using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;

namespace ActiveCitizen.Model.Vote
{
    //base class for all user voting
    abstract class BaseVote : IPoint, IFeedItem, IAction
    {
        public int PointsAmount { get; set; }

        public string Title { get; set; }

        #region IFeed
        public Uri FeedAction { get; set; }
        public string FeedDescription { get; set; }
        public bool IsVisibleAtFeed { get; set; }
        public virtual FeedItemStyle FeedStyle { get { return FeedItemStyle.DefaultItemStyle; } }
        #endregion IFeed

        #region IAction
        public bool IsActionComplete { get; set; }
        public bool IsActionPassed { get; set; }
        public DateTime? ActionCompleteDateTime { get; set; }
        public IPoint CreditedWithPoints { get; set; }
        #endregion IAction
    }
}
