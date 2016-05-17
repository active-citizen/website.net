using System.Linq;
using System.Web.Mvc;
using ActiveCitizenWeb.StaticContentCMS.ViewModel.Faq;
using ActiveCitizen.Model.StaticContent.Faq;
using AutoMapper;
using ActiveCitizenWeb.Infrastructure.Provider;
using ActiveCitizen.Common;

namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    [Authorize(Roles= AgConsts.Roles.FaqListEditor)]
    public class FaqListController : Controller
    {
        public static class ErrorCodes
        {
            public const string CannotDeleteCategoryThatContainsQuestions = "CategoryNotEmpty";
        }

        private readonly IStaticContentManagementProvider _staticContentProvider;
        private readonly IMapper _mapper;

        public FaqListController(IStaticContentManagementProvider staticContentProvider, IMapper mapper)
        {
            _staticContentProvider = staticContentProvider;
            _mapper = mapper;
        }

        // GET: /FaqList
        public ActionResult Index()
        {
            var allCategories = _staticContentProvider.GetAllFaqListCategories();
            var model = new QuestionsVM
            {
                Questions = _staticContentProvider
                    .GetAllFaqListItems()
                    .OrderBy(q => q.Category.Order)
                    .ThenBy(q => q.Order)
                    .ToList(),
                EmptyCategories = allCategories
                    .Where(c => (c.Items == null || c.Items.Count == 0)).ToList(),
                CanAddQuestions = allCategories.Any()
            };

            return View(model);
        }

        private void BindCategoriesList(QuestionVM questionModel)
        {
            var listCategory = _staticContentProvider.GetAllFaqListCategories();

            if (questionModel.CategoryId == 0 && listCategory.Any())
            {
                questionModel.CategoryId = listCategory.First().Id;
            }

            questionModel.CategoryNames = new SelectList(listCategory, "Id", "Name", questionModel.CategoryId);
        }

        // GET: /FaqList/EditQuestion/{id}
        public ActionResult EditQuestion(int id)
        {
            var item = _staticContentProvider.GetFaqListItem(id) ?? new FaqListItem();

            var model = _mapper.Map<QuestionVM>(item);

            BindCategoriesList(model);

            return View(model);
        }

        // GET: /FaqList/NewQuestion
        public ActionResult NewQuestion()
        {
            var model = new QuestionVM();
            BindCategoriesList(model);

            //and add to the end by default
            var categoryItems = _staticContentProvider.GetFaqListCategory(model.CategoryId).Items;
            model.Order = categoryItems.Any() ? categoryItems.Max(c => c.Order) + 10 : 10;

            return View("EditQuestion", model);
        }

        // POST: /FaqList/NewQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewQuestion(QuestionVM model)
        {
            return HandleSaveQuestion(model);
        }

        // POST: /FaqList/EditQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(QuestionVM model)
        {
            return HandleSaveQuestion(model);
        }

        private ActionResult HandleSaveQuestion(QuestionVM model)
        {
            if (ModelState.IsValid)
            {
                var item = _mapper.Map<FaqListItem>(model);

                if (item.Id > 0)
                {
                    _staticContentProvider.PutFaqListItem(item);
                }
                else
                {
                    _staticContentProvider.PostFaqListItem(item);
                }

                return RedirectToAction("Index");
            }
            else
            {
                BindCategoriesList(model);
                return View("EditQuestion", model);
            }
        }

        // GET: /FaqList/EditCategory/{id}
        public ActionResult EditCategory(int id)
        {
            var model = _staticContentProvider.GetFaqListCategory(id);

            return View(model);
        }

        // GET: /FaqList/NewCategory
        public ActionResult NewCategory()
        {
            var model = new FaqListCategory();

            //by default next order
            var allCategories = _staticContentProvider.GetAllFaqListCategories();
            model.Order = allCategories.Any() ? allCategories.Max(c => c.Order) + 1 : 1;

            return View("EditCategory", model);
        }

        // POST: /FaqList/NewCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCategory(FaqListCategory model)
        {
            return HandleSaveCategory(model);
        }

        // POST: /FaqList/EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(FaqListCategory model)
        {
            return HandleSaveCategory(model);
        }

        private ActionResult HandleSaveCategory(FaqListCategory model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    _staticContentProvider.PutFaqListCategory(model);
                }
                else
                {
                    _staticContentProvider.PostFaqListCategory(model);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("EditCategory", model);
            }
        }

        // POST: /FaqList/DeleteCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            if (_staticContentProvider.IsFaqListCategoryDeletable(id))
            {
                _staticContentProvider.DeleteFaqListCategory(id);

                return RedirectToAction("Index");
            }
            else
            {
                //TODO: add error presentation at separate box
                TempData["Error"] = ErrorCodes.CannotDeleteCategoryThatContainsQuestions;
                return RedirectToAction("EditCategory", new { id = id });
            }
        }

        // POST: /FaqList/DeleteQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int id)
        {
            _staticContentProvider.DeleteFaqListItem(id);

            return RedirectToAction("Index");
        }
    }
}