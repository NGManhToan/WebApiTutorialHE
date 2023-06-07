using WebApiTutorialHE.Models.Category;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApiTutorialHE.Database.SharingModels;

namespace WebApiTutorialHE.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryListModel>> GetCategoryListModels();
        Task<Category>CreateCategory(CategoryListModel category);
    }
}
