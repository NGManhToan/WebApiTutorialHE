using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;
        private readonly ICategoryAction _categoryAction;

        public CategoryService(ICategoryQuery categoryQuery,ICategoryAction  categoryAction)
        {
            _categoryQuery = categoryQuery;
            _categoryAction = categoryAction;
        }
         
        public async Task<List<CategoryListModel>> GetCategoryListModels()
        {
            return await _categoryQuery.QueryListCategory();
        }

        public async Task<Category> CreateCategory(CategoryListModel category)
        {
            return await _categoryAction.CreateActionCategory(category);
        }
    }
}
