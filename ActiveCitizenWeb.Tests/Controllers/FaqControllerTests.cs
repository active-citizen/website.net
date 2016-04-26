using ActiveCitizen.Model.StaticContent.Faq;
using ActiveCitizenWeb.Infrastructure.Provider;
using ActiveCitizenWeb.StaticContentCMS.Controllers;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ActiveCitizenWeb.Tests.Controllers
{
    //push travis
    [TestClass]
    public class FaqControllerTests
    {
        private FaqListController faqController;

        [TestMethod]
        public void IndexFaqListItems()
        {
            /*
            var category1 = new FaqListCategory { Id = 1, Order = 1 };
            var category2 = new FaqListCategory { Id = 2, Order = 2 };
            var category3 = new FaqListCategory { Id = 3 };
            var category4 = new FaqListCategory { Id = 4 };

            var item1 = new FaqListItem { Id = 1, Order = 1, Category = category2 };
            var item2 = new FaqListItem { Id = 2, Order = 2, Category = category1 };
            var item3 = new FaqListItem { Id = 3, Order = 1, Category = category1 };

            var items = new List<FaqListItem> { item1, item2, item3 };
            var categories = new List<FaqListCategory> { category1, category2, category3, category4 };

            categories.ForEach(cat => cat.Items = items.Where(i => i.Category == cat).ToList());
            category4.Items = null;


            var providerMock = new Mock<IStaticContentProvider>();
            providerMock.Setup(p => p.GetAllItems()).Returns(items);
            providerMock.Setup(p => p.GetAllCategories()).Returns(categories);

            faqController = new FaqListController(providerMock.Object, null);

            var action = faqController.Index();

            Assert.IsInstanceOfType(action, typeof(ViewResult));
            var view = (ViewResult)action;

            Assert.IsInstanceOfType(view.Model, typeof(QuestionsVM));

            var model = (QuestionsVM)view.Model;

            // Number of elements
            Assert.AreEqual(3, model.Questions.Count);
            // Sort order
            Assert.AreEqual(0, model.Questions.IndexOf(item3));
            Assert.AreEqual(1, model.Questions.IndexOf(item2));
            Assert.AreEqual(2, model.Questions.IndexOf(item1));

            //Categories with no items
            Assert.IsTrue(
                model.Categories.Count == 2 &&
                model.Categories.IndexOf(category3) >= 0 &&
                model.Categories.IndexOf(category4) >= 0,
                "Categories list must contain only categories with empty Items list"
            );
            */
        }
    }
}
