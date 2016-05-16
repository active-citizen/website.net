using ActiveCitizenWeb.StaticContentCMS.ViewModel.UserManagement;
using System.Linq;
using System.Web.Mvc;
using ActiveCitizenWeb.Infrastructure.Provider;
using System;
using AutoMapper;
using ActiveCitizen.Common;

namespace ActiveCitizenWeb.StaticContentCMS.Controllers
{
    [Authorize(Roles = AgConsts.Roles.Admin)]
    public class UserManagementController : Controller
    {
        public static class ErrorCodes
        {
            public const string FailedToDeleteUser = "FailedToDeleteUser";
        }

        private readonly IUserManagementProvider provider;
        private readonly IMapper mapper;


        public UserManagementController(IUserManagementProvider provider, IMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        // GET: UserManagement
        public ActionResult Index()
        {
            var users = provider
                .GetUsers()
                .ToList()
                .Select(user => new UserListEntryModel(user, provider.GetUserRoles(user)));

            return View(users); 
        }

        // GET: UserManagement/Create
        public ActionResult Create()
        {
            var roles = provider.GetAllRoles().ToList();

            var model = new UserCreateModel(roles);

            return View(model);
        }

        // POST: UserManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateModel model)
        {
            var validationResults = model.Validate();
            if (validationResults.Any())
            {
                foreach (var result in validationResults)
                {
                    ModelState.AddModelError(result.MemberName, result.Message);
                }
                return View(model);
            }

            try
            {
                var user = model.Map(provider.GetAllRoles());

                var results = provider.CreateUser(user, model.Password);

                if (results.Any())
                {
                    foreach (var result in results)
                    {
                        ModelState.AddModelError("", result);
                    }

                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO add to log
                ModelState.AddModelError("", "Произошла ошибка при создании нового пользователя.");
                return View(model);
            }
        }

        // GET: UserManagement/Edit/{guid}
        public ActionResult Edit(string id)
        {
            var user = provider.GetUserById(id);
            var roles = provider.GetAllRoles().ToList();

            var model = new UserUpdateModel(user, roles);

            return View(model);
        }

        // POST: UserManagement/Edit/{guid}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, UserUpdateModel model)
        {
            var validationResults = model.Validate();
            if (validationResults.Any())
            {
                foreach (var result in validationResults)
                {
                    ModelState.AddModelError(result.MemberName, result.Message);
                }
                return View(model);
            }

            try
            {
                var user = model.Map(provider.GetAllRoles());

                var results = provider.UpdateUser(user);

                if (!results.Any() && !string.IsNullOrWhiteSpace(model.Password))
                {
                    results = provider.UpdateUserPassword(user.Id, model.Password);
                }

                if (results.Any())
                {
                    foreach (var result in results)
                    {
                        ModelState.AddModelError("", result);
                    }

                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO add to log
                ModelState.AddModelError("", "Произошла ошибка при сохранении изменений.");
                return View(model);
            }
        }

        // GET: UserManagement/Delete/{guid}
        public ActionResult Delete(string id)
        {
            var user = provider.GetUserById(id);
            ViewBag.UserName = user.UserName;
            return View();
        }

        // POST: UserManagement/Delete/{guid}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProcessing(string id)
        {
            try
            {
                provider.DeleteUser(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO add to log
                TempData["Error"] = ErrorCodes.FailedToDeleteUser;
                return View();
            }
        }
    }
}
