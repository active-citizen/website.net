using ActiveCitizen.Model.StaticContent.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using ActiveCitizenWeb.DataAccess.Context;

namespace ActiveCitizenWeb.Infrastructure.Provider
{
    public class StaticContentManagementProvider : IStaticContentManagementProvider
    {
        private readonly IStaticContentDbContext staticContentDbContext;

        public StaticContentManagementProvider(IStaticContentDbContext staticContentDbContext)
        {
            this.staticContentDbContext = staticContentDbContext;
        }

        public List<FaqListItem> GetAllFaqListItems()
        {
            return staticContentDbContext.FaqListItem.GetAll().ToList();
        }

        public FaqListItem GetFaqListItem(int id)
        {
            return staticContentDbContext.FaqListItem.GetById(id);
        }

        public bool IsFaqListItemExists(int id)
        {
            return staticContentDbContext.FaqListItem.GetById(id) != null;
        }

        public bool IsFaqListCategoryDeletable(int id)
        {
            bool any = staticContentDbContext.FaqListItem.GetAll().Any(e => e.Category.Id == id);

            return !any;
        }

        public static string CannotDelete = @"Нельзя удалить раздел, так как в разделе есть вопросы, вначале удалите все вопросы из раздела, а потом удаляйте раздел.";

        public void PutFaqListItem(FaqListItem item)
        {
            try
            {
                item.Category = GetFaqListCategory(item.CategoryId);
                staticContentDbContext.FaqListItem.Update(item);
            }
            catch (Exception)
            {
                //TODO - create error page with explanation
                throw;
            }
        }

        public void PutFaqListCategory(FaqListCategory category)
        {
            try
            {
                staticContentDbContext.FaqListCategory.Update(category);
            }
            catch (Exception)
            {
                //TODO - create error page with explanation
                throw;
            }
        }

        //
        public void PostFaqListItem(FaqListItem item)
        {
            item.Category = GetFaqListCategory(item.CategoryId);
            staticContentDbContext.FaqListItem.Insert(item);
        }

        public void PostFaqListCategory(FaqListCategory category)
        {
            staticContentDbContext.FaqListCategory.Insert(category);
        }

        public FaqListCategory GetFaqListCategory(int id)
        {
            return staticContentDbContext.FaqListCategory.GetById(id);
        }

        public void DeleteFaqListItem(int id)
        {
            staticContentDbContext.FaqListItem.Delete(id);
        }

        public void DeleteFaqListCategory(int id)
        {
            staticContentDbContext.FaqListCategory.Delete(id);
        }

        public List<FaqListCategory> GetAllFaqListCategories()
        {
            return staticContentDbContext.FaqListCategory.GetAll().ToList();
        }
    }
}