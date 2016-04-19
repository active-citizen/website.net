using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ActiveCitizen.Model.Shared
{
    public class FeedItemStyle
    {
        public static FeedItemStyle DefaultItemStyle = new FeedItemStyle("default-item");
        public static FeedItemStyle NewsItemStyle = new FeedItemStyle("news-item");
        public static FeedItemStyle CheckInItemStyle = new FeedItemStyle("checkin-item");
        public static FeedItemStyle PollItemStyle = new FeedItemStyle("poll-item");
        public static FeedItemStyle PublicHearingItemStyle = new FeedItemStyle("publichearing-item");
        public static FeedItemStyle SpecialNewsItemStyle = new FeedItemStyle("specialnews-item");
        public static FeedItemStyle ProfileItemStyle = new FeedItemStyle("profile-item");
        public static FeedItemStyle SocialItemStyle = new FeedItemStyle("social-item");
        public static FeedItemStyle Novelty = new FeedItemStyle("novelty-item");

        public static ICollection<FeedItemStyle> AllStyles = new List<FeedItemStyle> {
            NewsItemStyle,
            CheckInItemStyle,
            PollItemStyle,
            PublicHearingItemStyle,
            SpecialNewsItemStyle,
            ProfileItemStyle,
            SocialItemStyle,
            Novelty,
            DefaultItemStyle };

        internal string _style = string.Empty;

        public static implicit operator FeedItemStyle(string style)
        {
            if (style == null) return null;

            return AllStyles.FirstOrDefault(item => item._style == style) ?? DefaultItemStyle;
        }

        public static implicit operator string(FeedItemStyle style)
        {
            return style != null ? style.ToString() : null;
        }

        private FeedItemStyle(string style)
        {
            this._style = style;
        }

        public override string ToString()
        {
            return _style;
        }

        public override bool Equals(object obj)
        {
            var pair = obj as FeedItemStyle;
            return pair != null && pair._style == _style;
        }

        public override int GetHashCode()
        {
            return _style.GetHashCode();
        }
    }

    //represent an item that can be shown at the main feed
    //TODO: add to all objects that can be shown in main feed
    interface IFeedItem : ITitle
    {
        //can be null or empty
        string FeedDescription { get; set; }

        //redirect user to the appropriate element: vote, news, checkin and etc.
        Uri FeedAction { get; set; }

        bool IsVisibleAtFeed { get; set; }

        FeedItemStyle FeedStyle { get; }
    }
}
