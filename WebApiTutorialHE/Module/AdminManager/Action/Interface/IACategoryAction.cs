using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.Category;

namespace WebApiTutorialHE.Module.AdminManager.Action.Interface
{
    public interface IACategoryAction
    {
        Task<AdminListCategoryModel>AddCategory(ACategoryModel category);
        Task<Category> UpdateCategory(AEditCategoryModel updateCategory);
        Task<string> DeleteCategory(int id);
    }
}
