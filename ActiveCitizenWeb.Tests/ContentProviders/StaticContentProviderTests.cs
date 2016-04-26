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
        private IStaticContentProvider provider;
        private Mock<IFaqContext> dbContextMock;

        

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void GetAllItems()
        {
            var data = new List<FaqListItem>();
            data.Add(new FaqListItem());
            data.Add(new FaqListItem());
            data.Add(new FaqListItem() { Id = 10 });

            var dbItemSetMock = new Mock<IDbSet<FaqListItem>>();
            dbItemSetMock.Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());

            dbContextMock = new Mock<IFaqContext>();
            dbContextMock.SetupGet(c => c.FaqListItem).Returns(dbItemSetMock.Object);
            provider = new StaticContentProvider(dbContextMock.Object);

            var items = provider.GetAllItems();

            dbContextMock.Verify(i => i.FaqListItem, Times.Exactly(1), "GetAllItems failed to all FaqDbContext.FaqListCategory exactly one time");
            Assert.AreEqual(3, items.Count);
            Assert.IsNotNull(items.Find(c => c.Id == 10));
        }

        [TestMethod]
        public void GetFaqListItem()
        {
            var data = new List<FaqListItem>
            {
                new FaqListItem { Id = 222 },
                new FaqListItem { Id = 111 }
            };

            var faqDbContextMock = new FaqDbContextMock(data, null);

            provider = new StaticContentProvider(faqDbContextMock);

            var item = provider.GetFaqListItem(111);

            Assert.AreEqual(111, item.Id);
        }

        [TestMethod]
        public void IsFaqListItemExists()
        {
            var data = new List<FaqListItem>
            {
                new FaqListItem { Id = 123 }
            };

            var dbContext = new FaqDbContextMock(data, null);

            provider = new StaticContentProvider(dbContext);

            Assert.IsTrue(provider.IsFaqListItemExists(123));
            Assert.IsFalse(provider.IsFaqListItemExists(321));
        }

        [TestMethod]
        public void IsFaqListCategoryDeletable()
        {
            var items = new List<FaqListItem> { new FaqListItem() { Id = 1 } };
            var cats = new List<FaqListCategory>
            {
                new FaqListCategory() { Items = items, Id = 1 },
                new FaqListCategory() { Id = 2 }
            };
            items.Find(i => i.Id ==1).Category = cats.Find(c => c.Id == 1);

            var dbContext = new FaqDbContextMock(items, cats);

            provider = new StaticContentProvider(dbContext);

            Assert.IsTrue(provider.IsFaqListCategoryDeletable(2));
            Assert.IsFalse(provider.IsFaqListCategoryDeletable(1));
        }

        //[TestMethod]
        //public void PutFaqItem()
        //{
        //    FaqListCategory cat = new FaqListCategory() { Id = 1, Name = "test" };
        //    FaqListItem item = new FaqListItem() { Id = 1, Question = "test" };

        //    FaqDbContextMock context = new FaqDbContextMock(
        //        new List<FaqListItem> { item }, 
        //        new List<FaqListCategory> { cat }
        //        );

        //    provider = new StaticContentProvider(context);

        //    
        //    provider.PutFaqItem<FaqListCategory>(new FaqListCategory() { Id = 1, Name = "name" });

        //    Assert.AreEqual(1, context.FaqListCategory.Count());
        //    Assert.AreEqual("name", context.FaqListCategory.First(c => c.Id == 1).Name);
        //}

        //[TestMethod]
        //public void PostFaqItem()
        //{
        //    FaqListItem item = new FaqListItem() { Id = 1, Question = "test" };
        //    FaqDbContextMock context = new FaqDbContextMock( new List<FaqListItem> { item }, null);
        //    provider = new StaticContentProvider(context);

        //    provider.PostFaqItem(item);
        //    TODO - add verify SaveChanges 
        //    context.Verify();
        //}


    }
}
