using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.Category;

namespace WebApiTutorialHE.Module.AdminManager.Service.Interface
{
    public interface IACategoryService
    {
        Task<AdminListCategoryModel> AddCategoryService(ForceInfo forceInfo,ACategoryModel category);
        Task<Category>UpdateCategory(AEditCategoryModel update, ForceInfo forceInfo);
        Task<Category> DeleteCategory(ForceInfo forceInfo,int id);
    }
}
