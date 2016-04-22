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

            vm.Categories = _staticContentProvider.GetAllCategories()
                .Where(c => (c.Items == null || c.Items.Count == 0)).ToList();

            //sort by category and then by order
            vm.Questions.Sort(delegate(FaqListItem a, FaqListItem b) {
                if (a.Category.Order > b.Category.Order) return 1;
                else if (a.Category.Order < b.Category.Order) return -1;
                else
                {
                    if (a.Order > b.Order) return 1;
                    else if (a.Order < b.Order) return -1;
                    else return 0;
                }
            });

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
            FaqListCategory category = _staticContentProvider.GetAllCategories().First();
            vm.Category.Id = category.Id;
            vm.Category.Name = category.Name;

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
            if (ModelState.IsValid)
            {
                FaqListItem item = _mapper.Map<FaqListItem>(vm);
                item.Category = _staticContentProvider.GetCategory(item.Category.Id);

                if (item.Id > 0) _staticContentProvider.PutFaqItem(item);
                else _staticContentProvider.PostFaqItem(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("EditQuestion", vm);
            }
        }

        [HttpPost]
        public ActionResult SaveCategory(FaqListCategory vm)
        {
            if(ModelState.IsValid)
            { 
                if (vm.Id > 0) _staticContentProvider.PutFaqItem(vm);
                else _staticContentProvider.PostFaqItem(vm);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("EditCategory", vm);
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            if (_staticContentProvider.IsFaqListCategoryDeletable(id))
            {
                _staticContentProvider.DeleteFaqCategory(id);
                return RedirectToAction("Index");
            }
            else
            {
                //TODO: add error presentation at separate box
                TempData["Error"] = "Нельзя удалить раздел, так как в нем содержаться вопросы. Вначале удалите/перенесите все вопросы из раздела и потом удаляйте раздел.";
                return RedirectToAction("EditCategory/" + id.ToString());
            }
        }

        public ActionResult DeleteQuestion(int id)
        {
            _staticContentProvider.DeleteFaqListItem(id);
            return RedirectToAction("Index");
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