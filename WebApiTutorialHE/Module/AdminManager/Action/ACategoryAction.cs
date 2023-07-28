using System.util;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Model.Category;
using WebApiTutorialHE.UtilsService.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Action
{
    public class ACategoryAction:IACategoryAction
    {
        private readonly SharingContext _sharingContext;

        public ACategoryAction (SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }

        public async Task<AdminListCategoryModel> AddCategory(ACategoryModel category)
        {
            
            var addCategory = new Category()
            {
                Name = category.Name,
                CreatedBy = category.CreatedBy,
                LastModifiedBy = category.CreatedBy
            };
            
            _sharingContext.Categories.Add(addCategory);
            await _sharingContext.SaveChangesAsync();
            return new AdminListCategoryModel
            {
                Name = addCategory.Name,
            };
        }

        public async Task<Category> UpdateCategory(AEditCategoryModel updateCategory)
        {
            if (updateCategory.Name == null || updateCategory.Name.Length == 0)
            {
                throw new BadHttpRequestException("Tên vật phẩm không được để trống!");
            }

            var update = await _sharingContext.Categories.FindAsync(updateCategory.Id);
            if (update != null)
            {
                update.Name = updateCategory.Name;
                update.CreatedBy = updateCategory.CreatedBy;
                update.LastModifiedBy = updateCategory.CreatedBy;
                update.CreatedDate = Utils.DateNow();
            }

            _sharingContext.Categories.Update(update);
            await _sharingContext.SaveChangesAsync();
            return update;
        }




        public async Task<string> DeleteCategory(int id)
        {
            var delete = await _sharingContext.Categories.FindAsync(id);
            if (delete != null)
            {
                delete.IsDeleted = true;
            }
            else
            {
                return "Không tìm thấy id phù hợp";
            }
            _sharingContext.Categories.Update(delete);
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";

        }
    }
}
