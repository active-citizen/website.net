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
        private readonly StaticContentProvider _staticContentProvider;
        private readonly IMapper _mapper;

        public FAQController(StaticContentProvider staticContentProvider, IMapper mapper)
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
            CategoriesVM vm = new CategoriesVM();
            vm.Categories = _staticContentProvider.GetAllCategories();
            return View(vm);
        }

        public ActionResult EditCategory(int id)
        {
            FaqListCategory vm = new FaqListCategory();
            vm = _staticContentProvider.GetCategory(id);
            return View(vm);
        }

        public ActionResult EditQuestion(int id)
        {
            FaqListItem item = _staticContentProvider.GetFaqListItem(id) ?? new FaqListItem();

            QuestionVM vm = _mapper.Map<QuestionVM>(item);
            vm.CategoryNames = GetCategoryNames();

            return View(vm);
        }

        public ActionResult NewQuestion()
        {
            QuestionVM vm = new QuestionVM();
            vm.CategoryNames = GetCategoryNames();
            vm.Category = new FaqListCategory();

            //by defaul add to the first category
            vm.Category.Id = _staticContentProvider.GetAllCategories().First().Id;

            //and add to the end by default
            vm.Order = _staticContentProvider.GetCategory(vm.Category.Id).Items.Max(c => c.Order) + 10;

            return View("EditQuestion", vm);
        }

        private List<SelectListItem> GetCategoryNames()
        {
            List<FaqListCategory> listCategory = _staticContentProvider.GetAllCategories();

            List<SelectListItem> listSelect = listCategory.Select(category => new SelectListItem {
                Text = category.Name,
                Value = category.Id.ToString()
            }).ToList();

            return listSelect;
        }

        public ActionResult NewCategory()
        {
            FaqListCategory vm = new FaqListCategory();
            
            //by default next order
            vm.Order = _staticContentProvider.GetAllCategories().Max(c => c.Order) + 1;

            return View("EditCategory", vm);
        }

        [HttpPost]
        public ActionResult SaveQuestion(QuestionVM vm)
        {
            FaqListItem item = _mapper.Map<FaqListItem>(vm);
            item.Category = _staticContentProvider.GetCategory(item.Category.Id);

            if (item.Id > 0) _staticContentProvider.PutFaqItem(item);
            else _staticContentProvider.PostFaqItem(item);

            vm.CategoryNames = GetCategoryNames();

            return View("EditQuestion", vm);
        }

        [HttpPost]
        public ActionResult SaveCategory(FaqListCategory vm)
        {
            if (vm.Id > 0) _staticContentProvider.PutFaqItem(vm);
            else _staticContentProvider.PostFaqItem(vm);

            return View("Editcategory", vm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _staticContentProvider.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}