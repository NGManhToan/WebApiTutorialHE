using WebApiTutorialHE.Models.Category;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApiTutorialHE.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryListModel>> GetCategoryListModels();
    }
}
