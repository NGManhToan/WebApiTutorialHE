using Microsoft.EntityFrameworkCore;
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

        public async Task<AdminListCategoryModel>AddCategory(ACategoryModel category,ForceInfo forceInfo)
        {
            
            var addCategory = new Category()
            {
                Name = category.Name,
                CreatedBy = forceInfo.UserId,
                LastModifiedBy = forceInfo.UserId
            };
            
            _sharingContext.Categories.Add(addCategory);
            await _sharingContext.SaveChangesAsync();
            return new AdminListCategoryModel
            {
                Name = addCategory.Name,
            };
        }

        public async Task<Category>UpdateCategory(AEditCategoryModel updateCategory, ForceInfo forceInfo)
        {
            if (updateCategory.Name == null || updateCategory.Name.Length == 0)
            {
                throw new BadHttpRequestException("Tên vật phẩm không được để trống!");
            }

            var update = await _sharingContext.Categories.FindAsync(updateCategory.Id);
            if (update != null)
            {
                update.Name = updateCategory.Name;
                update.LastModifiedBy = forceInfo.UserId;
                update.CreatedDate = Utils.DateNow();
            }

            _sharingContext.Categories.Update(update);
            await _sharingContext.SaveChangesAsync();
            return update;
        }




        public async Task<Category>DeleteCategory(ForceInfo forceInfo,int id)
        {
            var delete = await _sharingContext.Categories.FindAsync(id);
            if (delete.IsDeleted == true)
            {
                throw new BadHttpRequestException($"User không tồn tại");
            }

            var userRole = await _sharingContext.UserRoles
                .Where(ur => ur.UserId == forceInfo.UserId)
                .Select(ur => ur.Role)
                .FirstOrDefaultAsync();

            if (userRole != null)
            {
                if (userRole.Id == 1)
                {
                    // Xóa user khi role = 1
                    delete.IsDeleted = true;
                    delete.LastModifiedBy = forceInfo.UserId;
                    await _sharingContext.SaveChangesAsync();
                }
                else if (userRole.Id == 2)
                {
                    var targetUserRole = await _sharingContext.UserRoles
                        .Where(ur => ur.UserId == delete.Id)
                        .Select(ur => ur.Role)
                        .FirstOrDefaultAsync();

                    if (targetUserRole != null && targetUserRole.Id == 1)
                    {
                        throw new BadHttpRequestException($"Không có quyền xóa user này");
                    }
                    else
                    {
                        // Xóa user khi role = 2 và user role != 1
                        delete.IsDeleted = true;
                        delete.LastModifiedBy = forceInfo.UserId;
                        await _sharingContext.SaveChangesAsync();
                    }
                }
                else if (userRole.Id == 3)
                {
                    throw new BadHttpRequestException($"Không có quyền xóa user");
                }
            }
            return delete;
            //if (delete != null)
            //{
            //    delete.IsDeleted = true;
            //}
            //else
            //{
            //    return "Không tìm thấy id phù hợp";
            //}
            //_sharingContext.Categories.Update(delete);
            //await _sharingContext.SaveChangesAsync();
            //return "Đã xóa";

        }
    }
}
