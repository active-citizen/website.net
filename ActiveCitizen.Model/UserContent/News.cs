using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;

namespace ActiveCitizen.Model.UserContent
{
    //news for users
    class News : IFeedItem
    {
        public Shared.Image TitleImage { get; set; }
        public HtmlBody InnerBody { get; set; }
        public DateTime PublishDate { get; set; }

        public string Title { get; set; }

        #region IFeed
        public Uri FeedAction { get; set; }
        public string FeedDescription { get; set; }
        public bool IsVisibleAtFeed { get; set; }
        public FeedItemStyle FeedStyle { get { return FeedItemStyle.NewsItemStyle; } }
        #endregion IFeed
    }
}
