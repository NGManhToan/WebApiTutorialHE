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

        public async Task<AdminListCategoryModel> AddCategoryService(ACategoryModel category)
        {
           return await _categoryAction.AddCategory(category);
        }

        public async Task<Category>UpdateCategory(AEditCategoryModel update)
        {
            return await _categoryAction.UpdateCategory(update);
        }

        public async Task<string> DeleteCategory(int id)
        {
            return await _categoryAction.DeleteCategory(id);
        }
    }
}
