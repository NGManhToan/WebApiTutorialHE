using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Manager.FilterAttr;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Model.Category;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;

namespace WebApiTutorialHE.Module.AdminManager.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ManagerAccess]
    public class ACategoryController : ControllerBase
    {
        private readonly IACategoryService _categoryService;

        public ACategoryController(IACategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [ManagerAccess]
        [HttpPost]
        public async Task<IActionResult>AddCategory(ACategoryModel category)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var add = await _categoryService.AddCategoryService(forceInfo,category);
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Them thanh cong",
                content = add
            });
        }

        [ManagerAccess]
        [HttpPut]
        public async Task<IActionResult> EditCategory(AEditCategoryModel editCategory)
        {
            if (editCategory.Name == null || editCategory.Name.Length == 0)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Tên vật phẩm không được để trống!",
                });
            }
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var update = await _categoryService.UpdateCategory(editCategory, forceInfo);
            if (update == null)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Chỉnh sửa không thành công",
                });
            }

            if (update.IsDeleted == true)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Chỉnh sửa không thành công",
                });
            }

            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Chỉnh sửa thành công",
                content = update
            });
        }

        [ManagerAccess]
        [HttpDelete]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            var forceInfo = new ForceInfo
            {
                UserId = Utils.GetUserIdFromToken(Request),
                DateNow = Utils.DateNow()
            };
            var delete = await _categoryService.DeleteCategory(forceInfo,id);
            if(delete == null)
            {
                return Ok(new ObjectResponse
                {
                    result = 0,
                    message = "Xóa không thành công",
                });
            }
            return Ok(new ObjectResponse
            {
                result = 1,
                message = "Xóa thành công",
                content = delete
            });
        }
    }
}
