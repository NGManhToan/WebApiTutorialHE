using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.Category;

namespace WebApiTutorialHE.Module.AdminManager.Action.Interface
{
    public interface IACategoryAction
    {
        Task<AdminListCategoryModel>AddCategory(ACategoryModel category, ForceInfo forceInfo);
        Task<Category> UpdateCategory(AEditCategoryModel updateCategory, ForceInfo forceInfo);
        Task<Category>DeleteCategory(ForceInfo forceInfo,int id);
    }
}
