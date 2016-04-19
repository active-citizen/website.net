using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;
using ActiveCitizen.Common.Attributes;
using System.Device.Location;

namespace ActiveCitizen.Model.CheckIn
{
    [Available(Device = DeviceType.MobileApp)]
    class CheckIn : IPoint, IFeedItem, IAction 
    {
        public string Title { get; set; }

        #region IFeed
        public Uri FeedAction { get; set; }
        public string FeedDescription { get; set; }
        public bool IsVisibleAtFeed { get; set; }
        public FeedItemStyle FeedStyle { get { return FeedItemStyle.CheckInItemStyle; } }
        #endregion IFeed

        #region IAction
        public bool IsActionComplete { get; set; }
        public bool IsActionPassed { get; set; }
        public DateTime? ActionCompleteDateTime { get; set; }
        public IPoint CreditedWithPoints { get; set; }
        #endregion IAction

        public int PointsAmount { get; set; }

        public string ShortDescription { get; set; }
        public HtmlBody Description { get; set; }

        public GeoCoordinate Coordinates { get; set; }

        //in meters
        public int Radius { get; set; }
    }
}
