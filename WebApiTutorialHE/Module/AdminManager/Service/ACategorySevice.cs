using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.Category;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Service
{
    public class ACategorySevice : IACategoryService
    {
        private readonly IACategoryAction _categoryAction;

        public ACategorySevice (IACategoryAction categoryAction)
        {
            _categoryAction = categoryAction;
        }

        public async Task<AdminListCategoryModel> AddCategoryService(ForceInfo forceInfo,ACategoryModel category)
        {
           return await _categoryAction.AddCategory(category, forceInfo);
        }

        public async Task<Category>UpdateCategory(AEditCategoryModel update, ForceInfo forceInfo)
        {
            return await _categoryAction.UpdateCategory(update, forceInfo);
        }

        public async Task<Category> DeleteCategory(ForceInfo forceInfo,int id)
        {
            return await _categoryAction.DeleteCategory(forceInfo,id);
        }
    }
}
