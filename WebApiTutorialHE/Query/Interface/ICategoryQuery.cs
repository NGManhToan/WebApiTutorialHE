using WebApiTutorialHE.Models.Category;

namespace WebApiTutorialHE.Query.Interface
{
    public interface ICategoryQuery
    {
        Task<List<CategoryListModel>> QueryListCategory();
    }
}
