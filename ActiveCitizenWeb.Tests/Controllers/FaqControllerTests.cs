using ActiveCitizen.Model.StaticContent.FAQ;
using ActiveCitizenWeb.DataAccess.Provider;
using ActiveCitizenWeb.StaticContentCMS.Controllers;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.FAQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ActiveCitizenWeb.Tests.Controllers
{
    [TestClass]
    public class FaqControllerTests
    {
        private FAQController faqController;

        [TestMethod]
        public void IndexFaqListItems()
        {
            var items = new List<FaqListItem>();

            var providerMock = new Mock<IStaticContentProvider>();
            providerMock.Setup(p => p.GetAllItems()).Returns(items);
            faqController = new FAQController(providerMock.Object, null);

            var action = faqController.Index();

            Assert.IsInstanceOfType(action, typeof(ViewResult));
            var view = (ViewResult)action;

            Assert.IsInstanceOfType(view.Model, typeof(QuestionsVM));

            var model = (QuestionsVM)view.Model;

            Assert.AreSame(items, model.Questions);
        }
    }
}
