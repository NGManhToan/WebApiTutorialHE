using WebApiTutorialHE.Models.Category;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;

namespace WebApiTutorialHE.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoryService(ICategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }
         
        public async Task<List<CategoryListModel>> GetCategoryListModels()
        {
            return await _categoryQuery.QueryListCategory();
        }
    }
}
