using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActiveCitizenWeb.DataAccess.Provider;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.FAQ;
using ActiveCitizen.Model.StaticContent.FAQ;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using AutoMapper;


namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    public class FAQController : Controller
    {
        private readonly IStaticContentProvider _staticContentProvider;
        private readonly IMapper _mapper;

        public FAQController(IStaticContentProvider staticContentProvider, IMapper mapper)
        {
            _staticContentProvider = staticContentProvider;
            _mapper = mapper;
        }

        // GET: FAQ
        public ActionResult Index()
        {
            QuestionsVM vm = new QuestionsVM();
            vm.Questions = _staticContentProvider.GetAllItems();
            return View(vm);
        }

        public ActionResult EditCategories()
        {
            return View();
        }

        public ActionResult EditQuestion(int id)
        {
            FaqListItem item = _staticContentProvider.GetFaqListItem(id) ?? new FaqListItem();

            QuestionVM vm = _mapper.Map<QuestionVM>(item);

            List<FaqListCategory> list = _staticContentProvider.GetAllCategories();

            vm.CategoryNames = list.Select(category => new SelectListItem
                    { Text = category.Name, Value = category.Id.ToString() }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        //TODO: how to use AllowHTM instead?
        public ActionResult SaveQuestion(QuestionVM vm)
        {
             return View(vm);
        }
    }
}