using ActiveCitizen.Model.StaticContent.FAQ;
using ActiveCitizenWeb.DataAccess.Context;
using ActiveCitizenWeb.DataAccess.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizenWeb.Tests.ContentProviders
{
    [TestClass]
    public class StaticContentProviderTests
    {
        private StaticContentProvider provider;
        private Mock<IFaqContext> dbContextMock;

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void GetAllItems()
        {
            var data = new List<FaqListItem>();

            var dbItemSetMock = new Mock<IDbSet<FaqListItem>>();
            dbItemSetMock.Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());

            dbContextMock = new Mock<IFaqContext>();
            dbContextMock.SetupGet(c => c.FaqListItem).Returns(dbItemSetMock.Object);
            provider = new StaticContentProvider(dbContextMock.Object);

            var items = provider.GetAllItems();

            dbContextMock.Verify(i => i.FaqListItem, Times.Exactly(1), "GetAllItems failed to all FaqDbContext.FaqListCategory exactly one time");
        }

        [TestMethod]
        public void GetFaqListItem()
        {
            var data = new List<FaqListItem>
            {
                new FaqListItem { Id = 222 },
                new FaqListItem { Id = 111 }
            };

            var faqDbContextMock = new FaqDbContextMock(data);

            provider = new StaticContentProvider(faqDbContextMock);

            var item = provider.GetFaqListItem(111);

            Assert.AreEqual(111, item.Id);
        }
    }
}
