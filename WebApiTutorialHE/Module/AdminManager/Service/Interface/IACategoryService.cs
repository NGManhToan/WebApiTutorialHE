using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.Category;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IACategoryService
    {
        Task<AdminListCategoryModel> AddCategoryService(ACategoryModel category);
        Task<Category>UpdateCategory(AEditCategoryModel update);
        Task<string> DeleteCategory(int id);
    }
}
